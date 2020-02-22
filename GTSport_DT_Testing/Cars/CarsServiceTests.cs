using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Cars.CarsForTesting;
using GTSport_DT_Testing.General;
using Npgsql;

namespace GTSport_DT_Testing.Cars
{
    [TestClass]
    public class CarsServiceTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;
        private static CarsService carsService;

        private static string carXXKey = "";

        private readonly string carXXManufacturerKey = Manufacturer3.PrimaryKey;
        private readonly string carXXDriveTrain = DriveTrain.FR;
        private readonly string carXXAspiration = Aspiration.NA;
        private readonly string driveTrainTest = DriveTrain.FF;
        private readonly string manufacturerName = Manufacturer2.Name;
        private readonly string countryDescription = Country2.Description;
        private readonly string regionDescription = Region2.Description;
        private readonly CarCategory.Category carXXCategory = CarCategory.N400;
        private readonly CarCategory.Category categoryMaxRange = CarCategory.N700;
        private readonly CarCategory.Category categoryMinRange = CarCategory.N100;

        private const string carXXName = "Mustang Mach 1 '71";
        private const string carXXDisplacementCC = "5752";
        private const string carXXPowerRPM = "";
        private const string carXXTorqueRPM = "";
        private const string carXXNewName = "Mustang Mach 1 1971";
        private const string badCarKey = "xx!!!xx";
        private const string badCarName = "GTR";
        private const string expectedMaxKey = "CAR900000016";
        private const int carXXYear = 1971;
        private const int carXXMaxPower = 299;
        private const int numberOfCars = 15;
        private const int numberOfCarsForManufacturer = 2;
        private const int yearMinRange = 1972;
        private const int yearMaxRange = 2020;
        private const int maxPowerMinRange = 100;
        private const int maxPowerMaxRange = 900;
        private const int numberOfCriteriaRecords = 1;
        private const double carXXPrice = 50000.00;
        private const double carXXTorqueFTLB = 0.00;
        private const double carXXLength = 189.50;
        private const double carXXWidth = 74.10;
        private const double carXXHeight = 50.10;
        private const double carXXWeight = 1615;
        private const double carXXMaxSpeed = 7.2;
        private const double carXXAcceleration = 5.0;
        private const double carXXBreaking = 1.8;
        private const double carXXCornering = 1.6;
        private const double carXXStability = 5.0;


        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            carsRepository = new CarsRepository(con);
            carsService = new CarsService(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
            regionsRepository.Flush();

            countriesRepository.Save(Country1);
            countriesRepository.Save(Country2);
            countriesRepository.Save(Country3);
            countriesRepository.Save(Country4);
            countriesRepository.Save(Country5);
            countriesRepository.Flush();

            manufacturersRepository.Save(Manufacturer1);
            manufacturersRepository.Save(Manufacturer2);
            manufacturersRepository.Save(Manufacturer3);
            manufacturersRepository.Save(Manufacturer4);
            manufacturersRepository.Save(Manufacturer5);
            manufacturersRepository.Save(Manufacturer6);
            manufacturersRepository.Save(Manufacturer7);
            manufacturersRepository.Save(Manufacturer8);
            manufacturersRepository.Save(Manufacturer9);
            manufacturersRepository.Flush();

            carsRepository.Save(Car1);
            carsRepository.Save(Car2);
            carsRepository.Save(Car3);
            carsRepository.Save(Car4);
            carsRepository.Save(Car5);
            carsRepository.Save(Car6);
            carsRepository.Save(Car7);
            carsRepository.Save(Car8);
            carsRepository.Save(Car9);
            carsRepository.Save(Car10);
            carsRepository.Save(Car11);
            carsRepository.Save(Car12);
            carsRepository.Save(Car13);
            carsRepository.Save(Car14);
            carsRepository.Save(Car15);
            carsRepository.Flush();
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                carsRepository.Refresh();
                carsRepository.Delete(Car1.PrimaryKey);
                carsRepository.Delete(Car2.PrimaryKey);
                carsRepository.Delete(Car3.PrimaryKey);
                carsRepository.Delete(Car4.PrimaryKey);
                carsRepository.Delete(Car5.PrimaryKey);
                carsRepository.Delete(Car6.PrimaryKey);
                carsRepository.Delete(Car7.PrimaryKey);
                carsRepository.Delete(Car8.PrimaryKey);
                carsRepository.Delete(Car9.PrimaryKey);
                carsRepository.Delete(Car10.PrimaryKey);
                carsRepository.Delete(Car11.PrimaryKey);
                carsRepository.Delete(Car12.PrimaryKey);
                carsRepository.Delete(Car13.PrimaryKey);
                carsRepository.Delete(Car14.PrimaryKey);
                carsRepository.Delete(Car15.PrimaryKey);
                carsRepository.Delete(carXXKey);
                carsRepository.Flush();

                manufacturersRepository.Refresh();
                manufacturersRepository.Delete(Manufacturer1.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer2.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer3.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer4.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer5.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer6.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer7.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer8.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer9.PrimaryKey);
                manufacturersRepository.Flush();

                countriesRepository.Refresh();
                countriesRepository.Delete(Country1.PrimaryKey);
                countriesRepository.Delete(Country2.PrimaryKey);
                countriesRepository.Delete(Country3.PrimaryKey);
                countriesRepository.Delete(Country4.PrimaryKey);
                countriesRepository.Delete(Country5.PrimaryKey);
                countriesRepository.Flush();

                regionsRepository.Refresh();
                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);
                regionsRepository.Flush();

                con.Close();
            }
        }

        [TestMethod]
        public void A010_GetByKey()
        {
            Car car = carsService.GetByKey(Car2.PrimaryKey);

            Assert.AreEqual(Car2.Name, car.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(CarNotFoundException))]
        public void A020_GetByKeyBadKey()
        {
            try
            {
                Car car = carsService.GetByKey(badCarKey);
            } catch (CarNotFoundException cnfe)
            {
                Assert.AreEqual(CarNotFoundException.CarKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A030_GetByName()
        {
            Car car = carsService.GetByName(Car3.Name);

            Assert.AreEqual(Car3.PrimaryKey, car.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(CarNotFoundException))]
        public void A040_GetByNameBadName()
        {
            try
            {
                Car car = carsService.GetByName(badCarName);
            }
            catch (CarNotFoundException cnfe)
            {
                Assert.AreEqual(CarNotFoundException.CarNameNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A050_SaveNewCar()
        {
            Car car = new Car("", carXXName, carXXManufacturerKey, carXXYear, carXXCategory,
                carXXPrice, carXXDisplacementCC, carXXMaxPower, carXXPowerRPM, carXXTorqueFTLB, carXXTorqueRPM,
                carXXDriveTrain, carXXAspiration, carXXLength, carXXWidth, carXXHeight, carXXWeight, carXXMaxSpeed,
                carXXAcceleration, carXXBreaking, carXXCornering, carXXStability);

            carsService.Save(ref car);

            carXXKey = car.PrimaryKey;
        }

        [TestMethod]
        public void A060_SaveChangedCar()
        {
            Car car = new Car(carXXKey, carXXNewName, carXXManufacturerKey, carXXYear, carXXCategory,
                carXXPrice, carXXDisplacementCC, carXXMaxPower, carXXPowerRPM, carXXTorqueFTLB, carXXTorqueRPM,
                carXXDriveTrain, carXXAspiration, carXXLength, carXXWidth, carXXHeight, carXXWeight, carXXMaxSpeed,
                carXXAcceleration, carXXBreaking, carXXCornering, carXXStability);

            carsService.Save(ref car);
        }

        [TestMethod]
        public void A070_DeleteCar()
        {
            carsService.Delete(carXXKey);
        }

        [TestMethod]
        public void A080_GetList()
        {
            List<Car> cars = carsService.GetList();

            Assert.AreEqual(numberOfCars, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A090_GetListOrdered()
        {
            List<Car> cars = carsService.GetList(orderedList: true);

            Assert.AreEqual(numberOfCars, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListForManufacturerKey()
        {
            List<Car> cars = carsService.GetListForManufacturerKey(Manufacturer1.PrimaryKey);

            Assert.AreEqual(numberOfCarsForManufacturer, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForManufacturerKeyOrdered()
        {
            List<Car> cars = carsService.GetListForManufacturerKey(Manufacturer1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfCarsForManufacturer, cars.Count);
            Assert.AreEqual(Car10.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_GetListForSearchCritera()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = yearMaxRange;
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;
            carSearchCriteria.DriveTrain = driveTrainTest;
            carSearchCriteria.ManufacturerName = manufacturerName;
            carSearchCriteria.CountryDescription = countryDescription;
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsService.GetListForSearchCriteria(carSearchCriteria);
            
            Assert.AreEqual(numberOfCriteriaRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A130_GetListForSearchCriteraOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = yearMaxRange;
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;
            carSearchCriteria.DriveTrain = driveTrainTest;
            carSearchCriteria.ManufacturerName = manufacturerName;
            carSearchCriteria.CountryDescription = countryDescription;
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsService.GetListForSearchCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCriteriaRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A140_ResetKey()
        {
            carsService.ResetKey();

            Car car = new Car("", carXXName, carXXManufacturerKey, carXXYear, carXXCategory,
                carXXPrice, carXXDisplacementCC, carXXMaxPower, carXXPowerRPM, carXXTorqueFTLB, carXXTorqueRPM,
                carXXDriveTrain, carXXAspiration, carXXLength, carXXWidth, carXXHeight, carXXWeight, carXXMaxSpeed,
                carXXAcceleration, carXXBreaking, carXXCornering, carXXStability);

            carsService.Save(ref car);

            carXXKey = car.PrimaryKey;

            Assert.AreEqual(expectedMaxKey, carXXKey);
        }
    }
}
