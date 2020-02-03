using GTSport_DT.Countries;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The form that allows the user to work with manufacturer data.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class ManufacturersForm : Form
    {
        private static Country emptyCountry = new Country("Empty", "", "");

        private NpgsqlConnection con;
        private CountriesService countiresService;
        private ManufacturersService manufacturersService;
        private Manufacturer workingManufacturer = new Manufacturer();

        /// <summary>Initializes a new instance of the <see cref="ManufacturersForm"/> class.</summary>
        public ManufacturersForm()
        {
            InitializeComponent();
        }

        /// <summary>Initializes a new instance of the <see cref="ManufacturersForm"/> class.</summary>
        /// <param name="con">The connection to the database.</param>
        /// <exception cref="ArgumentNullException">con</exception>
        public ManufacturersForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            manufacturersService = new ManufacturersService(con);
            countiresService = new CountriesService(con);

            //AddTestData();

            InitializeComponent();
        }

        public void UpdateFromOtherForms()
        {
            UpdateCountryList();

            SetToWorkingManufacturer();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            UpdateCountryList();
            SetButtons();
        }

        private void AddTestData()
        {
            Country japan = countiresService.GetByDescription("JAPAN");
            Country germany = countiresService.GetByDescription("GERMANY");
            Country usa = countiresService.GetByDescription("USA");
            Country unitedKingdom = countiresService.GetByDescription("UNITED KINGDOM");
            Country france = countiresService.GetByDescription("FRANCE");

            Manufacturer manufacturer = new Manufacturer("", "MAZDA", japan.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "VOLKSWAGEN", germany.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "FORD", usa.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "MERCEDES-BENZ", germany.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "PORSCHE", germany.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "JAGUAR", unitedKingdom.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "MCLAREN", unitedKingdom.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "ASTON MARTIN", unitedKingdom.PrimaryKey);
            manufacturersService.Save(ref manufacturer);

            manufacturer = new Manufacturer("", "BUGATTI", france.PrimaryKey);
            manufacturersService.Save(ref manufacturer);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetToWorkingManufacturer();

            txtName.Focus();

            SetButtons();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (workingManufacturer.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete manufacturer " + workingManufacturer.Name + "?", "Delete Manufacturer", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        manufacturersService.Delete(workingManufacturer.PrimaryKey);

                        UpdateList();

                        if (tvManufacturers.Nodes.Count > 0)
                        {
                            tvManufacturers.SelectedNode = tvManufacturers.Nodes[0];
                        }
                        else
                        {
                            workingManufacturer = new Manufacturer("", "", emptyCountry.PrimaryKey);
                            SetToWorkingManufacturer();
                        }

                        tvManufacturers.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a manufacturer to delete first.", "Error");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            workingManufacturer = new Manufacturer("", "", emptyCountry.PrimaryKey);

            SetToWorkingManufacturer();

            txtName.Focus();

            SetButtons();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateWorkingManufacturer();

            try
            {
                manufacturersService.Save(ref workingManufacturer);

                UpdateList();

                SetSelected(workingManufacturer.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtName.Focus();
            SetButtons();
        }

        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void SetButtons()
        {
            if (workingManufacturer.Name != txtName.Text || workingManufacturer.CountryKey != (string)cmbCountry.SelectedValue)
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
            }

            if (String.IsNullOrWhiteSpace(workingManufacturer.PrimaryKey))
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
                for (int i = 0; i < tvManufacturers.Nodes.Count; i++)
                {
                    Manufacturer manufacturer = (Manufacturer)tvManufacturers.Nodes[i].Tag;
                    if (manufacturer.PrimaryKey == primaryKey)
                    {
                        tvManufacturers.SelectedNode = tvManufacturers.Nodes[i];
                    }
                }
            }
        }

        private void SetToWorkingManufacturer()
        {
            txtName.Text = workingManufacturer.Name;
            cmbCountry.SelectedValue = workingManufacturer.CountryKey;
        }

        private void tvManufacturers_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingManufacturer = (Manufacturer)tvManufacturers.SelectedNode.Tag;

            SetToWorkingManufacturer();

            SetButtons();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void UpdateCountryList()
        {
            List<Country> countries = new List<Country>();

            countries.Add(emptyCountry);
            countries.AddRange(countiresService.GetList(orderedList: true));

            cmbCountry.ValueMember = "PrimaryKey";
            cmbCountry.DisplayMember = "Description";
            cmbCountry.DataSource = countries;
        }

        private void UpdateList()
        {
            List<Manufacturer> manufacturers = manufacturersService.GetList(true);

            tvManufacturers.BeginUpdate();
            tvManufacturers.Nodes.Clear();

            foreach (Manufacturer manufacturer in manufacturers)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = manufacturer;
                treeNode.Text = manufacturer.Name;

                tvManufacturers.Nodes.Add(treeNode);
            }

            tvManufacturers.EndUpdate();
        }

        private void UpdateWorkingManufacturer()
        {
            workingManufacturer.Name = txtName.Text;
            workingManufacturer.CountryKey = (string)cmbCountry.SelectedValue;
        }
    }
}