using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Fiche_nouveau_prof
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
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

        private void OuvertureLogiciel(object sender, EventArgs e)
        {
            CopieRessources();
            RemplirListeBox(ListeProfs,
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Nouveaux profs 2017-2018", "*.docx");
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

            fichierWord.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Nouveaux profs 2017-2018\\Identifiants " + Prénom.Text + " " + Nom.Text + ".docx");
            fichierWord.ExportAsFixedFormat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Nouveaux profs 2017-2018\\Identifiants " + Prénom.Text + " " + Nom.Text + ".pdf",
                         Word.WdExportFormat.wdExportFormatPDF);

            fichierWord.Close();
            microsoftWord.Quit();
            GC.Collect();

            RemplirListeBox(ListeProfs,
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Nouveaux profs 2017-2018", "*.docx");
        }

        private void BtnEnvoyer_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.orange.fr");
            mail.From = new MailAddress("admin@clg-stjacques.fr");
            mail.To.Add("manceaulaurent@yahoo.fr");
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            foreach (var item in ListeProfs.CheckedItems)
            {
                try
                {
                    Attachment attachment;
                    attachment = new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Nouveaux profs 2017-2018\\" + item);
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

            smtpServer.Send(mail);
            MessageBox.Show(@"mail Send");
        }

        public static string EnleverAccents(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }
    }
}