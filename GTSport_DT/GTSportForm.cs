using GTSport_DT.Countries;
using GTSport_DT.Owners;
using GTSport_DT.Regions;
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
        private void regionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRegionForm();
        }

        private void countriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCountryForm();
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
                ownersForm.Activate();
            }
        }

        private void ShowRegionForm()
        {
            Boolean createNewForm = true;

            RegionsForm regionForm = null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "RegionsForm")
                {
                    regionForm = (RegionsForm)form;
                    createNewForm = false;
                }
            }

            if (createNewForm)
            {
                regionForm = new RegionsForm(con);
                regionForm.MdiParent = this;
            }

            if (regionForm != null)
            {
                regionForm.Show();
                regionForm.Activate();
            }
        }

        private void ShowCountryForm()
        {
            Boolean createNewForm = true;

            CountriesForm countryForm = null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "CountriesForm")
                {
                    countryForm = (CountriesForm)form;
                    createNewForm = false;
                }
            }

            if (createNewForm)
            {
                countryForm = new CountriesForm(con);
                countryForm.MdiParent = this;
            }

            if (countryForm != null)
            {
                countryForm.Show();
                countryForm.Activate();
            }
        }

        private void tsslCurrentOwner_Click(object sender, EventArgs e)
        {
            ShowOwnerForm();
        }

        public void UpdateRegionsOnForms()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "CountriesForm")
                {
                    CountriesForm countriesForm = (CountriesForm)form;
                    countriesForm.UpdateFromOtherForms();
                }
            }
        }
    }
}
