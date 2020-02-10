using GTSport_DT.General.KeySequence;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;

namespace GTSport_DT_Testing
{
    [TestClass]
    public class KeySequenceRespositoryTests : TestBase
    {
        private static KeySequenceRepository keySequenceRepository;

        private static readonly string testTableName = "TEST";

        private static readonly int testInitalValue = 10;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            keySequenceRepository = new KeySequenceRepository(con);
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                con.Close();
            }
        }

        [TestMethod]
        public void A010_SaveNewTest()
        {

            KeySequence keySequence = new KeySequence();
            keySequence.TableName = testTableName;
            keySequence.LastKeyValue = testInitalValue;

            keySequenceRepository.SaveAndFlush(keySequence);

            KeySequence keySequenceCheck = keySequenceRepository.GetById(testTableName);

            Assert.IsNotNull(keySequenceCheck);
            Assert.AreEqual(testTableName, keySequenceCheck.TableName);
            Assert.AreEqual(testInitalValue, keySequenceCheck.LastKeyValue);

        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            var updateValue = testInitalValue + 20;

            KeySequence keySequence = new KeySequence();
            keySequence.TableName = testTableName;
            keySequence.LastKeyValue = updateValue;

            keySequenceRepository.SaveAndFlush(keySequence);

            KeySequence keySequenceCheck = keySequenceRepository.GetById(testTableName);

            Assert.IsNotNull(keySequenceCheck);
            Assert.AreEqual(testTableName, keySequenceCheck.TableName);
            Assert.AreEqual(updateValue, keySequenceCheck.LastKeyValue);

        }

        [TestMethod]
        public void A030_DeleteTest()
        {
            keySequenceRepository.DeleteAndFlush(testTableName);

            KeySequence keySequenceCheck = keySequenceRepository.GetById(testTableName);

            Assert.IsNull(keySequenceCheck);
        }
    }
}
