namespace Fiche_nouveau_prof
{
    partial class Informations
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
            this.lblNomComplet = new System.Windows.Forms.Label();
            this.LboxGroupes = new System.Windows.Forms.ListBox();
            this.lblHomeDirectory = new System.Windows.Forms.Label();
            this.PhotoElève = new System.Windows.Forms.PictureBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblGroupes = new System.Windows.Forms.Label();
            this.lblHomeDrive = new System.Windows.Forms.Label();
            this.lblProfilePath = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoElève)).BeginInit();
            this.SuspendLayout();
            // 
            // lblNomComplet
            // 
            this.lblNomComplet.AutoSize = true;
            this.lblNomComplet.BackColor = System.Drawing.SystemColors.Control;
            this.lblNomComplet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomComplet.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblNomComplet.Location = new System.Drawing.Point(63, 22);
            this.lblNomComplet.Name = "lblNomComplet";
            this.lblNomComplet.Size = new System.Drawing.Size(111, 20);
            this.lblNomComplet.TabIndex = 0;
            this.lblNomComplet.Text = "NomComplet";
            // 
            // LboxGroupes
            // 
            this.LboxGroupes.FormattingEnabled = true;
            this.LboxGroupes.Location = new System.Drawing.Point(265, 62);
            this.LboxGroupes.Name = "LboxGroupes";
            this.LboxGroupes.Size = new System.Drawing.Size(153, 186);
            this.LboxGroupes.TabIndex = 1;
            // 
            // lblHomeDirectory
            // 
            this.lblHomeDirectory.AutoSize = true;
            this.lblHomeDirectory.Location = new System.Drawing.Point(64, 326);
            this.lblHomeDirectory.Name = "lblHomeDirectory";
            this.lblHomeDirectory.Size = new System.Drawing.Size(77, 13);
            this.lblHomeDirectory.TabIndex = 2;
            this.lblHomeDirectory.Text = "HomeDirectory";
            // 
            // PhotoElève
            // 
            this.PhotoElève.Location = new System.Drawing.Point(67, 62);
            this.PhotoElève.Name = "PhotoElève";
            this.PhotoElève.Size = new System.Drawing.Size(148, 182);
            this.PhotoElève.TabIndex = 53;
            this.PhotoElève.TabStop = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.SystemColors.Control;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblDescription.Location = new System.Drawing.Point(63, 264);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(100, 20);
            this.lblDescription.TabIndex = 54;
            this.lblDescription.Text = "Description";
            // 
            // lblGroupes
            // 
            this.lblGroupes.AutoSize = true;
            this.lblGroupes.BackColor = System.Drawing.SystemColors.Control;
            this.lblGroupes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupes.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblGroupes.Location = new System.Drawing.Point(299, 22);
            this.lblGroupes.Name = "lblGroupes";
            this.lblGroupes.Size = new System.Drawing.Size(78, 20);
            this.lblGroupes.TabIndex = 55;
            this.lblGroupes.Text = "Groupes";
            // 
            // lblHomeDrive
            // 
            this.lblHomeDrive.AutoSize = true;
            this.lblHomeDrive.Location = new System.Drawing.Point(64, 375);
            this.lblHomeDrive.Name = "lblHomeDrive";
            this.lblHomeDrive.Size = new System.Drawing.Size(60, 13);
            this.lblHomeDrive.TabIndex = 56;
            this.lblHomeDrive.Text = "HomeDrive";
            // 
            // lblProfilePath
            // 
            this.lblProfilePath.AutoSize = true;
            this.lblProfilePath.Location = new System.Drawing.Point(64, 352);
            this.lblProfilePath.Name = "lblProfilePath";
            this.lblProfilePath.Size = new System.Drawing.Size(58, 13);
            this.lblProfilePath.TabIndex = 57;
            this.lblProfilePath.Text = "ProfilePath";
            // 
            // Informations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 525);
            this.Controls.Add(this.lblProfilePath);
            this.Controls.Add(this.lblHomeDrive);
            this.Controls.Add(this.lblGroupes);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.PhotoElève);
            this.Controls.Add(this.lblHomeDirectory);
            this.Controls.Add(this.LboxGroupes);
            this.Controls.Add(this.lblNomComplet);
            this.Name = "Informations";
            this.Text = "Informations";
            this.Load += new System.EventHandler(this.Informations_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PhotoElève)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNomComplet;
        private System.Windows.Forms.ListBox LboxGroupes;
        private System.Windows.Forms.Label lblHomeDirectory;
        private System.Windows.Forms.PictureBox PhotoElève;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblGroupes;
        private System.Windows.Forms.Label lblHomeDrive;
        private System.Windows.Forms.Label lblProfilePath;
    }
}