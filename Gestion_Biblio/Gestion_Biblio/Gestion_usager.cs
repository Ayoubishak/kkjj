using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    class Gestion_usager
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public void Ajouter(string nom, string prenom, string address, string tel, string email)
        {
            Usager u = new Usager(0, nom, prenom, address, tel, email);
            cnx.Open();
            string requete = "INSERT INTO [USAGER] ([NOM],[PRENOM],[ADDRESS],[TEL],[EMAIL]) VALUES (@Nom,@Prenom,@Address,@Tel,@Email)";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", u.Nom);
            cmd.Parameters.AddWithValue("@Prenom", u.Prenom);
            cmd.Parameters.AddWithValue("@Address", u.Address);
            cmd.Parameters.AddWithValue("@Tel", u.Tel);
            cmd.Parameters.AddWithValue("@Email", u.Email);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Usager a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }

        public void Maj(int id, string nom, string prenom, string address, string tel, string email, int retard)
        {
            cnx.Open();
            string requete = "UPDATE [USAGER] SET [NOM] =@Nom ,[PRENOM] =@Prenom ,[ADDRESS] =@Address ,[TEL] =@Tel ,[EMAIL] =@Email ,[RETARD] =@Retard WHERE [IDU] = @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Prenom", prenom);
            cmd.Parameters.AddWithValue("@Address", address);
            cmd.Parameters.AddWithValue("@Tel", tel);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Retard", retard);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Usager a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }

        public void Retirer(int id)
        {
            cnx.Open();
            string requete = "UPDATE [USAGER] SET [USAGERSUP] = 1 WHERE [IDU] = @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Usager a été retirer avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
    }
}
