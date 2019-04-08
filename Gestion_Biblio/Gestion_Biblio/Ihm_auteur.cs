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
        int g = 1;
        public Ihm_auteur()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
                    this.Majauteur(int.Parse(label4.Text), textBox1.Text, textBox2.Text);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
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

        private void Ajouter(string nom,string prenom)
        {
            Gestion_auteur ga = new Gestion_auteur();
            ga.Ajouter(nom, prenom);
            this.Load_All_Auteur();
            button2_Click(null, null);
        }

        private void Majauteur(int id,string nom, string prenom)
        {
            Gestion_auteur ga = new Gestion_auteur();
            ga.Maj(id, nom, prenom);
            this.Load_All_Auteur();
            button2_Click(null, null);
        }

        private void Modifier(int id,string nom, string prenom)
        {
            Gestion_auteur ga = new Gestion_auteur();
            ga.Maj(id,nom, prenom);
            button2_Click(null, null);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = true;
            label3.Visible = false;
            comboBox1.Visible = false;
            button1.Text = "Ajouter";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = false;
            label3.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Modifier";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void Ihm_auteur_Load(object sender, EventArgs e)
        {
            this.Load_All_Auteur();
        }
        private void Load_All_Auteur()
        {
            if (comboBox1.Text != "" || g == 1)
            {
                g--;
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
        }
    }
}
