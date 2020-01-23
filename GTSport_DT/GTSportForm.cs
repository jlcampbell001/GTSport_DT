using GTSport_DT.Owners;
using System;
using System.Windows.Forms;

namespace GTSport_DT
{
    public partial class GTSportForm : Form
    {
        public GTSportForm()
        {
            InitializeComponent();
        }

        private void ownersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OwnersForm ownersForm = new OwnersForm();
            ownersForm.Show();
            ownersForm.MdiParent = this;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
