using System;
using System.Collections.Generic;
using System.Text;
using GTSport_DT.General.KeySequence;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace GTSport_DT_Testing
{ 
    [TestClass]
    public class KeySequenceServiceTests : TestBase
    {
        private static KeySequenceService keySequenceService;
        private static KeySequenceRepository keySequenceRepository;

        private static readonly string testingTableName = "TESTING";
        private static readonly string keyPrefix = "TST";

        private static readonly string expectedNewValue = keyPrefix + "000000001";
        private static readonly string expectedExistingValue = keyPrefix + "000000002";

        private static readonly int newKeyValue = 999;

        private static readonly string expectedExistingValueAfterReset = keyPrefix + "000001000";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            keySequenceService = new KeySequenceService(con);
            keySequenceRepository = new KeySequenceRepository(con);

            DeleteTestRecord();
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                DeleteTestRecord();
                con.Close();
            }
        }

        [TestMethod]
        public void A010_GetNextKeyWithNewTable()
        {
            string newKey = keySequenceService.GetNextKey(testingTableName, keyPrefix);

            Assert.AreEqual(expectedNewValue, newKey);
        }

        [TestMethod]
        public void A020_GetNextKeyWithExistingTable()
        {
            string newKey = keySequenceService.GetNextKey(testingTableName, keyPrefix);

            Assert.AreEqual(expectedExistingValue, newKey);
        }

        [TestMethod]
        public void A030_ResetKeyValue()
        {
            keySequenceService.ResetKeyValue(testingTableName, newKeyValue);

            string keyValue = keySequenceService.GetNextKey(testingTableName, keyPrefix);

            Assert.AreEqual(expectedExistingValueAfterReset, keyValue);
        }

        private static void DeleteTestRecord()
        {
            var keySequence = keySequenceRepository.GetById(testingTableName);

            if (keySequence != null)
            {
                keySequenceRepository.Refresh();
                keySequenceRepository.DeleteAndFlush(testingTableName);
            }
        }
    }
}
