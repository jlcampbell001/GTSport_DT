﻿using GTSport_DT.Owners;
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
        private readonly string maxOwnKey = Owner3.PrimaryKey;

        private const int numberOfOwners = 3;
        private const int numberOfDefaultOwners = 1;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            ownersRepository = new OwnersRepository(con);
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
            ownersRepository.SaveAndFlush(Owner1);

            Owner ownerCheck = ownersRepository.GetById(Owner1.PrimaryKey);

            Assert.IsNotNull(ownerCheck);
            Assert.AreEqual(Owner1.PrimaryKey, ownerCheck.PrimaryKey);
            Assert.AreEqual(Owner1.OwnerName, ownerCheck.OwnerName);
            Assert.AreEqual(Owner1.DefaultOwner, ownerCheck.DefaultOwner);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Owner owner = Owner1;
            
            owner.OwnerName = ownerNameChange;

            ownersRepository.SaveAndFlush(owner);

            Owner ownerCheck = ownersRepository.GetById(Owner1.PrimaryKey);

            Assert.IsNotNull(ownerCheck);
            Assert.AreEqual(Owner1.PrimaryKey, ownerCheck.PrimaryKey);
            Assert.AreEqual(ownerNameChange, ownerCheck.OwnerName);
        }

        [TestMethod]
        public void A030_DeleteTest()
        {
            string pk = Owner1.PrimaryKey;

            ownersRepository.DeleteAndFlush(pk);

            Owner ownerCheck = ownersRepository.GetById(pk);

            Assert.IsNull(ownerCheck);
        }

        [TestMethod]
        public void A040_Add3Owners()
        {
            ownersRepository.Save(Owner1);
            ownersRepository.Save(Owner2);
            ownersRepository.Save(Owner3);
            ownersRepository.Flush();

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
            Assert.AreEqual(Owner1.PrimaryKey, owners[0].PrimaryKey);
        }

        [TestMethod]
        public void A060_GetByName()
        {
            Owner foundOwner = ownersRepository.GetByName(Owner3.OwnerName);

            Assert.AreEqual(Owner3.PrimaryKey, foundOwner.PrimaryKey);
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

            Assert.AreEqual(Owner2.PrimaryKey, foundOwner.PrimaryKey);
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
            ownersRepository.Delete(Owner1.PrimaryKey);
            ownersRepository.Delete(Owner2.PrimaryKey);
            ownersRepository.Delete(Owner3.PrimaryKey);
            ownersRepository.Flush();
        }
    }
}
