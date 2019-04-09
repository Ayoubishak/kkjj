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
    public partial class Ihm_oeuvre_et_exemplaire : Form
    {
        SqlConnection cnx = new SqlConnection(Properties.Settings.Default.Biblio);
        int g = 1;
        public Ihm_oeuvre_et_exemplaire()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Enabled = false;
            button8.Enabled = true;
            button7.Visible = true;
            label1.Text = "Exemplaire a modifier";
            button5.Text = "Ajouter Exemplaire";
            button6.Text = "Modifier Exemplaire";
            panel4.Visible = false;
            panel1.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label6.Text = "";
            label7.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            textBox1.Text = "";
        }

        public void Ajoutermagazine(string titre)
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Ajoutermagazine(titre);
            button2_Click(null, null);
            this.Load_All_Magazine();
        }

        public void Majmagazine(int idm, string titre)
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Majmagazine(idm, titre);
            button2_Click(null, null);
            this.Load_All_Magazine();
        }

        public void Ajouterlivre(string titre, int ida)
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Ajouterlivre(titre, ida);
            button2_Click(null, null);
            this.Load_All_Livre();
        }

        public void Majlivre(int idl, string titre, int ida)
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Majlivre(idl, titre, ida);
            button2_Click(null, null);
            this.Load_All_Livre();
        }

        private void Ihm_oeuvre_et_exemplaire_Load(object sender, EventArgs e)
        {
            this.Load_All_Livre();
            this.Load_All_Auteur();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text=="Ajouter")
            {
                if (button4.Enabled == false)
                    this.Ajoutermagazine(textBox1.Text);

                else
                    this.Ajouterlivre(textBox1.Text, int.Parse(label7.Text));
            }
            if(button1.Text == "Modifier")
            {
                if (button4.Enabled == false)
                    this.Majmagazine(int.Parse(label6.Text), textBox1.Text);

                else
                    this.Majlivre(int.Parse(label6.Text), textBox1.Text, int.Parse(label7.Text));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Load_All_Livre();
            button3.Enabled = false;
            button4.Enabled = true;
            if (button8.Enabled == false)
            {
                panel3.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Load_All_Magazine();
            button3.Enabled = true;
            button4.Enabled = false;
            if (button8.Enabled == false)
            {
                panel3.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            button5.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = true;
            button1.Text = "Ajouter";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = true;
            button1.Text = "Modifier";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = false;
            button1.Text = "Retirer";
        }
        private void Load_All_Magazine()
        {
                comboBox1.Items.Clear();
                cnx.Open();
                string requete = "SELECT [TITRE] FROM [OEUVRE] where [TYPE]='Magazine'";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                comboBox1.Items.Add(Dr[0].ToString());
                comboBox4.Items.Add(Dr[0].ToString());
                }
                Dr.Close();
                cnx.Close();
            comboBox4 = comboBox1;
            
        }
        private void Load_All_Livre()
        {
            comboBox1.Items.Clear();
            cnx.Open();
            string requete = "SELECT [TITRE] FROM [OEUVRE] where [TYPE]='Livre'";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBox1.Items.Add(Dr[0].ToString());
                comboBox4.Items.Add(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
        }
        private void Load_All_Auteur()
        {
            if (comboBox2.Text != "" || g == 1)
            {
                g--;
                comboBox2.Items.Clear();
                label7.Text = "";
                cnx.Open();
                string requete = "SELECT DISTINCT([NOMA]+' '+[PRENOMA]) FROM [AUTEUR]";
                SqlCommand cmd = new SqlCommand(requete, cnx);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    comboBox2.Items.Add(Dr[0].ToString());
                }
                Dr.Close();
                cnx.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Text = "";
            if (comboBox1.Text != "" || g == 1)
            {
                Auteur a = new Auteur();
                g--;
                string requete = "";
                if (button3.Enabled == false)
                    requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Livre'";
                else
                    requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Magazine'";
                cnx.Open();
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Titre", comboBox1.Text);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    label6.Text = Dr[0].ToString();
                    textBox1.Text = Dr[2].ToString();
                    if (button3.Enabled == false)
                        a.Id = int.Parse(Dr[1].ToString());
                }
                Dr.Close();
                cnx.Close();
                a = a.Identifier(a.Id);
                comboBox2.Text = a.Nom + " " + a.Prenom;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cnx.Open();
            string requete = "SELECT [IDA] FROM [AUTEUR] where ([NOMA]+' '+[PRENOMA]) = @Auteur";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Auteur", comboBox2.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                label7.Text = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button9.Enabled = true;
            button8.Enabled = false;
            button7.Visible = false;          
            label1.Text = "Oeuvre a modifier";
            button5.Text = "Ajouter Oeuvre";
            button6.Text = "Modifier Oeuvre";
            panel4.Visible = true;
            panel1.Visible = false;
          
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Text = "";
            if (comboBox4.Text != "" || g == 1)
            {
                g--;
                string requete = "";
                if (button3.Enabled == false)
                    requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Livre'";
                else
                    requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Magazine'";
                cnx.Open();
                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.Parameters.AddWithValue("@Titre", comboBox1.Text);
                SqlDataReader Dr = cmd.ExecuteReader();
                while (Dr.Read())
                {
                    label8.Text = Dr[0].ToString();
                }
                Dr.Close();
                cnx.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
