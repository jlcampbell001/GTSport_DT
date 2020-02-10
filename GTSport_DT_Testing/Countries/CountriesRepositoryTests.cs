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
    public class CountriesRepositoryTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;

        private static readonly string expectedMaxKey = Country5.PrimaryKey;

        private const string descriptionChange = "Changed Description";
        private const int numberOfCountires = 5;
        private const int expectedRegion2Records = 3;
        private const string badDescription = "XXX_BAD_DESCRIPTION_XXX";


        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
            regionsRepository.Flush();
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
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
            countriesRepository.SaveAndFlush(Country1);

            Country countryCheck = countriesRepository.GetById(Country1.PrimaryKey);

            Assert.IsNotNull(countryCheck);
            Assert.AreEqual(Country1.PrimaryKey, countryCheck.PrimaryKey);
            Assert.AreEqual(Country1.Description, countryCheck.Description);
            Assert.AreEqual(Country1.RegionKey, countryCheck.RegionKey);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Country country = new Country(Country1.PrimaryKey, Country1.Description, Country1.RegionKey);

            country.Description = descriptionChange;

            countriesRepository.SaveAndFlush(country);

            Country countryCheck = countriesRepository.GetById(Country1.PrimaryKey);

            Assert.IsNotNull(countryCheck);
            Assert.AreEqual(Country1.PrimaryKey, countryCheck.PrimaryKey);
            Assert.AreEqual(descriptionChange, countryCheck.Description);
        }

        [TestMethod]
        public void A030_Delete()
        {
            countriesRepository.DeleteAndFlush(Country1.PrimaryKey);

            Country countryCheck = countriesRepository.GetById(Country1.PrimaryKey);

            Assert.IsNull(countryCheck);
        }

        [TestMethod]
        public void A040_Add5Countries()
        {
            countriesRepository.Save(Country1);
            countriesRepository.Save(Country2);
            countriesRepository.Save(Country3);
            countriesRepository.Save(Country4);
            countriesRepository.Save(Country5);
            countriesRepository.Flush();
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<Country> countries = countriesRepository.GetList();

            Assert.AreEqual(numberOfCountires, countries.Count);
        }

        [TestMethod]
        public void A060_GetListOrdered()
        {
            List<Country> countries = countriesRepository.GetList(orderedList: true);

            Assert.AreEqual(numberOfCountires, countries.Count);
            Assert.AreEqual(Country5.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByDescription()
        {
            Country country = countriesRepository.GetByDescription(Country2.Description);

            Assert.AreEqual(Country2.PrimaryKey, country.PrimaryKey);
        }

        [TestMethod]
        public void A080_GetByDescriptionBadDescription()
        {
            Country country = countriesRepository.GetByDescription(badDescription);

            Assert.IsNull(country);
        }

        [TestMethod]
        public void A090_GetMaxKey()
        {
            string maxKey = countriesRepository.GetMaxKey();

            Assert.AreEqual(expectedMaxKey, maxKey);
        }

        [TestMethod]
        public void A100_GetListForRegionKey()
        {
            List<Country> countries = countriesRepository.GetListForRegionKey(Region2.PrimaryKey);

            Assert.AreEqual(expectedRegion2Records, countries.Count);
            Assert.AreEqual(Country2.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForRegionKeyOrdered()
        {
            List<Country> countries = countriesRepository.GetListForRegionKey(Region2.PrimaryKey, orderedList: true);

            Assert.AreEqual(expectedRegion2Records, countries.Count);
            Assert.AreEqual(Country5.PrimaryKey, countries[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_Delete5Countries()
        {
            countriesRepository.Delete(Country1.PrimaryKey);
            countriesRepository.Delete(Country2.PrimaryKey);
            countriesRepository.Delete(Country3.PrimaryKey);
            countriesRepository.Delete(Country4.PrimaryKey);
            countriesRepository.Delete(Country5.PrimaryKey);
            countriesRepository.Flush();
        }
    }
}
