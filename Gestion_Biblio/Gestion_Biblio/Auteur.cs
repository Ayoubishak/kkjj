using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Auteur
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int id;
        private string nom;
        private string prenom;

        //Getter Setter
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }

        public Auteur()
        {
        }

        public Auteur(int id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
        }

        public Auteur Identifier(int id) //Identifier auteur
        {
            Auteur a = new Auteur();
            a.Id = id;
            cnx.Open();
            string requete = "SELECT * FROM [AUTEUR] where [IDA] = @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                a.Nom = Dr[1].ToString();
                a.Prenom = Dr[2].ToString();

            }
            Dr.Close();
            cnx.Close();
            return a;
        }
        
    }
}
