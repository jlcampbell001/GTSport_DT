using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
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

namespace GTSport_DT.OwnerCars
{
    public partial class FilterCarsForm : Form
    {
        private static Manufacturer emptyManufacturer = new Manufacturer("Empty", "", "");
        private static Country emptyCountry = new Country("Empty", "", "");
        private static Regions.Region emptyRegion = new Regions.Region("Empty", "");

        private NpgsqlConnection con;
        private ManufacturersService manufacturersService;
        private CountriesService countiresService;
        private RegionsService regionsService;

        public CarSearchCriteria returnCriteria { get; set; }

        public FilterCarsForm()
        {
            InitializeComponent();
        }

        public FilterCarsForm(CarSearchCriteria returnCriteria, NpgsqlConnection con)
        {
            this.returnCriteria = returnCriteria ?? throw new ArgumentNullException(nameof(returnCriteria));
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            manufacturersService = new ManufacturersService(con);
            countiresService = new CountriesService(con);
            regionsService = new RegionsService(con);

            InitializeComponent();
        }

        private void UpdateCategoryLists()
        {
            cmbCategoryTo.DisplayMember = "Description";
            cmbCategoryTo.DataSource = CarCategory.categories;

            var fromCategories = CarCategory.categories.Clone();

            cmbCategoryFrom.DisplayMember = "Description";
            cmbCategoryFrom.DataSource = fromCategories;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateReturnCriteria();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateCategoryLists();
            UpdateDrivetrainList();
            UpdateManufacturerList();
            UpdateCountryList();
            UpdateRegionList();

            SetToSearchCriteria();
        }

        private void SetToSearchCriteria()
        {
            if (returnCriteria.CategoryFrom == null)
            {
                cmbCategoryFrom.SelectedItem = CarCategory.Empty;
            }
            else
            {
                cmbCategoryFrom.SelectedItem = returnCriteria.CategoryFrom;
            }

            if (returnCriteria.CategoryTo == null)
            {
                cmbCategoryTo.SelectedItem = CarCategory.Empty;
            }
            else
            {
                cmbCategoryTo.SelectedItem = returnCriteria.CategoryTo;
            }

            nudYearFrom.Value = (decimal)returnCriteria.YearFrom;
            nudYearTo.Value = (decimal)returnCriteria.YearTo;
            nudMaxPowerFrom.Value = (decimal)returnCriteria.MaxPowerFrom;
            nudMaxPowerTo.Value = (decimal)returnCriteria.MaxPowerTo;

            if (returnCriteria.DriveTrain == null)
            {
                cmbDrivetrain.SelectedItem = DriveTrain.Empty;
            }
            else
            {
                cmbDrivetrain.SelectedItem = returnCriteria.DriveTrain;
            }

            if (returnCriteria.ManufacturerName == null)
            {
                cmbManufacturer.SelectedItem = emptyManufacturer;
            } else
            {
                cmbManufacturer.SelectedValue = returnCriteria.ManufacturerName;
            }

            if (returnCriteria.CountryDescription == null)
            {
                cmbCountry.SelectedItem = emptyCountry;
            }
            else
            {
                cmbCountry.SelectedValue = returnCriteria.CountryDescription;
            }

            if (returnCriteria.RegionDescription == null)
            {
                cmbRegion.SelectedItem = emptyRegion;
            }
            else
            {
                cmbRegion.SelectedValue = returnCriteria.RegionDescription;
            }
        }

        private void UpdateReturnCriteria()
        {
            if (cmbCategoryFrom.SelectedItem == CarCategory.Empty)
            {
                returnCriteria.CategoryFrom = null;
            }
            else
            {
                returnCriteria.CategoryFrom = (CarCategory.Category)cmbCategoryFrom.SelectedItem;
            }

            if (cmbCategoryTo.SelectedItem == CarCategory.Empty)
            {
                returnCriteria.CategoryTo = null;
            }
            else
            {
                returnCriteria.CategoryTo = (CarCategory.Category)cmbCategoryTo.SelectedItem;
            }

            returnCriteria.YearFrom = Decimal.ToInt32(nudYearFrom.Value);
            returnCriteria.YearTo = Decimal.ToInt32(nudYearTo.Value);
            returnCriteria.MaxPowerFrom = Decimal.ToInt32(nudMaxPowerFrom.Value);
            returnCriteria.MaxPowerTo = Decimal.ToInt32(nudMaxPowerTo.Value);

            if (cmbDrivetrain.SelectedItem == DriveTrain.Empty)
            {
                returnCriteria.DriveTrain = null;
            } else
            {
                returnCriteria.DriveTrain = cmbDrivetrain.SelectedItem.ToString();
            }

            if (cmbManufacturer.SelectedItem == emptyManufacturer)
            {
                returnCriteria.ManufacturerName = null;
            } else
            {
                returnCriteria.ManufacturerName = (string)cmbManufacturer.SelectedValue;
            }

            if (cmbCountry.SelectedItem == emptyCountry)
            {
                returnCriteria.CountryDescription = null;
            }
            else
            {
                returnCriteria.CountryDescription = (string)cmbCountry.SelectedValue;
            }

            if (cmbRegion.SelectedItem == emptyRegion)
            {
                returnCriteria.RegionDescription = null;
            }
            else
            {
                returnCriteria.RegionDescription = (string)cmbRegion.SelectedValue;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void nudYearFrom_Enter(object sender, EventArgs e)
        {
            nudYearFrom.Select(0, nudYearFrom.Text.Length);
        }

        private void nudYearTo_Enter(object sender, EventArgs e)
        {
            nudYearTo.Select(0, nudYearTo.Text.Length);
        }

        private void nudMaxPower_Enter(object sender, EventArgs e)
        {
            nudMaxPowerFrom.Select(0, nudMaxPowerFrom.Text.Length);
        }

        private void nudMaxPowerTo_Enter(object sender, EventArgs e)
        {
            nudMaxPowerTo.Select(0, nudMaxPowerTo.Text.Length);
        }

        private void UpdateDrivetrainList()
        {
            cmbDrivetrain.DataSource = DriveTrain.DriveTrains;
        }

        private void UpdateManufacturerList()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            manufacturers.Add(emptyManufacturer);
            manufacturers.AddRange(manufacturersService.GetList(orderedList: true));

            cmbManufacturer.ValueMember = "Name";
            cmbManufacturer.DisplayMember = "Name";
            cmbManufacturer.DataSource = manufacturers;
        }

        private void UpdateCountryList()
        {
            List<Country> countries = new List<Country>();

            countries.Add(emptyCountry);
            countries.AddRange(countiresService.GetList(orderedList: true));

            cmbCountry.ValueMember = "Description";
            cmbCountry.DisplayMember = "Description";
            cmbCountry.DataSource = countries;
        }

        private void UpdateRegionList()
        {
            List<Regions.Region> regions = new List<Regions.Region>();

            regions.Add(emptyRegion);
            regions.AddRange(regionsService.GetList(orderedList: true));

            cmbRegion.ValueMember = "Description";
            cmbRegion.DisplayMember = "Description";
            cmbRegion.DataSource = regions;
        }

    }
}
