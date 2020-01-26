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
    public class OwnersServiceTests : TestBase
    {
        private static OwnersRepository ownersRepository;
        private static OwnersService ownersService;

        private const string Owner4Name = "XXX_Test_Owner_4_XXX";
        private const Boolean Owner4Default = false;
        private const string Owner4NewName = "XXX_New_Owner_4_XXX";

        private const string BadKey = "ZXZ_!_001";
        private const string BadName = "JFUEKNCK DFJI";

        private const string Owner4PrimaryKeyExpectedAfterReset = "OWN900000004";

        private static string Owner4PrimaryKey = "";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            ownersRepository = new OwnersRepository(con);
            ownersService = new OwnersService(con);

            ownersRepository.Save(owner1);
            ownersRepository.Save(owner2);
            ownersRepository.Save(owner3);
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            if (con != null)
            {
                ownersRepository.Delete(owner1.PrimaryKey);
                ownersRepository.Delete(owner2.PrimaryKey);
                ownersRepository.Delete(owner3.PrimaryKey);
                ownersRepository.Delete(Owner4PrimaryKey);

                con.Close();
            }
        }

        [TestMethod]
        public void A010_SaveNewOwner()
        {
            Owner owner = new Owner("", Owner4Name, Owner4Default);

            ownersService.Save(ref owner);

            Owner4PrimaryKey = owner.PrimaryKey;
        }

        [TestMethod]
        public void A020_SaveUpdateOwner()
        {
            Owner owner = new Owner(Owner4PrimaryKey, Owner4NewName, Owner4Default);

            ownersService.Save(ref owner);
        }

        [TestMethod]
        public void A030_DeleteOwner()
        {
            ownersService.Delete(Owner4PrimaryKey);
        }

        [TestMethod]
        public void A040_ResetKey()
        {
            ownersService.ResetKey();

            Owner owner = new Owner("", Owner4Name, Owner4Default);

            ownersService.Save(ref owner);

            Owner4PrimaryKey = owner.PrimaryKey;

            Assert.AreEqual(Owner4PrimaryKeyExpectedAfterReset, owner.PrimaryKey);
        }

        [TestMethod]
        public void A050_GetOwnerByKey()
        {
            Owner owner = ownersService.GetByKey(owner3.PrimaryKey);

            Assert.AreEqual(owner3.OwnerName, owner.OwnerName);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNotFoundException))]
        public void A060_GetOwnerByKeyBadKey()
        {
            try
            {
                Owner owner = ownersService.GetByKey(BadKey);
            }
            catch (OwnerNotFoundException onfe)
            {
                Assert.AreEqual(OwnerNotFoundException.OwnerKeyNotFoundMsg, onfe.Message);
                throw onfe;
            }
        }

        [TestMethod]
        public void A070_GetOwnerByName()
        {
            Owner owner = ownersService.GetByName(owner2.OwnerName);

            Assert.AreEqual(owner2.PrimaryKey, owner.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNotFoundException))]
        public void A080_GetOwnerByNameBadName()
        {
            try
            {
                Owner owner = ownersService.GetByName(BadName);
            }
            catch (OwnerNotFoundException onfe)
            {
                Assert.AreEqual(OwnerNotFoundException.OwnerNameNotFoundMsg, onfe.Message);
                throw onfe;
            }
        }

        [TestMethod]
        public void A090_GetDefaultOwner()
        {
            Owner owner = ownersService.GetDefaultOwner();

            Assert.AreEqual(owner2.PrimaryKey, owner.PrimaryKey);
        }

        [TestMethod]
        public void A100_GetDefaultOwner_NoOwnerIsDefaultButDefaultOwnerNameExists()
        {
            DeleteDefaultOwner();

            ClearDefaultOwners();

            Owner defaultOwner = new Owner();
            defaultOwner.OwnerName = OwnersService.DefaultOwnerName;
            defaultOwner.DefaultOwner = false;

            ownersService.Save(ref defaultOwner);

            Owner owner = ownersService.GetDefaultOwner();

            Assert.AreEqual(OwnersService.DefaultOwnerName, owner.OwnerName);

            DeleteDefaultOwner();
        }

        [TestMethod]
        public void A110_GetDefaultOwner_NoOwnerIsDefaultAndDefaultOwnerNameNoExists()
        {
            DeleteDefaultOwner();

            ClearDefaultOwners();

            Owner owner = ownersService.GetDefaultOwner();

            Assert.AreEqual(OwnersService.DefaultOwnerName, owner.OwnerName);

            DeleteDefaultOwner();
        }

        [TestMethod]
        public void A120_SaveNewDefaultOwner()
        {
            ClearDefaultOwners();

            Owner ownerNo3 = new Owner(owner3.PrimaryKey, owner3.OwnerName, true);
            ownersRepository.Save(ownerNo3);

            Owner ownerNo2 = new Owner(owner2.PrimaryKey, owner2.OwnerName, true);

            ownersService.Save(ref ownerNo2);

            Owner defaultOwner = ownersService.GetDefaultOwner();

            Assert.AreEqual(owner2.PrimaryKey, defaultOwner.PrimaryKey);
        }

        private void DeleteDefaultOwner()
        {
            Owner owner = ownersRepository.GetByName(OwnersService.DefaultOwnerName);

            if (owner != null)
            {
                ownersRepository.Delete(owner.PrimaryKey);
            }
        }

        private void ClearDefaultOwners()
        {
            List<Owner> defaultOwners = ownersRepository.GetAllDefaultOwners();

            foreach(Owner owner in defaultOwners)
            {
                owner.DefaultOwner = false;

                ownersRepository.Save(owner);
            }
        }
    }
}
