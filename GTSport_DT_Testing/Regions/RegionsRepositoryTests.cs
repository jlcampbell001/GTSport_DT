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
    public class RegionsRepositoryTests : TestBase
    {
        private static RegionsRepository regionsRepository;

        private const string descriptionChange = "Changed Description";
        private const int numberofRegions = 3;
        private const string badDescription = "XXX_BAD_DESCRIPTION_XXX";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            if (con != null)
            {
                con.Close();
            }
        }

        [TestMethod]
        public void A010_SaveNewTest()
        {
            regionsRepository.Save(region1);

            Region regionCheck = regionsRepository.GetById(region1.PrimaryKey);

            Assert.IsNotNull(regionCheck);
            Assert.AreEqual(region1.PrimaryKey, regionCheck.PrimaryKey);
            Assert.AreEqual(region1.Description, regionCheck.Description);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Region region = region1;

            region.Description = descriptionChange;

            regionsRepository.Save(region);

            Region regionCheck = regionsRepository.GetById(region1.PrimaryKey);

            Assert.IsNotNull(regionCheck);
            Assert.AreEqual(region1.PrimaryKey, regionCheck.PrimaryKey);
            Assert.AreEqual(region1.Description, regionCheck.Description);
        }

        [TestMethod]
        public void A030_Delete()
        {
            regionsRepository.Delete(region1.PrimaryKey);

            Region regionCheck = regionsRepository.GetById(region1.PrimaryKey);

            Assert.IsNull(regionCheck);
        }

        [TestMethod]
        public void A040_Add3Regions()
        {
            regionsRepository.Save(region1);
            regionsRepository.Save(region2);
            regionsRepository.Save(region3);
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<Region> regions = regionsRepository.GetList();

            Assert.AreEqual(numberofRegions, regions.Count);
        }

        [TestMethod]
        public void A060_GetListOrdered()
        {
            List<Region> regions = regionsRepository.GetList(orderedList: true);

            Assert.AreEqual(numberofRegions, regions.Count);
            Assert.AreEqual(region3.PrimaryKey, regions[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByDesciption()
        {
            Region region = regionsRepository.GetByDescription(region2.Description);

            Assert.AreEqual(region2.PrimaryKey, region.PrimaryKey);
        }

        [TestMethod]
        public void A080_GetByDescriptionBadDescription()
        {
            Region region = regionsRepository.GetByDescription(badDescription);

            Assert.IsNull(region);
        }

        [TestMethod]
        public void A090_GetMaxKey()
        {
            string maxKey = regionsRepository.GetMaxKey();

            Assert.AreEqual(region3.PrimaryKey, maxKey);
        }

        [TestMethod]
        public void A100_Delete3Owners()
        {
            regionsRepository.Delete(region1.PrimaryKey);
            regionsRepository.Delete(region2.PrimaryKey);
            regionsRepository.Delete(region3.PrimaryKey);
        }
    }
}
