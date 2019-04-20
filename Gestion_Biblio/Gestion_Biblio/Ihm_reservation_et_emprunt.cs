using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    public partial class Ihm_reservation_et_emprunt : Form
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public Ihm_reservation_et_emprunt()
        {
            InitializeComponent();
        }

        private void Ihm_reservation_et_emprunt_Load(object sender, EventArgs e)
        {
            this.Load_All_Oeuvre();
            this.Load_All_Usager();
        }
        private void Load_All_Oeuvre() //Charger toute les oeuvres
        {
            comboBox2.Items.Clear();
            cnx.Open();
            string requete = "SELECT [TITRE] FROM [OEUVRE]";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
                comboBox2.Items.Add(Dr[0].ToString());
            Dr.Close();
            cnx.Close();
        }

        private void Load_All_Usager() //Charger tous les usagers non retirer
        {
                comboBox1.Items.Clear();
                cnx.Open();
                string requete = "SELECT [NOM]+' '+[PRENOM] FROM [USAGER] WHERE [USAGERSUP] = 0";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    comboBox1.Items.Add(Dr[0].ToString());
                }
                Dr.Close();
                cnx.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Identifier usager
        {
            comboBox3.Items.Clear();
            dataGridView1.DataSource = null;
            label4.Text = "";
            cnx.Open();
            string requete = "SELECT * FROM [USAGER] WHERE ([NOM]+' '+[PRENOM]) = @Nomp";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Nomp", comboBox1.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                label4.Text = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();

            if (button4.Enabled == false)
            {
                comboBox2.Items.Clear();
                cnx.Open();
                if (button6.Enabled == false)
                    requete = "SELECT o.[TITRE] FROM [OEUVRE] o, [RESERVATION] r WHERE  r.IDO = o.IDO AND r.IDU =@Idu AND r.[DATEANNULATION] IS NULL";
                else
                    requete = "SELECT o.[TITRE] FROM [EMPRUNT] e, [EXEMPLAIRE] ex, [OEUVRE] o WHERE e.[IDE] = ex.[IDE] AND ex.[IDO] = o.[IDO] AND e.[IDU]=@Idu AND e.[DATERETOUR] IS NULL";
                cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Idu", int.Parse(label4.Text));
                Dr = cmd.ExecuteReader();
                while (Dr.Read())
                    comboBox2.Items.Add(Dr[0].ToString());
                Dr.Close();
                cnx.Close();
            }
            if (label4.Text != "")
            {
                this.Load_Datagridview();
            }
        }

        public void Load_Datagridview() //Remplir le datagridview
        {
            cnx.Open();
            string requete = "";
            if (button5.Enabled == false)
                requete = "SELECT o.[TITRE] [Titre de l'oeuvre], o.[TYPE] [Type de l'oeuvre], ex.[IDE] [Identifiant de l'éxemplaire], e.[DATEJOURE] [Date de l'emprunt] FROM [OEUVRE] o,[EXEMPLAIRE] ex, [EMPRUNT] e WHERE ex.IDE = e.IDE AND ex.IDO = o.IDO AND e.IDU =@Idu AND e.[DATERETOUR] IS NULL";
            else
                requete = "SELECT o.[TITRE] [Titre de l'oeuvre], o.[TYPE] [Type de l'oeuvre], r.[DATEJOURR] [Date de la réservation] FROM [OEUVRE] o, [RESERVATION] r WHERE  r.IDO = o.IDO AND r.IDU =@Idu AND r.[DATEANNULATION] IS NULL";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Idu", int.Parse(label4.Text));
            SqlDataReader Dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(Dr);
            Dr.Close();
            dataGridView1.DataSource = t;
            dataGridView1.CurrentCell = null;
            dataGridView1.Refresh();
            cnx.Close();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //Identifier oeuvre
        {
            comboBox3.Items.Clear();

            cnx.Open();
            string requete = "SELECT [IDO] FROM [OEUVRE] WHERE [TITRE] = @Titre";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Titre", comboBox2.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                label5.Text = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();

            cnx.Open();
            requete = "SELECT [IDE] FROM [EXEMPLAIRE] WHERE [IDO] = @Ido AND [DISPONIBLE] = 1";
            cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ido", int.Parse(label5.Text));
            Dr = cmd.ExecuteReader();
            while (Dr.Read())
                comboBox3.Items.Add(Dr[0].ToString());
            Dr.Close();
            cnx.Close();
        }

        private void button3_Click(object sender, EventArgs e) //Modifier formulaire l'emprunt ou la reservation
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            if (button5.Enabled == false)
            {
                button1.Text = "Emprunter";
                label7.Text = "Liste des emprunts en cours";
            }
            else
            {
                button1.Text = "Réserver";
                label7.Text = "Liste des réservations en cours";
            }
            panel2.Visible = true;
            panel4.Visible = false;
            button3.Enabled = false;
            button4.Enabled = true;
            comboBox2.Items.Clear();
            this.Load_All_Oeuvre();
        }

        private void button4_Click(object sender, EventArgs e) //Modifier formulaire pour retourner des exemplaire ou annuler des reservation
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            if (button5.Enabled == false)
            {
                button1.Text = "Retourner";
                panel4.Visible = true;
                label7.Text = "Choisisez une oeuvre a rendre";
            }
            else
            {
                button1.Text = "Annuler Réservation";
                panel4.Visible = false;
                label7.Text = "Choisisez une réservation a annuler";
            }
            panel2.Visible = false;
            button3.Enabled = true;
            button4.Enabled = false;
            if (label4.Text != "")
            {
                comboBox2.Items.Clear();
                cnx.Open();
                string requete = "SELECT o.[TITRE] FROM [EMPRUNT] e, [EXEMPLAIRE] ex, [OEUVRE] o WHERE e.[IDE] = ex.[IDE] AND ex.[IDO] = o.[IDO] AND e.[IDU]=@Idu AND e.[DATERETOUR] IS NULL";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Idu", int.Parse(label4.Text));
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                    comboBox2.Items.Add(Dr[0].ToString());
                Dr.Close();
                cnx.Close();
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e) //Modifier formulaire pour la gestion des emprunts
        {
            button1.Text = "Emprunter";
            button3.Text = "Emprunter Oeuvre";
            button4.Text = "Retourner Oeuvre";
            panel1.Visible = true;
            button5.Enabled = false;
            button6.Enabled = true;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            button2_Click(null, null);
        }

        private void button6_Click(object sender, EventArgs e) //Modifier formulaire pour la gestion des reservations
        {
            button1.Text = "Réserver";
            button3.Text = "Réserver Oeuvre";
            button4.Text = "Annuler réservation";
            panel1.Visible = false;
            panel4.Visible = false;
            button5.Enabled = true;
            button6.Enabled = false;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            button2_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e) //Réinitialiser le formulaire
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            button3_Click(null, null);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            label4.Text = "";
            label5.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //Remplir les champs oeuvre et exemplaire
        {
            comboBox2.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if(button5.Enabled==false)
                comboBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
        public void Emprunter(int idu, int ide) //Emprunter un exemplaire
        {
            Gestion_reservation_et_emprunt gre = new Gestion_reservation_et_emprunt();
            gre.Emprunter(idu,ide);
            this.Load_Datagridview();
            button2_Click(null, null);
        }
        public void Rendre(int idu, int ide, string etat) //Retour de l'exemplaire
        {
            Gestion_reservation_et_emprunt gre = new Gestion_reservation_et_emprunt();
            gre.Rendre(idu, ide, etat);
            this.Load_Datagridview();
            button2_Click(null, null);
        }
        public void Reserver(int idu, int ido) //Faire réservation
        {
            Gestion_reservation_et_emprunt gre = new Gestion_reservation_et_emprunt();
            gre.Reserver(idu, ido);
            this.Load_Datagridview();
            button2_Click(null, null);
        }
        public void Annuler(int idu, int ido) //Annuler réservation
        {
            Gestion_reservation_et_emprunt gre = new Gestion_reservation_et_emprunt();
            gre.Annuler(idu, ido);
            this.Load_Datagridview();
            button2_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e) //Executer (Emprunt, Retour, Réservation, Annulation)
        {
            Boolean b = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();

            if (comboBox1.Text == string.Empty)
            {
                errorProvider1.SetError(comboBox1, "il faut choisir un usager");
                b = true;
            }

            if (comboBox2.Text == string.Empty && button3.Enabled == false)
            {
                errorProvider2.SetError(comboBox2, "il faut choisir une oeuvre");
                b = true;
            }

            if (comboBox3.Text == string.Empty && button3.Enabled == false && button5.Enabled == false)
            {
                errorProvider3.SetError(comboBox3, "il faut choisir un exemplaire");
                b = true;
            }

            if (dataGridView1.SelectedRows.Count == 0 && button6.Enabled == false && button4.Enabled==false)
            {
                errorProvider4.SetError(dataGridView1, "il faut choisir une réservation a annuler");
                b = true;
            }

            if (dataGridView1.SelectedRows.Count == 0 && button5.Enabled == false && button4.Enabled == false)
            {
                errorProvider4.SetError(dataGridView1, "il faut choisir un emprunt a rendre");
                b = true;
            }

            if (comboBox4.Text == string.Empty && button4.Enabled == false && button5.Enabled == false)
            {
                errorProvider5.SetError(comboBox4, "il faut choisir un état");
                b = true;
            }

            if (b == false)
            {
                if (button1.Text == "Emprunter")
                {
                    this.Emprunter(int.Parse(label4.Text), int.Parse(comboBox3.Text));
                }
                if (button1.Text == "Réserver")
                {
                    this.Reserver(int.Parse(label4.Text), int.Parse(label5.Text));
                }
                if (button1.Text == "Retourner")
                {
                    this.Rendre(int.Parse(label4.Text), int.Parse(comboBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString()), comboBox4.Text);
                }
                if (button1.Text == "Annuler Réservation")
                {
                    DialogResult r = MessageBox.Show("Voulez-vous vraiment annuler cette réservation ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (r == DialogResult.Yes)
                        this.Annuler(int.Parse(label4.Text), int.Parse(label5.Text));
                }
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
