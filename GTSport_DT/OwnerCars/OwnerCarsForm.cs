using GTSport_DT.Cars;
using GTSport_DT.Manufacturers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The owners car form.</summary>
    /// <seealso cref="System.Windows.Forms.Form"/>
    public partial class OwnerCarsForm : Form
    {
        private static Manufacturer emptyManufacturer = new Manufacturer("Empty", "", "");
        private CarsService carsService;
        private NpgsqlConnection con;
        private ManufacturersService manufacturersService;
        private Car workingCar = new Car();

        /// <summary>Initializes a new instance of the <see cref="OwnerCarsForm"/> class.</summary>
        public OwnerCarsForm()
        {
            InitializeComponent();
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerCarsForm"/> class.</summary>
        /// <param name="con">The connection.</param>
        /// <exception cref="ArgumentNullException">con</exception>
        public OwnerCarsForm(NpgsqlConnection con)
        {
            this.con = con ?? throw new ArgumentNullException(nameof(con));

            manufacturersService = new ManufacturersService(con);
            carsService = new CarsService(con);

            //AddTestData();

            InitializeComponent();
        }

        /// <summary>Updates form with changes from other forms.</summary>
        public void UpdateFromOtherForms()
        {
            UpdateManufacturerList();

            SetToWorkingCar();
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateList();
            UpdateManufacturerList();
            UpdateCategoryList();
            UpdateDrivetrainList();
            UpdateAspirationList();
            SetButtons();
        }

        private void AddTestData()
        {
            Manufacturer mazada = manufacturersService.GetByName("MAZDA");
            Manufacturer volkswagen = manufacturersService.GetByName("VOLKSWAGEN");
            Manufacturer ford = manufacturersService.GetByName("FORD");

            Car car = new Car("", "Roadster S (ND) '15", mazada.PrimaryKey, 2015, CarCategory.N100,
                24900.00, "1496", 128, "7000", 110.6, "5000", DriveTrain.FR, Aspiration.Empty, 154.1,
                68.3, 48.6, 2183, 4.1, 2.8, 1.3, 1.1, 4.9);
            carsService.Save(ref car);

            car = new Car("", "Golf VII GTI '14", volkswagen.PrimaryKey, 2014, CarCategory.N200,
                38330.00, "1984", 219, "4700", 258.2, "1500", DriveTrain.FF, Aspiration.Empty, 168.3,
                70.90, 57.10, 3064, 5.3, 2.7, 1.5, 1.3, 5.3);
            carsService.Save(ref car);

            car = new Car("", "Focus ST '15", ford.PrimaryKey, 1998, CarCategory.N300,
                29250.00, "1999", 250, "5500", 669.70, "2500", DriveTrain.FF, Aspiration.Empty, 171.70,
                71.70, 58.40, 3223, 5.7, 2.5, 1.7, 1.4, 5.2);
            carsService.Save(ref car);
        }

        private void btnCancelCar_Click(object sender, EventArgs e)
        {
            SetToWorkingCar();

            txtNameCar.Focus();

            SetButtons();
        }

        private void btnDeleteCar_Click(object sender, EventArgs e)
        {
            if (workingCar.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete car " + workingCar.Name + "?", "Delete Car", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        carsService.Delete(workingCar.PrimaryKey);

                        UpdateList();

                        if (tvOwnedCars.Nodes.Count > 0)
                        {
                            tvOwnedCars.SelectedNode = tvOwnedCars.Nodes[0];
                        }
                        else
                        {
                            workingCar = new Car("", "", emptyManufacturer.PrimaryKey, 1900, CarCategory.Empty,
                                0.00, "", 0, "", 0.00, "", DriveTrain.Empty, Aspiration.Empty, 0.00, 0.00, 0.00,
                                0.00, 0.00, 0.0, 0.0, 0.0, 0.0);
                            SetToWorkingCar();
                        }

                        tvOwnedCars.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("Select a car to delete first.", "Error");
            }
        }

        private void btnNewCar_Click(object sender, EventArgs e)
        {
            workingCar = new Car("", "", emptyManufacturer.PrimaryKey, 1900, CarCategory.Empty,
                0.00, "", 0, "", 0.00, "", DriveTrain.Empty, Aspiration.Empty, 0.00, 0.00, 0.00,
                0.00, 0.00, 0.0, 0.0, 0.0, 0.0);

            SetToWorkingCar();

            txtNameCar.Focus();

            SetButtons();
        }

        private void btnSaveCar_Click(object sender, EventArgs e)
        {
            UpdateWorkingCar();

            try
            {
                carsService.Save(ref workingCar);

                UpdateList();

                SetSelected(workingCar.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtNameCar.Focus();
            SetButtons();
        }

        private void cmbAspiration_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void cmbDrivetrain_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void cmbManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudAcceleration_Enter(object sender, EventArgs e)
        {
            nudAcceleration.Select(0, nudAcceleration.Text.Length);
        }

        private void nudAcceleration_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudBraking_Enter(object sender, EventArgs e)
        {
            nudBraking.Select(0, nudBraking.Text.Length);
        }

        private void nudBraking_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudCornering_Enter(object sender, EventArgs e)
        {
            nudCornering.Select(0, nudCornering.Text.Length);
        }

        private void nudCornering_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudHeight_Enter(object sender, EventArgs e)
        {
            nudHeight.Select(0, nudHeight.Text.Length);
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudLength_Enter(object sender, EventArgs e)
        {
            nudLength.Select(0, nudLength.Text.Length);
        }

        private void nudLength_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudMaxPower_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudMaxSpeed_Enter(object sender, EventArgs e)
        {
            nudMaxSpeed.Select(0, nudMaxSpeed.Text.Length);
        }

        private void nudMaxSpeed_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudPrice_Enter(object sender, EventArgs e)
        {
            nudPrice.Select(0, nudPrice.Text.Length);
        }

        private void nudPrice_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudStability_Enter(object sender, EventArgs e)
        {
            nudStability.Select(0, nudStability.Text.Length);
        }

        private void nudStability_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudTorqueFTLB_Enter(object sender, EventArgs e)
        {
            nudTorqueFTLB.Select(0, nudTorqueFTLB.Text.Length);
        }

        private void nudTorqueFTLB_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudWeight_Enter(object sender, EventArgs e)
        {
            nudWeight.Select(0, nudWeight.Text.Length);
        }

        private void nudWeight_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudWidth_Enter(object sender, EventArgs e)
        {
            nudWidth.Select(0, nudWidth.Text.Length);
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudYear_Enter(object sender, EventArgs e)
        {
            nudYear.Select(0, nudYear.Text.Length);
        }

        private void nudYear_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void SetButtons()
        {
            if (workingCar.Name != txtNameCar.Text
                || workingCar.ManufacturerKey != (string)cmbManufacturer.SelectedValue
                || workingCar.Year != nudYear.Value
                || workingCar.Category != (CarCategory.Category)cmbCategory.SelectedItem
                || workingCar.Price != Decimal.ToDouble(nudPrice.Value)
                || workingCar.DisplacementCC != txtDisplacementCC.Text
                || workingCar.MaxPower != nudMaxPower.Value
                || workingCar.PowerRPM != txtPowerRPM.Text
                || workingCar.TorqueFTLB != Decimal.ToDouble(nudTorqueFTLB.Value)
                || workingCar.TorqueRPM != txtTorqueRPM.Text
                || workingCar.DriveTrain != cmbDrivetrain.SelectedItem.ToString()
                || workingCar.Aspiration != cmbAspiration.SelectedItem.ToString()
                || workingCar.Length != Decimal.ToDouble(nudLength.Value)
                || workingCar.Width != Decimal.ToDouble(nudWidth.Value)
                || workingCar.Height != Decimal.ToDouble(nudHeight.Value)
                || workingCar.Weight != Decimal.ToDouble(nudWeight.Value)
                || workingCar.MaxSpeed != Decimal.ToDouble(nudMaxSpeed.Value)
                || workingCar.Acceleration != Decimal.ToDouble(nudAcceleration.Value)
                || workingCar.Braking != Decimal.ToDouble(nudBraking.Value)
                || workingCar.Cornering != Decimal.ToDouble(nudCornering.Value)
                || workingCar.Stability != Decimal.ToDouble(nudStability.Value))
            {
                btnSaveCar.Enabled = true;
                btnCancelCar.Enabled = true;
            }
            else
            {
                btnSaveCar.Enabled = false;
                btnCancelCar.Enabled = false;
            }

            if (String.IsNullOrWhiteSpace(workingCar.PrimaryKey))
            {
                btnDeleteCar.Enabled = false;
            }
            else
            {
                btnDeleteCar.Enabled = true;
            }
        }

        private void SetSelected(string primaryKey)
        {
            if (!String.IsNullOrWhiteSpace(primaryKey))
            {
                for (int i = 0; i < tvOwnedCars.Nodes.Count; i++)
                {
                    Car car = (Car)tvOwnedCars.Nodes[i].Tag;
                    if (car.PrimaryKey == primaryKey)
                    {
                        tvOwnedCars.SelectedNode = tvOwnedCars.Nodes[i];
                    }
                }
            }
        }

        private void SetToWorkingCar()
        {
            txtNameCar.Text = workingCar.Name;
            cmbManufacturer.SelectedValue = workingCar.ManufacturerKey;
            nudYear.Value = workingCar.Year;
            cmbCategory.SelectedItem = workingCar.Category;
            nudPrice.Value = (decimal)workingCar.Price;
            txtDisplacementCC.Text = workingCar.DisplacementCC;
            nudMaxPower.Value = workingCar.MaxPower;
            txtPowerRPM.Text = workingCar.PowerRPM;
            nudTorqueFTLB.Value = (decimal)workingCar.TorqueFTLB;
            txtTorqueRPM.Text = workingCar.TorqueRPM;
            cmbDrivetrain.SelectedItem = workingCar.DriveTrain;
            cmbAspiration.SelectedItem = workingCar.Aspiration;
            nudLength.Value = (decimal)workingCar.Length;
            nudWidth.Value = (decimal)workingCar.Width;
            nudHeight.Value = (decimal)workingCar.Height;
            nudWeight.Value = (decimal)workingCar.Weight;
            nudMaxSpeed.Value = (decimal)workingCar.MaxSpeed;
            nudAcceleration.Value = (decimal)workingCar.Acceleration;
            nudBraking.Value = (decimal)workingCar.Braking;
            nudCornering.Value = (decimal)workingCar.Cornering;
            nudStability.Value = (decimal)workingCar.Stability;
        }

        private void tvOwnedCars_AfterSelect(object sender, TreeViewEventArgs e)
        {
            workingCar = (Car)tvOwnedCars.SelectedNode.Tag;

            SetToWorkingCar();

            SetButtons();
        }

        private void txtDisplacementCC_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void txtNameCar_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void txtPowerRPM_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void txtTorqueRPM_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void UpdateAspirationList()
        {
            cmbAspiration.DataSource = Aspiration.Aspirations;
        }

        private void UpdateCategoryList()
        {
            cmbCategory.DisplayMember = "Description";
            cmbCategory.DataSource = CarCategory.categories;
        }

        private void UpdateDrivetrainList()
        {
            cmbDrivetrain.DataSource = DriveTrain.DriveTrains;
        }

        private void UpdateList()
        {
            List<Car> cars = carsService.GetList(true);

            tvOwnedCars.BeginUpdate();
            tvOwnedCars.Nodes.Clear();

            foreach (Car car in cars)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Tag = car;
                treeNode.Text = car.Name;

                tvOwnedCars.Nodes.Add(treeNode);
            }

            tvOwnedCars.EndUpdate();
        }

        private void UpdateManufacturerList()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();

            manufacturers.Add(emptyManufacturer);
            manufacturers.AddRange(manufacturersService.GetList(orderedList: true));

            cmbManufacturer.ValueMember = "PrimaryKey";
            cmbManufacturer.DisplayMember = "Name";
            cmbManufacturer.DataSource = manufacturers;
        }

        private void UpdateWorkingCar()
        {
            workingCar.Name = txtNameCar.Text;
            workingCar.ManufacturerKey = (string)cmbManufacturer.SelectedValue;
            workingCar.Year = Decimal.ToInt32(nudYear.Value);
            workingCar.Category = (CarCategory.Category)cmbCategory.SelectedItem;
            workingCar.Price = Decimal.ToDouble(nudPrice.Value);
            workingCar.DisplacementCC = txtDisplacementCC.Text;
            workingCar.MaxPower = Decimal.ToInt32(nudMaxPower.Value);
            workingCar.PowerRPM = txtPowerRPM.Text;
            workingCar.TorqueFTLB = Decimal.ToDouble(nudTorqueFTLB.Value);
            workingCar.TorqueRPM = txtTorqueRPM.Text;
            workingCar.DriveTrain = cmbDrivetrain.SelectedItem.ToString();
            workingCar.Aspiration = cmbAspiration.SelectedItem.ToString();
            workingCar.Length = Decimal.ToDouble(nudLength.Value);
            workingCar.Width = Decimal.ToDouble(nudWidth.Value);
            workingCar.Height = Decimal.ToDouble(nudHeight.Value);
            workingCar.Weight = Decimal.ToDouble(nudWeight.Value);
            workingCar.MaxSpeed = Decimal.ToDouble(nudMaxSpeed.Value);
            workingCar.Acceleration = Decimal.ToDouble(nudAcceleration.Value);
            workingCar.Braking = Decimal.ToDouble(nudBraking.Value);
            workingCar.Cornering = Decimal.ToDouble(nudCornering.Value);
            workingCar.Stability = Decimal.ToDouble(nudStability.Value);
        }
    }
}