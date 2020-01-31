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
    public class RegionsServiceTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static RegionsService regionsService;

        private static string region4Key = "";

        private const string region4Descirption = "TEST_REGION_4";
        private const string region4NewDescription = "NEW_REGION_4";

        private const string badRegionKey = "X!!990000009";
        private const string badDescription = "XXX_BAD_DESCRIP_XXX";

        private const string expectedMaxKey = "REG900000004";
        private const int expectedNumberOfRecords = 4;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsService = new RegionsService(con);
            regionsRepository = new RegionsRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);
                regionsRepository.Delete(region4Key);
                con.Close();
            }
        }

        [TestMethod]
        public void A010_GetByKey()
        {
            Region region = regionsService.GetByKey(Region3.PrimaryKey);

            Assert.AreEqual(Region3.Description, region.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(RegionNotFoundException))]
        public void A020_GetByKeyBadKey()
        {
            try
            {
                Region region = regionsService.GetByKey(badRegionKey);
            } catch (RegionNotFoundException rnfe)
            {
                Assert.AreEqual(RegionNotFoundException.RegionKeyNotFoundMsg, rnfe.Message);
                throw rnfe;
            }
        }

        [TestMethod]
        public void A030_GetByDescription()
        {
            Region region = regionsService.GetByDescription(Region1.Description);

            Assert.AreEqual(Region1.PrimaryKey, region.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(RegionNotFoundException))]
        public void A040_GetByDescriptionBadDescription()
        {
            try
            {
                Region region = regionsService.GetByDescription(badDescription);
            } catch (RegionNotFoundException rnfe)
            {
                Assert.AreEqual(RegionNotFoundException.RegionDescriptionNotFoundMsg, rnfe.Message);
                throw rnfe;
            }
        }

        [TestMethod]
        public void A050_SaveNewRegion()
        {
            Region region = new Region("", region4Descirption);

            regionsService.Save(ref region);

            region4Key = region.PrimaryKey;
        }

        [TestMethod]
        public void A060_SaveUpdateRegion()
        {
            Region region = new Region(region4Key, region4NewDescription);

            regionsService.Save(ref region);
        }

        [TestMethod]
        public void A070_DeleteRegion()
        {
            regionsService.Delete(region4Key);
        }

        [TestMethod]
        public void A080_ResetKeys()
        {
            regionsService.ResetKey();

            Region region = new Region("", region4Descirption);

            regionsService.Save(ref region);

            region4Key = region.PrimaryKey;

            Assert.AreEqual(expectedMaxKey, region4Key);
        }

        [TestMethod]
        public void A090_GetList()
        {
            List<Region> regions = regionsService.GetList();

            Assert.AreEqual(expectedNumberOfRecords, regions.Count);
            Assert.AreEqual(Region1.PrimaryKey, regions[0].PrimaryKey);
        }

        [TestMethod]
        public void A100_GetListOrdered()
        {
            List<Region> regions = regionsService.GetList(orderedList: true);

            Assert.AreEqual(expectedNumberOfRecords, regions.Count);
            Assert.AreEqual(Region3.PrimaryKey, regions[0].PrimaryKey);
        }
    }
}
