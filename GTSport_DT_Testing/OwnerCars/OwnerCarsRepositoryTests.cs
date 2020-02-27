using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.OwnerCars;
using GTSport_DT.Owners;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System.Collections.Generic;
using static GTSport_DT_Testing.Cars.CarsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.OwnerCars.OwnerCarsForTesting;
using static GTSport_DT_Testing.Owners.OwnersForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.OwnerCars
{
    [TestClass]
    public class OwnerCarsRepositoryTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;
        private static OwnersRepository ownersRepository;
        private static OwnerCarsRepository ownerCarsRepository;

        private static readonly string expectedMaxKey = OwnerCar3.PrimaryKey;

        private const string carIDChange = "NEW ID";
        private const string badCarID = "X!XBAD IDX!X";
        private const int numberOfOwnerCars = 3;
        private const int numberOfOwnerCarsForOwner = 2;
        private const int numberOfOwnerCarsForCar = 1;
        private const int numberOfOwnerCarsStatistics = 2;
        private const int numberOfOwnedCars = 3;
        private const int numberOfUnqiueOwnedCars = 2;

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
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
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
        public void A010_SaveNewTest()
        {
            ownerCarsRepository.SaveAndFlush(OwnerCar1);

            OwnerCar ownerCar = ownerCarsRepository.GetById(OwnerCar1.PrimaryKey);

            Assert.IsNotNull(ownerCar);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCar.PrimaryKey);
            Assert.AreEqual(OwnerCar1.OwnerKey, ownerCar.OwnerKey);
            Assert.AreEqual(OwnerCar1.CarKey, ownerCar.CarKey);
            Assert.AreEqual(OwnerCar1.CarID, ownerCar.CarID);
            Assert.AreEqual(OwnerCar1.PaintJob, ownerCar.PaintJob);
            Assert.AreEqual(OwnerCar1.MaxPower, ownerCar.MaxPower);
            Assert.AreEqual(OwnerCar1.PowerLevel, ownerCar.PowerLevel);
            Assert.AreEqual(OwnerCar1.WeightReductionLevel, ownerCar.WeightReductionLevel);
            Assert.AreEqual(OwnerCar1.AcquiredDate, ownerCar.AcquiredDate);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            OwnerCar ownerCar = new OwnerCar(OwnerCar1.PrimaryKey, OwnerCar1.OwnerKey, OwnerCar1.CarKey, carIDChange,
                OwnerCar1.PaintJob, OwnerCar1.MaxPower, OwnerCar1.PowerLevel, OwnerCar1.WeightReductionLevel, OwnerCar1.AcquiredDate);

            ownerCarsRepository.SaveAndFlush(ownerCar);

            OwnerCar ownerCarCheck = ownerCarsRepository.GetById(OwnerCar1.PrimaryKey);

            Assert.IsNotNull(ownerCarCheck);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCarCheck.PrimaryKey);
            Assert.AreEqual(OwnerCar1.OwnerKey, ownerCarCheck.OwnerKey);
            Assert.AreEqual(OwnerCar1.CarKey, ownerCarCheck.CarKey);
            Assert.AreEqual(carIDChange, ownerCarCheck.CarID);
            Assert.AreEqual(OwnerCar1.PaintJob, ownerCarCheck.PaintJob);
            Assert.AreEqual(OwnerCar1.MaxPower, ownerCarCheck.MaxPower);
            Assert.AreEqual(OwnerCar1.PowerLevel, ownerCarCheck.PowerLevel);
            Assert.AreEqual(OwnerCar1.WeightReductionLevel, ownerCarCheck.WeightReductionLevel);
            Assert.AreEqual(OwnerCar1.AcquiredDate, ownerCarCheck.AcquiredDate);
        }

        [TestMethod]
        public void A030_Delete()
        {
            ownerCarsRepository.DeleteAndFlush(OwnerCar1.PrimaryKey);

            OwnerCar ownerCarCheck = ownerCarsRepository.GetById(OwnerCar1.PrimaryKey);

            Assert.IsNull(ownerCarCheck);
        }

        [TestMethod]
        public void A040_Add3OwnerCars()
        {
            ownerCarsRepository.Save(OwnerCar1);
            ownerCarsRepository.Save(OwnerCar2);
            ownerCarsRepository.Save(OwnerCar3);
            ownerCarsRepository.Flush();
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetList();

            Assert.AreEqual(numberOfOwnerCars, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A060_GetListOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetList(orderedList: true);

            Assert.AreEqual(numberOfOwnerCars, ownerCars.Count);
            Assert.AreEqual(OwnerCar3.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByCarId()
        {
            OwnerCar ownerCar = ownerCarsRepository.GetByCarID(OwnerCar2.CarID);

            Assert.AreEqual(OwnerCar2.PrimaryKey, ownerCar.PrimaryKey);
        }

        [TestMethod]
        public void A080_GetByCarIdBadCarID()
        {
            OwnerCar ownerCar = ownerCarsRepository.GetByCarID(badCarID);

            Assert.IsNull(ownerCar);
        }

        [TestMethod]
        public void A090_GetMaxKey()
        {
            string maxKey = ownerCarsRepository.GetMaxKey();

            Assert.AreEqual(expectedMaxKey, maxKey);
        }

        [TestMethod]
        public void A100_GetListForOwnerKey()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetListForOwnerKey(Owner1.PrimaryKey);

            Assert.AreEqual(numberOfOwnerCarsForOwner, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForOwnerKeyOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetListForOwnerKey(Owner1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfOwnerCarsForOwner, ownerCars.Count);
            Assert.AreEqual(OwnerCar3.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_GetListForCarKey()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetListForForCarKey(Car1.PrimaryKey);

            Assert.AreEqual(numberOfOwnerCarsForCar, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A130_GetListForCarKeyOrdered()
        {
            List<OwnerCar> ownerCars = ownerCarsRepository.GetListForForCarKey(Car1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfOwnerCarsForCar, ownerCars.Count);
            Assert.AreEqual(OwnerCar1.PrimaryKey, ownerCars[0].PrimaryKey);
        }

        [TestMethod]
        public void A140_AddOwnerCar()
        {
            ownerCarsRepository.SaveAndFlush(OwnerCar4);
        }

        [TestMethod]
        public void A150_GetOwnerCarStatistics()
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = ownerCarsRepository.GetOwnerCarStatistics(OwnerCar1.OwnerKey);

            Assert.AreEqual(numberOfOwnerCarsStatistics, ownerCarsStatistics.Count);

            var totalOwnedCars = 0;
            foreach(OwnerCarsStatistic ownerCarsStatistic in ownerCarsStatistics)
            {
                totalOwnedCars = totalOwnedCars + ownerCarsStatistic.carsOwned;
            }

            Assert.AreEqual(numberOfOwnedCars, totalOwnedCars);
        }

        [TestMethod]
        public void A150_GetUniqueOwnerCarStatistics()
        {
            List<OwnerCarsStatistic> ownerCarsStatistics = ownerCarsRepository.GetUniqueOwnerCarStatistics(OwnerCar1.OwnerKey);

            Assert.AreEqual(numberOfOwnerCarsStatistics, ownerCarsStatistics.Count);

            var totalOwnedCars = 0;
            foreach (OwnerCarsStatistic ownerCarsStatistic in ownerCarsStatistics)
            {
                totalOwnedCars = totalOwnedCars + ownerCarsStatistic.uniqueCarsOwned;
            }

            Assert.AreEqual(numberOfUnqiueOwnedCars, totalOwnedCars);
        }

        [TestMethod]
        public void A170_Delete4OwnerCars()
        {
            ownerCarsRepository.Refresh();
            ownerCarsRepository.Delete(OwnerCar1.PrimaryKey);
            ownerCarsRepository.Delete(OwnerCar2.PrimaryKey);
            ownerCarsRepository.Delete(OwnerCar3.PrimaryKey);
            ownerCarsRepository.Delete(OwnerCar4.PrimaryKey);
            ownerCarsRepository.Flush();
        }
    }
}
