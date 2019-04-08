using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    class Gestion_auteur
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public void Ajouter(string nom, string prenom)
        {
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [AUTEUR] WHERE [NOMA] =@Nom AND [PRENOMA] =@Prenom";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Prenom", prenom);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
            if (t > 0)
            {
                MessageBox.Show("Auteur éxiste déja !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                cnx.Open();
                requete = "INSERT INTO AUTEUR (NOMA,PRENOMA) VALUES(@Nom,@Prenom)";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.ExecuteNonQuery();
                MessageBox.Show("L'Auteur a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cnx.Close();
            }
        }
        public void Maj(int id, string nom, string prenom)
        {
            cnx.Open();
            string requete = "UPDATE [AUTEUR] SET [NOMA]=@Nom ,[PRENOMA]=@Prenom WHERE [IDA]=@Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Prenom", prenom);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Auteur a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Auteur at = new Auteur();
            cnx.Close();
        }
    }
}
