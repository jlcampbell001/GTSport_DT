using GTSport_DT.Regions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTSport_DT.Countries
{
    public partial class CountriesForm : Form
    {
        private static Regions.Region emptyRegion = new Regions.Region("Empty", "");

        private NpgsqlConnection con;
        private CountriesService countriesService;
        private RegionsService regionsService;

        private Country workingCountry = new Country();

        public CountriesForm()
        {
            InitializeComponent();
        }

        public CountriesForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            countriesService = new CountriesService(con);
            regionsService = new RegionsService(con);

            // add test data.
            // AddTestData();

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            UpdateRegionList();
            SetButtons();
        }

        private void tvCountries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingCountry = (Country)tvCountries.SelectedNode.Tag;

            SetToWorkingCountry();

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingCountry();

            txtDescription.Focus();

            SetButtons();
        }
        
        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        // ********************************************************************************
        /// <summary>
        /// Update the tree list.
        /// </summary>
        // ********************************************************************************
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

        // ********************************************************************************
        /// <summary>
        /// Enabled / Disable the buttons.
        /// </summary>
        // ********************************************************************************
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

        private void UpdateRegionList()
        {
            List<Regions.Region> regions = new List<Regions.Region>();

            regions.Add(emptyRegion);
            regions.AddRange(regionsService.GetList(orderedList: true));

            cmbRegion.ValueMember = "PrimaryKey";
            cmbRegion.DisplayMember = "Description";
            cmbRegion.DataSource = regions;
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

        private void SetToWorkingCountry()
        {
            txtDescription.Text = workingCountry.Description;
            cmbRegion.SelectedValue = workingCountry.RegionKey;
        }

        private void UpdateWorkingCountry()
        {
            workingCountry.Description = txtDescription.Text;
            workingCountry.RegionKey = (string)cmbRegion.SelectedValue;
        }

        // ********************************************************************************
        /// <summary>
        /// Sets the tree views selected country to the country with the passed primary key.
        /// </summary>
        /// <param name="primaryKey"></param>
        // ********************************************************************************
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

        public void UpdateFromOtherForms()
        {
            UpdateRegionList();

            SetToWorkingCountry();
        }
    }
}
