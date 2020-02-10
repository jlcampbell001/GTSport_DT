using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Cars.CarsForTesting;


namespace GTSport_DT_Testing.Cars
{
    [TestClass]
    public class CarValidationTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;
        private static CarValidation carValidation;

        private static readonly string carXXName = Car9.Name;
        private static readonly string carXXManufacturerKey = Manufacturer1.PrimaryKey;
        private static readonly CarCategory.Category badCategory = new CarCategory.Category("XX", "XXX");

        private const string carXXKey = "XXX900000004";
        private const string badDriveTrain = "XX";
        private const string badAspiration = "XX";
        private const string badManufacturerKey = "!!!XXX!!!";
        private const string badCarKey = "!!!XXX!!!";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            carsRepository = new CarsRepository(con);
            carValidation = new CarValidation(con);

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
        public void A010_ValidateDriveTrain()
        {
            carValidation.ValidateDriveTrain(DriveTrain.MR);
        }

        [TestMethod]
        [ExpectedException(typeof(CarDriveTrainNotValidException))]
        public void A020_ValidateDriveTrainBadDriveTrain()
        {
            try
            {
                carValidation.ValidateDriveTrain(badDriveTrain);
            } catch (CarDriveTrainNotValidException cdtnve)
            {
                Assert.AreEqual(CarDriveTrainNotValidException.CarDriveTrainNotValidMsg, cdtnve.Message);
                throw cdtnve;
            }
        }

        [TestMethod]
        public void A030_ValidateAspiration()
        {
            carValidation.ValidateAspiration(Aspiration.SC);
        }

        [TestMethod]
        [ExpectedException(typeof(CarAspirationNotValidException))]
        public void A040_ValidateAspiratioBadAspiration()
        {
            try
            {
                carValidation.ValidateAspiration(badAspiration);
            } catch (CarAspirationNotValidException canve)
            {
                Assert.AreEqual(CarAspirationNotValidException.CarAspirationNotValidMsg, canve.Message);
                throw canve;
            }
        }

        [TestMethod]
        public void A050_ValidateCategory()
        {
            carValidation.ValidateCategory(CarCategory.GRX);
        }

        [TestMethod]
        [ExpectedException(typeof(CarCategoryNotValidException))]
        public void A060_ValidateCategoryBadCategory()
        {
            try
            {
                carValidation.ValidateCategory(badCategory);
            } catch (CarCategoryNotValidException ccnve)
            {
                Assert.AreEqual(CarCategoryNotValidException.CarCategoryNotValidMsg, ccnve.Message);
                throw ccnve;
            }
        }

        [TestMethod]
        public void A070_ValidateSave()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = Manufacturer1.PrimaryKey;
            car.Category = CarCategory.GRB;
            car.Name = Car1.Name;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            carValidation.ValidateSave(car);
        }

        [TestMethod]
        [ExpectedException(typeof(CarNameNotSetException))]
        public void A080_ValidateSaveNameNotSet()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = Manufacturer1.PrimaryKey;
            car.Category = CarCategory.GRB;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            } catch (CarNameNotSetException cnnse)
            {
                Assert.AreEqual(CarNameNotSetException.CarNameNotSetMsg, cnnse.Message);
                throw cnnse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarManufacturerKeyNotSetException))]
        public void A090_ValidateSaveManufacturerKeyNotSet()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.Category = CarCategory.GRB;
            car.Name = Car1.Name;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (CarManufacturerKeyNotSetException cmknse)
            {
                Assert.AreEqual(CarManufacturerKeyNotSetException.CarManufacturerKeyNotSetMsg, cmknse.Message);
                throw cmknse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarDriveTrainNotValidException))]
        public void A100_ValidateSaveDriveTrainNotValid()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = Manufacturer1.PrimaryKey;
            car.Category = CarCategory.GRB;
            car.Name = Car1.Name;
            car.DriveTrain = badDriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (CarDriveTrainNotValidException cdtnve)
            {
                Assert.AreEqual(CarDriveTrainNotValidException.CarDriveTrainNotValidMsg, cdtnve.Message);
                throw cdtnve;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarAspirationNotValidException))]
        public void A110_ValidateSaveAspirationNotValid()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = Manufacturer1.PrimaryKey;
            car.Category = CarCategory.GRB;
            car.Name = Car1.Name;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = badAspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (CarAspirationNotValidException canve)
            {
                Assert.AreEqual(CarAspirationNotValidException.CarAspirationNotValidMsg, canve.Message);
                throw canve;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarCategoryNotValidException))]
        public void A120_ValidateSaveCategoryNotValid()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = Manufacturer1.PrimaryKey;
            car.Category = badCategory;
            car.Name = Car1.Name;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (CarCategoryNotValidException ccnve)
            {
                Assert.AreEqual(CarCategoryNotValidException.CarCategoryNotValidMsg, ccnve.Message);
                throw ccnve;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarNameAlreadyExistsException))]
        public void A130_ValidateSaveNameAlreadyExists()
        {
            Car car = new Car();
            car.PrimaryKey = carXXKey;
            car.ManufacturerKey = carXXManufacturerKey;
            car.Category = CarCategory.GRB;
            car.Name = carXXName;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (CarNameAlreadyExistsException cnaee)
            {
                Assert.AreEqual(CarNameAlreadyExistsException.CarNameAlreadyExistsMsg, cnaee.Message);
                throw cnaee;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNotFoundException))]
        public void A140_ValidateSaveBadManufcatureKey()
        {
            Car car = new Car();
            car.PrimaryKey = Car1.PrimaryKey;
            car.ManufacturerKey = badManufacturerKey;
            car.Category = CarCategory.GRB;
            car.Name = Car1.Name;
            car.DriveTrain = Car1.DriveTrain;
            car.Aspiration = Car1.Aspiration;

            try
            {
                carValidation.ValidateSave(car);
            }
            catch (ManufacturerNotFoundException mnfe)
            {
                Assert.AreEqual(ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg, mnfe.Message);
                throw mnfe;
            }
        }

        [TestMethod]
        public void A150_ValidateDelete()
        {
            carValidation.ValidateDelete(Car1.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(CarNotFoundExcpetion))]
        public void A160_ValidateDeleteBadCarKey()
        {
            try
            {
                carValidation.ValidateDelete(badCarKey);
            } catch (CarNotFoundExcpetion cnfe)
            {
                Assert.AreEqual(CarNotFoundExcpetion.CarKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A170_ValidateDeleteCarkeyInUse()
        {

        }

        [TestMethod]
        public void A180_ValidateSearchCritera()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = CarCategory.N300;
            carSearchCriteria.CategoryTo = CarCategory.N600;
            carSearchCriteria.DriveTrain = DriveTrain.FourWD;

            carValidation.ValidateSearchCriteria(carSearchCriteria);
        }

        [TestMethod]
        [ExpectedException(typeof(CarCategoryNotValidException))]
        public void A190_ValidateSearchCriteraBadFromCategory()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = badCategory;
            carSearchCriteria.CategoryTo = CarCategory.N600;
            carSearchCriteria.DriveTrain = DriveTrain.FourWD;

            try
            {
                carValidation.ValidateSearchCriteria(carSearchCriteria);
            } catch (CarCategoryNotValidException ccnve)
            {
                Assert.AreEqual(CarCategoryNotValidException.CarCategoryNotValidMsg, ccnve.Message);
                throw ccnve;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarCategoryNotValidException))]
        public void A200_ValidateSearchCriteraBadToCategory()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = CarCategory.N300;
            carSearchCriteria.CategoryTo = badCategory;
            carSearchCriteria.DriveTrain = DriveTrain.FourWD;

            try
            {
                carValidation.ValidateSearchCriteria(carSearchCriteria);
            }
            catch (CarCategoryNotValidException ccnve)
            {
                Assert.AreEqual(CarCategoryNotValidException.CarCategoryNotValidMsg, ccnve.Message);
                throw ccnve;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarDriveTrainNotValidException))]
        public void A210_ValidateSearchCriteraBadDriveTrain()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = CarCategory.N300;
            carSearchCriteria.CategoryTo = CarCategory.N600;
            carSearchCriteria.DriveTrain = badDriveTrain;

            try
            {
                carValidation.ValidateSearchCriteria(carSearchCriteria);
            }
            catch (CarDriveTrainNotValidException cdtnve)
            {
                Assert.AreEqual(CarDriveTrainNotValidException.CarDriveTrainNotValidMsg, cdtnve.Message);
                throw cdtnve;
            }
        }

    }
}
