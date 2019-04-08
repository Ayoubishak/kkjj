﻿using System;
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
        static int g = 1;
        public Ihm_usager()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean b = false;
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
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
            if (textBox3.Text == string.Empty)
            {
                errorProvider3.SetError(textBox3, "il faut remplir ce champ");
                b = true;
            }
            if (textBox4.Text == string.Empty)
            {
                errorProvider4.SetError(textBox4, "il faut remplir ce champ");
                b = true;
            }
            if (richTextBox1.Text == string.Empty)
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
                        this.Maj(int.Parse(comboBox1.SelectedText), textBox1.Text, textBox2.Text, richTextBox1.Text, textBox3.Text, textBox4.Text, int.Parse(numericUpDown1.Value.ToString()));
                    else
                        this.Retirer(int.Parse(comboBox1.Text.ToString()));
                }
            }
        }

        private void Ajouter(string nom, string prenom, string address, string tel, string email)
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Ajouter(nom, prenom, address, tel, email);
            button2_Click(null, null);
            this.Load_All_Usager();
        }
        private void Maj(int id, string nom, string prenom, string address, string tel, string email,int retard)
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Maj(id, nom, prenom, address, tel, email, retard);
            button2_Click(null, null);
            this.Load_All_Usager();
        }
        private void Retirer(int id)
        {
            Gestion_usager gu = new Gestion_usager();
            gu.Retirer(id);
            button2_Click(null, null);
            this.Load_All_Usager();
        }

        private void button2_Click(object sender, EventArgs e)
        {
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
            label8.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
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
            label7.Visible =false;
            label8.Visible = false;
            numericUpDown1.Visible = false;
            comboBox1.Visible = false;
            button1.Text = "Ajouter";
        }

        private void button4_Click(object sender, EventArgs e)
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
            label8.Visible = false;
            numericUpDown1.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Modifier";
        }

        private void button5_Click(object sender, EventArgs e)
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
            label8.Visible = false;
            numericUpDown1.Visible = true;
            comboBox1.Visible = true;
            button1.Text = "Retirer";
        }
        private void Load_All_Usager()
        {
            if (comboBox1.Text != "" || g==1)
            {
                g--;
                label8.Visible = false;
                comboBox1.Items.Clear();
                cnx.Open();
                string requete = "SELECT [IDU] FROM [USAGER]";
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

        private void Ihm_usager_Load(object sender, EventArgs e)
        {
            this.Load_All_Usager();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
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
                if(Dr[7].ToString()=="True")
                    label8.Visible = true;
            }
            Dr.Close();
            cnx.Close();
        }
    }
}
