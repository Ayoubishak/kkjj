using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    class Gestion_reservation_et_emprunt
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        Usager u = new Usager();
        Exemplaire ex = new Exemplaire();
        Oeuvre o = new Oeuvre();

        public void Emprunter(int idu, int ide) //Ajout d'un nouvelle emprunt
        {
            //Identification des éléments
            u = u.Identifier(idu);
            ex = ex.Identifier(ide);
            Emprunt e = new Emprunt(DateTime.UtcNow.Date, default(DateTime), u, ex);
            //Ajout d'un emprunt d'éxemplaire par un usager
            cnx.Open();
            string requete = "INSERT INTO [EMPRUNT] ([IDU],[IDE]) VALUES (@Idu,@Ide)";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", e.Usager.Id);
            cmd.Parameters.AddWithValue("@Ide", e.Exemplaire.Id);
            cmd.ExecuteNonQuery();
            cnx.Close();

            //Vérifier si l'usager a réserver oeuvre de cet éxemplaire
            int t = 0;
            cnx.Open();
            requete = "SELECT COUNT(*) from RESERVATION WHERE [IDU]=@Idu AND [IDO]=@Ido AND [DATEANNULATION] IS NULL";
            cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", e.Usager.Id);
            cmd.Parameters.AddWithValue("@Ido", e.Exemplaire.Oeuvre.Id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();

            //Si l'usager a une réservation, on l'annule
            if (t != 0)
            {
                cnx.Open();
                requete = "UPDATE [RESERVATION] SET [DATEANNULATION] = GETDATE() WHERE [IDU]=@Idu AND [IDO]=@Ido AND [DATEANNULATION] IS NULL";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Idu", e.Usager.Id);
                cmd.Parameters.AddWithValue("@Ido", e.Exemplaire.Oeuvre.Id);
                cmd.ExecuteNonQuery();
                cnx.Close();
            }

            //Rendre l'éxemplaire emprunter non disponible
            cnx.Open();
            requete = "UPDATE [EXEMPLAIRE] SET [DISPONIBLE] = 0 WHERE [IDE] =@Ide";
            cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ide", e.Exemplaire.Id);
            cmd.ExecuteNonQuery();
            cnx.Close();
            MessageBox.Show("Action a été éxecuter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void Rendre(int idu, int ide, string etat) //Ajout de Date de retour pour un emprunt spécifique
        {
            //Identification des éléments
            u = u.Identifier(idu);
            ex = ex.Identifier(ide);
            ex.Disponible = true;
            //Ajouter la date de retour a l'emprunt
            cnx.Open();
            string requete = "UPDATE [EMPRUNT] SET [DATERETOUR] = GETDATE() WHERE [IDU]=@Idu AND [IDE]=@Ide AND [DATERETOUR] IS NULL";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", u.Id);
            cmd.Parameters.AddWithValue("@Ide", ex.Id);
            cmd.ExecuteNonQuery();
            cnx.Close();

            //Rendre l'éxemplaire emprunter disponible et enregister son nouveaux état
            cnx.Open();
            requete = "UPDATE [EXEMPLAIRE] SET [DISPONIBLE] = 1, [ETAT] =@Etat WHERE [IDE] =@Ide";
            cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ide", ex.Id);
            cmd.Parameters.AddWithValue("@Etat", etat);
            cmd.ExecuteNonQuery();
            cnx.Close();
            MessageBox.Show("Action a été éxecuter avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public void Reserver(int idu, int ido) //Ajout d'une nouvelle réservation
        {
            //Identification des éléments
            u = u.Identifier(idu);
            o = o.Identifier(ido);
            Reservation r = new Reservation(DateTime.UtcNow.Date, default(DateTime), u, o);
            //Verifier si l'usager a une réservation similaire encours
            int t = 0;
            cnx.Open();
            string requete = "SELECT COUNT(*) FROM [RESERVATION] WHERE [IDU]=@Idu AND [IDO]=@Ido AND [DATEANNULATION] IS NULL";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", r.Usager.Id);
            cmd.Parameters.AddWithValue("@Ido", r.Oeuvre.Id);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                t = int.Parse(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();

            //Si Non, Ajouter Réservation
            if (t == 0)
            {
                cnx.Open();
                requete = "INSERT INTO [RESERVATION] ([IDU],[IDO]) VALUES (@Idu,@Ido)";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Idu", r.Usager.Id);
                cmd.Parameters.AddWithValue("@Ido", r.Oeuvre.Id);
                cmd.ExecuteNonQuery();
                cnx.Close();
                MessageBox.Show("Oeuvre a été réserver avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
                MessageBox.Show("L'Usager a déja réserver cette oeuvre", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Annuler(int idu, int ido) //Annuler réservation
        {
            //Annuler réservation
            cnx.Open();
            string requete = "UPDATE [RESERVATION] SET [DATEANNULATION] = GETDATE() WHERE [IDU]=@Idu AND [IDO]=@Ido AND [DATEANNULATION] IS NULL";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", idu);
            cmd.Parameters.AddWithValue("@Ido", ido);
            cmd.ExecuteNonQuery();
            cnx.Close();
            MessageBox.Show("Réservation a été annuler avec Succès", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }
}
