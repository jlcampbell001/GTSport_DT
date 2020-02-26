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
        private OwnerCarsService ownerCarsService;
        private NpgsqlConnection con;
        private ManufacturersService manufacturersService;
        private Car workingCar = new Car();
        private OwnerCar workingOwnerCar = new OwnerCar("", "", "", "", "", 0, 0, 0, DateTime.Today);
        private CarSearchCriteria carSearchCriteria = new CarSearchCriteria();

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
            ownerCarsService = new OwnerCarsService(con);

            //AddCarTestData();

            InitializeComponent();
        }

        /// <summary>Updates form with changes from other forms.</summary>
        public void UpdateFromOtherForms()
        {
            UpdateManufacturerList();

            UpdateList();

            SetToWorkingCar();
            SetToWorkingOwnerCar();
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

        private void AddCarTestData()
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

        private void AddOwnerCarTestData()
        {
            Car roadster = carsService.GetByName("Roadster S (ND) '15");
            Car Focus = carsService.GetByName("Focus ST '15");

            string ownerKey = GetCurrentOwnerKey();

            OwnerCar ownerCar = new OwnerCar("", ownerKey, roadster.PrimaryKey, roadster.Name + "_02", "Red", roadster.MaxPower,
                2, 1, DateTime.Today);
            ownerCarsService.Save(ref ownerCar);

            ownerCar = new OwnerCar("", ownerKey, Focus.PrimaryKey, Focus.Name + "_xx", "Blue", Focus.MaxPower,
                1, 0, DateTime.Today.AddDays(-45));
            ownerCarsService.Save(ref ownerCar);

            ownerCar = new OwnerCar("", ownerKey, roadster.PrimaryKey, roadster.Name + "_01", "Silver", roadster.MaxPower,
                0, 0, DateTime.Today.AddDays(-400));
            ownerCarsService.Save(ref ownerCar);
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

                            workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), "", "", "", 0, 0, 0, DateTime.Today);
                            SetToWorkingOwnerCar();
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

            workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), "", "", "", 0, 0, 0, DateTime.Today);

            SetToWorkingOwnerCar();

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

                SetSelected(workingCar.PrimaryKey, "");
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
            Boolean editingCar = false;
            Boolean editingOwnerCar = false;

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
                btnNewCar.Enabled = true;
                btnSaveOwnerCar.Enabled = false;
                btnCancelOwnedCar.Enabled = false;
                btnNewOwnerCar.Enabled = false;

                editingCar = true;
            }
            else if (workingOwnerCar.CarID != txtCarID.Text
                || workingOwnerCar.PaintJob != txtPaintJob.Text
                || workingOwnerCar.AcquiredDate != dtpAcquired.Value
                || workingOwnerCar.MaxPower != nudOwnerCarMaxPower.Value
                || workingOwnerCar.PowerLevel != nudPowerLevel.Value
                || workingOwnerCar.WeightReductionLevel != nudWeightReductionLevel.Value)
            {
                btnSaveCar.Enabled = false;
                btnCancelCar.Enabled = false;
                btnNewCar.Enabled = false;
                btnSaveOwnerCar.Enabled = true;
                btnCancelOwnedCar.Enabled = true;
                btnNewOwnerCar.Enabled = true;

                editingOwnerCar = true;

            }
            else
            {
                btnSaveCar.Enabled = false;
                btnCancelCar.Enabled = false;
                btnNewCar.Enabled = true;
                btnSaveOwnerCar.Enabled = false;
                btnCancelOwnedCar.Enabled = false;
                btnNewOwnerCar.Enabled = true;
            }

            if (String.IsNullOrWhiteSpace(workingCar.PrimaryKey) || editingOwnerCar)
            {
                btnDeleteCar.Enabled = false;
            }
            else
            {
                btnDeleteCar.Enabled = true;
            }

            if (String.IsNullOrWhiteSpace(workingOwnerCar.PrimaryKey) || editingCar)
            {
                btnDeleteOwnerCar.Enabled = false;
            }
            else
            {
                btnDeleteOwnerCar.Enabled = true;
            }
        }

        private void SetSelected(string carKey, string ownerCarKey)
        {
            if (tvOwnedCars.Nodes.Count == 0)
            {
                workingCar = new Car("", "", emptyManufacturer.PrimaryKey, 1900, CarCategory.Empty,
                    0.00, "", 0, "", 0.00, "", DriveTrain.Empty, Aspiration.Empty, 0.00, 0.00, 0.00,
                    0.00, 0.00, 0.0, 0.0, 0.0, 0.0);

                SetToWorkingCar();

                workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), "", "", "", 0, 0, 0, DateTime.Today);

                SetToWorkingOwnerCar();
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(carKey) || !String.IsNullOrWhiteSpace(ownerCarKey))
                {
                    for (int i = 0; i < tvOwnedCars.Nodes.Count; i++)
                    {
                        if (String.IsNullOrWhiteSpace(ownerCarKey))
                        {
                            Car car = (Car)tvOwnedCars.Nodes[i].Tag;
                            if (car.PrimaryKey == carKey)
                            {
                                tvOwnedCars.SelectedNode = tvOwnedCars.Nodes[i];
                            }

                            foreach (TreeNode treeNode in tvOwnedCars.Nodes[i].Nodes)
                            {
                                OwnerCar ownerCar = (OwnerCar)treeNode.Tag;
                                if (ownerCar.PrimaryKey == ownerCarKey)
                                {
                                    tvOwnedCars.SelectedNode = treeNode;
                                }
                            }
                        }
                    }
                }
                else
                {
                    tvOwnedCars.SelectedNode = tvOwnedCars.Nodes[0];
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
            if (tvOwnedCars.SelectedNode.Parent == null)
            {
                workingCar = (Car)tvOwnedCars.SelectedNode.Tag;
                workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), workingCar.PrimaryKey, "", "", 0, 0, 0, DateTime.Today);
            }
            else
            {
                workingCar = (Car)tvOwnedCars.SelectedNode.Parent.Tag;
                workingOwnerCar = (OwnerCar)tvOwnedCars.SelectedNode.Tag;
            }

            SetToWorkingCar();
            SetToWorkingOwnerCar();

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
            // Put it here so the owner key can be grabbed from the parent form.
            //AddOwnerCarTestData();

            List<Car> cars = carsService.GetListForSearchCriteria(carSearchCriteria, true);

            List<OwnerCar> ownerCars = ownerCarsService.GetListForOwnerKey(GetCurrentOwnerKey(), true);

            tvOwnedCars.BeginUpdate();
            tvOwnedCars.Nodes.Clear();

            var searchValue = txtSearchText.Text.ToUpper();

            foreach (Car car in cars)
            {
                if (!String.IsNullOrWhiteSpace(searchValue))
                {
                    var carName = car.Name.ToUpper();

                    if (!carName.Contains(searchValue))
                    {
                        continue;
                    }
                }

                TreeNode treeNode = new TreeNode();
                treeNode.Tag = car;
                treeNode.Text = car.Name;

                AddOwnerCarsToList(ref treeNode, ownerCars);

                tvOwnedCars.Nodes.Add(treeNode);
            }

            tvOwnedCars.EndUpdate();

            SetSelected(workingCar.PrimaryKey, workingOwnerCar.PrimaryKey);
        }

        private void AddOwnerCarsToList(ref TreeNode carNode, List<OwnerCar> ownerCars)
        {
            Car car = (Car)carNode.Tag;

            int ownerCarCount = 0;

            foreach (OwnerCar ownerCar in ownerCars)
            {
                if (ownerCar.CarKey == car.PrimaryKey)
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Tag = ownerCar;
                    treeNode.Text = ownerCar.CarID;

                    carNode.Nodes.Add(treeNode);
                    ownerCarCount++;
                }
            }

            if (ownerCarCount > 0)
            {
                carNode.Text = "(" + ownerCarCount + ") - " + carNode.Text;

            }
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

        private string GetCurrentOwnerKey()
        {
            GTSportForm workingParentForm = (GTSportForm)this.ParentForm;

            return workingParentForm.currentOwner.PrimaryKey;
        }

        private void txtCarID_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void SetToWorkingOwnerCar()
        {
            txtCarID.Text = workingOwnerCar.CarID;
            txtPaintJob.Text = workingOwnerCar.PaintJob;
            dtpAcquired.Value = workingOwnerCar.AcquiredDate;
            nudOwnerCarMaxPower.Value = workingOwnerCar.MaxPower;
            nudPowerLevel.Value = workingOwnerCar.PowerLevel;
            nudWeightReductionLevel.Value = workingOwnerCar.WeightReductionLevel;
        }

        private void UpdateWokringOwnerCar()
        {
            workingOwnerCar.CarID = txtCarID.Text;
            workingOwnerCar.PaintJob = txtPaintJob.Text;
            workingOwnerCar.AcquiredDate = dtpAcquired.Value;
            workingOwnerCar.MaxPower = Decimal.ToInt32(nudOwnerCarMaxPower.Value);
            workingOwnerCar.PowerLevel = Decimal.ToInt32(nudPowerLevel.Value);
            workingOwnerCar.WeightReductionLevel = Decimal.ToInt32(nudWeightReductionLevel.Value);
        }

        private void txtPaintJob_TextChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void dtpAcquired_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudOwnerCarMaxPower_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudWeightReductionLevel_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void nudPowerLevel_ValueChanged(object sender, EventArgs e)
        {
            SetButtons();
        }

        private void btnSaveOwnerCar_Click(object sender, EventArgs e)
        {
            UpdateWokringOwnerCar();

            try
            {
                ownerCarsService.Save(ref workingOwnerCar);

                UpdateList();

                SetSelected(workingCar.PrimaryKey, workingOwnerCar.PrimaryKey);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            txtCarID.Focus();
            SetButtons();
        }

        private void nudMaxPower_Enter(object sender, EventArgs e)
        {
            nudMaxPower.Select(0, nudMaxPower.Text.Length);
        }

        private void nudOwnerCarMaxPower_Enter(object sender, EventArgs e)
        {
            nudOwnerCarMaxPower.Select(0, nudOwnerCarMaxPower.Text.Length);
        }

        private void nudPowerLevel_Enter(object sender, EventArgs e)
        {
            nudPowerLevel.Select(0, nudPowerLevel.Text.Length);
        }

        private void nudWeightReductionLevel_Enter(object sender, EventArgs e)
        {
            nudWeightReductionLevel.Select(0, nudWeightReductionLevel.Text.Length);
        }

        private void btnDeleteOwnerCar_Click(object sender, EventArgs e)
        {
            if (workingOwnerCar.PrimaryKey != null)
            {
                if (MessageBox.Show("Do you wish to delete owned car ID " + workingOwnerCar.CarID + "?", "Delete Owned Car", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        ownerCarsService.Delete(workingOwnerCar.PrimaryKey);

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

                            workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), "", "", "", 0, 0, 0, DateTime.Today);
                            SetToWorkingOwnerCar();
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
                MessageBox.Show("Select an owned car to delete first.", "Error");
            }
        }

        private void btnNewOwnerCar_Click(object sender, EventArgs e)
        {
            workingOwnerCar = new OwnerCar("", GetCurrentOwnerKey(), workingCar.PrimaryKey, workingCar.Name, "",
                workingCar.MaxPower, 0, 0, DateTime.Today);

            SetToWorkingOwnerCar();

            txtCarID.Focus();

            SetButtons();
        }

        private void btnCancelOwnedCar_Click(object sender, EventArgs e)
        {
            SetToWorkingOwnerCar();

            txtCarID.Focus();

            SetButtons();

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            UpdateList();

            txtSearchText.Focus();
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateList();
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            if (tvOwnedCars.Nodes.Count > 0)
            {
                Random random = new Random();
                var randomCar = random.Next(tvOwnedCars.Nodes.Count);

                tvOwnedCars.SelectedNode = tvOwnedCars.Nodes[randomCar];

                tvOwnedCars.Focus();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearchText.Text = "";
            carSearchCriteria = new CarSearchCriteria();

            UpdateList();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            FilterCarsForm filterCarsForm = new FilterCarsForm(carSearchCriteria, con);

            if (filterCarsForm.ShowDialog(this) == DialogResult.OK)
            {
                carSearchCriteria = filterCarsForm.returnCriteria;
                UpdateList();
                tvOwnedCars.Focus();
            }

            filterCarsForm.Dispose();
        }
    }
}