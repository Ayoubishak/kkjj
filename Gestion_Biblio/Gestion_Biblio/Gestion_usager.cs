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
        public void Ajouter(string nom, string prenom, string adress, string tel, string email) //Ajouter usager
        {
            Usager u = new Usager(0, nom, prenom, adress, tel, email, 0, false);
            //Vérifier ci usager active existe 
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [USAGER] WHERE [NOM] = @Nom AND [PRENOM] = @Prenom AND [USAGERSUP] = 0";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", u.Nom);
            cmd.Parameters.AddWithValue("@Prenom", u.Prenom);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
            if (t > 0)
            {
                MessageBox.Show("Usager éxiste déja !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //Si Non, Ajouter l'usager
            else
            {
                cnx.Open();
                requete = "INSERT INTO [USAGER] ([NOM],[PRENOM],[ADRESS],[TEL],[EMAIL]) VALUES (@Nom,@Prenom,@Adress,@Tel,@Email)";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Nom", u.Nom);
                cmd.Parameters.AddWithValue("@Prenom", u.Prenom);
                cmd.Parameters.AddWithValue("@Adress", u.Adress);
                cmd.Parameters.AddWithValue("@Tel", u.Tel);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.ExecuteNonQuery();
                MessageBox.Show("L'Usager a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cnx.Close();
            }
        }

        public void Maj(int idu, string nom, string prenom, string adress, string tel, string email, int retard)
        {
            //Vérifier ci usager active existe 
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [USAGER] WHERE [NOM] = @Nom AND [PRENOM] = @Prenom AND [IDU] != @Id AND [USAGERSUP] = 0";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Prenom", prenom);
            cmd.Parameters.AddWithValue("@Id", idu);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
            if (t > 0)
            {
                MessageBox.Show("Usager éxiste déja !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            //Si Non, modifier l'usager
            else
            {
                cnx.Open();
                requete = "UPDATE [USAGER] SET [NOM] =@Nom ,[PRENOM] =@Prenom ,[ADRESS] =@Adress ,[TEL] =@Tel ,[EMAIL] =@Email ,[RETARD] =@Retard WHERE [IDU] = @Id";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Id", idu);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.Parameters.AddWithValue("@Adress", adress);
                cmd.Parameters.AddWithValue("@Tel", tel);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Retard", retard);
                cmd.ExecuteNonQuery();
                MessageBox.Show("L'Usager a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cnx.Close();
            }
        }

        public void Retirer(int idu) //Retirer usager
        {
            cnx.Open();
            string requete = "UPDATE [USAGER] SET [USAGERSUP] = 1 WHERE [IDU] = @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", idu);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Usager a été retirer avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
    }
}
