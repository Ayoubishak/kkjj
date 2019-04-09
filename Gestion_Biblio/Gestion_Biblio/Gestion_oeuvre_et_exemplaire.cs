using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    class Gestion_oeuvre_et_exemplaire
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public void Ajoutermagazine(string titre)
        {
            Magazine m = new Magazine(0, titre);
            cnx.Open();
            string requete = "INSERT INTO [OEUVRE] ([TITRE],[TYPE],[DELAI_RETOUR]) VALUES (@Titre,'Magazine', @Delai)";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Titre", m.Titre);
            cmd.Parameters.AddWithValue("@Delai", m.Delai_retour);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Le Magazine a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Majmagazine(int idm, string titre)
        {
            cnx.Open();
            string requete = "UPDATE [OEUVRE] SET [TITRE] = @Titre WHERE [IDO]=@Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Titre", titre);
            cmd.Parameters.AddWithValue("@Id", idm);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Le Magazine a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Ajouterlivre(string titre,int ida)
        {
            Auteur a = new Auteur();
            Livre l = new Livre(0,titre,a.Identifier(ida));
            cnx.Open();
            string requete = "INSERT INTO [OEUVRE] ([IDA],[TITRE],[TYPE],[DELAI_RETOUR]) VALUES (@Ida,@Titre,'Livre', @Delai)";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ida", l.Auteur.Id);
            cmd.Parameters.AddWithValue("@Titre", l.Titre);
            cmd.Parameters.AddWithValue("@Delai", l.Delai_retour);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Le Livre a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Majlivre(int idl, string titre, int ida)
        {
            cnx.Open();
            string requete = "UPDATE [OEUVRE] SET [TITRE] = @Titre, [IDA] = @Ida WHERE [IDO]=@Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Titre", titre);
            cmd.Parameters.AddWithValue("@Id", idl);
            cmd.Parameters.AddWithValue("@Ida", ida);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Le Livre a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Ajouterexemplaire()
        {

        }
        public void Majexemplaire()
        {

        }
        public void Retirerexemplaire()
        {

        }
    }
}
