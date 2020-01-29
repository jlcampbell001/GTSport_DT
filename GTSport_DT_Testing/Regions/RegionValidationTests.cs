using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.Regions
{
    [TestClass]
    public class RegionValidationTests : TestBase
    {
        private static RegionValidation regionValidation;
        private static RegionsRepository regionsRepository;

        private const string BadRegionKey = "X!!990000009";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionValidation = new RegionValidation(con);
            regionsRepository = new RegionsRepository(con);

            regionsRepository.Save(region1);
            regionsRepository.Save(region2);
            regionsRepository.Save(region3);
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            if (con != null)
            {
                regionsRepository.Delete(region1.PrimaryKey);
                regionsRepository.Delete(region2.PrimaryKey);
                regionsRepository.Delete(region3.PrimaryKey);
                con.Close();
            }
        }

        [TestMethod]
        public void A010_ValidateSave()
        {
            regionValidation.ValidateSave(region1);
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
            Region duplicateDescription = new Region("", region2.Description);

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
            regionValidation.ValidateDelete(region3.PrimaryKey);

        }

        [TestMethod]
        [ExpectedException(typeof(RegionNotFoundException))]
        public void A500_ValidateDeleteKeyNotFound()
        {
            try
            {
                regionValidation.ValidateDelete(BadRegionKey);
            } catch (RegionNotFoundException rnfe)
            {
                Assert.AreEqual(RegionNotFoundException.RegionKeyNotFoundMsg, rnfe.Message);
                throw rnfe;
            }
        }
    }
}
