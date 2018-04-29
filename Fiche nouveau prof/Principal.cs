using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Mail;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

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

        private string Utilisateur()
        {
            string utilisateur = txtUtilisateur.Text;
            return utilisateur;
        }

        private string MotDePasse()
        {
            string motDePasse = txtMotDePasse.Text;
            return motDePasse;
        }

        private void OuvertureLogiciel(object sender, EventArgs e)
        {
            CopieRessources();
            RemplirListeBox(ListeProfs, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
            Nom.Focus();
            Nom.Select();
            txtAdresseIp.Text = @"172.16.0.1";
            txtDomaine1.Text = @"stj";
            txtDomaine2.Text = @"lan";
            txtUtilisateur.Text = @"administrateur";
            txtMotDePasse.Text = @"Lothlu85";
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

        private void CopieRessources()
        {
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.docx";
            if (File.Exists(chemin)) File.Delete(chemin);
            var assembly = Assembly.GetExecutingAssembly();
            var source = assembly.GetManifestResourceStream("Fiche_nouveau_prof.Resources.NouveauProf.docx");
            var destination = File.Open(chemin, FileMode.CreateNew);
            CopieFichiersTypeWord(source, destination);
            source?.Dispose();
            destination.Dispose();
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
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            var microsoftWord = new Word.Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.docx";
            var fichierWord = microsoftWord.Documents.Add(chemin);
            microsoftWord.Visible = false;
            int i = 0;
            int j = 0;

            foreach (Word.Field champs in fichierWord.Fields)
            {
                if (champs.Code.Text.Contains("Prénom") && i == 0)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Prénom.Text));
                    i = 1;
                }
                else if (champs.Code.Text.Contains("Prénom") && i == 1)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(EnleverAccents(Prénom.Text));
                }
                else if (champs.Code.Text.Contains("Nom") && j == 0)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Nom.Text));
                    j = 1;
                }
                else if (champs.Code.Text.Contains("Nom") && j == 1)
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(EnleverAccents(Nom.Text));
                }
                else if (champs.Code.Text.Contains("Email"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(Email.Text);
                }
                else if (champs.Code.Text.Contains("Copieur"))
                {
                    champs.Select();
                    microsoftWord.Selection.TypeText(Copieur.Text);
                }
            }

            fichierWord.SaveAs(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants " + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Prénom.Text) + " " + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Nom.Text) + ".docx");
            fichierWord.ExportAsFixedFormat(@"X:\Année 2017-2018\Nouveaux profs 2017-2018\Identifiants " + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Prénom.Text) + " " + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(Nom.Text) + ".pdf",
                         Word.WdExportFormat.wdExportFormatPDF);

            fichierWord.Close();
            microsoftWord.Quit();
            GC.Collect();
            RemplirListeBox(ListeProfs, @"X:\Année 2017-2018\Nouveaux profs 2017-2018", "*.*");
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

            foreach (var item in ListeProfs.CheckedItems)
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
            smtpServer.Credentials = new System.Net.NetworkCredential("laurent_manceau@orange.fr", "Lothlu85");
            smtpServer.EnableSsl = false;

            //smtpServer.Send(mail);
            MessageBox.Show(@"Mail envoyé");
        }

        private void BtnConnexionAD_Click(object sender, EventArgs e)
        {
            DirectoryEntry entry = new DirectoryEntry(ConnectionAd(), txtDomaine1.Text + "\\" + Utilisateur(), MotDePasse());
            DirectorySearcher ds = new DirectorySearcher(entry);
            ds.Filter = "(&(&(objectClass=user)(memberOf=CN=Administrateurs,CN=Builtin,DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text + "))(samAccountName=" + Utilisateur() + "))";
            SearchResult result = ds.FindOne();

            if (result != null)
            {
                lblEtatConnexionAd.Text = @"Connecté à Active Directory";
            }
            else
            {
                lblEtatConnexionAd.Text = @"Echec de la connexion";
            }
        }

        private void BtnCréationUtilisateurAdClick(object sender, EventArgs e)
        {
            if (rdBtnEleve.Checked)
                CréationUtilisateur(Nom.Text, Prénom.Text, txbGroupe.Text, rdBtnEleve.Text);
            if (rdBtnProf.Checked)
                CréationUtilisateur(Nom.Text, Prénom.Text, txbGroupe.Text, rdBtnProf.Text);
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

        private void BtnLancer_Click(object sender, EventArgs e)
        {
            ImportUtilisateurs();
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
            byte[] bytes = Encoding.GetEncoding("Cyrillic").GetBytes(txt); //Tailspin uses Cyrillic (ISO-8859-5); others use Hebraw (ISO-8859-8)
            return Encoding.ASCII.GetString(bytes);
        }

        private void NettoyageTextbox()
        {
            foreach (Control textbox in Controls)
            {
                if (textbox is TextBox) textbox.Text = "";
                Nom.Select();
            }
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

            string adPath1 = "LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text;
            DirectoryEntry entry = new DirectoryEntry(adPath1, txtDomaine1.Text + "\\" + Utilisateur(), MotDePasse());
            DirectoryEntries users = entry.Children;
            DirectoryEntry newuser = users.Add("CN=" + nouveauNom, "user");
            newuser.Properties["samAccountName"].Value = nouveauNom;
            newuser.Properties["uid"].Value = nouveauNom;
            newuser.Properties["userPrincipalName"].Value = nouveauNom + "@stj.lan";
            newuser.Properties["givenName"].Value = prénom;
            newuser.Properties["sn"].Value = nom;
            newuser.Properties["displayname"].Add(nom + " " + prénom);
            newuser.Properties["mail"].Add(prénom + "." + nom + "@clg-stjacques.fr");
            if (ou == "Eleves") { newuser.Properties["description"].Add("Eleve de " + groupe); }
            if (ou == "Profs") { newuser.Properties["description"].Add(groupe); }
            newuser.Properties["profilePath"].Add(@"\\Serveur2008\profil\" + ou + @"\" + nouveauNom);
            newuser.Properties["homedirectory"].Add(@"\\Serveur2008\" + ou + @"\" + nouveauNom);
            newuser.Properties["homedrive"].Add("H:");
            newuser.CommitChanges();

            newuser.Invoke("SetPassword", new object[] { "Toto1234" });
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
            DéfinirPermissions(nouveauNom, @"\\serveur2008\" + ou + @"\" + nouveauNom, FileSystemRights.FullControl, AccessControlType.Allow);
            DéfinirPermissions("profs", @"\\serveur2008\" + ou + @"\" + nouveauNom, FileSystemRights.FullControl, AccessControlType.Allow);
            DéfinirPermissions("Administrateurs", @"\\serveur2008\" + ou + @"\" + nouveauNom, FileSystemRights.FullControl, AccessControlType.Allow);
            PartagerDossier(@"E:\Users\" + ou + @"\" + nouveauNom, nouveauNom + @"$", @"Dossier personnel de " + nouveauNom, nouveauNom);
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

        private void ImportUtilisateurs()
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(lblCheminFichierExcel.Text);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

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

        private void CréationGroupe(string ou, string groupe)
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
                DirectoryEntry group = entry.Children.Add("CN=" + groupe, "group");
                group.Properties["sAmAccountName"].Value = groupe;
                if (groupe == "Eleves")
                {
                    group.Properties["groupType"].Value =
                         ActiveDs.ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_DOMAIN_LOCAL_GROUP | ActiveDs.ADS_GROUP_TYPE_ENUM.ADS_GROUP_TYPE_SECURITY_ENABLED;
                }
                group.CommitChanges();
                entry.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        private void CréationGroupeDistribution(string ou, string groupe)
        {
            DirectoryEntry entry = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85");
            DirectoryEntry group = entry.Children.Add("CN=distri-" + groupe, "group");
            group.Properties["sAmAccountName"].Value = "distri-" + groupe;
            group.Properties["groupType"].Value = 0x2;
            group.Properties["mail"].Value = "Classe" + groupe + "@clg-stjacques.fr";
            group.CommitChanges();
            entry.Close();
        }

        private void AjouterUtilisateurAuGroupe(string userId, string groupName, string ou)
        {
            DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/CN=" + groupName + ", OU=" + ou + ", OU=college, DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            dirEntry.Properties["member"].Add("CN=" + userId + ",OU=" + ou + ",OU=college,DC=stj,DC=lan");
            dirEntry.CommitChanges();
            dirEntry.Close();
        }

        private void DéfinirPermissions(string utilisateur, string cheminDossier, FileSystemRights accès, AccessControlType accèsType)
        {
            DirectoryEntry deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var fileSystemRule = new FileSystemAccessRule(sid, accès, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, accèsType);

            directorySecurity.AddAccessRule(fileSystemRule);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private void ModifierPermissions(string utilisateur, string cheminDossier, FileSystemRights accès, AccessControlType accèsType)
        {
            DirectoryEntry deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            var fileSystemRule = new FileSystemAccessRule(sid, accès, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None, accèsType);

            bool result;
            directorySecurity.ModifyAccessRule(AccessControlModification.Add, fileSystemRule, out result);
            directoryInfo.SetAccessControl(directorySecurity);
        }

        private void SupprimerToutesPermissions(string cheminDossier)
        {
            var directoryInfo = new DirectoryInfo(cheminDossier);
            DirectorySecurity objSecObj = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection acl = objSecObj.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions
            foreach (FileSystemAccessRule ace in acl) //to remove any other permission
            {
                objSecObj.PurgeAccessRules(ace.IdentityReference);  //same as use objSecObj.RemoveAccessRuleSpecific(ace);
            }

            directoryInfo.SetAccessControl(objSecObj);
        }

        private void SupprimerPermissions(string utilisateur, string cheminDossier)
        {
            DirectoryEntry deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            var directoryInfo = new DirectoryInfo(cheminDossier);
            var directorySecurity = directoryInfo.GetAccessControl();

            DirectorySecurity objSecObj = directoryInfo.GetAccessControl();
            AuthorizationRuleCollection acl = directorySecurity.GetAccessRules(true, true, typeof(NTAccount));
            objSecObj.SetAccessRuleProtection(true, false); //to remove inherited permissions

            objSecObj.PurgeAccessRules(sid);  //same as use objSecObj.RemoveAccessRuleSpecific(ace);

            directoryInfo.SetAccessControl(objSecObj);
        }

        private bool UtilisateurExiste(string utilisateur)
        {
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, "DC=stj,DC=lan", @"stj\administrateur", "Lothlu85"))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, utilisateur))
                {
                    if (foundUser != null)
                        return true;
                    return false;
                }
            }
        }

        private bool GroupeExiste(string groupe)
        {
            using (var domainContext = new PrincipalContext(ContextType.Domain, txtAdresseIp.Text, "DC=stj,DC=lan", @"stj\administrateur", "Lothlu85"))
            {
                using (var foundUser = GroupPrincipal.FindByIdentity(domainContext, IdentityType.Name, groupe))
                {
                    if (foundUser != null)
                        return true;
                    return false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SupprimerPermissions("Profs", @"\\serveur2008\Eleves\Boilou2");
            //SupprimerToutesPermissions(@"\\serveur2008\Eleves\Boilou2");
            //DéfinirPermissions("Administrateurs", @"\\serveur2008\Eleves\Boilou2", FileSystemRights.FullControl,AccessControlType.Allow);
            //ModifierPermissions("Administrateurs", @"\\serveur2008\Eleves\Boilou2", FileSystemRights.Read,AccessControlType.Allow);
            //PartagerDossier(@"E:\Commun_prof\Polices", @"Polices$", @"Laurent", "Administrateurs");
        }

        private string OuChoisie()
        {
            string ou1 = null;
            foreach (RadioButton ou in grpBoxOu.Controls)
            {
                if (ou.Checked)
                    ou1 = ou.Text;
            }
            return ou1;
        }

        public void PartagerDossier(string folderPath, string shareName, string description, string utilisateur)
        {
            DirectoryEntry deOu = new DirectoryEntry("LDAP://" + txtAdresseIp.Text + "/DC=" + txtDomaine1.Text + ",DC=" + txtDomaine2.Text, @"stj\administrateur", "Lothlu85", AuthenticationTypes.Secure);
            DirectorySearcher rechercher = new DirectorySearcher(deOu, "(sAMAccountName=" + utilisateur + ")");
            SearchResult compteAd = rechercher.FindOne();
            SecurityIdentifier sid = new SecurityIdentifier(compteAd.Properties["objectSid"][0] as byte[], 0);

            byte[] utenteSidArray = new byte[sid.BinaryLength];
            sid.GetBinaryForm(utenteSidArray, 0);

            ManagementObject oGrpTrustee = new ManagementClass(new ManagementPath("Win32_Trustee"), null);
            oGrpTrustee["Name"] = utilisateur;
            oGrpTrustee["SID"] = utenteSidArray;

            ManagementObject oGrpAce = new ManagementClass(new ManagementPath("Win32_Ace"), null);
            oGrpAce["AccessMask"] = 2032127;//Full access
            oGrpAce["AceFlags"] = AceFlags.ObjectInherit | AceFlags.ContainerInherit; //propagate the AccessMask to the subfolders
            oGrpAce["AceType"] = AceType.AccessAllowed;
            oGrpAce["Trustee"] = oGrpTrustee;

            ManagementObject oGrpSecurityDescriptor = new ManagementClass(new ManagementPath("Win32_SecurityDescriptor"), null);
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
    }
}