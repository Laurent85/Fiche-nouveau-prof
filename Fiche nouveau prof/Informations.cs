using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fiche_nouveau_prof
{
    public partial class Informations : Form
    {
        public Informations()
        {
            InitializeComponent();
        }

        private void Informations_Load(object sender, EventArgs e)
        {
            lblNomComplet.Text = Principal.NomPrénom;
            lblDescription.Text = Principal.Description;
            lblHomeDirectory.Text = @"Mappage en " + Principal.HomeDrive + @"   " + Principal.HomeDirectory;
            //lblHomeDrive.Text = Principal.HomeDrive;
            lblProfilePath.Text = @"Chemin du profil : " + Principal.ProfilePath;
            LboxGroupes.DataSource = Principal.CollecterGroupesUtilisateur(Principal.Compte);
            PhotoElève.Image = Principal.Photo;
        }
    }
}
