using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Biblio
{
    class Magazine : Oeuvre
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        private int delai_retour = 30;

        public int Delai_retour { get => delai_retour; set => delai_retour = value; }

        public Magazine()
        {
        }

        public Magazine(int id, string titre) : base(id, titre) 
        {
        }

        public new Magazine Identifier(int id) //Identifier magazine
        {
            Magazine m = new Magazine();
            m.Id = id;
            cnx.Open();
            string requete = "SELECT [TITRE] FROM [OEUVRE] WHERE [IDO]=@id AND [TYPE]='Magazine'";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                m.Titre = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();
            return m;
        }
    }
}
