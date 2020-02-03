using GTSport_DT.Regions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTSport_DT.Countries
{
    /// <summary>The form that lets a user work with the countries data.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class CountriesForm : Form
    {
        private static Regions.Region emptyRegion = new Regions.Region("Empty", "");

        private NpgsqlConnection con;
        private CountriesService countriesService;
        private RegionsService regionsService;

        private Country workingCountry = new Country();

        /// <summary>Initializes a new instance of the <see cref="CountriesForm"/> class.</summary>
        public CountriesForm()
        {
            InitializeComponent();
        }

        /// <summary>Initializes a new instance of the <see cref="CountriesForm"/> class.</summary>
        /// <param name="con">The connection to the postgreSQL database.</param>
        /// <exception cref="ArgumentNullException">con</exception>
        public CountriesForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            countriesService = new CountriesService(con);
            regionsService = new RegionsService(con);

            // add test data. AddTestData();

            InitializeComponent();
        }

        /// <summary>Updates the region drop down list when they are modified in another form.</summary>
        public void UpdateFromOtherForms()
        {
            UpdateRegionList();

            SetToWorkingCountry();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            UpdateRegionList();
            SetButtons();
        }

        private void AddTestData()
        {
            Regions.Region america = regionsService.GetByDescription("AMERICA");
            Regions.Region eurpoe = regionsService.GetByDescription("EUROPE");
            Regions.Region asia = regionsService.GetByDescription("ASIA-PACIFIC");

            Country country = new Country("", "JAPAN", asia.PrimaryKey);
            countriesService.Save(ref country);

            country = new Country("", "GERMANY", eurpoe.PrimaryKey);
            countriesService.Save(ref country);

            country = new Country("", "USA", america.PrimaryKey);
            countriesService.Save(ref country);

            country = new Country("", "UNITED KINGDOM", eurpoe.PrimaryKey);
            countriesService.Save(ref country);

            country = new Country("", "FRANCE", eurpoe.PrimaryKey);
            countriesService.Save(ref country);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingCountry();

            txtDescription.Focus();

            SetButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (workingCountry.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete country " + workingCountry.Description + "?", "Delete Country", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        countriesService.Delete(workingCountry.PrimaryKey);

                        UpdateList();

                        if (tvCountries.Nodes.Count > 0)
                        {
                            tvCountries.SelectedNode = tvCountries.Nodes[0];
                        }
                        else
                        {
                            workingCountry = new Country("", "", emptyRegion.PrimaryKey);
                            SetToWorkingCountry();
                        }

                        UpdateOtherForms();

                        tvCountries.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a country to delete first.", "Error");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            workingCountry = new Country("", "", emptyRegion.PrimaryKey);

            SetToWorkingCountry();

            txtDescription.Focus();

            SetButtons();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateWorkingCountry();

            try
            {
                countriesService.Save(ref workingCountry);

                UpdateList();

                SetSelected(workingCountry.PrimaryKey);

                UpdateOtherForms();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtDescription.Focus();
            SetButtons();
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void SetButtons()
        {
            if (workingCountry.Description != txtDescription.Text || workingCountry.RegionKey != (string)cmbRegion.SelectedValue)
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }

            if (String.IsNullOrWhiteSpace(workingCountry.PrimaryKey))
            {
                btnDelete.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
            }
        }

        private void SetSelected(string primaryKey)
        {
            if (!String.IsNullOrWhiteSpace(primaryKey))
            {
                for (int i = 0; i < tvCountries.Nodes.Count; i++)
                {
                    Country country = (Country)tvCountries.Nodes[i].Tag;
                    if (country.PrimaryKey == primaryKey)
                    {
                        tvCountries.SelectedNode = tvCountries.Nodes[i];
                    }
                }
            }
        }

        private void SetToWorkingCountry()
        {
            txtDescription.Text = workingCountry.Description;
            cmbRegion.SelectedValue = workingCountry.RegionKey;
        }

        private void tvCountries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingCountry = (Country)tvCountries.SelectedNode.Tag;

            SetToWorkingCountry();

            SetButtons();
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void UpdateList()
        {
            List<Country> countries = countriesService.GetList(true);

            tvCountries.BeginUpdate();
            tvCountries.Nodes.Clear();

            foreach (Country country in countries)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = country;
                treeNode.Text = country.Description;

                tvCountries.Nodes.Add(treeNode);
            }

            tvCountries.EndUpdate();
        }

        private void UpdateOtherForms()
        {
            GTSportForm workingParentForm = (GTSportForm)this.ParentForm;

            workingParentForm.UpdateCountriesOnForms();
        }

        private void UpdateRegionList()
        {
            List<Regions.Region> regions = new List<Regions.Region>();

            regions.Add(emptyRegion);
            regions.AddRange(regionsService.GetList(orderedList: true));

            cmbRegion.ValueMember = "PrimaryKey";
            cmbRegion.DisplayMember = "Description";
            cmbRegion.DataSource = regions;
        }

        private void UpdateWorkingCountry()
        {
            workingCountry.Description = txtDescription.Text;
            workingCountry.RegionKey = (string)cmbRegion.SelectedValue;
        }
    }
}