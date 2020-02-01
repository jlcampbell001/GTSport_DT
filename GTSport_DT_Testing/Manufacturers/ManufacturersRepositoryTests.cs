using GTSport_DT.Countries;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT.Manufacturers
{
    [TestClass]
    public class ManufacturersRepositoryTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;

        private static readonly string expectedMaxKey = Manufacturer9.PrimaryKey;

        private const string nameChange = "Name Changed";
        private const string badName = "BAD!Name";

        private const int numberOfManufacturers = 9;
        private const int numberOfManufacturersForCountry = 3;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);

            countriesRepository.Save(Country1);
            countriesRepository.Save(Country2);
            countriesRepository.Save(Country3);
            countriesRepository.Save(Country4);
            countriesRepository.Save(Country5);
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                countriesRepository.Delete(Country1.PrimaryKey);
                countriesRepository.Delete(Country2.PrimaryKey);
                countriesRepository.Delete(Country3.PrimaryKey);
                countriesRepository.Delete(Country4.PrimaryKey);
                countriesRepository.Delete(Country5.PrimaryKey);

                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);

                con.Close();
            }
        }

        [TestMethod]
        public void A010_SaveNewTest()
        {
            manufacturersRepository.Save(Manufacturer1);

            Manufacturer manufacturerCheck = manufacturersRepository.GetById(Manufacturer1.PrimaryKey);

            Assert.IsNotNull(manufacturerCheck);
            Assert.AreEqual(Manufacturer1.PrimaryKey, manufacturerCheck.PrimaryKey);
            Assert.AreEqual(Manufacturer1.Name, manufacturerCheck.Name);
            Assert.AreEqual(Manufacturer1.CountryKey, manufacturerCheck.CountryKey);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Manufacturer manufacturer = new Manufacturer(Manufacturer1.PrimaryKey, Manufacturer1.Name, Manufacturer1.CountryKey);

            manufacturer.Name = nameChange;

            manufacturersRepository.Save(manufacturer);

            Manufacturer manufacturerCheck = manufacturersRepository.GetById(Manufacturer1.PrimaryKey);

            Assert.IsNotNull(manufacturerCheck);
            Assert.AreEqual(Manufacturer1.PrimaryKey, manufacturerCheck.PrimaryKey);
            Assert.AreEqual(nameChange, manufacturerCheck.Name);
            Assert.AreEqual(Manufacturer1.CountryKey, manufacturerCheck.CountryKey);
        }

        [TestMethod]
        public void A030_Delete()
        {
            manufacturersRepository.Delete(Manufacturer1.PrimaryKey);

            Manufacturer manufacturerCheck = manufacturersRepository.GetById(Manufacturer1.PrimaryKey);

            Assert.IsNull(manufacturerCheck);
        }

        [TestMethod]
        public void A040_Add9Countries()
        {
            manufacturersRepository.Save(Manufacturer1);
            manufacturersRepository.Save(Manufacturer2);
            manufacturersRepository.Save(Manufacturer3);
            manufacturersRepository.Save(Manufacturer4);
            manufacturersRepository.Save(Manufacturer5);
            manufacturersRepository.Save(Manufacturer6);
            manufacturersRepository.Save(Manufacturer7);
            manufacturersRepository.Save(Manufacturer8);
            manufacturersRepository.Save(Manufacturer9);
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetList();

            Assert.AreEqual(numberOfManufacturers, manufacturers.Count);
        }

        [TestMethod]
        public void A060_GetListOrdered()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetList(orderedList: true);

            Assert.AreEqual(numberOfManufacturers, manufacturers.Count);
            Assert.AreEqual(Manufacturer8.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByName()
        {
            Manufacturer manufacturer = manufacturersRepository.GetByName(Manufacturer2.Name);

            Assert.AreEqual(Manufacturer2.PrimaryKey, manufacturer.PrimaryKey);
        }

        [TestMethod]
        public void A080_GetByNameBadName()
        {
            Manufacturer manufacturer = manufacturersRepository.GetByName(badName);

            Assert.IsNull(manufacturer);
        }

        [TestMethod]
        public void A090_GetMaxKey()
        {
            string maxKey = manufacturersRepository.GetMaxKey();

            Assert.AreEqual(expectedMaxKey, maxKey);
        }

        [TestMethod]
        public void A100_GetListForCountryKey()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetListForCountryKey(Manufacturer2.CountryKey);

            Assert.AreEqual(numberOfManufacturersForCountry, manufacturers.Count);
            Assert.AreEqual(Manufacturer2.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForCountryKeyOrdered()
        {
            List<Manufacturer> manufacturers = manufacturersRepository.GetListForCountryKey(Manufacturer2.CountryKey, orderedList: true);

            Assert.AreEqual(numberOfManufacturersForCountry, manufacturers.Count);
            Assert.AreEqual(Manufacturer4.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_Delete9Countries()
        {
            manufacturersRepository.Delete(Manufacturer1.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer2.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer3.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer4.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer5.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer6.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer7.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer8.PrimaryKey);
            manufacturersRepository.Delete(Manufacturer9.PrimaryKey);
        }
    }
}
