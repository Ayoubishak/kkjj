using System;
using System.Data.SqlClient;

namespace Gestion_Biblio
{
    class Usager
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int id;
        private string nom;
        private string prenom;
        private string adresse;
        private string tel;
        private string email;
        private int retard = 0;
        private Boolean usagersup = false;

        //Getter Setter
        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Email { get => email; set => email = value; }
        public int Retard { get => retard; set => retard = value; }
        public bool Usagersup { get => usagersup; set => usagersup = value; }

        public Usager()
        {

        }

        public Usager(int id, string nom, string prenom, string adresse, string tel, string email, int retard, Boolean usagersup)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.tel = tel;
            this.email = email;
            this.retard = retard;
            this.usagersup = usagersup;
        }

        public Usager Identifier(int id) //Identifier usager
        {
            Usager u = new Usager();
            u.Id = id;
            cnx.Open();
            string requete = "SELECT * FROM [USAGER] WHERE [IDU]= @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                u.Nom = Dr[1].ToString();
                u.Prenom = Dr[2].ToString();
                u.Adresse= Dr[3].ToString();
                u.Tel= Dr[4].ToString();
                u.Email=Dr[5].ToString();
                u.Retard= int.Parse(Dr[6].ToString());
                u.Usagersup=Boolean.Parse(Dr[7].ToString());
            }
            Dr.Close();
            cnx.Close();
            return u;
        }
       
    }
}
