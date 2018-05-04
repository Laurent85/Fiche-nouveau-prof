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
            this.ListeRésultats = new System.Windows.Forms.CheckedListBox();
            this.txbNom = new System.Windows.Forms.TextBox();
            this.txbPrénom = new System.Windows.Forms.TextBox();
            this.BtnCréerFiche = new System.Windows.Forms.Button();
            this.txbEmail = new System.Windows.Forms.TextBox();
            this.txbCopieur = new System.Windows.Forms.TextBox();
            this.BtnEnvoyerMail = new System.Windows.Forms.Button();
            this.lblNom = new System.Windows.Forms.Label();
            this.lblPrénom = new System.Windows.Forms.Label();
            this.lblMail = new System.Windows.Forms.Label();
            this.lblCodeCopieur = new System.Windows.Forms.Label();
            this.txtMotDePasse = new System.Windows.Forms.TextBox();
            this.lblMotDePasse = new System.Windows.Forms.Label();
            this.txtUtilisateur = new System.Windows.Forms.TextBox();
            this.lblNomUtilisateur = new System.Windows.Forms.Label();
            this.txtDomaine2 = new System.Windows.Forms.TextBox();
            this.lblDomaine2 = new System.Windows.Forms.Label();
            this.txtDomaine1 = new System.Windows.Forms.TextBox();
            this.lblDomaine1 = new System.Windows.Forms.Label();
            this.txtAdresseIp = new System.Windows.Forms.TextBox();
            this.lblAdresseIp = new System.Windows.Forms.Label();
            this.BtnCréationUtilisateurAd = new System.Windows.Forms.Button();
            this.lblGroupe = new System.Windows.Forms.Label();
            this.txbGroupe = new System.Windows.Forms.TextBox();
            this.lblMdp = new System.Windows.Forms.Label();
            this.txbMdp = new System.Windows.Forms.TextBox();
            this.BtnConnexionAD = new System.Windows.Forms.Button();
            this.lblEtatConnexionAd = new System.Windows.Forms.Label();
            this.grpBxConnexionAd = new System.Windows.Forms.GroupBox();
            this.btnImportUtilisateurs = new System.Windows.Forms.Button();
            this.lblCheminFichierExcel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lblTitre = new System.Windows.Forms.Label();
            this.cboxOu = new System.Windows.Forms.ComboBox();
            this.lblOu = new System.Windows.Forms.Label();
            this.BtnSuppressionFiche = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnSuppressionCompte = new System.Windows.Forms.Button();
            this.lblNombreListeProfs = new System.Windows.Forms.Label();
            this.BtnMotDePasse = new System.Windows.Forms.Button();
            this.TxbRechercherCompte = new System.Windows.Forms.TextBox();
            this.grpboxChoixFiltre = new System.Windows.Forms.GroupBox();
            this.rdBtnOrdinateurs = new System.Windows.Forms.RadioButton();
            this.rdBtnGroupes = new System.Windows.Forms.RadioButton();
            this.rdBtnUtilisateurs = new System.Windows.Forms.RadioButton();
            this.grpbxChoixContexte = new System.Windows.Forms.GroupBox();
            this.rdBtnTravaillerSurAd = new System.Windows.Forms.RadioButton();
            this.rdBtnCréationFicheProf = new System.Windows.Forms.RadioButton();
            this.ChkBxSéléctionnerTout = new System.Windows.Forms.CheckBox();
            this.BtnImportPhotos = new System.Windows.Forms.Button();
            this.lblCheminPhotos = new System.Windows.Forms.Label();
            this.PhotoElève = new System.Windows.Forms.PictureBox();
            this.BtnLancerImportPhotos = new System.Windows.Forms.Button();
            this.lblClasseElève = new System.Windows.Forms.Label();
            this.lblCompteUtilisateur = new System.Windows.Forms.Label();
            this.grpBxConnexionAd.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpboxChoixFiltre.SuspendLayout();
            this.grpbxChoixContexte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoElève)).BeginInit();
            this.SuspendLayout();
            // 
            // ListeRésultats
            // 
            this.ListeRésultats.BackColor = System.Drawing.Color.LemonChiffon;
            this.ListeRésultats.FormattingEnabled = true;
            this.ListeRésultats.Location = new System.Drawing.Point(835, 266);
            this.ListeRésultats.Name = "ListeRésultats";
            this.ListeRésultats.Size = new System.Drawing.Size(279, 469);
            this.ListeRésultats.Sorted = true;
            this.ListeRésultats.TabIndex = 0;
            this.ListeRésultats.SelectedIndexChanged += new System.EventHandler(this.ListeRésultats_SelectedIndexChanged);
            // 
            // txbNom
            // 
            this.txbNom.BackColor = System.Drawing.Color.LightYellow;
            this.txbNom.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txbNom.Location = new System.Drawing.Point(132, 90);
            this.txbNom.Name = "txbNom";
            this.txbNom.Size = new System.Drawing.Size(158, 20);
            this.txbNom.TabIndex = 1;
            // 
            // txbPrénom
            // 
            this.txbPrénom.BackColor = System.Drawing.Color.LightYellow;
            this.txbPrénom.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txbPrénom.Location = new System.Drawing.Point(132, 129);
            this.txbPrénom.Name = "txbPrénom";
            this.txbPrénom.Size = new System.Drawing.Size(158, 20);
            this.txbPrénom.TabIndex = 2;
            // 
            // BtnCréerFiche
            // 
            this.BtnCréerFiche.Location = new System.Drawing.Point(189, 355);
            this.BtnCréerFiche.Name = "BtnCréerFiche";
            this.BtnCréerFiche.Size = new System.Drawing.Size(121, 23);
            this.BtnCréerFiche.TabIndex = 7;
            this.BtnCréerFiche.Text = "Créer la fiche";
            this.BtnCréerFiche.UseVisualStyleBackColor = true;
            this.BtnCréerFiche.Click += new System.EventHandler(this.BtnValider_Click);
            // 
            // txbEmail
            // 
            this.txbEmail.BackColor = System.Drawing.Color.LightYellow;
            this.txbEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txbEmail.Location = new System.Drawing.Point(132, 172);
            this.txbEmail.Name = "txbEmail";
            this.txbEmail.Size = new System.Drawing.Size(158, 20);
            this.txbEmail.TabIndex = 3;
            // 
            // txbCopieur
            // 
            this.txbCopieur.BackColor = System.Drawing.Color.LightYellow;
            this.txbCopieur.Location = new System.Drawing.Point(132, 213);
            this.txbCopieur.Name = "txbCopieur";
            this.txbCopieur.Size = new System.Drawing.Size(158, 20);
            this.txbCopieur.TabIndex = 4;
            // 
            // BtnEnvoyerMail
            // 
            this.BtnEnvoyerMail.Location = new System.Drawing.Point(1175, 225);
            this.BtnEnvoyerMail.Name = "BtnEnvoyerMail";
            this.BtnEnvoyerMail.Size = new System.Drawing.Size(141, 23);
            this.BtnEnvoyerMail.TabIndex = 6;
            this.BtnEnvoyerMail.Text = "Envoyer eMail";
            this.BtnEnvoyerMail.UseVisualStyleBackColor = true;
            this.BtnEnvoyerMail.Click += new System.EventHandler(this.BtnEnvoyer_Click);
            // 
            // lblNom
            // 
            this.lblNom.AutoSize = true;
            this.lblNom.Location = new System.Drawing.Point(90, 93);
            this.lblNom.Name = "lblNom";
            this.lblNom.Size = new System.Drawing.Size(29, 13);
            this.lblNom.TabIndex = 7;
            this.lblNom.Text = "Nom";
            // 
            // lblPrénom
            // 
            this.lblPrénom.AutoSize = true;
            this.lblPrénom.Location = new System.Drawing.Point(76, 132);
            this.lblPrénom.Name = "lblPrénom";
            this.lblPrénom.Size = new System.Drawing.Size(43, 13);
            this.lblPrénom.TabIndex = 8;
            this.lblPrénom.Text = "Prénom";
            // 
            // lblMail
            // 
            this.lblMail.AutoSize = true;
            this.lblMail.Location = new System.Drawing.Point(39, 175);
            this.lblMail.Name = "lblMail";
            this.lblMail.Size = new System.Drawing.Size(81, 13);
            this.lblMail.TabIndex = 9;
            this.lblMail.Text = "eMail personnel";
            // 
            // lblCodeCopieur
            // 
            this.lblCodeCopieur.AutoSize = true;
            this.lblCodeCopieur.Location = new System.Drawing.Point(49, 216);
            this.lblCodeCopieur.Name = "lblCodeCopieur";
            this.lblCodeCopieur.Size = new System.Drawing.Size(70, 13);
            this.lblCodeCopieur.TabIndex = 10;
            this.lblCodeCopieur.Text = "Code copieur";
            // 
            // txtMotDePasse
            // 
            this.txtMotDePasse.BackColor = System.Drawing.Color.LightYellow;
            this.txtMotDePasse.Location = new System.Drawing.Point(157, 229);
            this.txtMotDePasse.Name = "txtMotDePasse";
            this.txtMotDePasse.PasswordChar = '*';
            this.txtMotDePasse.Size = new System.Drawing.Size(153, 20);
            this.txtMotDePasse.TabIndex = 23;
            // 
            // lblMotDePasse
            // 
            this.lblMotDePasse.AutoSize = true;
            this.lblMotDePasse.Location = new System.Drawing.Point(14, 232);
            this.lblMotDePasse.Name = "lblMotDePasse";
            this.lblMotDePasse.Size = new System.Drawing.Size(77, 13);
            this.lblMotDePasse.TabIndex = 22;
            this.lblMotDePasse.Text = "Mot de passe :";
            // 
            // txtUtilisateur
            // 
            this.txtUtilisateur.BackColor = System.Drawing.Color.LightYellow;
            this.txtUtilisateur.Location = new System.Drawing.Point(157, 179);
            this.txtUtilisateur.Name = "txtUtilisateur";
            this.txtUtilisateur.Size = new System.Drawing.Size(153, 20);
            this.txtUtilisateur.TabIndex = 21;
            // 
            // lblNomUtilisateur
            // 
            this.lblNomUtilisateur.AutoSize = true;
            this.lblNomUtilisateur.Location = new System.Drawing.Point(14, 182);
            this.lblNomUtilisateur.Name = "lblNomUtilisateur";
            this.lblNomUtilisateur.Size = new System.Drawing.Size(90, 13);
            this.lblNomUtilisateur.TabIndex = 20;
            this.lblNomUtilisateur.Text = "Nom d\'utilisateur :";
            // 
            // txtDomaine2
            // 
            this.txtDomaine2.BackColor = System.Drawing.Color.LightYellow;
            this.txtDomaine2.Location = new System.Drawing.Point(157, 129);
            this.txtDomaine2.Name = "txtDomaine2";
            this.txtDomaine2.Size = new System.Drawing.Size(153, 20);
            this.txtDomaine2.TabIndex = 19;
            // 
            // lblDomaine2
            // 
            this.lblDomaine2.AutoSize = true;
            this.lblDomaine2.Location = new System.Drawing.Point(14, 132);
            this.lblDomaine2.Name = "lblDomaine2";
            this.lblDomaine2.Size = new System.Drawing.Size(64, 13);
            this.lblDomaine2.TabIndex = 18;
            this.lblDomaine2.Text = "Domaine 2 :";
            // 
            // txtDomaine1
            // 
            this.txtDomaine1.BackColor = System.Drawing.Color.LightYellow;
            this.txtDomaine1.Location = new System.Drawing.Point(157, 80);
            this.txtDomaine1.Name = "txtDomaine1";
            this.txtDomaine1.Size = new System.Drawing.Size(153, 20);
            this.txtDomaine1.TabIndex = 17;
            // 
            // lblDomaine1
            // 
            this.lblDomaine1.AutoSize = true;
            this.lblDomaine1.Location = new System.Drawing.Point(14, 83);
            this.lblDomaine1.Name = "lblDomaine1";
            this.lblDomaine1.Size = new System.Drawing.Size(64, 13);
            this.lblDomaine1.TabIndex = 16;
            this.lblDomaine1.Text = "Domaine 1 :";
            // 
            // txtAdresseIp
            // 
            this.txtAdresseIp.BackColor = System.Drawing.Color.LightYellow;
            this.txtAdresseIp.Location = new System.Drawing.Point(157, 30);
            this.txtAdresseIp.Name = "txtAdresseIp";
            this.txtAdresseIp.Size = new System.Drawing.Size(153, 20);
            this.txtAdresseIp.TabIndex = 15;
            // 
            // lblAdresseIp
            // 
            this.lblAdresseIp.AutoSize = true;
            this.lblAdresseIp.Location = new System.Drawing.Point(14, 33);
            this.lblAdresseIp.Name = "lblAdresseIp";
            this.lblAdresseIp.Size = new System.Drawing.Size(115, 13);
            this.lblAdresseIp.TabIndex = 14;
            this.lblAdresseIp.Text = "Adresse ip du serveur :";
            // 
            // BtnCréationUtilisateurAd
            // 
            this.BtnCréationUtilisateurAd.Location = new System.Drawing.Point(23, 355);
            this.BtnCréationUtilisateurAd.Name = "BtnCréationUtilisateurAd";
            this.BtnCréationUtilisateurAd.Size = new System.Drawing.Size(121, 23);
            this.BtnCréationUtilisateurAd.TabIndex = 8;
            this.BtnCréationUtilisateurAd.Text = "Créer compte AD";
            this.BtnCréationUtilisateurAd.UseVisualStyleBackColor = true;
            this.BtnCréationUtilisateurAd.Click += new System.EventHandler(this.BtnCréationUtilisateurAdClick);
            // 
            // lblGroupe
            // 
            this.lblGroupe.AutoSize = true;
            this.lblGroupe.Location = new System.Drawing.Point(76, 259);
            this.lblGroupe.Name = "lblGroupe";
            this.lblGroupe.Size = new System.Drawing.Size(42, 13);
            this.lblGroupe.TabIndex = 26;
            this.lblGroupe.Text = "Groupe";
            // 
            // txbGroupe
            // 
            this.txbGroupe.BackColor = System.Drawing.Color.LightYellow;
            this.txbGroupe.Location = new System.Drawing.Point(132, 256);
            this.txbGroupe.Name = "txbGroupe";
            this.txbGroupe.Size = new System.Drawing.Size(158, 20);
            this.txbGroupe.TabIndex = 5;
            // 
            // lblMdp
            // 
            this.lblMdp.AutoSize = true;
            this.lblMdp.Location = new System.Drawing.Point(49, 301);
            this.lblMdp.Name = "lblMdp";
            this.lblMdp.Size = new System.Drawing.Size(71, 13);
            this.lblMdp.TabIndex = 28;
            this.lblMdp.Text = "Mot de passe";
            // 
            // txbMdp
            // 
            this.txbMdp.BackColor = System.Drawing.Color.LightYellow;
            this.txbMdp.Location = new System.Drawing.Point(132, 298);
            this.txbMdp.Name = "txbMdp";
            this.txbMdp.Size = new System.Drawing.Size(158, 20);
            this.txbMdp.TabIndex = 6;
            // 
            // BtnConnexionAD
            // 
            this.BtnConnexionAD.Location = new System.Drawing.Point(157, 286);
            this.BtnConnexionAD.Name = "BtnConnexionAD";
            this.BtnConnexionAD.Size = new System.Drawing.Size(153, 23);
            this.BtnConnexionAD.TabIndex = 29;
            this.BtnConnexionAD.Text = "Tester la connexion";
            this.BtnConnexionAD.UseVisualStyleBackColor = true;
            this.BtnConnexionAD.Click += new System.EventHandler(this.BtnConnexionAD_Click);
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
            // grpBxConnexionAd
            // 
            this.grpBxConnexionAd.BackColor = System.Drawing.Color.LemonChiffon;
            this.grpBxConnexionAd.Controls.Add(this.BtnConnexionAD);
            this.grpBxConnexionAd.Controls.Add(this.lblAdresseIp);
            this.grpBxConnexionAd.Controls.Add(this.txtAdresseIp);
            this.grpBxConnexionAd.Controls.Add(this.lblEtatConnexionAd);
            this.grpBxConnexionAd.Controls.Add(this.lblDomaine1);
            this.grpBxConnexionAd.Controls.Add(this.txtDomaine1);
            this.grpBxConnexionAd.Controls.Add(this.lblDomaine2);
            this.grpBxConnexionAd.Controls.Add(this.txtDomaine2);
            this.grpBxConnexionAd.Controls.Add(this.lblNomUtilisateur);
            this.grpBxConnexionAd.Controls.Add(this.txtUtilisateur);
            this.grpBxConnexionAd.Controls.Add(this.lblMotDePasse);
            this.grpBxConnexionAd.Controls.Add(this.txtMotDePasse);
            this.grpBxConnexionAd.Location = new System.Drawing.Point(18, 174);
            this.grpBxConnexionAd.Name = "grpBxConnexionAd";
            this.grpBxConnexionAd.Size = new System.Drawing.Size(336, 537);
            this.grpBxConnexionAd.TabIndex = 33;
            this.grpBxConnexionAd.TabStop = false;
            this.grpBxConnexionAd.Text = "Connexion AD";
            // 
            // btnImportUtilisateurs
            // 
            this.btnImportUtilisateurs.Location = new System.Drawing.Point(23, 395);
            this.btnImportUtilisateurs.Name = "btnImportUtilisateurs";
            this.btnImportUtilisateurs.Size = new System.Drawing.Size(121, 23);
            this.btnImportUtilisateurs.TabIndex = 9;
            this.btnImportUtilisateurs.Text = "Fichier utilisateurs";
            this.btnImportUtilisateurs.UseVisualStyleBackColor = true;
            this.btnImportUtilisateurs.Click += new System.EventHandler(this.BtnImportUtilisateurs_Click);
            // 
            // lblCheminFichierExcel
            // 
            this.lblCheminFichierExcel.AutoSize = true;
            this.lblCheminFichierExcel.Location = new System.Drawing.Point(20, 440);
            this.lblCheminFichierExcel.Name = "lblCheminFichierExcel";
            this.lblCheminFichierExcel.Size = new System.Drawing.Size(0, 13);
            this.lblCheminFichierExcel.TabIndex = 35;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1341, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblTitre
            // 
            this.lblTitre.AutoSize = true;
            this.lblTitre.Font = new System.Drawing.Font("Script MT Bold", 45F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitre.ForeColor = System.Drawing.Color.DarkCyan;
            this.lblTitre.Location = new System.Drawing.Point(446, 9);
            this.lblTitre.Name = "lblTitre";
            this.lblTitre.Size = new System.Drawing.Size(573, 72);
            this.lblTitre.TabIndex = 38;
            this.lblTitre.Text = "Gestion des utilisateurs";
            // 
            // cboxOu
            // 
            this.cboxOu.BackColor = System.Drawing.Color.LightYellow;
            this.cboxOu.FormattingEnabled = true;
            this.cboxOu.Location = new System.Drawing.Point(132, 45);
            this.cboxOu.Name = "cboxOu";
            this.cboxOu.Size = new System.Drawing.Size(158, 21);
            this.cboxOu.TabIndex = 39;
            this.cboxOu.SelectedIndexChanged += new System.EventHandler(this.CboxOu_SelectedIndexChanged);
            // 
            // lblOu
            // 
            this.lblOu.AutoSize = true;
            this.lblOu.Location = new System.Drawing.Point(20, 48);
            this.lblOu.Name = "lblOu";
            this.lblOu.Size = new System.Drawing.Size(100, 13);
            this.lblOu.TabIndex = 40;
            this.lblOu.Text = "Unité d\'organisation";
            // 
            // BtnSuppressionFiche
            // 
            this.BtnSuppressionFiche.Location = new System.Drawing.Point(1175, 269);
            this.BtnSuppressionFiche.Name = "BtnSuppressionFiche";
            this.BtnSuppressionFiche.Size = new System.Drawing.Size(141, 23);
            this.BtnSuppressionFiche.TabIndex = 41;
            this.BtnSuppressionFiche.Text = "Supprimer fiche";
            this.BtnSuppressionFiche.UseVisualStyleBackColor = true;
            this.BtnSuppressionFiche.Click += new System.EventHandler(this.BtnSuppressionFiche_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LemonChiffon;
            this.groupBox2.Controls.Add(this.txbCopieur);
            this.groupBox2.Controls.Add(this.txbNom);
            this.groupBox2.Controls.Add(this.lblOu);
            this.groupBox2.Controls.Add(this.txbPrénom);
            this.groupBox2.Controls.Add(this.cboxOu);
            this.groupBox2.Controls.Add(this.BtnCréerFiche);
            this.groupBox2.Controls.Add(this.txbEmail);
            this.groupBox2.Controls.Add(this.lblNom);
            this.groupBox2.Controls.Add(this.lblCheminFichierExcel);
            this.groupBox2.Controls.Add(this.lblPrénom);
            this.groupBox2.Controls.Add(this.btnImportUtilisateurs);
            this.groupBox2.Controls.Add(this.lblMail);
            this.groupBox2.Controls.Add(this.lblCodeCopieur);
            this.groupBox2.Controls.Add(this.lblMdp);
            this.groupBox2.Controls.Add(this.BtnCréationUtilisateurAd);
            this.groupBox2.Controls.Add(this.txbMdp);
            this.groupBox2.Controls.Add(this.txbGroupe);
            this.groupBox2.Controls.Add(this.lblGroupe);
            this.groupBox2.Location = new System.Drawing.Point(429, 240);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 471);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Création compte ou fiche";
            // 
            // BtnSuppressionCompte
            // 
            this.BtnSuppressionCompte.Location = new System.Drawing.Point(1175, 323);
            this.BtnSuppressionCompte.Name = "BtnSuppressionCompte";
            this.BtnSuppressionCompte.Size = new System.Drawing.Size(141, 23);
            this.BtnSuppressionCompte.TabIndex = 43;
            this.BtnSuppressionCompte.Text = "Supprimer le compte";
            this.BtnSuppressionCompte.UseVisualStyleBackColor = true;
            this.BtnSuppressionCompte.Click += new System.EventHandler(this.BtnSuppressionCompteAd_Click);
            // 
            // lblNombreListeProfs
            // 
            this.lblNombreListeProfs.AutoSize = true;
            this.lblNombreListeProfs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreListeProfs.ForeColor = System.Drawing.Color.OrangeRed;
            this.lblNombreListeProfs.Location = new System.Drawing.Point(906, 748);
            this.lblNombreListeProfs.Name = "lblNombreListeProfs";
            this.lblNombreListeProfs.Size = new System.Drawing.Size(52, 17);
            this.lblNombreListeProfs.TabIndex = 44;
            this.lblNombreListeProfs.Text = "label6";
            // 
            // BtnMotDePasse
            // 
            this.BtnMotDePasse.Location = new System.Drawing.Point(1175, 367);
            this.BtnMotDePasse.Name = "BtnMotDePasse";
            this.BtnMotDePasse.Size = new System.Drawing.Size(141, 23);
            this.BtnMotDePasse.TabIndex = 45;
            this.BtnMotDePasse.Text = "Réinitialiser mot de passe";
            this.BtnMotDePasse.UseVisualStyleBackColor = true;
            this.BtnMotDePasse.Click += new System.EventHandler(this.BtnMotDePasse_Click);
            // 
            // TxbRechercherCompte
            // 
            this.TxbRechercherCompte.Location = new System.Drawing.Point(1175, 184);
            this.TxbRechercherCompte.Name = "TxbRechercherCompte";
            this.TxbRechercherCompte.Size = new System.Drawing.Size(141, 20);
            this.TxbRechercherCompte.TabIndex = 46;
            this.TxbRechercherCompte.TextChanged += new System.EventHandler(this.TxbRechercherCompte_TextChanged);
            // 
            // grpboxChoixFiltre
            // 
            this.grpboxChoixFiltre.BackColor = System.Drawing.Color.LemonChiffon;
            this.grpboxChoixFiltre.Controls.Add(this.rdBtnOrdinateurs);
            this.grpboxChoixFiltre.Controls.Add(this.rdBtnGroupes);
            this.grpboxChoixFiltre.Controls.Add(this.rdBtnUtilisateurs);
            this.grpboxChoixFiltre.Location = new System.Drawing.Point(835, 174);
            this.grpboxChoixFiltre.Name = "grpboxChoixFiltre";
            this.grpboxChoixFiltre.Size = new System.Drawing.Size(279, 60);
            this.grpboxChoixFiltre.TabIndex = 47;
            this.grpboxChoixFiltre.TabStop = false;
            this.grpboxChoixFiltre.Text = "Choix du filtre";
            // 
            // rdBtnOrdinateurs
            // 
            this.rdBtnOrdinateurs.AutoSize = true;
            this.rdBtnOrdinateurs.Location = new System.Drawing.Point(194, 29);
            this.rdBtnOrdinateurs.Name = "rdBtnOrdinateurs";
            this.rdBtnOrdinateurs.Size = new System.Drawing.Size(79, 17);
            this.rdBtnOrdinateurs.TabIndex = 2;
            this.rdBtnOrdinateurs.TabStop = true;
            this.rdBtnOrdinateurs.Text = "Ordinateurs";
            this.rdBtnOrdinateurs.UseVisualStyleBackColor = true;
            this.rdBtnOrdinateurs.CheckedChanged += new System.EventHandler(this.RdBtnFiltre_CheckedChanged);
            // 
            // rdBtnGroupes
            // 
            this.rdBtnGroupes.AutoSize = true;
            this.rdBtnGroupes.Location = new System.Drawing.Point(105, 29);
            this.rdBtnGroupes.Name = "rdBtnGroupes";
            this.rdBtnGroupes.Size = new System.Drawing.Size(65, 17);
            this.rdBtnGroupes.TabIndex = 1;
            this.rdBtnGroupes.TabStop = true;
            this.rdBtnGroupes.Text = "Groupes";
            this.rdBtnGroupes.UseVisualStyleBackColor = true;
            this.rdBtnGroupes.CheckedChanged += new System.EventHandler(this.RdBtnFiltre_CheckedChanged);
            // 
            // rdBtnUtilisateurs
            // 
            this.rdBtnUtilisateurs.AutoSize = true;
            this.rdBtnUtilisateurs.Location = new System.Drawing.Point(6, 29);
            this.rdBtnUtilisateurs.Name = "rdBtnUtilisateurs";
            this.rdBtnUtilisateurs.Size = new System.Drawing.Size(76, 17);
            this.rdBtnUtilisateurs.TabIndex = 0;
            this.rdBtnUtilisateurs.TabStop = true;
            this.rdBtnUtilisateurs.Text = "Utilisateurs";
            this.rdBtnUtilisateurs.UseVisualStyleBackColor = true;
            this.rdBtnUtilisateurs.CheckedChanged += new System.EventHandler(this.RdBtnFiltre_CheckedChanged);
            // 
            // grpbxChoixContexte
            // 
            this.grpbxChoixContexte.BackColor = System.Drawing.Color.LemonChiffon;
            this.grpbxChoixContexte.Controls.Add(this.rdBtnTravaillerSurAd);
            this.grpbxChoixContexte.Controls.Add(this.rdBtnCréationFicheProf);
            this.grpbxChoixContexte.Location = new System.Drawing.Point(429, 174);
            this.grpbxChoixContexte.Name = "grpbxChoixContexte";
            this.grpbxChoixContexte.Size = new System.Drawing.Size(339, 60);
            this.grpbxChoixContexte.TabIndex = 48;
            this.grpbxChoixContexte.TabStop = false;
            this.grpbxChoixContexte.Text = "Choix du contexte";
            // 
            // rdBtnTravaillerSurAd
            // 
            this.rdBtnTravaillerSurAd.AutoSize = true;
            this.rdBtnTravaillerSurAd.Location = new System.Drawing.Point(170, 31);
            this.rdBtnTravaillerSurAd.Name = "rdBtnTravaillerSurAd";
            this.rdBtnTravaillerSurAd.Size = new System.Drawing.Size(163, 17);
            this.rdBtnTravaillerSurAd.TabIndex = 1;
            this.rdBtnTravaillerSurAd.TabStop = true;
            this.rdBtnTravaillerSurAd.Text = "Travailler sur Active Directory";
            this.rdBtnTravaillerSurAd.UseVisualStyleBackColor = true;
            this.rdBtnTravaillerSurAd.CheckedChanged += new System.EventHandler(this.RdBtnContexte_CheckedChanged);
            // 
            // rdBtnCréationFicheProf
            // 
            this.rdBtnCréationFicheProf.AutoSize = true;
            this.rdBtnCréationFicheProf.Location = new System.Drawing.Point(6, 29);
            this.rdBtnCréationFicheProf.Name = "rdBtnCréationFicheProf";
            this.rdBtnCréationFicheProf.Size = new System.Drawing.Size(118, 17);
            this.rdBtnCréationFicheProf.TabIndex = 0;
            this.rdBtnCréationFicheProf.TabStop = true;
            this.rdBtnCréationFicheProf.Text = "Créer une fiche prof";
            this.rdBtnCréationFicheProf.UseVisualStyleBackColor = true;
            this.rdBtnCréationFicheProf.CheckedChanged += new System.EventHandler(this.RdBtnContexte_CheckedChanged);
            // 
            // ChkBxSéléctionnerTout
            // 
            this.ChkBxSéléctionnerTout.AutoSize = true;
            this.ChkBxSéléctionnerTout.Location = new System.Drawing.Point(837, 244);
            this.ChkBxSéléctionnerTout.Name = "ChkBxSéléctionnerTout";
            this.ChkBxSéléctionnerTout.Size = new System.Drawing.Size(106, 17);
            this.ChkBxSéléctionnerTout.TabIndex = 49;
            this.ChkBxSéléctionnerTout.Text = "Sélectionner tout";
            this.ChkBxSéléctionnerTout.UseVisualStyleBackColor = true;
            this.ChkBxSéléctionnerTout.CheckedChanged += new System.EventHandler(this.ChkBxSéléctionnerTout_CheckedChanged);
            // 
            // BtnImportPhotos
            // 
            this.BtnImportPhotos.Location = new System.Drawing.Point(1175, 411);
            this.BtnImportPhotos.Name = "BtnImportPhotos";
            this.BtnImportPhotos.Size = new System.Drawing.Size(141, 23);
            this.BtnImportPhotos.TabIndex = 50;
            this.BtnImportPhotos.Text = "Importer les photos";
            this.BtnImportPhotos.UseVisualStyleBackColor = true;
            this.BtnImportPhotos.Click += new System.EventHandler(this.BtnImportPhotos_Click);
            // 
            // lblCheminPhotos
            // 
            this.lblCheminPhotos.AutoSize = true;
            this.lblCheminPhotos.Location = new System.Drawing.Point(1175, 452);
            this.lblCheminPhotos.Name = "lblCheminPhotos";
            this.lblCheminPhotos.Size = new System.Drawing.Size(179, 13);
            this.lblCheminPhotos.TabIndex = 51;
            this.lblCheminPhotos.Text = "X:\\Année 2017-2018\\Photos élèves";
            // 
            // PhotoElève
            // 
            this.PhotoElève.Location = new System.Drawing.Point(1175, 553);
            this.PhotoElève.Name = "PhotoElève";
            this.PhotoElève.Size = new System.Drawing.Size(148, 182);
            this.PhotoElève.TabIndex = 52;
            this.PhotoElève.TabStop = false;
            // 
            // BtnLancerImportPhotos
            // 
            this.BtnLancerImportPhotos.Location = new System.Drawing.Point(1332, 411);
            this.BtnLancerImportPhotos.Name = "BtnLancerImportPhotos";
            this.BtnLancerImportPhotos.Size = new System.Drawing.Size(75, 23);
            this.BtnLancerImportPhotos.TabIndex = 53;
            this.BtnLancerImportPhotos.Text = "Lancer";
            this.BtnLancerImportPhotos.UseVisualStyleBackColor = true;
            this.BtnLancerImportPhotos.Click += new System.EventHandler(this.BtnLancerImportPhotos_Click);
            // 
            // lblClasseElève
            // 
            this.lblClasseElève.AutoSize = true;
            this.lblClasseElève.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClasseElève.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblClasseElève.Location = new System.Drawing.Point(1196, 746);
            this.lblClasseElève.Name = "lblClasseElève";
            this.lblClasseElève.Size = new System.Drawing.Size(52, 17);
            this.lblClasseElève.TabIndex = 54;
            this.lblClasseElève.Text = "label1";
            // 
            // lblCompteUtilisateur
            // 
            this.lblCompteUtilisateur.AutoSize = true;
            this.lblCompteUtilisateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompteUtilisateur.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblCompteUtilisateur.Location = new System.Drawing.Point(1172, 521);
            this.lblCompteUtilisateur.Name = "lblCompteUtilisateur";
            this.lblCompteUtilisateur.Size = new System.Drawing.Size(52, 17);
            this.lblCompteUtilisateur.TabIndex = 55;
            this.lblCompteUtilisateur.Text = "label1";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Cornsilk;
            this.ClientSize = new System.Drawing.Size(1472, 791);
            this.Controls.Add(this.lblCompteUtilisateur);
            this.Controls.Add(this.lblClasseElève);
            this.Controls.Add(this.BtnLancerImportPhotos);
            this.Controls.Add(this.PhotoElève);
            this.Controls.Add(this.lblCheminPhotos);
            this.Controls.Add(this.BtnImportPhotos);
            this.Controls.Add(this.ChkBxSéléctionnerTout);
            this.Controls.Add(this.grpbxChoixContexte);
            this.Controls.Add(this.grpboxChoixFiltre);
            this.Controls.Add(this.TxbRechercherCompte);
            this.Controls.Add(this.BtnMotDePasse);
            this.Controls.Add(this.lblNombreListeProfs);
            this.Controls.Add(this.BtnSuppressionCompte);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnSuppressionFiche);
            this.Controls.Add(this.lblTitre);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpBxConnexionAd);
            this.Controls.Add(this.BtnEnvoyerMail);
            this.Controls.Add(this.ListeRésultats);
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Principal";
            this.Load += new System.EventHandler(this.OuvertureLogiciel);
            this.grpBxConnexionAd.ResumeLayout(false);
            this.grpBxConnexionAd.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpboxChoixFiltre.ResumeLayout(false);
            this.grpboxChoixFiltre.PerformLayout();
            this.grpbxChoixContexte.ResumeLayout(false);
            this.grpbxChoixContexte.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PhotoElève)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox ListeRésultats;
        private System.Windows.Forms.TextBox txbNom;
        private System.Windows.Forms.TextBox txbPrénom;
        private System.Windows.Forms.Button BtnCréerFiche;
        private System.Windows.Forms.TextBox txbEmail;
        private System.Windows.Forms.TextBox txbCopieur;
        private System.Windows.Forms.Button BtnEnvoyerMail;
        private System.Windows.Forms.Label lblNom;
        private System.Windows.Forms.Label lblPrénom;
        private System.Windows.Forms.Label lblMail;
        private System.Windows.Forms.Label lblCodeCopieur;
        private System.Windows.Forms.TextBox txtMotDePasse;
        private System.Windows.Forms.Label lblMotDePasse;
        private System.Windows.Forms.TextBox txtUtilisateur;
        private System.Windows.Forms.Label lblNomUtilisateur;
        private System.Windows.Forms.TextBox txtDomaine2;
        private System.Windows.Forms.Label lblDomaine2;
        private System.Windows.Forms.TextBox txtDomaine1;
        private System.Windows.Forms.Label lblDomaine1;
        private System.Windows.Forms.TextBox txtAdresseIp;
        private System.Windows.Forms.Label lblAdresseIp;
        private System.Windows.Forms.Button BtnCréationUtilisateurAd;
        private System.Windows.Forms.Label lblGroupe;
        private System.Windows.Forms.TextBox txbGroupe;
        private System.Windows.Forms.Label lblMdp;
        private System.Windows.Forms.TextBox txbMdp;
        private System.Windows.Forms.Button BtnConnexionAD;
        private System.Windows.Forms.Label lblEtatConnexionAd;
        private System.Windows.Forms.GroupBox grpBxConnexionAd;
        private System.Windows.Forms.Button btnImportUtilisateurs;
        private System.Windows.Forms.Label lblCheminFichierExcel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblTitre;
        private System.Windows.Forms.ComboBox cboxOu;
        private System.Windows.Forms.Label lblOu;
        private System.Windows.Forms.Button BtnSuppressionFiche;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnSuppressionCompte;
        private System.Windows.Forms.Label lblNombreListeProfs;
        private System.Windows.Forms.Button BtnMotDePasse;
        private System.Windows.Forms.TextBox TxbRechercherCompte;
        private System.Windows.Forms.GroupBox grpboxChoixFiltre;
        private System.Windows.Forms.RadioButton rdBtnOrdinateurs;
        private System.Windows.Forms.RadioButton rdBtnGroupes;
        private System.Windows.Forms.RadioButton rdBtnUtilisateurs;
        private System.Windows.Forms.GroupBox grpbxChoixContexte;
        private System.Windows.Forms.RadioButton rdBtnTravaillerSurAd;
        private System.Windows.Forms.RadioButton rdBtnCréationFicheProf;
        private System.Windows.Forms.CheckBox ChkBxSéléctionnerTout;
        private System.Windows.Forms.Button BtnImportPhotos;
        private System.Windows.Forms.Label lblCheminPhotos;
        private System.Windows.Forms.PictureBox PhotoElève;
        private System.Windows.Forms.Button BtnLancerImportPhotos;
        private System.Windows.Forms.Label lblClasseElève;
        private System.Windows.Forms.Label lblCompteUtilisateur;
    }
}

