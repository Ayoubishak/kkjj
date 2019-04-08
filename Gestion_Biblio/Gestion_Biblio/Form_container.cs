﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestion_Biblio
{
    public partial class Form_container : Form
    {
        public Form_container()
        {
            InitializeComponent();
        }

        public void fermer()
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }
            return;
        }

        private void gestionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fermer();
            this.Text = "Gestion Usager";
            Ihm_usager iu = new Ihm_usager();
            iu.MdiParent = this;
            iu.Dock = DockStyle.Fill;
            iu.Show();
        }

        private void gestionAuteurToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fermer();
            this.Text = "Gestion Auteur";
            Ihm_auteur ia = new Ihm_auteur();
            ia.MdiParent = this;
            ia.Dock = DockStyle.Fill;
            ia.Show();
        }
    }
}
