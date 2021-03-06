﻿using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using DataTable = System.Data.DataTable;
using MailMessage = System.Net.Mail.MailMessage;
using Path = System.IO.Path;
using TableStyles = OfficeOpenXml.Table.TableStyles;
using TextBox = System.Windows.Forms.TextBox;

namespace Fiche_nouveau_prof
{
    public partial class Principal : Form
    {
        public string OuCollege = "OU=College,DC=stj,DC=lan";
        public string DateDuJour = DateTime.Now.ToString("dd/MM");
        public static string NomPrénom;
        public static string Compte;
        public static string HomeDirectory;
        public static string HomeDrive;
        public static string ProfilePath;
        public static string Description;
        public static Image Photo;

        public Principal()
        {
            InitializeComponent();
        }

        private string DomainesDc()
        {
            string domaines;
            if (txtDomaine3.Text == "")
            {
                domaines = "DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text;
            }
            else
            {
                domaines = "DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text + ",DC=" + txtDomaine3.Text;
            }
            return domaines;
        }

        private string Authentification()
        {
            string authentification;
            if (txtDomaine3.Text == "")
            {
                authentification = txtDomaine1.Text + @"\" + txtUtilisateur.Text;
            }
            else
            {
                authentification = @"dirstj\" + txtUtilisateur.Text;
            }
            return authentification;
        }

        private void OuvertureLogiciel(object sender, EventArgs e)
        {
            CopieRessources("Fiche_nouveau_prof.Resources.NouveauProf.docx", "\\NouveauProf.docx");
            CopieRessources("Fiche_nouveau_prof.Resources.FichesEleves.docx", "\\FichesEleves.docx");
            CopieRessources("Fiche_nouveau_prof.Resources.DelProf2.exe", "\\DelProf2.exe");
            RemplirListeBox(ListeRésultats, @"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019", "*.*");
            txbNom.Focus();
            txbNom.Select();
            txtAdresseIp.Text = @"172.16.0.1";
            txtDomaine1.Text = @"stj";
            txtDomaine2.Text = @"lan";
            txtUtilisateur.Text = @"administrateur";
            txtMotDePasse.Text = @"Lothlu85";
            RdBtnServeurPeda.Checked = true;
            RemplirComboboxOu();
            cboxOu.SelectedIndex = 1;
            RemplirComboboxDescription();
            rdBtnUtilisateurs.Checked = true;
            rdBtnTravaillerSurAd.Checked = true;
            BtnSuppressionPhoto.Visible = false;
            BtnAjoutPhoto.Visible = false;
            btnInformations.Visible = false;
            btnDateNaissance.Visible = false;
            ChercherAnniversaire();
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            var microsoftWord = new Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.docx";
            var fichierWord = microsoftWord.Documents.Add(chemin);
            microsoftWord.Visible = false;
            var i = 0;
            var j = 0;

            foreach (Field champs in fichierWord.Fields)
                if (champs.Code.Text.Contains("Prénom") && i == 0)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text));
                    i = 1;
                }
                else if (champs.Code.Text.Contains("Prénom") && i == 1)
                {
                    champs.Select();
                    string prenom = EnleverAccents(txbPrénom.Text);
                    microsoftWord.Selection.TypeText(prenom.ToLower());
                }
                else if (champs.Code.Text.Contains("Nom") && j == 0)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbNom.Text));
                    j = 1;
                }
                else if (champs.Code.Text.Contains("Nom") && j == 1)
                {
                    champs.Select();
                    string nom = EnleverAccents(txbNom.Text);
                    microsoftWord.Selection.TypeText(nom.ToLower());
                }
                else if (champs.Code.Text.Contains("Email"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(txbEmail.Text);
                }
                else if (champs.Code.Text.Contains("Copieur"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(txbCopieur.Text);
                }
                else if (champs.Code.Text.Contains("id_ED"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(txbIdED.Text);
                }
                else if (champs.Code.Text.Contains("mdp_ED"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(txbMdpED.Text);
                }

            fichierWord.SaveAs(@"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019\Identifiants " +
                               CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text) + " " +
                               CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbNom.Text) + ".docx");
            fichierWord.ExportAsFixedFormat(
                @"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019\Identifiants " +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text) + " " +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbNom.Text) + ".pdf",
                WdExportFormat.wdExportFormatPDF);

            fichierWord.Close();
            microsoftWord.Quit();
            GC.Collect();
            RemplirListeBox(ListeRésultats, @"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019", "*.*");
            NettoyageTextbox();
        }

        private void BtnEnvoyer_Click(object sender, EventArgs e)
        {
            var mail = new MailMessage();
            var smtpServer = new SmtpClient("smtp.orange.fr");
            mail.From = new MailAddress("admin@clg-stjacques.fr");
            mail.To.Add("secretariat@collegesaintjacques.fr");
            //mail.To.Add("laurent.manceau@collegesaintjacques.fr");
            mail.Subject = "Identifiants profs";
            mail.Body = "Ci-joint les identifiants nécessaires";

            foreach (var item in ListeRésultats.CheckedItems)
                try
                {
                    var attachment =
                        new Attachment(@"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019\" + item);
                    mail.Attachments.Add(attachment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            smtpServer.Port = 25;
            smtpServer.Credentials = new NetworkCredential("laurent_manceau@orange.fr", "Lothlu85");
            smtpServer.EnableSsl = false;
            smtpServer.Send(mail);
            MessageBox.Show(@"Mail envoyé");
        }

        private void BtnConnexionAD_Click(object sender, EventArgs e)
        {
            var entry = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(), Authentification(),
                txtMotDePasse.Text);
            var ds = new DirectorySearcher(entry);
            ds.Filter = "(&(&(objectClass=user)(memberOf=CN=Administrateurs,CN=Builtin," + DomainesDc() +
                        "))(samAccountName=" + txtUtilisateur.Text + "))";
            var result = ds.FindOne();

            if (result != null)
                lblEtatConnexionAd.Text = @"Connexion réussie !";
            else
                lblEtatConnexionAd.Text = @"Echec de la connexion";
            entry.Close();
            RemplirComboboxOu();
            cboxOu.SelectedIndex = 1;
            TxbRechercherCompte_TextChanged(sender, e);
        }

        private void BtnCréationUtilisateurAdClick(object sender, EventArgs e)
        {
            if (lblCheminFichierExcel.Text != "" && cboxOu.SelectedItem.ToString() != "") ImportUtilisateurs();
            if (lblCheminFichierExcel.Text == "" && txbNom.Text != "" && txbGroupe.Text != "" &&
                txbPrénom.Text != "" && cboxOu.SelectedItem.ToString() != "")
                CréationUtilisateur(txbNom.Text, txbPrénom.Text, txbGroupe.Text, OuChoisie(), txbIdED.Text,
                    txbMdpED.Text);
            TxbRechercherCompte_TextChanged(sender, e);
        }

        private void BtnImportUtilisateurs_Click(object sender, EventArgs e)
        {
            var fdlg = new OpenFileDialog();
            fdlg.Title = @"C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = @"All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
                lblCheminFichierExcel.Text = fdlg.FileName;
        }

        private void BtnSuppressionFiche_Click(object sender, EventArgs e)
        {
            foreach (var item in ListeRésultats.CheckedItems)
                File.Delete(@"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019\" + (string)item);
            RemplirListeBox(ListeRésultats, @"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019", "*.*");
        }

        private void BtnSuppressionCompteAd_Click(object sender, EventArgs e)
        {
            SuppressionCompteAd();
            TxbRechercherCompte_TextChanged(sender, e);
        }

        private void BtnMotDePasse_Click(object sender, EventArgs e)
        {
            RazMotDePasse();
        }

        private void BtnImportPhotos_Click(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            var result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)

                lblCheminPhotos.Text = folderDlg.SelectedPath;
        }

        private void BtnLancerImportPhotos_Click(object sender, EventArgs e)
        {
            AjouterPhoto();
        }

        private void BtnAjoutPhoto_Click(object sender, EventArgs e)
        {
            AjouterPhotoIndividuelle();
        }

        private void BtnSuppressionPhoto_Click(object sender, EventArgs e)
        {
            var liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            liste.Columns.Add("NomComplet");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                var rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                var ouSearch = new DirectorySearcher(rootDse);
                if (rdBtnUtilisateurs.Checked)
                    ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                if (rdBtnGroupes.Checked)
                    ouSearch.Filter = "(&(objectCategory=Group)(Name=" + item + "))";
                var résultats = ouSearch.FindOne();
                var row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                row["NomComplet"] = item;
                liste.Rows.Add(row);
            }

            var dialogResult =
                MessageBox.Show(@"Etes vous certain de vouloir supprimer " + liste.Rows.Count + @" photo(s) ?",
                    @"Ajout de photoe(s)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    var ad = new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                    var toutsLesRésultats = ad.Children;
                    if (RdBtnServeurAdmin.Checked)
                    {
                        var compteAvecPhoto = toutsLesRésultats.Find("CN=" + rang["NomComplet"]);

                        compteAvecPhoto.Properties["thumbnailPhoto"].Clear();
                        compteAvecPhoto.CommitChanges();
                    }
                    if (RdBtnServeurPeda.Checked)
                    {
                        var compteAvecPhoto = toutsLesRésultats.Find("CN=" + rang["Compte"]);

                        compteAvecPhoto.Properties["thumbnailPhoto"].Clear();
                        compteAvecPhoto.CommitChanges();
                    }
                }
                AfficherPhotoElève();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void BtnSynthèseDesComptesAd(object sender, EventArgs e)
        {
            SynthèseComptesAd();
        }

        private void BtnInformations_Click(object sender, EventArgs e)
        {
            Informations infos = new Informations();
            infos.Show();
        }

        private void BtnSuppressionProfils(object sender, EventArgs e)
        {
            SuppressionProfils();
        }

        private void BtnChoisirDossierACopier(object sender, EventArgs e)
        {
            var folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            var result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)

                lblCheminPhotos.Text = folderDlg.SelectedPath;
        }

        private void BtnCopierDossier(object sender, EventArgs e)
        {
            CopieDossier();
        }

        private void TxbRechercherCompte_TextChanged(object sender, EventArgs e)
        {
            SupprimerInfosPhoto();

            switch (FiltreChoisi())
            {
                case "Utilisateurs":
                    RemplirListBoxAd("Person", "DisplayName");
                    break;

                case "Ordinateurs":
                    RemplirListBoxAd("Computer", "Name");
                    break;

                case "Groupes":
                    RemplirListBoxAd("Group", "Name");
                    break;
            }
        }

        private void CboxOu_SelectedIndexChanged(object sender, EventArgs e)
        {
            SupprimerInfosPhoto();

            switch (FiltreChoisi())
            {
                case "Utilisateurs":
                    RemplirComboboxDescription();
                    RemplirListBoxAd("Person", "DisplayName");
                    break;

                case "Ordinateurs":
                    RemplirListBoxAd("Computer", "Name");
                    break;

                case "Groupes":
                    RemplirListBoxAd("Group", "Name");
                    break;
            }
        }

        private void CboxDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            RdBtnFiltre_CheckedChanged(sender, e);
        }

        private void ChkBxSéléctionnerTout_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBxSéléctionnerTout.Checked)
                for (var i = 0; i < ListeRésultats.Items.Count; i++)
                    ListeRésultats.SetItemChecked(i, true);
            else
                for (var i = 0; i < ListeRésultats.Items.Count; i++)
                    ListeRésultats.SetItemChecked(i, false);
        }

        private void ListeRésultats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdBtnTravaillerSurAd.Checked)
                AfficherPhotoElève();
        }

        private void RdBtnContexte_CheckedChanged(object sender, EventArgs e)
        {
            SupprimerInfosPhoto();

            switch (ContexteChoisi())
            {
                case "Créer une fiche prof":
                    RemplirListeBox(ListeRésultats, @"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019",
                        "*.*");
                    cboxOu.Enabled = false;
                    txbEmail.Enabled = true;
                    txbCopieur.Enabled = true;
                    txbGroupe.Enabled = false;
                    txbMdp.Enabled = true;
                    BtnCréationUtilisateurAd.Enabled = false;
                    btnImportUtilisateurs.Enabled = false;
                    grpboxChoixFiltre.Enabled = false;
                    TxbRechercherCompte.Enabled = false;
                    BtnSuppressionCompte.Enabled = false;
                    BtnMotDePasse.Enabled = false;
                    BtnCréerFiche.Enabled = true;
                    BtnEnvoyerMail.Enabled = true;
                    BtnSuppressionFiche.Enabled = true;
                    grpBxConnexionAd.Enabled = false;
                    BtnImportPhotos.Enabled = false;
                    lblCheminPhotos.Enabled = false;
                    BtnLancerImportPhotos.Enabled = false;
                    BtnSynthèse.Enabled = false;
                    BtnAjoutPhoto.Visible = false;
                    btnInformations.Visible = false;
                    txbIdED.Enabled = true;
                    txbMdpED.Enabled = true;
                    break;

                case "Travailler sur Active Directory":
                    cboxOu.Enabled = true;
                    txbEmail.Enabled = false;
                    txbCopieur.Enabled = false;
                    txbGroupe.Enabled = true;
                    txbMdp.Enabled = false;
                    BtnCréationUtilisateurAd.Enabled = true;
                    btnImportUtilisateurs.Enabled = true;
                    grpboxChoixFiltre.Enabled = true;
                    TxbRechercherCompte.Enabled = true;
                    BtnSuppressionCompte.Enabled = true;
                    BtnMotDePasse.Enabled = true;
                    BtnCréerFiche.Enabled = false;
                    BtnEnvoyerMail.Enabled = false;
                    BtnSuppressionFiche.Enabled = false;
                    grpBxConnexionAd.Enabled = true;
                    BtnImportPhotos.Enabled = true;
                    lblCheminPhotos.Enabled = true;
                    BtnLancerImportPhotos.Enabled = true;
                    BtnSynthèse.Enabled = true;
                    txbIdED.Enabled = true;
                    txbMdpED.Enabled = true;
                    TxbRechercherCompte_TextChanged(sender, e);
                    break;
            }
        }

        private void RdBtnFiltre_CheckedChanged(object sender, EventArgs e)
        {
            SupprimerInfosPhoto();

            switch (FiltreChoisi())
            {
                case "Utilisateurs":
                    RemplirListBoxAd("Person", "DisplayName");
                    break;

                case "Ordinateurs":
                    RemplirListBoxAd("Computer", "Name");
                    break;

                case "Groupes":
                    RemplirListBoxAd("Group", "Name");
                    break;
            }
        }

        private void RdBtnChoixServeur(object sender, EventArgs e)
        {
            if (RdBtnServeurPeda.Checked)
            {
                txtAdresseIp.Text = @"172.16.0.1";
                txtDomaine1.Text = @"stj";
                txtDomaine2.Text = @"lan";
                txtDomaine3.Text = @"";
                txtUtilisateur.Text = @"administrateur";
                txtMotDePasse.Text = @"Lothlu85";
                BtnConnexionAD_Click(sender, e);
            }
            if (RdBtnServeurAdmin.Checked)
            {
                txtAdresseIp.Text = @"172.16.9.199";
                txtDomaine1.Text = @"dir";
                txtDomaine2.Text = @"collegesaintjacques";
                txtDomaine3.Text = @"fr";
                txtUtilisateur.Text = @"administrateur";
                txtMotDePasse.Text = @"Lothlu85$$";
                BtnConnexionAD_Click(sender, e);
            }
        }

        private void CréerFicheElève(object sender, MouseEventArgs e)
        {
            #region Création tableau des comptes AD

            var de =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                    DomainesDc(), Authentification(), txtMotDePasse.Text);
            SearchResultCollection results;

            var ds = new DirectorySearcher(de);
            ds.PropertiesToLoad.Add("sAMAccountName");
            ds.PropertiesToLoad.Add("DisplayName");
            ds.PropertiesToLoad.Add("Description");
            ds.PropertiesToLoad.Add("mail");
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("company");

            ds.Filter = "(&(objectCategory=person)(objectClass=user))";
            results = ds.FindAll();

            var dt = new DataTable();
            dt.Columns.Add("Nom complet", typeof(string));
            dt.Columns.Add("Nom d'utilisateur", typeof(string));
            dt.Columns.Add("Mot de passe", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Adresse eMail", typeof(string));
            dt.Columns.Add("id ED", typeof(string));
            dt.Columns.Add("mdp ED", typeof(string));

            foreach (SearchResult sr in results)
            {
                var dr = dt.NewRow();
                var entry = sr.GetDirectoryEntry();

                if (entry.Properties["DisplayName"].Value.ToString() == ListeRésultats.SelectedItem.ToString())
                {
                    if (entry.Properties["sAMAccountName"].Count > 0)
                    {
                        dr["Nom d'utilisateur"] = entry.Properties["sAMAccountName"].Value.ToString().ToLower();
                        dr["Mot de passe"] = "Toto1234";
                    }
                    if (entry.Properties["DisplayName"].Count > 0)
                        dr["Nom complet"] = entry.Properties["DisplayName"].Value.ToString();
                    if ((entry.Properties["Description"].Count > 0) &&
                        (entry.Properties["Description"].Value.ToString().Contains("Eleve")))
                        dr["Description"] = entry.Properties["Description"].Value.ToString()
                            .Substring(entry.Properties["Description"].Value.ToString().Length - 2, 2);
                    if ((entry.Properties["Description"].Count > 0) &&
                        (!entry.Properties["Description"].Value.ToString().Contains("Eleve")))
                        dr["Description"] = entry.Properties["Description"].Value.ToString();
                    if (entry.Properties["mail"].Count > 0)
                        dr["Adresse eMail"] = entry.Properties["mail"].Value.ToString().ToLower();
                    if (entry.Properties["department"].Count > 0)
                        dr["id ED"] = entry.Properties["department"].Value.ToString();
                    if (entry.Properties["company"].Count > 0)
                        dr["mdp ED"] = entry.Properties["company"].Value.ToString();
                    dt.Rows.Add(dr);
                }
            }

            #endregion Création tableau des comptes AD

            #region Création du fichier CSV

            var fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + ListeRésultats.SelectedItem + ".csv");
            CréerFichierCsv(dt, fullFilePath);

            #endregion Création du fichier CSV

            #region Conversion du fichier CSV en XLSX

            var csvFileName = @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + ListeRésultats.SelectedItem + ".csv";
            var excelFileName = @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + ListeRésultats.SelectedItem + ".xlsx";

            var worksheetsName = "Comptes " + ListeRésultats.SelectedItem;

            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.EOL = @"\r\n"; // DEFAULT IS "\r\n";
            // format.TextQualifier = '"';

            File.Delete(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " + ListeRésultats.SelectedItem + ".xlsx");

            using (var package = new ExcelPackage(new FileInfo(excelFileName)))
            {
                var worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFileName), format, TableStyles.Medium27, true);
                package.Save();
                package.Dispose();
                GC.Collect();
            }

            #endregion Conversion du fichier CSV en XLSX

            #region Mise en forme du fichier XLSX

            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Open(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " +
                                                  ListeRésultats.SelectedItem + ".xlsx");
            Worksheet worksheet1 = xlWorkBook.Worksheets[1];
            worksheet1.Columns.AutoFit();
            var sortBy = worksheet1.Range["D2", "D1000"];
            var sortBy1 = worksheet1.Range["A2", "A1000"];
            var sortRange = worksheet1.Range["A2", "G1000"];
            worksheet1.Sort.SortFields.Clear();
            worksheet1.Sort.SetRange(sortRange);
            worksheet1.Sort.SortFields.Add(sortBy, 0, SortOrder.Ascending);
            worksheet1.Sort.SortFields.Add(sortBy1, 0, SortOrder.Ascending);
            worksheet1.Sort.Header = XlYesNoGuess.xlYes;
            worksheet1.Sort.MatchCase = false;
            worksheet1.Sort.Orientation = XlSortOrientation.xlSortColumns;
            worksheet1.Sort.SortMethod = XlSortMethod.xlPinYin;
            worksheet1.Sort.Apply();
            worksheet1.PageSetup.Orientation = XlPageOrientation.xlLandscape;
            xlWorkBook.Save();
            xlWorkBook.Close();
            xlApp.Quit();
            GC.Collect();

            #endregion Mise en forme du fichier XLSX

            #region Publipostage du fichier XLSX

            var wordApp = new Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FichesEleves.docx";
            var oDoc = wordApp.Documents.Add(chemin);
            wordApp.Visible = false;
            object qry = "select *from [Comptes " + ListeRésultats.SelectedItem + "$]";
            object nullobject = Missing.Value;
            oDoc.MailMerge.MainDocumentType = WdMailMergeMainDocType.wdFormLetters;
            oDoc.MailMerge.OpenDataSource(
                @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + ListeRésultats.SelectedItem + ".xlsx", ref nullobject,
                ref nullobject, ref nullobject,
                ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                ref nullobject, ref nullobject, ref qry, ref nullobject, ref nullobject, ref nullobject);
            oDoc.MailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument;
            oDoc.MailMerge.Execute(false);

            var oLetters = wordApp.ActiveDocument;
            oLetters.SaveAs2(@"\\Serveur2008\Laurent$\Comptes AD\Fiches " + ListeRésultats.SelectedItem + ".docx",
                WdSaveFormat.wdFormatDocumentDefault);
            oLetters.ExportAsFixedFormat(
                @"\\Serveur2008\Laurent$\Comptes AD\Fiches " + ListeRésultats.SelectedItem + ".pdf",
                WdExportFormat.wdExportFormatPDF);
            oLetters.Close(WdSaveOptions.wdDoNotSaveChanges);
            oDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
            wordApp.Quit();
            GC.Collect();

            #endregion Publipostage du fichier XLSX
        }

        private void OuvrirFichierListeRésultats(object sender, MouseEventArgs e)
        {
            if (rdBtnCréationFicheProf.Checked)
            {
                string file = ListeRésultats.SelectedItem.ToString();
                string fullFileName = Path.Combine(@"\\Serveur2008\Laurent$\Année 2018-2019\Nouveaux profs 2018-2019",
                    file);
                Process.Start(fullFileName);
            }
        }

        private void CopieRessources(string fichierSource, string fichierDestination)
        {
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + fichierDestination;
            if (File.Exists(chemin)) File.Delete(chemin);
            var assembly = Assembly.GetExecutingAssembly();
            var source = assembly.GetManifestResourceStream(fichierSource);
            var destination = File.Open(chemin, FileMode.CreateNew);
            CopieFichiersTypeWord(source, destination);
            source?.Dispose();
            destination.Dispose();
        }

        private void CopieFichiersTypeWord(Stream input, Stream output)
        {
            var buffer = new byte[32768];
            while (true)
            {
                var read = input.Read(buffer, 0, buffer.Length);
                if (read <= 0)
                    return;
                output.Write(buffer, 0, read);
            }
        }

        private void NettoyageTextbox()
        {
            foreach (Control textbox in Controls)
            {
                if (textbox is TextBox) textbox.Text = "";
                txbNom.Select();
            }
        }

        //public static string EnleverAccents(string text)
        //{
        //    if (string.IsNullOrWhiteSpace(text))
        //        return text;

        //    text = text.Normalize(NormalizationForm.FormD);
        //    var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
        //    return new string(chars).Normalize(NormalizationForm.FormC);
        //}

        private static string EnleverAccents(string txt)
        {
            txt = txt.Replace(" ", "");
            txt = txt.ToLower();
            txt = txt.First().ToString().ToUpper() + txt.Substring(1);
            var
                bytes = Encoding.GetEncoding("Cyrillic")
                    .GetBytes(txt); //Tailspin uses Cyrillic (ISO-8859-5); others use Hebraw (ISO-8859-8)
            return Encoding.ASCII.GetString(bytes);
        }

        private string OuChoisie()
        {
            var ou1 = cboxOu.SelectedItem.ToString();
            return ou1;
        }

        private string ContexteChoisi()
        {
            string filtre = null;
            foreach (RadioButton filtrechoisi in grpbxChoixContexte.Controls)
                if (filtrechoisi.Checked)
                    filtre = filtrechoisi.Text;
            return filtre;
        }

        private string FiltreChoisi()
        {
            string filtre = null;
            foreach (RadioButton filtrechoisi in grpboxChoixFiltre.Controls)
                if (filtrechoisi.Checked)
                    filtre = filtrechoisi.Text;
            return filtre;
        }

        private void RemplirComboboxOu()
        {
            cboxOu.Items.Clear();
            var rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=college," + DomainesDc(),
                    Authentification(), txtMotDePasse.Text);

            var ouSearch = new DirectorySearcher(rootDse);
            ouSearch.Filter = "(objectCategory=OrganizationalUnit)";
            SearchResultCollection résultats;
            résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
                cboxOu.Items.Add(résultat.Properties["Name"][0].ToString());

            cboxOu.Refresh();
        }

        private void RemplirComboboxDescription()
        {
            cbxDescription.Items.Clear();
            var rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," + DomainesDc(),
                    Authentification(), txtMotDePasse.Text);

            var ouSearch = new DirectorySearcher(rootDse);
            ouSearch.Filter = "(objectCategory=user)";
            var résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
                if (résultat.Properties.Contains("Description"))
                {
                    if ((!cbxDescription.Items.Contains(résultat.Properties["Description"][0].ToString()) &&
                         (!résultat.Properties["Description"][0].ToString().Contains("1B"))))
                        cbxDescription.Items.Add(résultat.Properties["Description"][0].ToString());
                }

            cbxDescription.Items.Add("-Tout le monde-");
            cbxDescription.Refresh();
            cbxDescription.SelectedIndex = 0;
        }

        private void RemplirListeBox(CheckedListBox lsb, string folder, string fileType)
        {
            lsb.Items.Clear();
            var dinfo = new DirectoryInfo(folder);
            var files = dinfo.GetFiles(fileType, SearchOption.AllDirectories);
            foreach (var file in files)
                lsb.Items.Add(file.Name);
            lblNombreListeProfs.Text = lsb.Items.Count + @" enregistrements";
        }

        private void RemplirListBoxAd(string catégorie, string propriété)
        {
            ListeRésultats.Items.Clear();
            var rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                    DomainesDc(), Authentification(), txtMotDePasse.Text);

            var ouSearch = new DirectorySearcher(rootDse);
            if (!cbxDescription.SelectedItem.ToString().Contains("Tout le monde"))
            {
                if (TxbRechercherCompte.Text != "")
                    ouSearch.Filter = "(&(objectCategory=" + catégorie + ") (name=*" + TxbRechercherCompte.Text +
                                      "*)(description=" +
                                      cbxDescription.SelectedItem + "))";
                else
                    ouSearch.Filter = "(&(objectCategory=" + catégorie + ")(description=" +
                                      cbxDescription.SelectedItem + "))";
            }
            if (cbxDescription.SelectedItem.ToString().Contains("Tout le monde"))
            {
                if (TxbRechercherCompte.Text != "")
                    ouSearch.Filter = "(&(objectCategory=" + catégorie + ") (name=*" + TxbRechercherCompte.Text + "*))";
                else
                    ouSearch.Filter = "(objectCategory=" + catégorie + ")";
            }

            SearchResultCollection résultats;
            résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
                if (!résultat.Properties[propriété][0].ToString().Contains("Eleve"))
                    ListeRésultats.Items.Add(résultat.Properties[propriété][0].ToString());

            lblNombreListeProfs.Text = ListeRésultats.Items.Count + @" enregistrements";
        }

        private bool UtilisateurExiste(string utilisateur)
        {
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, DomainesDc(),
                Authentification(), txtMotDePasse.Text))
            {
                using (var foundUser =
                    UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, utilisateur))
                {
                    if (foundUser != null)
                        return true;
                    return false;
                }
            }
        }

        private bool GroupeExiste(string groupe)
        {
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, DomainesDc(),
                Authentification(), txtMotDePasse.Text))
            {
                using (var foundUser = GroupPrincipal.FindByIdentity(domainContext, IdentityType.Name, groupe))
                {
                    if (foundUser != null)
                        return true;
                    return false;
                }
            }
        }

        private void ImportUtilisateurs()
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(lblCheminFichierExcel.Text);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var rowCount = xlRange.Rows.Count;

            for (var i = 2; i <= rowCount; i++)
            {
                CréationDossierUtilisateur(xlRange.Cells[i, 2].Value2.ToString());
                CréationUtilisateur(xlRange.Cells[i, 2].Value2.ToString(), xlRange.Cells[i, 3].Value2.ToString(),
                    xlRange.Cells[i, 1].Value2.ToString(), OuChoisie(), xlRange.Cells[i, 5].Value2.ToString(),
                    xlRange.Cells[i, 6].Value2.ToString());
            }

            xlWorkbook.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void CréationUtilisateur(string nom, string prénom, string groupe, string ou, string idEd, string mdpEd)
        {
            nom = EnleverAccents(nom);
            prénom = EnleverAccents(prénom);

            var i = 1;
            var nouveauNom = nom;
            while (UtilisateurExiste(nouveauNom))
            {
                nouveauNom = nom + i;
                i++;
            }

            var adPath1 = "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, " + DomainesDc();
            var entry = new DirectoryEntry(adPath1, Authentification(), txtMotDePasse.Text);
            var users = entry.Children;
            var newuser = users.Add("CN=" + nouveauNom, "user");
            newuser.Properties["samAccountName"].Value = nouveauNom;
            newuser.Properties["uid"].Value = nouveauNom;
            newuser.Properties["userPrincipalName"].Value = nouveauNom + "@stj.lan";
            newuser.Properties["givenName"].Value = prénom;
            newuser.Properties["sn"].Value = nom;
            newuser.Properties["displayname"].Add(nom + " " + prénom);
            newuser.Properties["mail"].Add(prénom + "." + nom + "@clg-stjacques.fr");
            if (ou == "Eleves")
            {
                newuser.Properties["description"].Add("Eleve de " + groupe);
                newuser.Properties["department"].Add(idEd);
                newuser.Properties["company"].Add(mdpEd);
            }
            if (ou == "Profs")
                newuser.Properties["description"].Add(groupe);
            newuser.Properties["profilePath"].Add(@"\\Serveur2008\profil\" + ou + @"\" + nouveauNom);
            newuser.Properties["homedirectory"].Add(@"\\Serveur2008\" + ou + @"\" + nouveauNom);
            newuser.Properties["homedrive"].Add("H:");
            newuser.CommitChanges();

            newuser.Invoke("SetPassword", "Toto1234");
            newuser.Properties["pwdLastSet"].Value = 0;
            newuser.Properties["userAccountControl"].Value = 0x0200;
            newuser.CommitChanges();

            if (ou == "Eleves")
            {
                if (GroupeExiste("Classe" + groupe) == false)
                    CréationGroupe(ou, "Classe" + groupe);
                AjouterUtilisateurAuGroupe(nouveauNom, "Classe" + groupe, ou);

                if (GroupeExiste("distri-" + groupe) == false)
                    CréationGroupeDistribution(ou, groupe);
                AjouterUtilisateurAuGroupe(nouveauNom, "distri-" + groupe, ou);
            }

            if (GroupeExiste(ou) == false)
                CréationGroupe(ou, ou);
            AjouterUtilisateurAuGroupe(nouveauNom, ou, ou);

            CréationDossierUtilisateur(nouveauNom);
            SupprimerToutesPermissions(@"\\Serveur2008\" + ou + @"\" + nouveauNom);
            DéfinirPermissions(nouveauNom, @"\\serveur2008\" + ou + @"\" + nouveauNom, FileSystemRights.FullControl,
                AccessControlType.Allow);
            DéfinirPermissions("profs", @"\\serveur2008\" + ou + @"\" + nouveauNom, FileSystemRights.FullControl,
                AccessControlType.Allow);
            DéfinirPermissions("Administrateurs", @"\\serveur2008\" + ou + @"\" + nouveauNom,
                FileSystemRights.FullControl, AccessControlType.Allow);
            PartagerDossier(@"E:\Users\" + ou + @"\" + nouveauNom, nouveauNom + @"$",
                @"Dossier personnel de " + nouveauNom, nouveauNom);
            if (ou == "Eleves")
            {
                CréationDossierClasse(groupe);
                SupprimerToutesPermissions(@"\\Serveur2008\Classe\" + groupe);
                DéfinirPermissions("Classe" + groupe, @"\\serveur2008\Classe\" + groupe, FileSystemRights.FullControl,
                    AccessControlType.Allow);
                DéfinirPermissions("profs", @"\\serveur2008\Classe\" + groupe, FileSystemRights.FullControl,
                    AccessControlType.Allow);
                DéfinirPermissions("Administrateurs", @"\\serveur2008\Classe\" + groupe, FileSystemRights.FullControl,
                    AccessControlType.Allow);
                PartagerDossier(@"E:\Classe\" + groupe, groupe + @"$", @"Dossier personnel de " + groupe,
                    "Classe" + groupe);
            }
        }

        private void CréationDossierUtilisateur(string nom)
        {
            Directory.CreateDirectory(@"\\Serveur2008\" + OuChoisie() + @"\" + nom);
        }

        private void CréationDossierClasse(string nom)
        {
            Directory.CreateDirectory(@"\\Serveur2008\Classe\" + nom);
        }

        private void CréationGroupe(string ou, string groupe)
        {
            try
            {
                var entry =
                    new DirectoryEntry(
                        "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, " + DomainesDc(),
                        Authentification(), txtMotDePasse.Text);
                var group = entry.Children.Add("CN=" + groupe, "group");
                group.Properties["sAmAccountName"].Value = groupe;
                if (groupe == "Eleves")
                    group.Properties["groupType"].Value = -2147483644; // Groupe local de sécurité
                group.CommitChanges();
                entry.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void CréationGroupeDistribution(string ou, string groupe)
        {
            var entry =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, " + DomainesDc(), Authentification(),
                    txtMotDePasse.Text);
            var group = entry.Children.Add("CN=distri-" + groupe, "group");
            group.Properties["sAmAccountName"].Value = "distri-" + groupe;
            group.Properties["groupType"].Value = 0x2;
            group.Properties["mail"].Value = "Classe" + groupe + "@clg-stjacques.fr";
            group.CommitChanges();
            entry.Close();
        }

        private void AjouterUtilisateurAuGroupe(string userId, string groupName, string ou)
        {
            var dirEntry =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/CN=" + groupName + ", OU=" + ou + ", OU=college, " +
                    DomainesDc(), Authentification(), txtMotDePasse.Text,
                    AuthenticationTypes.Secure);
            dirEntry.Properties["member"].Add("CN=" + userId + ",OU=" + ou + ",OU=college," + DomainesDc());
            dirEntry.CommitChanges();
            dirEntry.Close();
        }

        private void DéfinirPermissions(string utilisateur, string cheminDossier, FileSystemRights accès,
            AccessControlType accèsType)
        {
            var deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(),
                    Authentification(), txtMotDePasse.Text, AuthenticationTypes.Secure);
            var rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            var compteAd = rechercher.FindOne();
            var sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var fileSystemRule = new FileSystemAccessRule(sid, accès,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, accèsType);

            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private void PartagerDossier(string folderPath, string shareName, string description, string utilisateur)
        {
            var deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(),
                    Authentification(), txtMotDePasse.Text, AuthenticationTypes.Secure);
            var rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            var compteAd = rechercher.FindOne();
            var sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var utenteSidArray = new byte[sid.BinaryLength];
            sid.GetBinaryForm(utenteSidArray, 0);

            ManagementObject oGrpTrustee = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            oGrpTrustee["Name"] = utilisateur;
            oGrpTrustee["SID"] = utenteSidArray;

            ManagementObject oGrpAce = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            oGrpAce["AccessMask"] = 2032127; //Full access
            oGrpAce["AceFlags"] =
                AceFlags.ObjectInherit | AceFlags.ContainerInherit; //propagate the AccessMask to the subfolders
            oGrpAce["AceType"] = AceType.AccessAllowed;
            oGrpAce["Trustee"] = oGrpTrustee;

            ManagementObject oGrpSecurityDescriptor =
                new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
            oGrpSecurityDescriptor["ControlFlags"] = 4; //SE_DACL_PRESENT
            oGrpSecurityDescriptor["DACL"] = new object[] { oGrpAce };
            //for creating share on remote computer use:
            var options = new ConnectionOptions();
            options.Username = txtUtilisateur.Text;
            options.Password = txtMotDePasse.Text;
            var wmiScope = new ManagementScope(@"\\serveur2008\root\cimv2", options);
            wmiScope.Connect();
            var wmiShare = new ManagementClass(wmiScope, new ManagementPath("Win32_Share"), null);
            var mc = new ManagementClass("win32_share");
            var inParams = wmiShare.GetMethodParameters("Create");
            inParams["Description"] = description;
            inParams["Name"] = shareName;
            inParams["Path"] = folderPath;
            inParams["Type"] = 0x0; //Disk Drive
            inParams["MaximumAllowed"] = null;
            inParams["Password"] = null;
            inParams["Access"] = oGrpSecurityDescriptor;
            var outParams = wmiShare.InvokeMethod("Create", inParams, null);
        }

        private void SuppressionCompteAd()
        {
            var liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                var rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                var ouSearch = new DirectorySearcher(rootDse);
                if (rdBtnUtilisateurs.Checked)
                    ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                if (rdBtnGroupes.Checked)
                    ouSearch.Filter = "(&(objectCategory=Group)(Name=" + item + "))";
                var résultats = ouSearch.FindOne();
                var row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                liste.Rows.Add(row);
            }

            var dialogResult =
                MessageBox.Show(@"Etes vous certain de vouloir supprimer " + liste.Rows.Count + @" enregistrement(s) ?",
                    @"Suppression de compte(s)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    var ad = new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                    var toutsLesRésultats = ad.Children;
                    var compteASupprimer = toutsLesRésultats.Find("CN=" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\" + OuChoisie() + @"\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\" + OuChoisie() + @"\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\Desktop$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Desktop$\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\DesktopProfs$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\DesktopProfs$\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\Appdata$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Appdata$\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\AppdataProfs$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\AppdataProfs$\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"]);
                    ClearAttributes(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"] + @".V2");
                    SuppressionDossier(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"] + @".V2");
                    toutsLesRésultats.Remove(compteASupprimer);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void ClearAttributes(string chemin)
        {
            if (Directory.Exists(chemin))
            {
                DirectoryInfo diFolder = new DirectoryInfo(chemin);
                diFolder.Attributes = FileAttributes.Normal;
                string[] files = files = Directory.GetFiles(chemin);
                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                }
                string[] subDirs = Directory.GetDirectories(chemin);

                foreach (string dir in subDirs)
                {
                    ClearAttributes(dir);
                }
            }
        }

        private void SuppressionDossier(string chemin)
        {
            var dir = new DirectoryInfo(chemin);

            if (dir.Exists)
            {
                dir.Attributes = FileAttributes.Normal;
                foreach (var subDir in dir.GetDirectories())
                    subDir.Attributes = FileAttributes.Normal;
                foreach (var file in dir.GetFiles())
                    file.Attributes = FileAttributes.Normal;
                dir.Delete(true);
            }
        }

        private void SupprimerPermissions(string utilisateur, string cheminDossier)
        {
            var deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(),
                    Authentification(), txtMotDePasse.Text, AuthenticationTypes.Secure);
            var rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            var compteAd = rechercher.FindOne();
            var sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var objSecObj = directoryInfo.GetAccessControl();
            var acl = directorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions

            objSecObj.PurgeAccessRules(sid); //same as use objSecObj.RemoveAccessRuleSpecific(ace);

            directoryInfo.SetAccessControl(objSecObj);
        }

        private void SupprimerToutesPermissions(string cheminDossier)
        {
            var directoryInfo = new DirectoryInfo(cheminDossier);
            var objSecObj = directoryInfo.GetAccessControl();
            var acl = objSecObj.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions
            foreach (FileSystemAccessRule ace in acl) //to remove any other permission
                objSecObj.PurgeAccessRules(ace
                    .IdentityReference); //same as use objSecObj.RemoveAccessRuleSpecific(ace);

            directoryInfo.SetAccessControl(objSecObj);
        }

        private void ModifierPermissions(string utilisateur, string cheminDossier, FileSystemRights accès,
            AccessControlType accèsType)
        {
            var deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(),
                    Authentification(), txtMotDePasse.Text, AuthenticationTypes.Secure);
            var rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            var compteAd = rechercher.FindOne();
            var sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var fileSystemRule = new FileSystemAccessRule(sid, accès,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, accèsType);

            bool result;
            directorySecurity.ModifyAccessRule(AccessControlModification.Add, fileSystemRule, out result);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private void RazMotDePasse()
        {
            var liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                var rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                var ouSearch = new DirectorySearcher(rootDse);
                ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                var résultats = ouSearch.FindOne();
                var row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                liste.Rows.Add(row);
            }

            var dialogResult =
                MessageBox.Show(
                    @"Etes vous certain de vouloir réinitialiser le mot de passe pour " + liste.Rows.Count +
                    @" enregistrement(s) ?", @"Réinitialisation de mot de passe", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    var user =
                        new DirectoryEntry(rang["Chemin"].ToString(), Authentification(), txtMotDePasse.Text);
                    user.Invoke("SetPassword", "Toto1234");
                    user.Properties["pwdLastSet"].Value = 0;
                    user.Properties["userAccountControl"].Value = 0x0200;
                    user.CommitChanges();
                    user.Close();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void CopieDossier()
        {
            var liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                var rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                var ouSearch = new DirectorySearcher(rootDse);
                ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                var résultats = ouSearch.FindOne();
                var row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                liste.Rows.Add(row);
            }

            var dialogResult =
                MessageBox.Show(
                    @"Etes vous certain de vouloir copier ce dossier pour " + liste.Rows.Count +
                    @" enregistrement(s) ?", @"Copie d'un dossier", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    //Now Create all of the directories
                    string SourcePath = lblCheminPhotos.Text;
                    string DestinationPath = @"\\serveur2008\eleves\" + rang["Compte"].ToString() + @"\";
                    foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                        SearchOption.AllDirectories))
                        Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));

                    //Copy all the files & Replaces any files with the same name
                    foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                        SearchOption.AllDirectories))
                        File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        public static Image RedimensionnerPhoto(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        private void AjouterPhoto()
        {
            var liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            liste.Columns.Add("NomComplet");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                var rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                var ouSearch = new DirectorySearcher(rootDse);
                if (rdBtnUtilisateurs.Checked)
                    ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                if (rdBtnGroupes.Checked)
                    ouSearch.Filter = "(&(objectCategory=Group)(Name=" + item + "))";
                var résultats = ouSearch.FindOne();
                var row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                row["NomComplet"] = item;
                liste.Rows.Add(row);
            }

            var dialogResult =
                MessageBox.Show(@"Etes vous certain de vouloir ajouter " + liste.Rows.Count + @" photo(s) ?",
                    @"Ajout de photoe(s)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    var ad = new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                        DomainesDc(), Authentification(), txtMotDePasse.Text);
                    var tousLesRésultats = ad.Children;
                    var compteAvecPhoto = tousLesRésultats.Find("CN=" + rang["Compte"]);

                    Directory.CreateDirectory(lblCheminPhotos.Text + @"\" + "PicResize");
                    if (File.Exists(lblCheminPhotos.Text + @"\" + rang["NomComplet"] + @".jpg"))
                    {
                        using (var image = Image.FromFile(lblCheminPhotos.Text + @"\" + rang["NomComplet"] + @".jpg"))
                        using (var newImage = RedimensionnerPhoto(image, 157, 203))
                        {
                            newImage.Save(lblCheminPhotos.Text + @"\PicResize\" + rang["NomComplet"] + @".jpg",
                                ImageFormat.Jpeg);
                        }

                        var fs = new FileStream(lblCheminPhotos.Text + @"\PicResize\" + rang["NomComplet"] + @".jpg",
                            FileMode.Open);
                        var br = new BinaryReader(fs);
                        br.BaseStream.Seek(0, SeekOrigin.Begin);
                        byte[] ba;
                        ba = br.ReadBytes((int)br.BaseStream.Length);
                        fs.Close();
                        compteAvecPhoto.Properties["thumbnailPhoto"].Clear();
                        compteAvecPhoto.Properties["thumbnailPhoto"].Insert(0, ba);
                        compteAvecPhoto.CommitChanges();
                    }

                    string[] files = Directory.GetFiles(lblCheminPhotos.Text + @"\" + "PicResize");
                    foreach (string file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    Directory.Delete(lblCheminPhotos.Text + @"\" + "PicResize");
                }
                //AfficherPhotoElève();
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void AjouterPhotoIndividuelle()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"P:\ALCUIN\Photos\Eleves\SAINT JACQUES\";
            openFileDialog1.Title = @"Choisir une photo...";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                string cheminComplet = openFileDialog1.FileName;
                string fileName = openFileDialog1.SafeFileName;
                string chemin = openFileDialog1.FileName.Replace(fileName, "");

                var deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(), Authentification(),
                    txtMotDePasse.Text);
                var rechercher = new DirectorySearcher(deOu, "(DisplayName=" + ListeRésultats.SelectedItem + ")");
                var compteAd = rechercher.FindOne();

                if (compteAd != null)
                {
                    Directory.CreateDirectory(chemin + @"\" + "PicResize");
                    var directoryEntry = compteAd.GetDirectoryEntry();

                    var compte = directoryEntry.Properties["SamAccountName"][0].ToString();

                    using (var image = Image.FromFile(cheminComplet))
                    using (var newImage = RedimensionnerPhoto(image, 157, 203))
                    {
                        newImage.Save(chemin + @"\PicResize\" + fileName, ImageFormat.Jpeg);
                    }

                    var fs = new FileStream(chemin + @"\PicResize\" + fileName,
                        FileMode.Open);
                    var br = new BinaryReader(fs);
                    br.BaseStream.Seek(0, SeekOrigin.Begin);
                    byte[] ba;
                    ba = br.ReadBytes((int)br.BaseStream.Length);
                    fs.Close();
                    directoryEntry.Properties["thumbnailPhoto"].Clear();
                    directoryEntry.Properties["thumbnailPhoto"].Insert(0, ba);
                    directoryEntry.CommitChanges();

                    string[] files = Directory.GetFiles(chemin + @"\" + "PicResize");
                    foreach (string file in files)
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                    }
                    Directory.Delete(chemin + @"\" + "PicResize");
                }
            }
        }

        private void AfficherPhotoElève()
        {
            var deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/" + DomainesDc(), Authentification(),
                txtMotDePasse.Text);
            var rechercher = new DirectorySearcher(deOu, "(DisplayName=" + ListeRésultats.SelectedItem + ")");
            var compteAd = rechercher.FindOne();
            BtnAjoutPhoto.Visible = true;
            btnInformations.Visible = true;
            if (compteAd != null)
            {
                var directoryEntry = compteAd.GetDirectoryEntry();

                var compte = directoryEntry.Properties["SamAccountName"][0].ToString();

                SupprimerInfosPhoto();
                NomPrénom = "";
                HomeDirectory = "";

                if ((directoryEntry.Properties.Contains("thumbnailPhoto")) &&
                    ((byte[])directoryEntry.Properties["thumbnailPhoto"][0] != null))
                {
                    var pic = (byte[])directoryEntry.Properties["thumbnailPhoto"][0];
                    var ms = new MemoryStream(pic);
                    PhotoElève.Image = Image.FromStream(ms);
                    //PhotoElève.LoadAsync();
                    Compte = directoryEntry.Properties["SamAccountName"][0].ToString();
                    lblCompteUtilisateur.Text = @"Compte : " + compte;

                    if (directoryEntry.Properties.Contains("Description"))
                    {
                        lblClasseElève.Text = directoryEntry.Properties["Description"][0].ToString();
                        Description = directoryEntry.Properties["Description"][0].ToString();
                    }
                    if (directoryEntry.Properties.Contains("DisplayName"))
                        NomPrénom = directoryEntry.Properties["DisplayName"][0].ToString();
                    if (directoryEntry.Properties.Contains("homeDirectory"))
                        HomeDirectory = directoryEntry.Properties["homeDirectory"][0].ToString();
                    if (directoryEntry.Properties.Contains("homeDrive"))
                        HomeDrive = directoryEntry.Properties["homeDrive"][0].ToString();
                    if (directoryEntry.Properties.Contains("profilePath"))
                        ProfilePath = directoryEntry.Properties["profilePath"][0].ToString();
                    Photo = Image.FromStream(ms);
                    BtnSuppressionPhoto.Visible = true;
                }
                else BtnSuppressionPhoto.Visible = false;
                lblCompteUtilisateur.Text = @"Compte : " + compte;
            }
        }

        private void SupprimerInfosPhoto()
        {
            PhotoElève.Image = null;
            PhotoElève.Refresh();
            lblClasseElève.Text = "";
            lblCompteUtilisateur.Text = "";
            BtnSuppressionPhoto.Visible = false;
        }

        private void SynthèseComptesAd()
        {
            #region Création tableau des comptes AD

            var de =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," + DomainesDc(),
                    Authentification(), txtMotDePasse.Text);
            SearchResultCollection results;

            var ds = new DirectorySearcher(de);
            ds.PropertiesToLoad.Add("sAMAccountName");
            ds.PropertiesToLoad.Add("DisplayName");
            ds.PropertiesToLoad.Add("Description");
            ds.PropertiesToLoad.Add("mail");
            ds.PropertiesToLoad.Add("department");
            ds.PropertiesToLoad.Add("company");

            ds.Filter = "(&(objectCategory=person)(objectClass=user))";
            results = ds.FindAll();

            var dt = new DataTable();
            dt.Columns.Add("Nom complet", typeof(string));
            dt.Columns.Add("Nom d'utilisateur", typeof(string));
            dt.Columns.Add("Mot de passe", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Adresse eMail", typeof(string));
            dt.Columns.Add("id ED", typeof(string));
            dt.Columns.Add("mdp ED", typeof(string));

            foreach (SearchResult sr in results)
            {
                var dr = dt.NewRow();
                var entry = sr.GetDirectoryEntry();
                if (!(entry.Properties["sAMAccountName"].Value.ToString().Contains("Eleve")))
                {
                    if (entry.Properties["sAMAccountName"].Count > 0)
                    {
                        dr["Nom d'utilisateur"] = entry.Properties["sAMAccountName"].Value.ToString().ToLower();
                        dr["Mot de passe"] = "Toto1234";
                    }
                    if (entry.Properties["DisplayName"].Count > 0)
                        dr["Nom complet"] = entry.Properties["DisplayName"].Value.ToString();
                    if ((entry.Properties["Description"].Count > 0) &&
                        (entry.Properties["Description"].Value.ToString().Contains("Eleve")))
                        dr["Description"] = entry.Properties["Description"].Value.ToString()
                            .Substring(entry.Properties["Description"].Value.ToString().Length - 2, 2);
                    if ((entry.Properties["Description"].Count > 0) &&
                        (!entry.Properties["Description"].Value.ToString().Contains("Eleve")))
                        dr["Description"] = entry.Properties["Description"].Value.ToString();
                    if (entry.Properties["mail"].Count > 0)
                        dr["Adresse eMail"] = entry.Properties["mail"].Value.ToString().ToLower();
                    if (entry.Properties["department"].Count > 0)
                        dr["id ED"] = entry.Properties["department"].Value.ToString();
                    if (entry.Properties["company"].Count > 0)
                        dr["mdp ED"] = entry.Properties["company"].Value.ToString();
                    dt.Rows.Add(dr);
                }
            }

            #endregion Création tableau des comptes AD

            #region Création du fichier CSV

            var fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".csv");
            CréerFichierCsv(dt, fullFilePath);

            #endregion Création du fichier CSV

            #region Conversion du fichier CSV en XLSX

            var csvFileName = @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".csv";
            var excelFileName = @"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".xlsx";

            var worksheetsName = "Comptes " + cboxOu.SelectedItem;

            var format = new ExcelTextFormat();
            format.Delimiter = ',';
            format.EOL = @"\r\n"; // DEFAULT IS "\r\n";
            // format.TextQualifier = '"';

            File.Delete(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".xlsx");

            using (var package = new ExcelPackage(new FileInfo(excelFileName)))
            {
                var worksheet = package.Workbook.Worksheets.Add(worksheetsName);
                worksheet.Cells["A1"].LoadFromText(new FileInfo(csvFileName), format, TableStyles.Medium27, true);
                package.Save();
                package.Dispose();
                GC.Collect();
            }

            #endregion Conversion du fichier CSV en XLSX

            #region Mise en forme du fichier XLSX

            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook =
                xlApp.Workbooks.Open(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".xlsx");
            Worksheet worksheet1 = xlWorkBook.Worksheets[1];
            worksheet1.Columns.AutoFit();
            var sortBy = worksheet1.Range["D2", "D1000"];
            var sortBy1 = worksheet1.Range["A2", "A1000"];
            var sortRange = worksheet1.Range["A2", "G1000"];
            worksheet1.Sort.SortFields.Clear();
            worksheet1.Sort.SetRange(sortRange);
            worksheet1.Sort.SortFields.Add(sortBy, 0, SortOrder.Ascending);
            worksheet1.Sort.SortFields.Add(sortBy1, 0, SortOrder.Ascending);
            worksheet1.Sort.Header = XlYesNoGuess.xlYes;
            worksheet1.Sort.MatchCase = false;
            worksheet1.Sort.Orientation = XlSortOrientation.xlSortColumns;
            worksheet1.Sort.SortMethod = XlSortMethod.xlPinYin;
            worksheet1.Sort.Apply();
            worksheet1.PageSetup.Orientation = XlPageOrientation.xlLandscape;
            xlWorkBook.Save();
            xlWorkBook.Close();
            xlApp.Quit();
            GC.Collect();

            #endregion Mise en forme du fichier XLSX

            #region Publipostage du fichier XLSX

            var wordApp = new Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FichesEleves.docx";
            var oDoc = wordApp.Documents.Add(chemin);
            wordApp.Visible = false;
            object qry = "select *from [Comptes " + cboxOu.SelectedItem + "$]";
            object nullobject = Missing.Value;
            oDoc.MailMerge.MainDocumentType = WdMailMergeMainDocType.wdFormLetters;
            oDoc.MailMerge.OpenDataSource(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".xlsx",
                ref nullobject, ref nullobject, ref nullobject,
                ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject, ref nullobject,
                ref nullobject, ref nullobject, ref qry, ref nullobject, ref nullobject, ref nullobject);
            oDoc.MailMerge.Destination = WdMailMergeDestination.wdSendToNewDocument;
            oDoc.MailMerge.Execute(false);

            var oLetters = wordApp.ActiveDocument;
            oLetters.SaveAs2(@"\\Serveur2008\Laurent$\Comptes AD\Fiches " + cboxOu.SelectedItem + ".docx",
                WdSaveFormat.wdFormatDocumentDefault);
            oLetters.ExportAsFixedFormat(@"\\Serveur2008\Laurent$\Comptes AD\Fiches " + cboxOu.SelectedItem + ".pdf",
                WdExportFormat.wdExportFormatPDF);
            oLetters.Close(WdSaveOptions.wdDoNotSaveChanges);
            oDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
            wordApp.Quit();
            GC.Collect();

            #endregion Publipostage du fichier XLSX

            #region Ordonner fichier XLSX par classe

            var excelApp = new Microsoft.Office.Interop.Excel.Application();

            excelApp.Visible = true;

            var workbook =
                excelApp.Workbooks.Add(@"\\Serveur2008\Laurent$\Comptes AD\Comptes " + cboxOu.SelectedItem + ".xlsx");

            var ws = excelApp.Worksheets[1] as Worksheet;

            if (ws != null)
            {
                var usedRange = ws.UsedRange;
                var startRow = usedRange.Row;
                var endRow = startRow + usedRange.Rows.Count - 1;
                var ligne = 0;

                for (var row = 2; row <= endRow; row++)
                {
                    var count = excelApp.Worksheets.Count;
                    if (usedRange.Cells[row, 4].Value != usedRange.Cells[row + 1, 4].Value && ligne == 0)
                    {
                        count++;
                        workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);

                        workbook.Worksheets[count].Name = usedRange.Cells[row, 4].Value.ToString();
                        ligne = 2;
                    }
                    if (usedRange.Cells[row, 4].Value == usedRange.Cells[row + 1, 4].Value && ligne == 0)
                    {
                        count++;
                        workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);

                        workbook.Worksheets[count].Name = usedRange.Cells[row, 4].Value.ToString();
                        ligne = 2;
                    }
                    if (usedRange.Cells[row, 4].Value != usedRange.Cells[row + 1, 4].Value && ligne > 0)
                    {
                        workbook.Sheets.Add(After: workbook.Sheets[workbook.Sheets.Count]);
                        workbook.Worksheets[count].Name = usedRange.Cells[row, 4].Value.ToString();
                        workbook.Worksheets[count].Columns.AutoFit();
                        var from = workbook.Worksheets[1].Range("A" + row + ":G" + row);
                        var to = workbook.Worksheets[count].Range("A" + ligne + ":G" + ligne);
                        @from.copy(to);
                        ligne = 2;
                    }
                    if (usedRange.Cells[row, 4].Value == usedRange.Cells[row + 1, 4].Value && ligne > 0)
                    {
                        var from = workbook.Worksheets[1].Range("A" + row + ":G" + row);
                        var to = workbook.Worksheets[count].Range("A" + ligne + ":G" + ligne);
                        @from.copy(to);
                        ligne++;
                        workbook.Worksheets[count].Cells[1, 1].Value = "Nom (" + (ligne - 1) + " élèves)";
                        workbook.Worksheets[count].Cells[1, 2].Value = "Nom d'utilisateur";
                        workbook.Worksheets[count].Cells[1, 3].Value = "Mot de passe";
                        workbook.Worksheets[count].Cells[1, 4].Value = "Classe";
                        workbook.Worksheets[count].Cells[1, 5].Value = "Adresse e-Mail";
                        workbook.Worksheets[count].Cells[1, 6].Value = "id ED";
                        workbook.Worksheets[count].Cells[1, 7].Value = "mdp ED";
                        workbook.Worksheets[count].Rows(1).RowHeight = 30;
                        workbook.Worksheets[count].Rows(1).HorizontalAlignment =
                            Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        workbook.Worksheets[count].Rows(1).VerticalAlignment =
                            Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                        workbook.Worksheets[count].Cells[1, 1].EntireRow.Font.Bold = true;
                        Microsoft.Office.Interop.Excel.Range range1 = workbook.Worksheets[count].Range["A1", "G1"];
                        range1.Interior.Color = Color.LightGray;
                        workbook.Worksheets[count].PageSetup.LeftMargin = 1;
                        workbook.Worksheets[count].PageSetup.RightMargin = 1;
                    }
                }
            }
            File.Delete(@"\\Serveur2008\Laurent$\Comptes AD\Comptes AD " + cboxOu.SelectedItem + " par classe.xlsx");

            workbook.SaveAs(@"\\Serveur2008\Laurent$\Comptes AD\Comptes AD " + cboxOu.SelectedItem + " par classe.xlsx",
                XlFileFormat.xlWorkbookDefault);
            workbook.Close();
            excelApp.Quit();
            GC.Collect();

            #endregion Ordonner fichier XLSX par classe
        }

        private void CréerFichierCsv(DataTable dt, string strPath)
        {
            var sw = new StreamWriter(strPath, false);
            var columnCount = dt.Columns.Count;
            for (var i = 0; i < columnCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < columnCount - 1)
                    sw.Write(",");
            }
            sw.Write(sw.NewLine);

            foreach (DataRow dr in dt.Rows)
            {
                for (var i = 0; i < columnCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                        sw.Write(dr[i].ToString());
                    if (i < columnCount - 1)
                        sw.Write(",");
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }

        public static StringCollection CollecterGroupesUtilisateur(string strUser)
        {
            StringCollection groups = new StringCollection();
            try
            {
                DirectoryEntry obEntry = new DirectoryEntry(
                    "LDAP://172.16.0.1/DC=stj,DC=lan", @"stj\administrateur", "Lothlu85");
                DirectorySearcher srch = new DirectorySearcher(obEntry,
                    "(sAMAccountName=" + strUser + ")");
                SearchResult res = srch.FindOne();
                if (null != res)
                {
                    DirectoryEntry obUser = new DirectoryEntry(res.Path, @"stj\administrateur", "Lothlu85");
                    // Invoke Groups method.
                    object obGroups = obUser.Invoke("Groups");
                    foreach (object ob in (IEnumerable)obGroups)
                    {
                        // Create object for each group.
                        DirectoryEntry obGpEntry = new DirectoryEntry(ob);
                        groups.Add(obGpEntry.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.Message);
            }

            return groups;
        }

        private void SuppressionProfils()
        {
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DelProf2.exe";

            foreach (var item in ListeRésultats.CheckedItems)
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.CreateNoWindow = false;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = chemin;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    startInfo.Arguments = "/u /c" + @":" + item;

                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess?.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
        }

        private void btnDateNaissance_Click(object sender, EventArgs e)
        {
            MajUtilisateurs();
        }

        private void MajUtilisateurs()
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(lblCheminFichierExcel.Text);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var rowCount = xlRange.Rows.Count;

            for (var i = 2; i <= rowCount; i++)
            {
                double myOaDate = xlRange.Cells[i, 4].Value2;
                DateTime dateNaissance = DateTime.FromOADate(myOaDate);
                MajUtilisateur(xlRange.Cells[i, 5].Value2.ToString(), dateNaissance.ToString("dd/MM/yyyy"));
            }

            xlWorkbook.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void MajUtilisateur(string idEd, string naissance)
        {
            var rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                    DomainesDc(), Authentification(), txtMotDePasse.Text);
            var ouSearch = new DirectorySearcher(rootDse);

            ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(department=" + idEd + "))";

            var résultats = ouSearch.FindOne();
            if (résultats != null)
            {
                DirectoryEntry entryToUpdate = résultats.GetDirectoryEntry();
                entryToUpdate.Properties["title"].Value = naissance;
                entryToUpdate.CommitChanges();
            }
        }

        private void ChercherAnniversaire()
        {
            var rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college," +
                    DomainesDc(), Authentification(), txtMotDePasse.Text);

            var ouSearch = new DirectorySearcher(rootDse);

            ouSearch.Filter = "(&(objectCategory=person)(objectClass=user))";

            SearchResultCollection résultats;
            résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
            {
                if (résultat.Properties["title"][0].ToString().Contains(DateDuJour))
                    lblAnniversaire.Text = lblAnniversaire.Text + résultat.Properties["displayName"][0].ToString() + @" - ";
            }
        }
    }
}