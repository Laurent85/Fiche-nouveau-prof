namespace Fiche_nouveau_prof
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListeProfs = new System.Windows.Forms.CheckedListBox();
            this.Nom = new System.Windows.Forms.TextBox();
            this.Prénom = new System.Windows.Forms.TextBox();
            this.BtnValider = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListeProfs
            // 
            this.ListeProfs.FormattingEnabled = true;
            this.ListeProfs.Location = new System.Drawing.Point(676, 152);
            this.ListeProfs.Name = "ListeProfs";
            this.ListeProfs.Size = new System.Drawing.Size(279, 319);
            this.ListeProfs.TabIndex = 0;
            // 
            // Nom
            // 
            this.Nom.Location = new System.Drawing.Point(299, 193);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(100, 20);
            this.Nom.TabIndex = 1;
            // 
            // Prénom
            // 
            this.Prénom.Location = new System.Drawing.Point(299, 250);
            this.Prénom.Name = "Prénom";
            this.Prénom.Size = new System.Drawing.Size(100, 20);
            this.Prénom.TabIndex = 2;
            // 
            // BtnValider
            // 
            this.BtnValider.Location = new System.Drawing.Point(299, 386);
            this.BtnValider.Name = "BtnValider";
            this.BtnValider.Size = new System.Drawing.Size(75, 23);
            this.BtnValider.TabIndex = 3;
            this.BtnValider.Text = "Valider";
            this.BtnValider.UseVisualStyleBackColor = true;
            this.BtnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 493);
            this.Controls.Add(this.BtnValider);
            this.Controls.Add(this.Prénom);
            this.Controls.Add(this.Nom);
            this.Controls.Add(this.ListeProfs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ListeProfs;
        private System.Windows.Forms.TextBox Nom;
        private System.Windows.Forms.TextBox Prénom;
        private System.Windows.Forms.Button BtnValider;
    }
}

