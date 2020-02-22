using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.OwnerCars;
using GTSport_DT.Owners;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Cars.CarsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.OwnerCars.OwnerCarsForTesting;
using static GTSport_DT_Testing.Owners.OwnersForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.OwnerCars
{
    [TestClass]
    public class OwnerCarsServiceTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;
        private static OwnersRepository ownersRepository;
        private static OwnerCarsRepository ownerCarsRepository;
        private static OwnerCarsService ownerCarsService;

        private static string ownerCar4Key = "";

        private readonly string ownerCar4OwnerKey = Owner3.PrimaryKey;
        private readonly string ownerCar4CarKey = Car4.PrimaryKey;
        private readonly string ownerCar4CarID = Car4.Name + "_TESTING";
        private readonly int ownerCar4MaxPower = Car4.MaxPower;
        private readonly DateTime ownerCar4AcquiredDate = new DateTime(2017, 08, 20);

        private const string badOwnerCarKey = "X!X999999999";
        private const string badCarID = "X!XBADIDX!X";
        private const string ownerCar4PaintJob = "White";
        private const string newCarID = "Testing owner car 4 update";
        private const string expectedMaxKey = "OWC900000004";
        private const int ownerCar4PowerLevel = 0;
        private const int ownerCar4WeightReductionLevel = 1;
        private const int numberOfOwnerCars = 3;
        private const int numberOfOwnerCarsForOwner = 2;
        private const int numberOfOwnerCarsForCar = 1;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            carsRepository = new CarsRepository(con);
            ownersRepository = new OwnersRepository(con);
            ownerCarsRepository = new OwnerCarsRepository(con);
            ownerCarsService = new OwnerCarsService(con);

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
                ownerCarsRepository.Delete(ownerCar4Key);
                ownerCarsRepository.Flush();

                ownersRepository.Refresh();
                ownersRepository.Delete(Owner1.PrimaryKey);
                ownersRepository.Delete(Owner2.PrimaryKey);
                ownersRepository.Delete(Owner3.PrimaryKey);
                ownersRepository.Delete(ownerCar4Key);
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
        public void A010_GetByKey()
        {
            OwnerCar ownerCar = ownerCarsService.GetByKey(OwnerCar2.PrimaryKey);

            Assert.AreEqual(OwnerCar2.CarID, ownerCar.CarID);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerCarNotFoundException))]
        public void A020_GetByKeyBadKey()
        {
            try
            {
                OwnerCar ownerCar = ownerCarsService.GetByKey(badOwnerCarKey);
            }
            catch (OwnerCarNotFoundException ocfe)
            {
                Assert.AreEqual(OwnerCarNotFoundException.OwnerCarKeyNotFoundMsg, ocfe.Message);
                throw ocfe;
            }
        }
        
        [TestMethod]
        public void A030_GetByCarID()
        {
            OwnerCar ownerCar = ownerCarsService.GetByCarID(OwnerCar2.CarID);

            Assert.AreEqual(OwnerCar2.PrimaryKey, ownerCar.PrimaryKey);
        }
        
        [TestMethod]
        [ExpectedException(typeof(OwnerCarNotFoundException))]
        public void A040_GetByCarIDBadID()
        {
            try
            {
                OwnerCar ownerCar = ownerCarsService.GetByCarID(badCarID);
            }
            catch (OwnerCarNotFoundException ocnfe)
            {
                Assert.AreEqual(OwnerCarNotFoundException.OwnerCarIDNotFoundMsg, ocnfe.Message);
                throw ocnfe;
            }
        }

        [TestMethod]
        public void A050_SaveNewOwnerCar()
        {
            OwnerCar ownerCar = new OwnerCar("", ownerCar4OwnerKey, ownerCar4CarKey, ownerCar4CarID, ownerCar4PaintJob,
                ownerCar4MaxPower, ownerCar4PowerLevel, ownerCar4WeightReductionLevel, ownerCar4AcquiredDate);

            ownerCarsService.Save(ref ownerCar);

            ownerCar4Key = ownerCar.PrimaryKey;
        }

        [TestMethod]
        public void A060_SaveUpdateOwnerCar()
        {
            OwnerCar ownerCar = new OwnerCar(ownerCar4Key, ownerCar4OwnerKey, ownerCar4CarKey, newCarID, ownerCar4PaintJob,
                ownerCar4MaxPower, ownerCar4PowerLevel, ownerCar4WeightReductionLevel, ownerCar4AcquiredDate);

            ownerCarsService.Save(ref ownerCar);
        }

        [TestMethod]
        public void A070_DeleteOwnerCar()
        {
            ownerCarsService.Delete(ownerCar4Key);
        }

        [TestMethod]
        public void A080_GetList()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetList();

            Assert.AreEqual(numberOfOwnerCars, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A090_GetListOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetList(orderedList: true);

            Assert.AreEqual(numberOfOwnerCars, ownerCars.Count);
            Assert.AreEqual(OwnerCar3.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListForOwnerKey()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetListForOwnerKey(Owner1.PrimaryKey);

            Assert.AreEqual(numberOfOwnerCarsForOwner, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForOwnerKeyOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetListForOwnerKey(Owner1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfOwnerCarsForOwner, ownerCars.Count);
            Assert.AreEqual(OwnerCar3.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_GetListForCarKey()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetListForCarKey(Car1.PrimaryKey);

            Assert.AreEqual(numberOfOwnerCarsForCar, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A130_GetListForCarKeyOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsService.GetListForCarKey(Car1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfOwnerCarsForCar, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A140_ResetKey()
        {
            ownerCarsService.ResetKey();

            OwnerCar ownerCar = new OwnerCar("", ownerCar4OwnerKey, ownerCar4CarKey, ownerCar4CarID, ownerCar4PaintJob,
                ownerCar4MaxPower, ownerCar4PowerLevel, ownerCar4WeightReductionLevel, ownerCar4AcquiredDate);

            ownerCarsService.Save(ref ownerCar);

            ownerCar4Key = ownerCar.PrimaryKey;

            Assert.AreEqual(expectedMaxKey, ownerCar4Key);
        }
    }
}
