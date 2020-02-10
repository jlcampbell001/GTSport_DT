using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;

namespace GTSport_DT_Testing.Countries
{
    [TestClass]
    public class CountryValidationTests : TestBase
    {
        private static CountryValidation countryValidation;
        private static CountriesRepository countriesRespository;
        private static RegionsRepository regionsRepository;
        private static ManufacturersRepository manufacturersRepository;

        private const string badRegionKey = "X!X900000001";
        private const string badCountryKey = "C!C999999999";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            countryValidation = new CountryValidation(con);
            countriesRespository = new CountriesRepository(con);
            regionsRepository = new RegionsRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
            regionsRepository.Flush();

            countriesRespository.Save(Country1);
            countriesRespository.Save(Country2);
            countriesRespository.Save(Country3);
            countriesRespository.Flush();

            manufacturersRepository.SaveAndFlush(Manufacturer1);
        }

        [TestMethod]
        public void ZZZZ_CleanUp()
        {
            if (con != null)
            {
                manufacturersRepository.DeleteAndFlush(Manufacturer1.PrimaryKey);

                countriesRespository.Refresh();
                countriesRespository.Delete(Country1.PrimaryKey);
                countriesRespository.Delete(Country2.PrimaryKey);
                countriesRespository.Delete(Country3.PrimaryKey);
                countriesRespository.Flush();

                regionsRepository.Refresh();
                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);
                regionsRepository.Flush();

                con.Close();
            }
        }

        [TestMethod]
        public void A010_ValidationSave()
        {
            countryValidation.ValidateSave(Country1);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryDescriptionNotSetException))]
        public void A020_ValidationSaveDescriptionNotSet()
        {
            Country country = new Country(Country1.PrimaryKey, "", Country1.RegionKey);

            try
            {
                countryValidation.ValidateSave(country);
            }
            catch (CountryDescriptionNotSetException cdnse)
            {
                Assert.AreEqual(CountryDescriptionNotSetException.CountryDescriptionNotSetMsg, cdnse.Message);
                throw cdnse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CountryRegionKeyNotSetException))]
        public void A030_ValidationSaveRegionKeyNotSet()
        {
            Country country = new Country(Country1.PrimaryKey, Country1.Description, "");

            try
            {
                countryValidation.ValidateSave(country);
            }
            catch (CountryRegionKeyNotSetException crknse)
            {
                Assert.AreEqual(CountryRegionKeyNotSetException.CountryRegionKeyNotSetMsg, crknse.Message);
                throw crknse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CountryDescriptionAlreadyExistsException))]
        public void A040_ValidationSaveDescriptionAlreadyExists()
        {
            Country country = new Country(Country1.PrimaryKey, Country2.Description, Country1.RegionKey);

            try
            {
                countryValidation.ValidateSave(country);
            }
            catch (CountryDescriptionAlreadyExistsException cdaee)
            {
                Assert.AreEqual(CountryDescriptionAlreadyExistsException.CountryDescriptionAlreadyExistsMsg, cdaee.Message);
                throw cdaee;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RegionNotFoundException))]
        public void A050_ValidationSaveBadRegionKey()
        {
            Country country = new Country(Country1.PrimaryKey, Country1.Description, badRegionKey);

            try
            {
                countryValidation.ValidateSave(country);
            }
            catch (RegionNotFoundException rnfe)
            {
                Assert.AreEqual(RegionNotFoundException.RegionKeyNotFoundMsg, rnfe.Message);
                throw rnfe;
            }
        }

        [TestMethod]
        public void A060_ValidationDelete()
        {
            countryValidation.ValidateDelete(Country3.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(CountryNotFoundException))]
        public void A070_ValidationDeleteBadCountryKey()
        {
            try
            {
                countryValidation.ValidateDelete(badCountryKey);
            } catch (CountryNotFoundException cnfe)
            {
                Assert.AreEqual(CountryNotFoundException.CountryKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CountryInUseException))]
        public void A080_ValidationDeleteCountryInUse()
        {
            try
            {
               countryValidation.ValidateDelete(Manufacturer1.CountryKey);
            } catch (CountryInUseException ciue) {
                Assert.AreEqual(CountryInUseException.CountryInUseCanNotBeDeletedManufacturerMsg, ciue.Message);
                throw ciue;
            }
        }
    }
}
