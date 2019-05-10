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
    public partial class Ihm_usager : Form
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public Ihm_usager()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Executer (Ajout, Modification, Retrait) d'un usager
        {
            Boolean b = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            if (textBox1.Text == string.Empty && button1.Text != "Retirer")
            {
                errorProvider1.SetError(textBox1, "il faut remplir ce champ");
                b = true;
            }
            if (textBox2.Text == string.Empty && button1.Text != "Retirer")
            {
                errorProvider2.SetError(textBox2, "il faut remplir ce champ");
                b = true;
            }
            if (textBox3.Text == string.Empty && button1.Text != "Retirer")
            {
                errorProvider3.SetError(textBox3, "il faut remplir ce champ");
                b = true;
            }
            if (textBox4.Text == string.Empty && button1.Text != "Retirer")
            {
                errorProvider4.SetError(textBox4, "il faut remplir ce champ");
                b = true;
            }
            if (richTextBox1.Text == string.Empty && button1.Text != "Retirer")
            {
                errorProvider5.SetError(richTextBox1, "il faut remplir ce champ");
                b = true;
            }
            if (comboBox1.Text == string.Empty && button1.Text!="Ajouter")
            {
                errorProvider6.SetError(comboBox1, "il faut choisir un identifiant d'un usager");
                b = true;
            }

            if (b == false)
            {
                if (button1.Text == "Ajouter")
                    this.Ajouter(textBox1.Text, textBox2.Text, richTextBox1.Text, textBox3.Text, textBox4.Text);
                else
                {
                    if (button1.Text == "Modifier")
                    {
                        DialogResult r = MessageBox.Show("Voulez-vous vraiment modifier cet usager ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                            this.Maj(int.Parse(comboBox1.Text), textBox1.Text, textBox2.Text, richTextBox1.Text, textBox3.Text, textBox4.Text, int.Parse(numericUpDown1.Value.ToString()));
                    }
                    else
                    {
                        DialogResult r = MessageBox.Show("Voulez-vous vraiment retirer cet usager ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                            this.Retirer(int.Parse(comboBox1.Text.ToString()));
                    }
                }
            }
        }

        private void Ajouter(string nom, string prenom, string adresse, string tel, string email) //Ajouter Usager
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Ajouter(nom, prenom, adresse, tel, email);
            button2_Click(null, null);
            this.Load_Datagridview();
            this.Load_All_Usager();
        }
        private void Maj(int idu, string nom, string prenom, string adresse, string tel, string email,int retard) //Modifier Usager
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Maj(idu, nom, prenom, adresse, tel, email, retard);
            button2_Click(null, null);
            this.Load_Datagridview();
            this.Load_All_Usager();
        }
        private void Retirer(int idu) //Retirer Usager
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Retirer(idu);
            button2_Click(null, null);
            this.Load_Datagridview();
            this.Load_All_Usager();
        }

        private void button2_Click(object sender, EventArgs e) //Réinitialiser le formulaire
        {
            dataGridView1.ClearSelection();
            comboBox1.SelectedIndex = -1;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            richTextBox1.Text = "";
            numericUpDown1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e) //Modifier le formulaire pour ajouter un usager
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            button3.Enabled = false;
            button4.Enabled = true;
            button5.Enabled = true;
            label6.Visible = false;
            label7.Visible = false;
            numericUpDown1.Visible = false;
            comboBox1.Visible = false;
            button1.Text = "Ajouter";
        }

        private void button4_Click(object sender, EventArgs e) //Modifier le formulaire pour modifier un usager
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            button3.Enabled = true;
            button4.Enabled = false;
            button5.Enabled = true;
            label6.Visible = true;
            label7.Visible = true;
            numericUpDown1.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Modifier";
        }

        private void button5_Click(object sender, EventArgs e) //Modifier le formulaire pour retirer un usager
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = false;
            label6.Visible = true;
            label7.Visible = true;
            numericUpDown1.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Retirer";
        }
        private void Load_All_Usager() //Charger les identifiants tous les utilisateur non retirer
        {
            comboBox1.Items.Clear();
            cnx.Open();
            string requete = "SELECT [IDU] FROM [USAGER] WHERE USAGERSUP = 0";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBox1.Items.Add(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();

        }

        private void Ihm_usager_Load(object sender, EventArgs e)
        {
            this.Load_All_Usager();
            this.Load_Datagridview();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Remplir les champs avec l'id
        {
            cnx.Open();
            string requete = "SELECT * FROM [USAGER] WHERE [IDU] = @Id";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Id", comboBox1.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                textBox1.Text = Dr[1].ToString();
                textBox2.Text = Dr[2].ToString();
                richTextBox1.Text = Dr[3].ToString();
                textBox3.Text = Dr[4].ToString();
                textBox4.Text = Dr[5].ToString();
                numericUpDown1.Value = decimal.Parse(Dr[6].ToString());
            }
            Dr.Close();
            cnx.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        public void Load_Datagridview() //Remplir le datagridview
        {
            cnx.Open();
            string requete = "SELECT [IDU] [Identifiant],[NOM] [Nom],[PRENOM] [Prénom],[ADRESSE] [Adresse],[TEL] [N° de Téléphone],[EMAIL] [Email],[RETARD] [Jour de Retard] FROM [USAGER] WHERE [USAGERSUP] = 0";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            SqlDataReader Dr = cmd.ExecuteReader();
            DataTable t = new DataTable();
            t.Load(Dr);
            Dr.Close();
            cnx.Close();
            dataGridView1.DataSource = t;
            dataGridView1.CurrentCell = null;
            dataGridView1.Refresh();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e) //Enlever la selection par defaut du datagridview 
        {
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
        }
    }
}
