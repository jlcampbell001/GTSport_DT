using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using GTSport_DT.Countries;

namespace GTSport_DT_Testing.Regions
{
    [TestClass]
    public class RegionValidationTests : TestBase
    {
        private static RegionValidation regionValidation;
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;

        private const string BadRegionKey = "X!!990000009";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionValidation = new RegionValidation(con);
            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);

            countriesRepository.Save(Country3);
        }

        [TestMethod]
        public void ZZZZ_CleanUp()
        {
            if (con != null)
            {
                countriesRepository.Delete(Country3.PrimaryKey);

                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);

                con.Close();
            }
        }

        [TestMethod]
        public void A010_ValidateSave()
        {
            regionValidation.ValidateSave(Region1);
        }

        [TestMethod]
        [ExpectedException(typeof(RegionDescriptionNotSetException))]
        public void A020_ValidateSaveDescriptionNotSet()
        {
            Region missingDescription = new Region();

            try
            {
                regionValidation.ValidateSave(missingDescription);
            }
            catch (RegionDescriptionNotSetException rdnse)
            {
                Assert.AreEqual(RegionDescriptionNotSetException.regionDescriptionNotSet, rdnse.Message);
                throw rdnse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RegionDescriptionAlreadyExistsException))]
        public void A030_ValidateSaveDescriptionAlreadyExists()
        {
            Region duplicateDescription = new Region("", Region2.Description);

            try
            {
                regionValidation.ValidateSave(duplicateDescription);
            }
            catch (RegionDescriptionAlreadyExistsException rdaee)
            {
                Assert.AreEqual(RegionDescriptionAlreadyExistsException.RegionDescriptionAlreadyExistsMsg, rdaee.Message);
                throw rdaee;
            }
        }

        [TestMethod]
        public void A040_ValidateDelete()
        {
            regionValidation.ValidateDelete(Region3.PrimaryKey);

        }

        [TestMethod]
        [ExpectedException(typeof(RegionNotFoundException))]
        public void A050_ValidateDeleteKeyNotFound()
        {
            try
            {
                regionValidation.ValidateDelete(BadRegionKey);
            }
            catch (RegionNotFoundException rnfe)
            {
                Assert.AreEqual(RegionNotFoundException.RegionKeyNotFoundMsg, rnfe.Message);
                throw rnfe;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(RegionInUseException))]
        public void A060_ValidateDeleteInUse()
        {
            try
            {
                regionValidation.ValidateDelete(Region1.PrimaryKey);
            } catch (RegionInUseException riue)
            {
                Assert.AreEqual(RegionInUseException.RegionInUseCanNotBeDeletedCountryMsg, riue.Message);
                throw riue;
            }
        }
    }
}
