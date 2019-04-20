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
        public Ihm_oeuvre_et_exemplaire()
        {
            InitializeComponent();
        }

        private void button9_Click(object sender, EventArgs e) //Modifier le formulaire pour la gestion des exemplaires
        {
            button2_Click(null, null);
            button9.Enabled = false;
            button8.Enabled = true;
            button7.Visible = true;
            label9.Text = "Choisir type d'Exemplaire";
            button5.Text = "Ajouter Exemplaire";
            button6.Text = "Modifier Exemplaire";
            panel4.Visible = false;
            panel1.Visible = true;
            if (button5.Enabled == true)
                panel5.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e) //Réinitialiser le formulaire
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            comboBox5.Items.Clear();
            label8.Text = "";
            label7.Text = "";
            label8.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            textBox1.Text = "";
        }

        public void Ajoutermagazine(string titre) //Ajouter magazine
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Ajoutermagazine(titre);
            button2_Click(null, null);
            this.Load_All_Magazine();
        }

        public void Majmagazine(int idm, string titre) //Modifier magazine
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Majmagazine(idm, titre);
            button2_Click(null, null);
            this.Load_All_Magazine();
        }

        public void Ajouterlivre(string titre, int ida) //Ajouter livre
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Ajouterlivre(titre, ida);
            button2_Click(null, null);
            this.Load_All_Livre();
        }

        public void Majlivre(int idl, string titre, int ida) //Modifier livre
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Majlivre(idl, titre, ida);
            button2_Click(null, null);
            this.Load_All_Livre();
        }

        public void Ajouterexemplaire(int ido, string etat) //Ajouter exemplaire
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Ajouterexemplaire(ido, etat);
            if (button3.Enabled == false)
                this.Load_All_Livre();
            else
                this.Load_All_Magazine();
            button2_Click(null, null); 
        }

        public void Majexemplaire(int ide, string etat) //Modifier exemplaire
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Majexemplaire(ide, etat);
            if (button3.Enabled == false)
                this.Load_All_Livre();
            else
                this.Load_All_Magazine();
            button2_Click(null, null);
        }

        public void Retirerexemplaire(int ide) //Retirer exemplaire
        {
            Gestion_oeuvre_et_exemplaire goe = new Gestion_oeuvre_et_exemplaire();
            goe.Retirerexemplaire(ide);
            if (button3.Enabled == false)
                this.Load_All_Livre();
            else
                this.Load_All_Magazine();
            button2_Click(null, null);
        }

        private void Ihm_oeuvre_et_exemplaire_Load(object sender, EventArgs e)
        {
            this.Load_All_Livre();
            this.Load_All_Auteur();
            
        }

        private void button1_Click(object sender, EventArgs e) //Executer (Ajout, Modification, Retrait) pour oeuvre ou exemplaire
        {
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
            Boolean b = false;
            if (textBox1.Text == string.Empty && button8.Enabled == false)
            {
                errorProvider1.SetError(textBox1, "il faut remplir ce champ");
                b = true;
            }
            if (comboBox1.Text == string.Empty && button5.Enabled == true)
            {
                errorProvider2.SetError(comboBox1, "il faut choisir une Oeuvre");
                b = true;
            }
            if (comboBox2.Text == string.Empty && button3.Enabled == false && button8.Enabled==false)
            {
                errorProvider3.SetError(comboBox2, "il faut choisir une Auteur");
                b = true;
            }
            if (comboBox5.Text == string.Empty && button9.Enabled == false && button5.Enabled == true)
            {
                errorProvider4.SetError(comboBox5, "il faut choisir un Exemplaire");
                b = true;
            }
            if (comboBox3.Text == string.Empty && button9.Enabled == false && button7.Enabled == true)
            {
                errorProvider5.SetError(comboBox3, "il faut choisir un Etat");
                b = true;
            }
            if (comboBox4.Text == string.Empty && button9.Enabled == false && button5.Enabled == false)
            {
                errorProvider6.SetError(comboBox4, "il faut choisir une Oeuvre");
                b = true;
            }
            if (b == false)
            {
                if (button8.Enabled == false)
                {
                    if (button1.Text == "Ajouter")
                    {
                        if (button4.Enabled == false)
                            this.Ajoutermagazine(textBox1.Text);

                        else
                            this.Ajouterlivre(textBox1.Text, int.Parse(label7.Text));
                    }
                    if (button1.Text == "Modifier")
                    {
                        DialogResult r = MessageBox.Show("Voulez-vous vraiment modifier cet oeuvre ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (r == DialogResult.Yes)
                        {
                            if (button4.Enabled == false)
                                this.Majmagazine(int.Parse(label6.Text), textBox1.Text);

                            else
                                this.Majlivre(int.Parse(label6.Text), textBox1.Text, int.Parse(label7.Text));
                        }
                    }
                }
                else
                {
                    if (button1.Text == "Ajouter")
                        this.Ajouterexemplaire(int.Parse(label8.Text), comboBox3.Text);
                    else
                    {
                        if (button1.Text == "Modifier")
                        {
                            DialogResult r = MessageBox.Show("Voulez-vous vraiment modifier cet exemplaire ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (r == DialogResult.Yes)
                                this.Majexemplaire(int.Parse(comboBox5.Text), comboBox3.Text);
                        }
                        else
                        {
                            DialogResult r = MessageBox.Show("Voulez-vous vraiment retirer cet exemplaire ?", "Attention !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (r == DialogResult.Yes)
                                this.Retirerexemplaire(int.Parse(comboBox5.Text));
                        }
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e) //Modifier le formulaire pour la saisie des informations d'un livre
        {
            button2_Click(null, null);
            this.Load_All_Livre();
            button3.Enabled = false;
            button4.Enabled = true;
            if (button8.Enabled == false)
            {
                panel3.Visible = true;
            }
        }

        private void button4_Click(object sender, EventArgs e) //Modifier le formulaire pour la saisie des informations d'un magazine
        {
            button2_Click(null, null);
            this.Load_All_Magazine();
            button3.Enabled = true;
            button4.Enabled = false;
            if (button8.Enabled == false)
            {
                panel3.Visible = false;
            }
        }

        private void button5_Click(object sender, EventArgs e) //Modifier le formulaire pour l'ajout
        {
            if (button9.Enabled == false)
            {
                panel5.Visible = true;
                panel8.Visible = true;
            }           
            panel6.Visible = true;
            panel2.Visible = false;
            button5.Enabled = false;
            button6.Enabled = true;
            button7.Enabled = true;
            button1.Text = "Ajouter";
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
        }

        private void button6_Click(object sender, EventArgs e) //Modifier le formulaire pour la modification
        {
            if (button9.Enabled == false)
            {
                panel5.Visible = true;
                panel8.Visible = true;
            }
            panel6.Visible = false;
            panel2.Visible = true;
            button5.Enabled = true;
            button6.Enabled = false;
            button7.Enabled = true;
            button1.Text = "Modifier";
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
        }

        private void button7_Click(object sender, EventArgs e) //Modifier le formulaire pour retirer un exemplaire
        {
            if (button9.Enabled == false)
            {
                panel5.Visible = true;
                panel8.Visible = false;
            }  
            panel6.Visible = false;
            panel2.Visible = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = false;
            button1.Text = "Retirer";
            errorProvider1.Clear();
            errorProvider2.Clear();
            errorProvider3.Clear();
            errorProvider4.Clear();
            errorProvider5.Clear();
            errorProvider6.Clear();
        }
        private void Load_All_Magazine() //Charger tous les magazine
        {
            label8.Text = "";
            label6.Text = "";
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
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
        }
        private void Load_All_Livre() //Charger tous les livres
        {
            label8.Text = "";
            label6.Text = "";
            comboBox1.Items.Clear();
            comboBox4.Items.Clear();
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
        private void Load_All_Exemplaire() //Charger tous les exemplaires
        {
            comboBox5.Items.Clear();
            cnx.Open();
            string requete = "SELECT [IDE] FROM [EXEMPLAIRE] WHERE [IDO]=@Ido AND [DISPONIBLE]=1";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ido", label6.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBox5.Items.Add(Dr[0].ToString());
            }
            Dr.Close();
            cnx.Close();
        }

        private void Load_All_Auteur() //Charger tous les auteurs
        {
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //Identifier l'id d'une oeuvre
        {
            label6.Text = "";
            Auteur a = new Auteur();
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
            if (button9.Enabled == false)
                this.Load_All_Exemplaire();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) //Identifier l'id d'un auteur
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

        private void button8_Click(object sender, EventArgs e) //Modifier le formulaire pour la gestion des oeuvres
        {
            button2_Click(null, null);
            button9.Enabled = true;
            button8.Enabled = false;
            button7.Visible = false;
            label9.Text = "Choisir type d'Oeuvre";
            button5.Text = "Ajouter Oeuvre";
            button6.Text = "Modifier Oeuvre";
            panel4.Visible = true;
            panel1.Visible = false;
            panel5.Visible = false;
            if (button7.Enabled == false)
                button5_Click(null, null);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) //Identifier l'id d'une oeuvre
        {
            label8.Text = "";
            string requete = "";
            if (button3.Enabled == false)
                requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Livre'";
            else
                requete = "SELECT * FROM [OEUVRE] WHERE [TITRE]=@Titre AND [TYPE]='Magazine'";
            cnx.Open();
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Titre", comboBox4.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                label8.Text = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) // Identifier l'id d'un exemplaire
        {
            cnx.Open();
            string requete = "SELECT [ETAT] FROM [EXEMPLAIRE] WHERE [IDE]= @Ide";
            SqlCommand cmd = new SqlCommand(requete, cnx);
            cmd.Parameters.AddWithValue("@Ide", comboBox5.Text);
            SqlDataReader Dr = cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBox3.Text = Dr[0].ToString();
            }
            Dr.Close();
            cnx.Close();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
