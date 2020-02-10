using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.Manufacturers
{
    [TestClass]
    public class ManufacturersServiceTests : TestBase
    {
        private static ManufacturersService manufacturersService;
        private static ManufacturersRepository manufacturersRepository;
        private static CountriesRepository countriesRepository;
        private static RegionsRepository regionsRepository;

        private static string manufacturer10Key = "";

        private readonly string manufacturer10CountryKey = Country2.PrimaryKey;

        private const string manufacturer10Name = "MANUFACTURER_10";
        private const string newManufacture10Name = "NEW_MANUFACTURER_X";
        private const string badManufactureKey = "D!X999999999";
        private const string badName = "BAD_MANUFACTURER_NAME";
        private const string expectedMaxKey = "MAN900000010";

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
            manufacturersService = new ManufacturersService(con);

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
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
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
                manufacturersRepository.Delete(manufacturer10Key);
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
            Manufacturer manufacturer = manufacturersService.GetByKey(Manufacturer2.PrimaryKey);

            Assert.AreEqual(Manufacturer2.Name, manufacturer.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNotFoundException))]
        public void A020_GetByKeyBadKey()
        {
            try
            {
                Manufacturer manufacturer = manufacturersService.GetByKey(badManufactureKey);
            } catch (ManufacturerNotFoundException mnfe)
            {
                Assert.AreEqual(ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg, mnfe.Message);
                throw mnfe;
            }
        }

        [TestMethod]
        public void A030_GetByName()
        {
            Manufacturer manufacturer = manufacturersService.GetByName(Manufacturer1.Name);

            Assert.AreEqual(Manufacturer1.PrimaryKey, manufacturer.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNotFoundException))]
        public void A040_GetbyNameBadName()
        {
            try
            {
                Manufacturer manufacturer = manufacturersService.GetByName(badName);
            } catch (ManufacturerNotFoundException mnfe)
            {
                Assert.AreEqual(ManufacturerNotFoundException.ManufacturerNameNotFoundMsg, mnfe.Message);
                throw mnfe;
            }
        }

        [TestMethod]
        public void A050_SaveNewManufacturer()
        {
            Manufacturer manufacturer = new Manufacturer("", manufacturer10Name, manufacturer10CountryKey);

            manufacturersService.Save(ref manufacturer);

            manufacturer10Key = manufacturer.PrimaryKey;
        }

        [TestMethod]
        public void A060_SaveChangedManufacturer()
        {
            Manufacturer manufacturer = new Manufacturer(manufacturer10Key, newManufacture10Name, manufacturer10CountryKey);

            manufacturersService.Save(ref manufacturer);
        }

        [TestMethod]
        public void A070_DeleteManufacturer()
        {
            manufacturersService.Delete(manufacturer10Key);
        }

        [TestMethod]
        public void A080_GetList()
        {
            List<Manufacturer> manufacturers = manufacturersService.GetList();

            Assert.AreEqual(numberOfManufacturers, manufacturers.Count);
            Assert.AreEqual(Manufacturer1.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A090_GetListOrdered()
        {
            List<Manufacturer> manufacturers = manufacturersService.GetList(orderedList: true);

            Assert.AreEqual(numberOfManufacturers, manufacturers.Count);
            Assert.AreEqual(Manufacturer8.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListForCountry()
        {
            List<Manufacturer> manufacturers = manufacturersService.GetListForCountryKey(Country2.PrimaryKey);

            Assert.AreEqual(numberOfManufacturersForCountry, manufacturers.Count);
            Assert.AreEqual(Manufacturer2.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForCountryOrdered()
        {
            List<Manufacturer> manufacturers = manufacturersService.GetListForCountryKey(Country2.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfManufacturersForCountry, manufacturers.Count);
            Assert.AreEqual(Manufacturer4.PrimaryKey, manufacturers[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_ResetKey()
        {
            manufacturersService.ResetKey();

            Manufacturer manufacturer = new Manufacturer("", manufacturer10Name, manufacturer10CountryKey);

            manufacturersService.Save(ref manufacturer);

            manufacturer10Key = manufacturer.PrimaryKey;

            Assert.AreEqual(expectedMaxKey, manufacturer10Key);
        }
    }
}
