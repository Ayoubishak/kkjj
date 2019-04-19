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
        public void Ajouter(string nom, string prenom) //Ajouter nouvelle auteur
        {
            Auteur a = new Auteur(0, nom, prenom);
            //Verifer si auteur existe déja
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [AUTEUR] WHERE [NOMA] = @Nom AND [PRENOMA] = @Prenom";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", a.Nom);
            cmd.Parameters.AddWithValue("@Prenom", a.Prenom);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
            //Si Non, ajouter auteur
            if (t > 0)
            {
                MessageBox.Show("Auteur éxiste déja !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnx.Open();
                requete = "INSERT INTO AUTEUR (NOMA,PRENOMA) VALUES(@Nom,@Prenom)";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Nom", a.Nom);
                cmd.Parameters.AddWithValue("@Prenom", a.Prenom);
                cmd.ExecuteNonQuery();
                MessageBox.Show("L'Auteur a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                cnx.Close();
            }
        }
        public void Maj(int ida, string nom, string prenom)
        {
            //Verifer si un autre auteur existe avec le méme nom 
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [AUTEUR] WHERE [NOMA] = @Nom AND [PRENOMA] = @Prenom AND [IDA] != @Ida";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nom", nom);
            cmd.Parameters.AddWithValue("@Prenom", prenom);
            cmd.Parameters.AddWithValue("@Ida", ida);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
            //Si Non, modifier auteur
            if (t > 0)
            {
                MessageBox.Show("Auteur éxiste déja !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                cnx.Open();
                requete = "UPDATE [AUTEUR] SET [NOMA]=@Nom ,[PRENOMA]=@Prenom WHERE [IDA]=@Id";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Id", ida);
                cmd.Parameters.AddWithValue("@Nom", nom);
                cmd.Parameters.AddWithValue("@Prenom", prenom);
                cmd.ExecuteNonQuery();
                MessageBox.Show("L'Auteur a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                Auteur at = new Auteur();
                cnx.Close();
            }
        }
    }
}
