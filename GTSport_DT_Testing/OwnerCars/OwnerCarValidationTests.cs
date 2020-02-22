using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.OwnerCars;
using GTSport_DT.Owners;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Cars.CarsForTesting;
using static GTSport_DT_Testing.OwnerCars.OwnerCarsForTesting;
using static GTSport_DT_Testing.Owners.OwnersForTesting;
using Npgsql;

namespace GTSport_DT_Testing.OwnerCars
{
    [TestClass]
    public class OwnerCarValidationTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;
        private static OwnerCarValidation ownerCarValidation;
        private static OwnerCarsRepository ownerCarsRepository;
        private static OwnersRepository ownersRepository;

        private const string badOwnerCarKey = "X!X999999999";
        private const string badOwnerKey = "X!X999999999";
        private const string badCarKey = "X!X999999999";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            carsRepository = new CarsRepository(con);
            ownerCarValidation = new OwnerCarValidation(con);
            ownerCarsRepository = new OwnerCarsRepository(con);
            ownersRepository = new OwnersRepository(con);

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

            ownersRepository.Save(Owner1);
            ownersRepository.Save(Owner2);
            ownersRepository.Save(Owner3);
            ownersRepository.Flush();

            ownerCarsRepository.Save(OwnerCar1);
            ownerCarsRepository.Save(OwnerCar2);
            ownerCarsRepository.Save(OwnerCar3);
            ownerCarsRepository.Flush();
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                ownerCarsRepository.Refresh();
                ownerCarsRepository.Delete(OwnerCar1.PrimaryKey);
                ownerCarsRepository.Delete(OwnerCar2.PrimaryKey);
                ownerCarsRepository.Delete(OwnerCar3.PrimaryKey);
                ownerCarsRepository.Flush();

                ownersRepository.Refresh();
                ownersRepository.Delete(Owner1.PrimaryKey);
                ownersRepository.Delete(Owner2.PrimaryKey);
                ownersRepository.Delete(Owner3.PrimaryKey);
                ownersRepository.Flush();

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
        public void A010_ValidateSave()
        {
            ownerCarValidation.ValidateSave(OwnerCar1);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerCarIDNotSetException))]
        public void A020_ValidateSaveCarIDNotSet()
        {
            OwnerCar ownerCar = new OwnerCar();

            ownerCar.PrimaryKey = OwnerCar1.PrimaryKey;
            ownerCar.OwnerKey = OwnerCar1.OwnerKey;
            ownerCar.CarKey = OwnerCar1.CarKey;
            ownerCar.CarID = "";
            ownerCar.PaintJob = OwnerCar1.PaintJob;
            ownerCar.MaxPower = OwnerCar1.MaxPower;
            ownerCar.PowerLevel = OwnerCar1.PowerLevel;
            ownerCar.WeightReductionLevel = OwnerCar1.WeightReductionLevel;
            ownerCar.AcquiredDate = OwnerCar1.AcquiredDate;

            try
            {
                ownerCarValidation.ValidateSave(ownerCar);
            } catch (OwnerCarIDNotSetException ocinse)
            {
                Assert.AreEqual(OwnerCarIDNotSetException.OwnerCarIDNotSetMsg, ocinse.Message);
                throw ocinse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerCarOwnerKeyNotSetException))]
        public void A030_ValidateSaveCarOwnerKeyNotSet()
        {
            OwnerCar ownerCar = new OwnerCar();

            ownerCar.PrimaryKey = OwnerCar1.PrimaryKey;
            ownerCar.OwnerKey = "";
            ownerCar.CarKey = OwnerCar1.CarKey;
            ownerCar.CarID = OwnerCar1.CarID;
            ownerCar.PaintJob = OwnerCar1.PaintJob;
            ownerCar.MaxPower = OwnerCar1.MaxPower;
            ownerCar.PowerLevel = OwnerCar1.PowerLevel;
            ownerCar.WeightReductionLevel = OwnerCar1.WeightReductionLevel;
            ownerCar.AcquiredDate = OwnerCar1.AcquiredDate;

            try
            {
                ownerCarValidation.ValidateSave(ownerCar);
            }
            catch (OwnerCarOwnerKeyNotSetException ocoknse)
            {
                Assert.AreEqual(OwnerCarOwnerKeyNotSetException.OwnerCarOwnerKeyNotSetMsg, ocoknse.Message);
                throw ocoknse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerCarCarKeyNotSetException))]
        public void A040_ValidateSaveCarCarKeyNotSet()
        {
            OwnerCar ownerCar = new OwnerCar();

            ownerCar.PrimaryKey = OwnerCar1.PrimaryKey;
            ownerCar.OwnerKey = OwnerCar1.OwnerKey;
            ownerCar.CarKey = "";
            ownerCar.CarID = OwnerCar1.CarID;
            ownerCar.PaintJob = OwnerCar1.PaintJob;
            ownerCar.MaxPower = OwnerCar1.MaxPower;
            ownerCar.PowerLevel = OwnerCar1.PowerLevel;
            ownerCar.WeightReductionLevel = OwnerCar1.WeightReductionLevel;
            ownerCar.AcquiredDate = OwnerCar1.AcquiredDate;

            try
            {
                ownerCarValidation.ValidateSave(ownerCar);
            }
            catch (OwnerCarCarKeyNotSetException occknse)
            {
                Assert.AreEqual(OwnerCarCarKeyNotSetException.OwnerCarCarKeyNotSetMsg, occknse.Message);
                throw occknse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNotFoundException))]
        public void A050_ValidateSaveCarBadOwnerKey()
        {
            OwnerCar ownerCar = new OwnerCar();

            ownerCar.PrimaryKey = OwnerCar1.PrimaryKey;
            ownerCar.OwnerKey = badOwnerKey;
            ownerCar.CarKey = OwnerCar1.CarKey;
            ownerCar.CarID = OwnerCar1.CarID;
            ownerCar.PaintJob = OwnerCar1.PaintJob;
            ownerCar.MaxPower = OwnerCar1.MaxPower;
            ownerCar.PowerLevel = OwnerCar1.PowerLevel;
            ownerCar.WeightReductionLevel = OwnerCar1.WeightReductionLevel;
            ownerCar.AcquiredDate = OwnerCar1.AcquiredDate;

            try
            {
                ownerCarValidation.ValidateSave(ownerCar);
            }
            catch (OwnerNotFoundException onfe)
            {
                Assert.AreEqual(OwnerNotFoundException.OwnerKeyNotFoundMsg, onfe.Message);
                throw onfe;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CarNotFoundException))]
        public void A060_ValidateSaveCarBadCarKey()
        {
            OwnerCar ownerCar = new OwnerCar();

            ownerCar.PrimaryKey = OwnerCar1.PrimaryKey;
            ownerCar.OwnerKey = OwnerCar1.OwnerKey;
            ownerCar.CarKey = badCarKey;
            ownerCar.CarID = OwnerCar1.CarID;
            ownerCar.PaintJob = OwnerCar1.PaintJob;
            ownerCar.MaxPower = OwnerCar1.MaxPower;
            ownerCar.PowerLevel = OwnerCar1.PowerLevel;
            ownerCar.WeightReductionLevel = OwnerCar1.WeightReductionLevel;
            ownerCar.AcquiredDate = OwnerCar1.AcquiredDate;

            try
            {
                ownerCarValidation.ValidateSave(ownerCar);
            }
            catch (CarNotFoundException cnfe)
            {
                Assert.AreEqual(CarNotFoundException.CarKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A070_ValidateDelete()
        {
            ownerCarValidation.ValidateDelete(OwnerCar1.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerCarNotFoundException))]
        public void A080_ValidateDeleteBadKey()
        {
            try
            {
                ownerCarValidation.ValidateDelete(badOwnerCarKey);
            } catch (OwnerCarNotFoundException ocnfe)
            {
                Assert.AreEqual(OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg, ocnfe.Message);
                throw ocnfe; 
            }
        }
    }
}
