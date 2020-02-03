﻿using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Owners;
using GTSport_DT.Regions;
using Npgsql;
using System;
using System.Windows.Forms;

namespace GTSport_DT
{
    /// <summary>The main form for the GT sport application.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class GTSportForm : Form
    {
        /// <summary>The current owner the application is working with.</summary>
        public Owner currentOwner = new Owner();

        /// <summary>The connection object to the postgreSQL database.</summary>
        protected static NpgsqlConnection con;

        /// <summary>The connection string to the database.</summary>
        protected static string cs = "Host=localhost;Port=5433;Username=GTSport;Password=GTSport;Database=GTSport";

        /// <summary>The owners service.</summary>
        protected OwnersService ownersService;

        /// <summary>Initializes a new instance of the <see cref="GTSportForm"/> class.</summary>
        public GTSportForm()
        {
            InitializeComponent();
        }

        /// <summary>Sets the current owner.</summary>
        /// <param name="owner">The owner.</param>
        public void SetCurrentOwner(Owner owner)
        {
            currentOwner = owner;

            tsslCurrentOwner.Text = currentOwner.OwnerName;
        }

        /// <summary>
        /// <para>Updates the countries on child forms when a country is changed / added / deleted.</para>
        /// <para>Called by the countries form.</para>
        /// </summary>
        public void UpdateCountriesOnForms()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "ManufacturersForm")
                {
                    ManufacturersForm manufacturersForm = (ManufacturersForm)form;
                    manufacturersForm.UpdateFromOtherForms();
                }
            }
        }

        /// <summary>
        /// <para>Updates the regions on child forms when a region is changed / added / deleted.</para>
        /// <para>Called by the regions form.</para>
        /// </summary>
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

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Closed"/> event.</summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            con.Close();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            con = new NpgsqlConnection(cs);
            con.Open();

            ownersService = new OwnersService(con);

            SetCurrentOwner(ownersService.GetDefaultOwner());
        }

        private void countriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCountryForm();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void manufacturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowManufacturerForm();
        }

        private void ownersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowOwnerForm();
        }

        private void regionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowRegionForm();
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

        private void ShowManufacturerForm()
        {
            Boolean createNewForm = true;

            ManufacturersForm manufacturersForm = null;

            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "ManufacturersForm")
                {
                    manufacturersForm = (ManufacturersForm)form;
                    createNewForm = false;
                }
            }

            if (createNewForm)
            {
                manufacturersForm = new ManufacturersForm(con);
                manufacturersForm.MdiParent = this;
            }

            if (manufacturersForm != null)
            {
                manufacturersForm.Show();
                manufacturersForm.Activate();
            }
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

        private void tsslCurrentOwner_Click(object sender, EventArgs e)
        {
            ShowOwnerForm();
        }
    }
}