namespace Fiche_nouveau_prof
{
    partial class Principal
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
            this.Email = new System.Windows.Forms.TextBox();
            this.Copieur = new System.Windows.Forms.TextBox();
            this.BtnEnvoyerMail = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPrénom = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblCodeCopieur = new System.Windows.Forms.Label();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUtilisateur = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDomaine2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDomaine1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAdresseIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCrétionUtilisateurAd = new System.Windows.Forms.Button();
            this.lblGroupe = new System.Windows.Forms.Label();
            this.txbGroupe = new System.Windows.Forms.TextBox();
            this.lblMdp = new System.Windows.Forms.Label();
            this.txbMdp = new System.Windows.Forms.TextBox();
            this.btnConnexionAD = new System.Windows.Forms.Button();
            this.lblEtatConnexionAd = new System.Windows.Forms.Label();
            this.rdBtnEleve = new System.Windows.Forms.RadioButton();
            this.rdBtnProf = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportUtilisateurs = new System.Windows.Forms.Button();
            this.lblCheminFichierExcel = new System.Windows.Forms.Label();
            this.btnLancer = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.grpBoxOu = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.grpBoxOu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListeProfs
            // 
            this.ListeProfs.FormattingEnabled = true;
            this.ListeProfs.Location = new System.Drawing.Point(670, 136);
            this.ListeProfs.Name = "ListeProfs";
            this.ListeProfs.Size = new System.Drawing.Size(279, 319);
            this.ListeProfs.TabIndex = 0;
            // 
            // Nom
            // 
            this.Nom.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.Nom.Location = new System.Drawing.Point(452, 136);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(100, 20);
            this.Nom.TabIndex = 1;
            // 
            // Prénom
            // 
            this.Prénom.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.Prénom.Location = new System.Drawing.Point(452, 175);
            this.Prénom.Name = "Prénom";
            this.Prénom.Size = new System.Drawing.Size(100, 20);
            this.Prénom.TabIndex = 2;
            // 
            // BtnValider
            // 
            this.BtnValider.Location = new System.Drawing.Point(452, 392);
            this.BtnValider.Name = "BtnValider";
            this.BtnValider.Size = new System.Drawing.Size(100, 23);
            this.BtnValider.TabIndex = 7;
            this.BtnValider.Text = "Créer la fiche";
            this.BtnValider.UseVisualStyleBackColor = true;
            this.BtnValider.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // Email
            // 
            this.Email.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.Email.Location = new System.Drawing.Point(452, 218);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(100, 20);
            this.Email.TabIndex = 3;
            // 
            // Copieur
            // 
            this.Copieur.Location = new System.Drawing.Point(452, 259);
            this.Copieur.Name = "Copieur";
            this.Copieur.Size = new System.Drawing.Size(100, 20);
            this.Copieur.TabIndex = 4;
            // 
            // BtnEnvoyerMail
            // 
            this.BtnEnvoyerMail.Location = new System.Drawing.Point(767, 472);
            this.BtnEnvoyerMail.Name = "BtnEnvoyerMail";
            this.BtnEnvoyerMail.Size = new System.Drawing.Size(96, 23);
            this.BtnEnvoyerMail.TabIndex = 6;
            this.BtnEnvoyerMail.Text = "Envoyer eMail";
            this.BtnEnvoyerMail.UseVisualStyleBackColor = true;
            this.BtnEnvoyerMail.Click += new System.EventHandler(this.BtnEnvoyer_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(410, 139);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(29, 13);
            this.lblNom.TabIndex = 7;
            this.lblNom.Text = "Nom";
            // 
            // lblPrénom
            // 
            this.lblPrénom.AutoSize = true;
            this.lblPrénom.Location = new System.Drawing.Point(396, 178);
            this.lblPrénom.Name = "lblPrénom";
            this.lblPrénom.Size = new System.Drawing.Size(43, 13);
            this.lblPrénom.TabIndex = 8;
            this.lblPrénom.Text = "Prénom";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(407, 221);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(32, 13);
            this.lblMail.TabIndex = 9;
            this.lblMail.Text = "eMail";
            // 
            // lblCodeCopieur
            // 
            this.lblCodeCopieur.AutoSize = true;
            this.lblCodeCopieur.Location = new System.Drawing.Point(369, 262);
            this.lblCodeCopieur.Name = "lblCodeCopieur";
            this.lblCodeCopieur.Size = new System.Drawing.Size(70, 13);
            this.lblCodeCopieur.TabIndex = 10;
            this.lblCodeCopieur.Text = "Code copieur";
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.Location = new System.Drawing.Point(157, 229);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.PasswordChar = '*';
            this.txtMotDePasse.Size = new System.Drawing.Size(153, 20);
            this.txtMotDePasse.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Mot de passe :";
            // 
            // txtUtilisateur
            // 
            this.txtUtilisateur.Location = new System.Drawing.Point(157, 179);
            this.txtUtilisateur.Name = "txtUtilisateur";
            this.txtUtilisateur.Size = new System.Drawing.Size(153, 20);
            this.txtUtilisateur.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Nom d\'utilisateur :";
            // 
            // txtDomaine2
            // 
            this.txtDomaine2.Location = new System.Drawing.Point(157, 129);
            this.txtDomaine2.Name = "txtDomaine2";
            this.txtDomaine2.Size = new System.Drawing.Size(153, 20);
            this.txtDomaine2.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 132);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Domaine 2 :";
            // 
            // txtDomaine1
            // 
            this.txtDomaine1.Location = new System.Drawing.Point(157, 80);
            this.txtDomaine1.Name = "txtDomaine1";
            this.txtDomaine1.Size = new System.Drawing.Size(153, 20);
            this.txtDomaine1.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Domaine 1 :";
            // 
            // txtAdresseIp
            // 
            this.txtAdresseIp.Location = new System.Drawing.Point(157, 30);
            this.txtAdresseIp.Name = "txtAdresseIp";
            this.txtAdresseIp.Size = new System.Drawing.Size(153, 20);
            this.txtAdresseIp.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Adresse ip du serveur :";
            // 
            // btnCrétionUtilisateurAd
            // 
            this.btnCrétionUtilisateurAd.Location = new System.Drawing.Point(452, 432);
            this.btnCrétionUtilisateurAd.Name = "btnCrétionUtilisateurAd";
            this.btnCrétionUtilisateurAd.Size = new System.Drawing.Size(100, 23);
            this.btnCrétionUtilisateurAd.TabIndex = 8;
            this.btnCrétionUtilisateurAd.Text = "Créer compte AD";
            this.btnCrétionUtilisateurAd.UseVisualStyleBackColor = true;
            this.btnCrétionUtilisateurAd.Click += new System.EventHandler(this.BtnCréationUtilisateurAdClick);
            // 
            // lblGroupe
            // 
            this.lblGroupe.AutoSize = true;
            this.lblGroupe.Location = new System.Drawing.Point(396, 305);
            this.lblGroupe.Name = "lblGroupe";
            this.lblGroupe.Size = new System.Drawing.Size(42, 13);
            this.lblGroupe.TabIndex = 26;
            this.lblGroupe.Text = "Groupe";
            // 
            // txbGroupe
            // 
            this.txbGroupe.Location = new System.Drawing.Point(452, 302);
            this.txbGroupe.Name = "txbGroupe";
            this.txbGroupe.Size = new System.Drawing.Size(100, 20);
            this.txbGroupe.TabIndex = 5;
            // 
            // lblMdp
            // 
            this.lblMdp.AutoSize = true;
            this.lblMdp.Location = new System.Drawing.Point(369, 347);
            this.lblMdp.Name = "lblMdp";
            this.lblMdp.Size = new System.Drawing.Size(71, 13);
            this.lblMdp.TabIndex = 28;
            this.lblMdp.Text = "Mot de passe";
            // 
            // txbMdp
            // 
            this.txbMdp.Location = new System.Drawing.Point(452, 344);
            this.txbMdp.Name = "txbMdp";
            this.txbMdp.Size = new System.Drawing.Size(100, 20);
            this.txbMdp.TabIndex = 6;
            // 
            // btnConnexionAD
            // 
            this.btnConnexionAD.Location = new System.Drawing.Point(157, 286);
            this.btnConnexionAD.Name = "btnConnexionAD";
            this.btnConnexionAD.Size = new System.Drawing.Size(153, 23);
            this.btnConnexionAD.TabIndex = 29;
            this.btnConnexionAD.Text = "Connexion au serveur AD";
            this.btnConnexionAD.UseVisualStyleBackColor = true;
            this.btnConnexionAD.Click += new System.EventHandler(this.BtnConnexionAD_Click);
            // 
            // lblEtatConnexionAd
            // 
            this.lblEtatConnexionAd.AutoSize = true;
            this.lblEtatConnexionAd.Location = new System.Drawing.Point(154, 336);
            this.lblEtatConnexionAd.Name = "lblEtatConnexionAd";
            this.lblEtatConnexionAd.Size = new System.Drawing.Size(66, 13);
            this.lblEtatConnexionAd.TabIndex = 30;
            this.lblEtatConnexionAd.Text = "Déconnecté";
            // 
            // rdBtnEleve
            // 
            this.rdBtnEleve.AutoSize = true;
            this.rdBtnEleve.Location = new System.Drawing.Point(76, 19);
            this.rdBtnEleve.Name = "rdBtnEleve";
            this.rdBtnEleve.Size = new System.Drawing.Size(57, 17);
            this.rdBtnEleve.TabIndex = 31;
            this.rdBtnEleve.TabStop = true;
            this.rdBtnEleve.Text = "Eleves";
            this.rdBtnEleve.UseVisualStyleBackColor = true;
            // 
            // rdBtnProf
            // 
            this.rdBtnProf.AutoSize = true;
            this.rdBtnProf.Location = new System.Drawing.Point(8, 19);
            this.rdBtnProf.Name = "rdBtnProf";
            this.rdBtnProf.Size = new System.Drawing.Size(49, 17);
            this.rdBtnProf.TabIndex = 32;
            this.rdBtnProf.TabStop = true;
            this.rdBtnProf.Text = "Profs";
            this.rdBtnProf.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnexionAD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAdresseIp);
            this.groupBox1.Controls.Add(this.lblEtatConnexionAd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDomaine1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDomaine2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUtilisateur);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtMotDePasse);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 361);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connexion AD";
            // 
            // btnImportUtilisateurs
            // 
            this.btnImportUtilisateurs.Location = new System.Drawing.Point(452, 472);
            this.btnImportUtilisateurs.Name = "btnImportUtilisateurs";
            this.btnImportUtilisateurs.Size = new System.Drawing.Size(100, 23);
            this.btnImportUtilisateurs.TabIndex = 9;
            this.btnImportUtilisateurs.Text = "Fichier utilisateurs";
            this.btnImportUtilisateurs.UseVisualStyleBackColor = true;
            this.btnImportUtilisateurs.Click += new System.EventHandler(this.BtnImportUtilisateurs_Click);
            // 
            // lblCheminFichierExcel
            // 
            this.lblCheminFichierExcel.AutoSize = true;
            this.lblCheminFichierExcel.Location = new System.Drawing.Point(449, 507);
            this.lblCheminFichierExcel.Name = "lblCheminFichierExcel";
            this.lblCheminFichierExcel.Size = new System.Drawing.Size(0, 13);
            this.lblCheminFichierExcel.TabIndex = 35;
            // 
            // btnLancer
            // 
            this.btnLancer.Location = new System.Drawing.Point(452, 541);
            this.btnLancer.Name = "btnLancer";
            this.btnLancer.Size = new System.Drawing.Size(75, 23);
            this.btnLancer.TabIndex = 10;
            this.btnLancer.Text = "Valider";
            this.btnLancer.UseVisualStyleBackColor = true;
            this.btnLancer.Click += new System.EventHandler(this.BtnLancer_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(605, 507);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // grpBoxOu
            // 
            this.grpBoxOu.Controls.Add(this.rdBtnEleve);
            this.grpBoxOu.Controls.Add(this.rdBtnProf);
            this.grpBoxOu.Location = new System.Drawing.Point(413, 75);
            this.grpBoxOu.Name = "grpBoxOu";
            this.grpBoxOu.Size = new System.Drawing.Size(139, 46);
            this.grpBoxOu.TabIndex = 37;
            this.grpBoxOu.TabStop = false;
            this.grpBoxOu.Text = "Unité d\'organisation";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1472, 575);
            this.Controls.Add(this.grpBoxOu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnLancer);
            this.Controls.Add(this.lblCheminFichierExcel);
            this.Controls.Add(this.btnImportUtilisateurs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblMdp);
            this.Controls.Add(this.txbMdp);
            this.Controls.Add(this.lblGroupe);
            this.Controls.Add(this.txbGroupe);
            this.Controls.Add(this.btnCrétionUtilisateurAd);
            this.Controls.Add(this.lblCodeCopieur);
            this.Controls.Add(this.lblMail);
            this.Controls.Add(this.lblPrénom);
            this.Controls.Add(this.lblNom);
            this.Controls.Add(this.BtnEnvoyerMail);
            this.Controls.Add(this.Copieur);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.BtnValider);
            this.Controls.Add(this.Prénom);
            this.Controls.Add(this.Nom);
            this.Controls.Add(this.ListeProfs);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.OuvertureLogiciel);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpBoxOu.ResumeLayout(false);
            this.grpBoxOu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ListeProfs;
        private System.Windows.Forms.TextBox Nom;
        private System.Windows.Forms.TextBox Prénom;
        private System.Windows.Forms.Button BtnValider;
        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.TextBox Copieur;
        private System.Windows.Forms.Button BtnEnvoyerMail;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPrénom;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblCodeCopieur;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUtilisateur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDomaine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDomaine1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAdresseIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCrétionUtilisateurAd;
        private System.Windows.Forms.Label lblGroupe;
        private System.Windows.Forms.TextBox txbGroupe;
        private System.Windows.Forms.Label lblMdp;
        private System.Windows.Forms.TextBox txbMdp;
        private System.Windows.Forms.Button btnConnexionAD;
        private System.Windows.Forms.Label lblEtatConnexionAd;
        private System.Windows.Forms.RadioButton rdBtnEleve;
        private System.Windows.Forms.RadioButton rdBtnProf;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnImportUtilisateurs;
        private System.Windows.Forms.Label lblCheminFichierExcel;
        private System.Windows.Forms.Button btnLancer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox grpBoxOu;
    }
}

