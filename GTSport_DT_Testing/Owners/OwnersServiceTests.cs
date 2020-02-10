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

        private const int NumberOfListRecordsExpected = 4;

        private static string Owner4PrimaryKey = "";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            ownersRepository = new OwnersRepository(con);
            ownersService = new OwnersService(con);

            ownersRepository.Save(Owner1);
            ownersRepository.Save(Owner2);
            ownersRepository.Save(Owner3);
            ownersRepository.Flush();

        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                DeleteDefaultOwner();

                ownersRepository.Refresh();
                ownersRepository.Delete(Owner1.PrimaryKey);
                ownersRepository.Delete(Owner2.PrimaryKey);
                ownersRepository.Delete(Owner3.PrimaryKey);
                ownersRepository.Delete(Owner4PrimaryKey);
                ownersRepository.Flush();

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
            Owner owner = ownersService.GetByKey(Owner3.PrimaryKey);

            Assert.AreEqual(Owner3.OwnerName, owner.OwnerName);
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
        public void A065_GetOwnerList()
        {
            List<Owner> owners = ownersService.GetList();

            Assert.AreEqual(NumberOfListRecordsExpected, owners.Count);
            Assert.AreEqual(Owner1.PrimaryKey, owners[0].PrimaryKey);
        }

        [TestMethod]
        public void A067_GetOwnerListOrdered()
        {
            List<Owner> owners = ownersService.GetList(true);

            Assert.AreEqual(NumberOfListRecordsExpected, owners.Count);
            Assert.AreEqual(Owner1.PrimaryKey, owners[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetOwnerByName()
        {
            Owner owner = ownersService.GetByName(Owner2.OwnerName);

            Assert.AreEqual(Owner2.PrimaryKey, owner.PrimaryKey);
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

            Assert.AreEqual(Owner2.PrimaryKey, owner.PrimaryKey);
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

            Owner ownerNo3 = new Owner(Owner3.PrimaryKey, Owner3.OwnerName, true);
            ownersRepository.SaveAndFlush(ownerNo3);

            Owner ownerNo2 = new Owner(Owner2.PrimaryKey, Owner2.OwnerName, true);

            ownersService.Save(ref ownerNo2);

            Owner defaultOwner = ownersService.GetDefaultOwner();

            Assert.AreEqual(Owner2.PrimaryKey, defaultOwner.PrimaryKey);
        }

        private void DeleteDefaultOwner()
        {
            Owner owner = ownersRepository.GetByName(OwnersService.DefaultOwnerName);

            if (owner != null)
            {
                ownersRepository.Refresh();
                ownersRepository.DeleteAndFlush(owner.PrimaryKey);
            }
        }

        private void ClearDefaultOwners()
        {
            List<Owner> defaultOwners = ownersRepository.GetAllDefaultOwners();

            ownersRepository.Refresh();
            foreach(Owner owner in defaultOwners)
            {
                owner.DefaultOwner = false;

                ownersRepository.Save(owner);
            }
            ownersRepository.Flush();
        }
    }
}
