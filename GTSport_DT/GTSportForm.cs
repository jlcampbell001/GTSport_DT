using GTSport_DT.Owners;
using Npgsql;
using System;
using System.Windows.Forms;

namespace GTSport_DT
{
    public partial class GTSportForm : Form
    {
        protected static string cs = "Host=localhost;Port=5433;Username=GTSport;Password=GTSport;Database=GTSport";
        protected static NpgsqlConnection con;

        protected OwnersService ownersService;

        public Owner currentOwner = new Owner();

        public GTSportForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            con = new NpgsqlConnection(cs);
            con.Open();

            ownersService = new OwnersService(con);

            SetCurrentOwner(ownersService.GetDefaultOwner());
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            con.Close();
        }

        private void ownersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOwnerForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetCurrentOwner(Owner owner)
        {
            currentOwner = owner;

            tsslCurrentOwner.Text = currentOwner.OwnerName;
        }

        private void ShowOwnerForm()
        {
            Boolean createNewForm = true;

            OwnersForm ownersForm = null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "OwnersForm")
                {
                    ownersForm = (OwnersForm)form;
                    createNewForm = false;
                }
            }

            if (createNewForm)
            {
                ownersForm = new OwnersForm(ownersService);
                ownersForm.MdiParent = this;
            }

            if (ownersForm != null)
            {
                ownersForm.Show();
            }
        }

        private void tsslCurrentOwner_Click(object sender, EventArgs e)
        {
            ShowOwnerForm();
        }
    }
}
