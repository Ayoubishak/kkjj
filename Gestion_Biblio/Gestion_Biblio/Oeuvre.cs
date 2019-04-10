using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Oeuvre
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int id;
        private string titre;

        public int Id { get => id; set => id = value; }
        public string Titre { get => titre; set => titre = value; }

        public Oeuvre()
        {
        }

        public Oeuvre(int id, string titre)
        {
            this.id = id;
            this.titre = titre;
        }

        public Oeuvre Identifier(int id)
        {
            Oeuvre o = new Oeuvre();
            o.Id = id;
            cnx.Open();
            string requete = "SELECT [TITRE] FROM [OEUVRE] WHERE [IDO]=@id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                o.Titre = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();
            return o;
        }
    }
}
