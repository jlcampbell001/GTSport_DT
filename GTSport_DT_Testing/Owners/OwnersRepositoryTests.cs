using GTSport_DT.Owners;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Owners.OwnersForTesting;

namespace GTSport_DT_Testing.Owners
{
    [TestClass]
    public class OwnersRepositoryTests : TestBase
    {

        private static OwnersRepository ownersRepository;

        private const string ownerNameChange = "CHANGED NAME";
        private const string badOwnerName = "XXX_";
        private readonly string maxOwnKey = owner3.PrimaryKey;

        private const int numberOfOwners = 3;
        private const int numberOfDefaultOwners = 1;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            ownersRepository = new OwnersRepository(con);
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
            ownersRepository.Save(owner1);

            Owner ownerCheck = ownersRepository.GetById(owner1.PrimaryKey);

            Assert.IsNotNull(ownerCheck);
            Assert.AreEqual(owner1.PrimaryKey, ownerCheck.PrimaryKey);
            Assert.AreEqual(owner1.OwnerName, ownerCheck.OwnerName);
            Assert.AreEqual(owner1.DefaultOwner, ownerCheck.DefaultOwner);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Owner owner = owner1;
            
            owner.OwnerName = ownerNameChange;

            ownersRepository.Save(owner);

            Owner ownerCheck = ownersRepository.GetById(owner1.PrimaryKey);

            Assert.IsNotNull(ownerCheck);
            Assert.AreEqual(owner1.PrimaryKey, ownerCheck.PrimaryKey);
            Assert.AreEqual(ownerNameChange, ownerCheck.OwnerName);
        }

        [TestMethod]
        public void A030_DeleteTest()
        {
            string pk = owner1.PrimaryKey;

            ownersRepository.Delete(pk);

            Owner ownerCheck = ownersRepository.GetById(pk);

            Assert.IsNull(ownerCheck);
        }

        [TestMethod]
        public void A040_Add3Owners()
        {
            ownersRepository.Save(owner1);
            ownersRepository.Save(owner2);
            ownersRepository.Save(owner3);
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<Owner> owners = ownersRepository.GetList();

            Assert.AreEqual(numberOfOwners, owners.Count);
        }

        [TestMethod]
        public void A055_GetListOrdered()
        {
            List<Owner> owners = ownersRepository.GetList(true);

            Assert.AreEqual(numberOfOwners, owners.Count);
            Assert.AreEqual(owner1.PrimaryKey, owners[0].PrimaryKey);
        }

        [TestMethod]
        public void A060_GetByName()
        {
            Owner foundOwner = ownersRepository.GetByName(owner3.OwnerName);

            Assert.AreEqual(owner3.PrimaryKey, foundOwner.PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByNameBadOwner()
        {
            Owner foundOwner = ownersRepository.GetByName(badOwnerName);

            Assert.IsNull(foundOwner);
        }

        [TestMethod]
        public void A080_GetDefaultOwner()
        {
            Owner foundOwner = ownersRepository.GetDefaultOwner();

            Assert.AreEqual(owner2.PrimaryKey, foundOwner.PrimaryKey);
        }

        [TestMethod]
        public void A090_GetAllDefaultOwners()
        {
            List<Owner> owners = ownersRepository.GetAllDefaultOwners();

            Assert.AreEqual(numberOfDefaultOwners, owners.Count);
        }

        [TestMethod]
        public void A100_GetMaxKey()
        {
            string maxKey = ownersRepository.GetMaxKey();

            Assert.AreEqual(maxOwnKey, maxKey);
        }

        [TestMethod]
        public void A110_Delete3Owners()
        {
            ownersRepository.Delete(owner1.PrimaryKey);
            ownersRepository.Delete(owner2.PrimaryKey);
            ownersRepository.Delete(owner3.PrimaryKey);
        }
    }
}
