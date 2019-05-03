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
    public partial class Ihm_auteur : Form
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        public Ihm_auteur()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //(Ajouter, Modifier) Auteur
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            Boolean b = false;
            if (textBox1.Text == string.Empty)
            {
                errorProvider1.SetError(textBox1, "il faut remplir ce champ");
                b = true;
            }
            if (textBox2.Text == string.Empty)
            {
                errorProvider2.SetError(textBox2, "il faut remplir ce champ");
                b = true;
            }
            if (label4.Text == string.Empty && button1.Text == "Modifier")
            {
                errorProvider3.SetError(comboBox1, "il faut choisir un Auteur a modifier");
                b = true;
            }
            if (b == false)
            {
                if (button1.Text == "Ajouter")
                    this.Ajouter(textBox1.Text, textBox2.Text);

                else
                {
                    DialogResult r = MessageBox.Show("Voulez-vous vraiment modifier cet auteur ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if(r==DialogResult.Yes)
                        this.Majauteur(int.Parse(label4.Text), textBox1.Text, textBox2.Text);
                }
            }
        }
        
        private void button2_Click(object sender, EventArgs e) //Réinitialiser le formulaire
        {
            comboBox1.SelectedIndex = -1;
            label4.Text = "";
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
        }

        private void Ajouter(string nom,string prenom) //Ajouter auteur
        {
            Gestion_auteur ga = new Gestion_auteur();
            ga.Ajouter(nom, prenom);
            this.Load_All_Auteur();
            button2_Click(null, null);
        }

        private void Majauteur(int ida,string nom, string prenom) //Modifier auteur
        {
            Gestion_auteur ga = new Gestion_auteur();
            ga.Maj(ida, nom, prenom);
            this.Load_All_Auteur();
            button2_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e) //Modifier formulaire pour l'ajout de l'auteur
        {
            button3.Enabled = false;
            button4.Enabled = true;
            label3.Visible = false;
            comboBox1.Visible = false;
            button1.Text = "Ajouter";
        }

        private void button4_Click(object sender, EventArgs e) //Modifier formulaire pour la modification de l'auteur
        {
            button3.Enabled = true;
            button4.Enabled = false;
            label3.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Modifier";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Identifier l'auteur
        {
            if (comboBox1.Text != "")
            {
                string[] np = comboBox1.Text.Split(' ');
                textBox1.Text = np[0];
                textBox2.Text = np[1];
                cnx.Open();
                string requete = "SELECT [IDA] FROM [AUTEUR] where ([NOMA]+' '+[PRENOMA]) = @Auteur";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Auteur", comboBox1.Text);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    label4.Text = Dr[0].ToString();
                }
                Dr.Close();
                cnx.Close();
            }
        }

        private void Ihm_auteur_Load(object sender, EventArgs e)
        {
            this.Load_All_Auteur();
        }
        private void Load_All_Auteur() //Charger tous le auteurs
        {
                comboBox1.Items.Clear();
                label4.Text = "";
                cnx.Open();
                string requete = "SELECT DISTINCT([NOMA]+' '+[PRENOMA]) FROM [AUTEUR]";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    comboBox1.Items.Add(Dr[0].ToString());
                }
                Dr.Close();
                cnx.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
