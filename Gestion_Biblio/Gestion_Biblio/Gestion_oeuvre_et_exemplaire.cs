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
        Auteur a = new Auteur();
        Oeuvre o = new Oeuvre();
        public void Ajoutermagazine(string titre) //Ajouter magazine
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
        public void Majmagazine(int idm, string titre) //Modifier magazine
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
        public void Ajouterlivre(string titre,int ida) //Ajouter livre
        {
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
        public void Majlivre(int idl, string titre, int ida) //Modifier livre
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
        public void Ajouterexemplaire(int ido, string etat) //Ajouter exemplaire
        {
            o = o.Identifier(ido);
            Exemplaire e = new Exemplaire(0, etat,o);
            cnx.Open();
            string requete = "INSERT INTO [EXEMPLAIRE] ([IDO],[ETAT]) VALUES (@Ido, @Etat)";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ido", e.Oeuvre.Id);
            cmd.Parameters.AddWithValue("@Etat", e.Etat);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Exemplaire a été ajouter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Majexemplaire(int ide, string etat) //Modifier exemplaire
        {
            cnx.Open();
            string requete = "UPDATE [EXEMPLAIRE] SET [ETAT] = @Etat WHERE [IDE] = @Ide";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Etat", etat);
            cmd.Parameters.AddWithValue("@Ide", ide);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Exemplaire a été modifier avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
        public void Retirerexemplaire(int ide) //Retirer exemplaire
        {
            cnx.Open();
            string requete = "UPDATE [EXEMPLAIRE] SET [DISPONIBLE] = 0 WHERE [IDE] = @Ide";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ide", ide);
            cmd.ExecuteNonQuery();
            MessageBox.Show("L'Exemplaire a été retirer avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            cnx.Close();
        }
    }
}
