using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Exemplaire
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int id;
        private string etat;
        private Boolean disponible =true;
        private Oeuvre oeuvre;

        public int Id { get => id; set => id = value; }
        public string Etat { get => etat; set => etat = value; }
        public bool Disponible { get => disponible; set => disponible = value; }
        internal Oeuvre Oeuvre { get => oeuvre; set => oeuvre = value; }

        public Exemplaire()
        {
        }

        public Exemplaire(int id, string etat, Oeuvre oeuvre)
        {
            this.Id = id;
            this.Etat = etat;
            this.Oeuvre = oeuvre;
        }

        public Exemplaire Identifier(int id)
        {
            Oeuvre o = new Oeuvre();
            Exemplaire e = new Exemplaire();
            e.Id = id;
            cnx.Open();
            string requete = "SELECT e.[IDO],e.[ETAT],e.[DISPONIBLE],o.TYPE FROM [EXEMPLAIRE] e,[OEUVRE] o WHERE [IDE]=@Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                o.Id = int.Parse(Dr[0].ToString());
                e.Etat = Dr[1].ToString();
                e.Disponible = Boolean.Parse(Dr[2].ToString());
            }
            Dr.Close();
            cnx.Close();
            o = o.Identifier(o.Id);
            e.Oeuvre = o;
            return e;
        }
            
    }
}
