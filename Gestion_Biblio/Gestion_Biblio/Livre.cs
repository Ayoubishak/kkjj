using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Livre : Oeuvre
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int delai_retour = 10;
        private Auteur auteur;

        public int Delai_retour { get => delai_retour; set => delai_retour = value; }
        internal Auteur Auteur { get => auteur; set => auteur = value; }

        public Livre()
        {
        }

        public Livre(int id, string titre, Auteur auteur) : base(id, titre)
        {
            this.auteur = auteur;
        }
  
        public new Livre Identifier(int id)
        {
            Auteur a = new Auteur();
            Livre l = new Livre();
            l.Id = id;
            cnx.Open();
            string requete = "SELECT [IDA],[TITRE] FROM [OEUVRE] WHERE [IDO]=@Id AND [TYPE]='Livre'";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                a.Id = int.Parse(Dr[0].ToString());
                l.Titre = Dr[1].ToString();
            }
            Dr.Close();
            cnx.Close();
            a = a.Identifier(a.Id);
            l.Auteur = a;
            return l;
        }
    }
}
