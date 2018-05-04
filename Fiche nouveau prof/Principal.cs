using ActiveDs;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using System;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
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
using Range = Microsoft.Office.Interop.Excel.Range;
using TextBox = System.Windows.Forms.TextBox;

namespace Fiche_nouveau_prof
{
    public partial class Principal : Form
    {
        public string OuCollege = "OU=College,DC=stj,DC=lan";

        public Principal()
        {
            InitializeComponent();
        }

        private string ConnectionAd()
        {
            string connectionAd = "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text;
            return connectionAd;
        }

        private void OuvertureLogiciel(object sender, EventArgs e)
        {
            CopieRessources("Fiche_nouveau_prof.Resources.NouveauProf.docx", "\\NouveauProf.docx");
            CopieRessources("Fiche_nouveau_prof.Resources.FichesEleves.docx", "\\FichesEleves.docx");
            RemplirListeBox(ListeRésultats, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
            txbNom.Focus();
            txbNom.Select();
            txtAdresseIp.Text = @"172.16.0.1";
            txtDomaine1.Text = @"stj";
            txtDomaine2.Text = @"lan";
            txtUtilisateur.Text = @"administrateur";
            txtMotDePasse.Text = @"Lothlu85";
            RemplirComboboxOu();
            cboxOu.SelectedIndex = 1;
            rdBtnUtilisateurs.Checked = true;
            rdBtnCréationFicheProf.Checked = true;
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            var microsoftWord = new Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.docx";
            var fichierWord = microsoftWord.Documents.Add(chemin);
            microsoftWord.Visible = false;
            int i = 0;
            int j = 0;

            foreach (Field champs in fichierWord.Fields)
            {
                if (champs.Code.Text.Contains("Prénom") && i == 0)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text));
                    i = 1;
                }
                else if (champs.Code.Text.Contains("Prénom") && i == 1)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(EnleverAccents(txbPrénom.Text));
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
                    microsoftWord.Selection.TypeText(EnleverAccents(txbNom.Text));
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
            }

            fichierWord.SaveAs(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants " +
                               CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text) + " " +
                               CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbNom.Text) + ".docx");
            fichierWord.ExportAsFixedFormat(
                @"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants " +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbPrénom.Text) + " " +
                CultureInfo.InvariantCulture.TextInfo.ToTitleCase(txbNom.Text) + ".pdf",
                WdExportFormat.wdExportFormatPDF);

            fichierWord.Close();
            microsoftWord.Quit();
            GC.Collect();
            RemplirListeBox(ListeRésultats, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
            NettoyageTextbox();
        }

        private void BtnEnvoyer_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.orange.fr");
            mail.From = new MailAddress("admin@clg-stjacques.fr");
            mail.To.Add("secretariat@collegesaintjacques.fr");
            mail.Subject = "Identifiants profs";
            mail.Body = "Ci-joint les identifiants nécessaires";

            foreach (var item in ListeRésultats.CheckedItems)
            {
                try
                {
                    //Attachment attachment;
                    var attachment = new Attachment(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\" + item);
                    mail.Attachments.Add(attachment);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            smtpServer.Port = 25;
            smtpServer.Credentials = new NetworkCredential("laurent_manceau@orange.fr", "Lothlu85");
            smtpServer.EnableSsl = false;

            //smtpServer.Send(mail);
            MessageBox.Show(@"Mail envoyé");
        }

        private void BtnConnexionAD_Click(object sender, EventArgs e)
        {
            DirectoryEntry entry = new DirectoryEntry(ConnectionAd(), txtDomaine1.Text + "\\" + txtUtilisateur.Text,
                txtMotDePasse.Text);
            DirectorySearcher ds = new DirectorySearcher(entry);
            ds.Filter = "(&(&(objectClass=user)(memberOf=CN=Administrateurs,CN=Builtin,DC=" + txtDomaine1.Text +
                        ",DC=" + txtDomaine2.Text + "))(samAccountName=" + txtUtilisateur.Text + "))";
            SearchResult result = ds.FindOne();

            if (result != null)
            {
                lblEtatConnexionAd.Text = @"Connexion réussie !";
            }
            else
            {
                lblEtatConnexionAd.Text = @"Echec de la connexion";
            }
        }

        private void BtnCréationUtilisateurAdClick(object sender, EventArgs e)
        {
            if ((lblCheminFichierExcel.Text != "") && (cboxOu.SelectedItem.ToString() != "")) ImportUtilisateurs();
            if ((lblCheminFichierExcel.Text == "") && (txbNom.Text != "") && (txbGroupe.Text != "") &&
                (txbPrénom.Text != "") && (cboxOu.SelectedItem.ToString() != ""))
                CréationUtilisateur(txbNom.Text, txbPrénom.Text, txbGroupe.Text, OuChoisie());
            TxbRechercherCompte_TextChanged(sender, e);
        }

        private void BtnImportUtilisateurs_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = @"C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = @"All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                lblCheminFichierExcel.Text = fdlg.FileName;
            }
        }

        private void BtnSuppressionFiche_Click(object sender, EventArgs e)
        {
            foreach (var item in ListeRésultats.CheckedItems)
            {
                File.Delete(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\" + (string)item);
            }
            RemplirListeBox(ListeRésultats, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
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
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)

            {
                lblCheminPhotos.Text = folderDlg.SelectedPath;
            }
        }

        private void BtnLancerImportPhotos_Click(object sender, EventArgs e)
        {
            AjouterPhoto();
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

        private void ChkBxSéléctionnerTout_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBxSéléctionnerTout.Checked)
            {
                for (int i = 0; i < ListeRésultats.Items.Count; i++)
                {
                    ListeRésultats.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < ListeRésultats.Items.Count; i++)
                {
                    ListeRésultats.SetItemChecked(i, false);
                }
            }
        }

        private void ListeRésultats_SelectedIndexChanged(object sender, EventArgs e)
        {
            AfficherPhotoElève();
        }

        private void RdBtnContexte_CheckedChanged(object sender, EventArgs e)
        {
            SupprimerInfosPhoto();

            switch (ContexteChoisi())
            {
                case "Créer une fiche prof":
                    RemplirListeBox(ListeRésultats, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
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

        private void button1_Click(object sender, EventArgs e)
        {
            //SupprimerPermissions("Profs", @"\\serveur2008\Eleves\Boilou2");
            //SupprimerToutesPermissions(@"\\serveur2008\Eleves\Boilou2");
            //DéfinirPermissions("Administrateurs", @"\\serveur2008\Eleves\Boilou2", FileSystemRights.FullControl,AccessControlType.Allow);
            //ModifierPermissions("Administrateurs", @"\\serveur2008\Eleves\Boilou2", FileSystemRights.Read,AccessControlType.Allow);
            //PartagerDossier(@"E:\Commun_prof\Polices", @"Polices$", @"Laurent", "Administrateurs");
            //AjouterPhoto();
            //AfficherPhotoElève("Achale");
            //SynthèseComptesAd();
            //RemplirComboboxOu();
            //RemplirListBoxAd("Person", "DisplayName");
            //RemplirListBoxAd("Computer", "Name");
            //RemplirListBoxAd("Group", "Name");
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
            byte[]
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
            {
                if (filtrechoisi.Checked)
                {
                    filtre = filtrechoisi.Text;
                }
            }
            return filtre;
        }

        private string FiltreChoisi()
        {
            string filtre = null;
            foreach (RadioButton filtrechoisi in grpboxChoixFiltre.Controls)
            {
                if (filtrechoisi.Checked)
                {
                    filtre = filtrechoisi.Text;
                }
            }
            return filtre;
        }

        private void RemplirComboboxOu()
        {
            DirectoryEntry rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=college,DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85");

            DirectorySearcher ouSearch = new DirectorySearcher(rootDse);
            ouSearch.Filter = "(objectCategory=OrganizationalUnit)";
            SearchResultCollection résultats;
            résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
            {
                cboxOu.Items.Add(résultat.Properties["Name"][0].ToString());
            }

            cboxOu.Refresh();
        }

        private void RemplirListeBox(CheckedListBox lsb, string folder, string fileType)
        {
            lsb.Items.Clear();
            DirectoryInfo dinfo = new DirectoryInfo(folder);
            FileInfo[] files = dinfo.GetFiles(fileType);
            foreach (FileInfo file in files)
            {
                lsb.Items.Add(file.Name);
            }
            lblNombreListeProfs.Text = lsb.Items.Count + @" enregistrements";
        }

        private void RemplirListBoxAd(string catégorie, string propriété)
        {
            ListeRésultats.Items.Clear();
            DirectoryEntry rootDse =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                    txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");

            DirectorySearcher ouSearch = new DirectorySearcher(rootDse);
            if (TxbRechercherCompte.Text != "")
                ouSearch.Filter = "(&(objectCategory=" + catégorie + ") (name=*" + TxbRechercherCompte.Text + "*))";
            else ouSearch.Filter = "(objectCategory=" + catégorie + ")";

            SearchResultCollection résultats;
            résultats = ouSearch.FindAll();

            foreach (SearchResult résultat in résultats)
            {
                ListeRésultats.Items.Add(résultat.Properties[propriété][0].ToString());
            }

            //cboxOu.Refresh();
            lblNombreListeProfs.Text = ListeRésultats.Items.Count + @" enregistrements";
        }

        private bool UtilisateurExiste(string utilisateur)
        {
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, "DC=stj,DC=lan",
                @"stj\administrateur", "Lothlu85"))
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
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, "DC=stj,DC=lan",
                @"stj\administrateur", "Lothlu85"))
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
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(lblCheminFichierExcel.Text);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;

            for (int i = 2; i <= rowCount; i++)
            {
                CréationDossierUtilisateur(xlRange.Cells[i, 2].Value2.ToString());
                CréationUtilisateur(xlRange.Cells[i, 2].Value2.ToString(), xlRange.Cells[i, 3].Value2.ToString(),
                    xlRange.Cells[i, 1].Value2.ToString(), OuChoisie());
            }

            xlWorkbook.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void CréationUtilisateur(string nom, string prénom, string groupe, string ou)
        {
            nom = EnleverAccents(nom);
            prénom = EnleverAccents(prénom);

            int i = 1;
            string nouveauNom = nom;
            while (UtilisateurExiste(nouveauNom))
            {
                nouveauNom = nom + i;
                i++;
            }

            string adPath1 = "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text +
                             ",DC=" + txtDomaine2.Text;
            DirectoryEntry entry = new DirectoryEntry(adPath1, txtDomaine1.Text + "\\" + txtUtilisateur.Text,
                txtMotDePasse.Text);
            DirectoryEntries users = entry.Children;
            DirectoryEntry newuser = users.Add("CN=" + nouveauNom, "user");
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
            }
            if (ou == "Profs")
            {
                newuser.Properties["description"].Add(groupe);
            }
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
                DirectoryEntry entry =
                    new DirectoryEntry(
                        "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" +
                        txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                DirectoryEntry group = entry.Children.Add("CN=" + groupe, "group");
                group.Properties["sAmAccountName"].Value = groupe;
                if (groupe == "Eleves")
                {
                    group.Properties["groupType"].Value =
                        ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP |
                        ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_SECURITY_ENABLED;
                }
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
            DirectoryEntry entry =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" +
                    txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
            DirectoryEntry group = entry.Children.Add("CN=distri-" + groupe, "group");
            group.Properties["sAmAccountName"].Value = "distri-" + groupe;
            group.Properties["groupType"].Value = 0x2;
            group.Properties["mail"].Value = "Classe" + groupe + "@clg-stjacques.fr";
            group.CommitChanges();
            entry.Close();
        }

        private void AjouterUtilisateurAuGroupe(string userId, string groupName, string ou)
        {
            DirectoryEntry dirEntry =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/CN=" + groupName + ", OU=" + ou + ", OU=college, DC=" +
                    txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85",
                    AuthenticationTypes.Secure);
            dirEntry.Properties["member"].Add("CN=" + userId + ",OU=" + ou + ",OU=college,DC=stj,DC=lan");
            dirEntry.CommitChanges();
            dirEntry.Close();
        }

        private void DéfinirPermissions(string utilisateur, string cheminDossier, FileSystemRights accès,
            AccessControlType accèsType)
        {
            DirectoryEntry deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var fileSystemRule = new FileSystemAccessRule(sid, accès,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, accèsType);

            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private void PartagerDossier(string folderPath, string shareName, string description, string utilisateur)
        {
            DirectoryEntry deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            byte[] utenteSidArray = new byte[sid.BinaryLength];
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
            ConnectionOptions options = new ConnectionOptions();
            options.Username = "administrateur";
            options.Password = "Lothlu85";
            var wmiScope = new ManagementScope(@"\\serveur2008\root\cimv2", options);
            wmiScope.Connect();
            ManagementClass wmiShare = new ManagementClass(wmiScope, new ManagementPath("Win32_Share"), null);
            ManagementClass mc = new ManagementClass("win32_share");
            ManagementBaseObject inParams = wmiShare.GetMethodParameters("Create");
            inParams["Description"] = description;
            inParams["Name"] = shareName;
            inParams["Path"] = folderPath;
            inParams["Type"] = 0x0; //Disk Drive
            inParams["MaximumAllowed"] = null;
            inParams["Password"] = null;
            inParams["Access"] = oGrpSecurityDescriptor;
            ManagementBaseObject outParams = wmiShare.InvokeMethod("Create", inParams, null);
        }

        private void SuppressionCompteAd()
        {
            DataTable liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                DirectoryEntry rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                        txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                DirectorySearcher ouSearch = new DirectorySearcher(rootDse);
                if (rdBtnUtilisateurs.Checked)
                {
                    ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                }
                if (rdBtnGroupes.Checked)
                {
                    ouSearch.Filter = "(&(objectCategory=Group)(Name=" + item + "))";
                }
                SearchResult résultats = ouSearch.FindOne();
                DataRow row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                liste.Rows.Add(row);
            }

            DialogResult dialogResult =
                MessageBox.Show(@"Etes vous certain de vouloir supprimer " + liste.Rows.Count + @" enregistrement(s) ?",
                    @"Suppression de compte(s)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    DirectoryEntry ad = new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                        txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                    DirectoryEntries toutsLesRésultats = ad.Children;
                    DirectoryEntry compteASupprimer = toutsLesRésultats.Find("CN=" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\" + OuChoisie() + @"\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Desktop$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\DesktopProfs$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Appdata$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\AppdataProfs$\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"]);
                    SuppressionDossier(@"\\Serveur2008\Profil\" + OuChoisie() + @"\" + rang["Compte"] + @".V2");
                    toutsLesRésultats.Remove(compteASupprimer);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void SuppressionDossier(string chemin)
        {
            DirectoryInfo dir = new DirectoryInfo(chemin);

            if (dir.Exists)
            {
                dir.Attributes = FileAttributes.Normal;
                foreach (var subDir in dir.GetDirectories())
                    subDir.Attributes = FileAttributes.Normal;
                foreach (var file in dir.GetFiles())
                {
                    file.Attributes = FileAttributes.Normal;
                }
                dir.Delete(true);
            }
        }

        private void SupprimerPermissions(string utilisateur, string cheminDossier)
        {
            DirectoryEntry deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            DirectorySecurity objSecObj = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection acl = directorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions

            objSecObj.PurgeAccessRules(sid); //same as use objSecObj.RemoveAccessRuleSpecific(ace);

            directoryInfo.SetAccessControl(objSecObj);
        }

        private void SupprimerToutesPermissions(string cheminDossier)
        {
            var directoryInfo = new DirectoryInfo(cheminDossier);
            DirectorySecurity objSecObj = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection acl = objSecObj.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions
            foreach (FileSystemAccessRule ace in acl) //to remove any other permission
            {
                objSecObj.PurgeAccessRules(ace
                    .IdentityReference); //same as use objSecObj.RemoveAccessRuleSpecific(ace);
            }

            directoryInfo.SetAccessControl(objSecObj);
        }

        private void ModifierPermissions(string utilisateur, string cheminDossier, FileSystemRights accès,
            AccessControlType accèsType)
        {
            DirectoryEntry deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

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
            DataTable liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                DirectoryEntry rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                        txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                DirectorySearcher ouSearch = new DirectorySearcher(rootDse);
                ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                SearchResult résultats = ouSearch.FindOne();
                DataRow row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                liste.Rows.Add(row);
            }

            DialogResult dialogResult =
                MessageBox.Show(
                    @"Etes vous certain de vouloir réinitialiser le mot de passe pour " + liste.Rows.Count +
                    @" enregistrement(s) ?", @"Réinitialisation de mot de passe)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    DirectoryEntry user =
                        new DirectoryEntry(rang["Chemin"].ToString(), @"stj\administrateur", "Lothlu85");
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

        private void AjouterPhoto()
        {
            DataTable liste = new DataTable();
            liste.Columns.Add("Chemin");
            liste.Columns.Add("Compte");
            liste.Columns.Add("NomComplet");
            foreach (var item in ListeRésultats.CheckedItems)
            {
                DirectoryEntry rootDse =
                    new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                        txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                DirectorySearcher ouSearch = new DirectorySearcher(rootDse);
                if (rdBtnUtilisateurs.Checked)
                {
                    ouSearch.Filter = "(&(objectCategory=Person)(objectClass=user)(displayName=" + item + "))";
                }
                if (rdBtnGroupes.Checked)
                {
                    ouSearch.Filter = "(&(objectCategory=Group)(Name=" + item + "))";
                }
                SearchResult résultats = ouSearch.FindOne();
                DataRow row = liste.NewRow();
                if (résultats != null)
                {
                    row["Chemin"] = résultats.Path;
                    row["Compte"] = résultats.Properties["samAccountName"][0].ToString();
                }
                row["NomComplet"] = item;
                liste.Rows.Add(row);
            }

            DialogResult dialogResult =
               MessageBox.Show(@"Etes vous certain de vouloir ajouter " + liste.Rows.Count + @" photo(s) ?",
                   @"Ajout de photoe(s)", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                foreach (DataRow rang in liste.Rows)
                {
                    DirectoryEntry ad = new DirectoryEntry(
                        @"LDAP://" + txtAdresseIp.Text + "/OU=" + cboxOu.SelectedItem + ",OU=college,DC=" +
                        txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                    DirectoryEntries toutsLesRésultats = ad.Children;
                    DirectoryEntry compteAvecPhoto = toutsLesRésultats.Find("CN=" + rang["Compte"]);
                    var fs = new FileStream(lblCheminPhotos.Text + @"\" + rang["NomComplet"] + @".jpg", FileMode.Open);
                    var br = new BinaryReader(fs);
                    br.BaseStream.Seek(0, SeekOrigin.Begin);
                    byte[] ba;
                    ba = br.ReadBytes((int)br.BaseStream.Length);
                    fs.Close();
                    compteAvecPhoto.Properties["thumbnailPhoto"].Clear();
                    compteAvecPhoto.Properties["thumbnailPhoto"].Insert(0, ba);
                    compteAvecPhoto.CommitChanges();
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void AfficherPhotoElève()
        {
            DirectoryEntry deOu =
                new DirectoryEntry(
                    "LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text,
                    @"stj\administrateur", "Lothlu85");
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(DisplayName=" + ListeRésultats.SelectedItem + ")");
            SearchResult compteAd = rechercher.FindOne();
            DirectoryEntry directoryEntry = compteAd.GetDirectoryEntry();

            string displayName = directoryEntry.Properties["displayName"][0].ToString();
            string firstName = directoryEntry.Properties["givenName"][0].ToString();
            string lastName = directoryEntry.Properties["sn"][0].ToString();
            string email = directoryEntry.Properties["mail"][0].ToString();
            string description = directoryEntry.Properties["Description"][0].ToString();
            string compte = directoryEntry.Properties["SamAccountName"][0].ToString();

            SupprimerInfosPhoto();

            if (directoryEntry.Properties.Contains("thumbnailPhoto") == true)
            {
                byte[] pic = (byte[])directoryEntry.Properties["thumbnailPhoto"][0];
                MemoryStream ms = new MemoryStream(pic);
                PhotoElève.Image = Image.FromStream(ms);
                lblClasseElève.Text = description;
                lblCompteUtilisateur.Text = @"Compte : " + compte;
            }
        }

        private void SupprimerInfosPhoto()
        {
            PhotoElève.Image = null;
            PhotoElève.Refresh();
            lblClasseElève.Text = "";
            lblCompteUtilisateur.Text = "";
        }

        private void SynthèseComptesAd()
        {
            var microsoftWord = new Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\FichesEleves.docx";
            var fichierWord = microsoftWord.Documents.Add(chemin);
            microsoftWord.Visible = false;
            // **Query all of the users within the AD**
            DirectoryEntry de =
                new DirectoryEntry(
                    @"LDAP://" + txtAdresseIp.Text + "/OU=Eleves,OU=college,DC=" + txtDomaine1.Text + ",DC=" +
                    txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
            SearchResultCollection results;

            var ds = new DirectorySearcher(de);
            ds.PropertiesToLoad.Add("sAMAccountName");
            ds.PropertiesToLoad.Add("DisplayName");
            ds.PropertiesToLoad.Add("Description");
            ds.PropertiesToLoad.Add("mail");

            //filters out inactive or invalid employee user accounts
            ds.Filter = "(&(objectCategory=person)(objectClass=user))";
            results = ds.FindAll();

            //header columns for Data Table
            DataTable dt = new DataTable();
            dt.Columns.Add("Nom complet", typeof(string));
            dt.Columns.Add("Nom d'utilisateur", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("Adresse eMail", typeof(string));

            foreach (SearchResult sr in results)
            {
                DataRow dr = dt.NewRow();
                DirectoryEntry entry = sr.GetDirectoryEntry();
                if (entry.Properties["sAMAccountName"].Count > 0)
                    dr["Nom d'utilisateur"] = entry.Properties["sAMAccountName"].Value.ToString();
                if (entry.Properties["DisplayName"].Count > 0)
                    dr["Nom complet"] = entry.Properties["DisplayName"].Value.ToString();
                if (entry.Properties["Description"].Count > 0)
                    dr["Description"] = entry.Properties["Description"].Value.ToString();
                if (entry.Properties["mail"].Count > 0)
                    dr["Adresse eMail"] = entry.Properties["mail"].Value.ToString();
                dt.Rows.Add(dr);

                foreach (Field champs in fichierWord.Fields)
                {
                    if (champs.Code.Text.Contains("NomComplet"))
                    {
                        champs.Select();
                        microsoftWord.Selection.TypeText(
                            EnleverAccents(entry.Properties["DisplayName"].Value.ToString()));
                    }
                }
            }

            fichierWord.SaveAs(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants.docx");
            fichierWord.ExportAsFixedFormat(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants.pdf",
                WdExportFormat.wdExportFormatPDF);

            fichierWord.Close();
            microsoftWord.Quit();
            GC.Collect();
            string fullFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"c:\ADWorkEmailPhone.csv");
            //move datatable into CSV
            CréerFichierCsv(dt, fullFilePath);
        }

        //creating the CSV file with the AD user info
        private void CréerFichierCsv(DataTable dt, string strPath)
        {
            StreamWriter sw = new StreamWriter(strPath, false);
            int columnCount = dt.Columns.Count;
            for (int i = 0; i < columnCount; i++)
            {
                sw.Write(dt.Columns[i]);
                if (i < columnCount - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);

            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        sw.Write(dr[i].ToString());
                    }
                    if (i < columnCount - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}