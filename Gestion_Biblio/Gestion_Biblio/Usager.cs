using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Usager
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int id;
        private string nom;
        private string prenom;
        private string address;
        private string tel;
        private string email;
        private int retard;
        private Boolean usagersup;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Address { get => address; set => address = value; }
        public string Tel { get => tel; set => tel = value; }
        public string Email { get => email; set => email = value; }
        public int Retard { get => retard; set => retard = value; }
        public bool Usagersup { get => usagersup; set => usagersup = value; }

        public Usager()
        {
            this.retard = 0;
            this.usagersup = false;
        }

        public Usager(int id, string nom, string prenom, string address, string tel, string email)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.address = address;
            this.tel = tel;
            this.email = email;
            this.retard = 0;
            this.usagersup = false;
        }

        public Usager Identifier(int id)
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
                u.Address= Dr[3].ToString();
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
