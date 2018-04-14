using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Fiche_nouveau_prof
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void CopieFichiersTypeExcel(Stream input, Stream output)
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
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.xlsx";
            if (File.Exists(chemin)) File.Delete(chemin);
            var assembly = Assembly.GetExecutingAssembly();
            var source = assembly.GetManifestResourceStream("Fiche_nouveau_prof.Resources.NouveauProf.xlsx");
            var destination = File.Open(chemin, FileMode.CreateNew);
            CopieFichiersTypeExcel(source, destination);
            source?.Dispose();
            destination.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //CopieRessources();
        }

        private void BtnValider_Click(object sender, EventArgs e)
        {
            var microsoftExcel = new Microsoft.Office.Interop.Excel.Application();
            var chemin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.xlsx";
            var fichierExcel = microsoftExcel.Workbooks.Open(chemin);
            var feuilleExcel = (Worksheet)fichierExcel.Sheets.Item[1];
            var cellule = feuilleExcel.Range["B3"];
            cellule.Value = Nom.Text;
            microsoftExcel.DisplayAlerts = false;
            fichierExcel.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NouveauProf.xlsx");
            fichierExcel.Close();
            GC.Collect();
        }

        public static void ReplaceMailMergeField(string msWordFileName,
    string mailMergeFieldName, string mailMergeFieldDesiredValue)
        {
            object docName = msWordFileName;
            object missing = Missing.Value;
            Word.MailMerge mailMerge;
            Word._Document doc;
            Word.Application app = new Word.Application();
            // Hide MS Word's window.
            app.Visible = false;
            doc = app.Documents.Open(ref docName,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing);
            mailMerge = doc.MailMerge;
            // Try to find the field name.
            foreach (Word.MailMergeField f in mailMerge.Fields)
            {
                // Assuming the field code is: MERGEFIELD  "mailMergeFieldName"
                if (f.Code.Text.IndexOf("MERGEFIELD  \"" + mailMergeFieldName + "\"") > -1)
                {
                    f.Select();
                    // Replace selected field with supplied value.
                    app.Selection.TypeText(mailMergeFieldDesiredValue);
                }
            }
            // Save changes and close MS Word.
            object saveChanges = Word.WdSaveOptions.wdSaveChanges;
            doc.Close(ref saveChanges, ref missing, ref missing);
            app.Quit(ref missing, ref missing, ref missing);
        }
    }
}