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
    public class OwnerValidationTests : TestBase
    {
        private const string BadKey = "ZXZ_!_001";

        private static OwnerValidation ownerValidation;

        private static OwnersRepository ownersRepository;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            ownerValidation = new OwnerValidation(con);

            ownersRepository = new OwnersRepository(con);

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
                ownersRepository.Refresh();
                ownersRepository.Delete(Owner1.PrimaryKey);
                ownersRepository.Delete(Owner2.PrimaryKey);
                ownersRepository.Delete(Owner3.PrimaryKey);
                ownersRepository.Flush();

                con.Close();
            }
        }

        [TestMethod]
        public void A010_ValidateSave()
        {
            Owner owner = new Owner(Owner1.PrimaryKey, Owner1.OwnerName, Owner1.DefaultOwner);

            ownerValidation.ValidateSave(owner);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNameNotSetException))]
        public void A020_ValidateSaveNameNotSet()
        {
            try
            {
                Owner owner = new Owner("", "", Owner1.DefaultOwner);

                ownerValidation.ValidateSave(owner);
            } catch (OwnerNameNotSetException onnse) 
            {
                Assert.AreEqual(OwnerNameNotSetException.OwnerNameNotSetMsg, onnse.Message);
                throw onnse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNameAlreadyExistsException))]
        public void A030_ValidateSaveNameAlreadyExists()
        {
            try
            {
                Owner owner = new Owner("", Owner1.OwnerName, Owner1.DefaultOwner);

                ownerValidation.ValidateSave(owner);
            } catch (OwnerNameAlreadyExistsException onaee)
            {
                Assert.AreEqual(OwnerNameAlreadyExistsException.OwnerNameAlreadyExistsMsg, onaee.Message);
                throw onaee;
            }
        }

        [TestMethod]
        public void A040_ValidateDelete()
        {
            ownerValidation.ValidateDelete(Owner2.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(OwnerNotFoundException))]
        public void ValidateDeleteBadKey()
        {
            try
            {
                ownerValidation.ValidateDelete(BadKey);
            } catch (OwnerNotFoundException onfe)
            {
                Assert.AreEqual(OwnerNotFoundException.OwnerKeyNotFoundMsg, onfe.Message);
                throw onfe;
            }
        }
    }
}
