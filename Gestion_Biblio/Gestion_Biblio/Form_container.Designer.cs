namespace Gestion_Biblio
{
    partial class Form_container
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_container));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gestionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionOeuvreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionAuteurToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionToolStripMenuItem1,
            this.gestionToolStripMenuItem,
            this.gestionOeuvreToolStripMenuItem,
            this.gestionAuteurToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1042, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gestionToolStripMenuItem1
            // 
            this.gestionToolStripMenuItem1.Name = "gestionToolStripMenuItem1";
            this.gestionToolStripMenuItem1.Size = new System.Drawing.Size(174, 20);
            this.gestionToolStripMenuItem1.Text = "Gestion Réservation/Emprunt";
            this.gestionToolStripMenuItem1.Click += new System.EventHandler(this.gestionToolStripMenuItem1_Click);
            // 
            // gestionToolStripMenuItem
            // 
            this.gestionToolStripMenuItem.Name = "gestionToolStripMenuItem";
            this.gestionToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.gestionToolStripMenuItem.Text = "Gestion Usager";
            this.gestionToolStripMenuItem.Click += new System.EventHandler(this.gestionToolStripMenuItem_Click);
            // 
            // gestionOeuvreToolStripMenuItem
            // 
            this.gestionOeuvreToolStripMenuItem.Name = "gestionOeuvreToolStripMenuItem";
            this.gestionOeuvreToolStripMenuItem.Size = new System.Drawing.Size(162, 20);
            this.gestionOeuvreToolStripMenuItem.Text = "Gestion Oeuvre/Exemplaire";
            this.gestionOeuvreToolStripMenuItem.Click += new System.EventHandler(this.gestionOeuvreToolStripMenuItem_Click);
            // 
            // gestionAuteurToolStripMenuItem1
            // 
            this.gestionAuteurToolStripMenuItem1.Name = "gestionAuteurToolStripMenuItem1";
            this.gestionAuteurToolStripMenuItem1.Size = new System.Drawing.Size(98, 20);
            this.gestionAuteurToolStripMenuItem1.Text = "Gestion Auteur";
            this.gestionAuteurToolStripMenuItem1.Click += new System.EventHandler(this.gestionAuteurToolStripMenuItem1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 718);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1042, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(494, 17);
            this.toolStripStatusLabel1.Text = "Application développé par Mohamed Haytham EL FADLAOUI et Anas RAHMOUN M1 MIAGE";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // Form_container
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 740);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form_container";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Form_container_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gestionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionAuteurToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gestionOeuvreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionToolStripMenuItem1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}