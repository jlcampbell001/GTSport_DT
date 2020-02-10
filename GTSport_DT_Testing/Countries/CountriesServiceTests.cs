using GTSport_DT.Countries;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.Countries
{
    [TestClass]
    public class CountriesServiceTests : TestBase
    {
        private static CountriesService countriesService;
        private static CountriesRepository countriesRepository;
        private static RegionsRepository regionsRepository;

        private const string country6Description = "COUNTRY_6";
        private const string newCountry6Description = "NEW_6TH_COUNTRY";
        private const string badCountryKey = "C!C999999999";
        private const string badCountryDescription = "BAD DESCRIPTION";
        private const string expectedCountry6Key = "COU900000006";

        private const int numberOfCountries = 5;
        private const int numberOfCountriesByRegion = 3;

        private readonly string country6RegionKey = Region2.PrimaryKey;

        private static string country6Key = "";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            countriesService = new CountriesService(con);
            countriesRepository = new CountriesRepository(con);
            regionsRepository = new RegionsRepository(con);

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
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                countriesRepository.Refresh();
                countriesRepository.Delete(Country1.PrimaryKey);
                countriesRepository.Delete(Country2.PrimaryKey);
                countriesRepository.Delete(Country3.PrimaryKey);
                countriesRepository.Delete(Country4.PrimaryKey);
                countriesRepository.Delete(Country5.PrimaryKey);
                countriesRepository.Delete(country6Key);
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
            Country country = countriesService.GetByKey(Country2.PrimaryKey);

            Assert.AreEqual(Country2.Description, country.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryNotFoundException))]
        public void A020_GetByKeyBadCountryKey()
        {
            try
            {
                Country country = countriesService.GetByKey(badCountryKey);
            } catch (CountryNotFoundException cnfe)
            {
                Assert.AreEqual(CountryNotFoundException.CountryKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A030_GetByDescription()
        {
            Country country = countriesService.GetByDescription(Country3.Description);

            Assert.AreEqual(Country3.PrimaryKey, country.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryNotFoundException))]
        public void A040_GetByDescriptionBadDescription()
        {
            try
            {
                Country country = countriesService.GetByDescription(badCountryDescription);
            }
            catch (CountryNotFoundException cnfe)
            {
                Assert.AreEqual(CountryNotFoundException.CountryDescriptionNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A050_SaveNewCountry()
        {
            Country country = new Country("", country6Description, country6RegionKey);

            countriesService.Save(ref country);

            country6Key = country.PrimaryKey;
        }

        [TestMethod]
        public void A060_SaveUpdateCountry()
        {
            Country country = new Country(country6Key, newCountry6Description, country6RegionKey);

            countriesService.Save(ref country);
        }

        [TestMethod]
        public void A070_DeleteCountry()
        {
            countriesService.Delete(country6Key);
        }

        [TestMethod]
        public void A080_GetList()
        {
            List<Country> countries = countriesService.GetList();

            Assert.AreEqual(numberOfCountries, countries.Count);
            Assert.AreEqual(Country1.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A090_GetListOrdered()
        {
            List<Country> countries = countriesService.GetList(orderedList: true);

            Assert.AreEqual(numberOfCountries, countries.Count);
            Assert.AreEqual(Country5.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListForRegionKey()
        {
            List<Country> countries = countriesService.GetListForRegionKey(Region2.PrimaryKey);

            Assert.AreEqual(numberOfCountriesByRegion, countries.Count);
            Assert.AreEqual(Country2.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListForRegionKeyOrdered()
        {
            List<Country> countries = countriesService.GetListForRegionKey(Region2.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfCountriesByRegion, countries.Count);
            Assert.AreEqual(Country5.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_ResetKey()
        {
            countriesService.ResetKey();

            Country country = new Country("", country6Description, country6RegionKey);

            countriesService.Save(ref country);

            country6Key = country.PrimaryKey;

            Assert.AreEqual(expectedCountry6Key, country6Key);
        }
    }
}
