using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using GTSport_DT.Cars;
using static GTSport_DT_Testing.Cars.CarsForTesting;

namespace GTSport_DT_Testing.Manufacturers
{
    [TestClass]
    public class ManufacturerValidationTests : TestBase
    {
        private static ManufacturerValidation manufacturerValidation;
        private static ManufacturersRepository manufacturersRepository;
        private static CountriesRepository countriesRepository;
        private static RegionsRepository regionsRepository;
        private static CarsRepository carsRepository;

        private const string badCountryKey = "C!X999999999";
        private const string badManufactureKey = "D!X999999999";

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            manufacturerValidation = new ManufacturerValidation(con);
            carsRepository = new CarsRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
            regionsRepository.Flush();

            countriesRepository.Save(Country1);
            countriesRepository.Save(Country2);
            countriesRepository.Save(Country3);
            countriesRepository.Flush();

            manufacturersRepository.Save(Manufacturer1);
            manufacturersRepository.Save(Manufacturer2);
            manufacturersRepository.Save(Manufacturer3);
            manufacturersRepository.Flush();

            carsRepository.SaveAndFlush(Car2);
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                carsRepository.DeleteAndFlush(Car2.PrimaryKey);

                manufacturersRepository.Refresh();
                manufacturersRepository.Delete(Manufacturer1.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer2.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer3.PrimaryKey);
                manufacturersRepository.Flush();

                countriesRepository.Refresh();
                countriesRepository.Delete(Country1.PrimaryKey);
                countriesRepository.Delete(Country2.PrimaryKey);
                countriesRepository.Delete(Country3.PrimaryKey);
                countriesRepository.Flush();

                regionsRepository.Refresh();
                regionsRepository.Delete(Region1.PrimaryKey);
                regionsRepository.Delete(Region2.PrimaryKey);
                regionsRepository.Delete(Region3.PrimaryKey);
                regionsRepository.Flush();

                con.Close();
            }
        }

        [TestMethod]
        public void A010_ValidateSave()
        {
            manufacturerValidation.ValidateSave(Manufacturer1);
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNameNotSetException))]
        public void A020_ValidateSaveNameNotSet()
        {
            Manufacturer manufacturer = new Manufacturer(Manufacturer1.PrimaryKey, "", Manufacturer1.CountryKey);

            try
            {
                manufacturerValidation.ValidateSave(manufacturer);
            } catch (ManufacturerNameNotSetException mnnse)
            {
                Assert.AreEqual(ManufacturerNameNotSetException.ManufacturerNameNotSetMsg, mnnse.Message);
                throw mnnse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerCountryKeyNotSetException))]
        public void A030_ValidateSaveCountryKeyNotSet()
        {
            Manufacturer manufacturer = new Manufacturer(Manufacturer1.PrimaryKey, Manufacturer1.Name, "");

            try
            {
                manufacturerValidation.ValidateSave(manufacturer);
            } catch (ManufacturerCountryKeyNotSetException mcknse)
            {
                Assert.AreEqual(ManufacturerCountryKeyNotSetException.ManufacturerCountryKeyNotSetMsg, mcknse.Message);
                throw mcknse;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNameAlreadyExistsException))]
        public void A040_ValidateSaveNameAlreadyExists()
        {
            Manufacturer manufacturer = new Manufacturer("", Manufacturer1.Name, Manufacturer1.CountryKey);

            try
            {
                manufacturerValidation.ValidateSave(manufacturer);
            } catch (ManufacturerNameAlreadyExistsException mnaee)
            {
                Assert.AreEqual(ManufacturerNameAlreadyExistsException.ManufacturerNameAlreadyExistsMsg, mnaee.Message);
                throw mnaee;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(CountryNotFoundException))]
        public void A050_ValidateSaveBadCountryKey()
        {
            Manufacturer manufacturer = new Manufacturer(Manufacturer1.PrimaryKey, Manufacturer1.Name, badCountryKey);

            try
            {
                manufacturerValidation.ValidateSave(manufacturer);
            } catch (CountryNotFoundException cnfe)
            {
                Assert.AreEqual(CountryNotFoundException.CountryKeyNotFoundMsg, cnfe.Message);
                throw cnfe;
            }
        }

        [TestMethod]
        public void A060_ValidateDelete()
        {
            manufacturerValidation.ValidateDelete(Manufacturer3.PrimaryKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerNotFoundException))]
        public void A070_ValidateDeleteBadManufacturerKey()
        {
            try
            {
                manufacturerValidation.ValidateDelete(badManufactureKey);
            } catch (ManufacturerNotFoundException mnfe)
            {
                Assert.AreEqual(ManufacturerNotFoundException.ManufacturerKeyNotFoundMsg, mnfe.Message);
                throw mnfe;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ManufacturerInUseException))]
        public void A080_ValidateDeleteInUse()
        {
            try
            {
                manufacturerValidation.ValidateDelete(Manufacturer2.PrimaryKey);
            } catch (ManufacturerInUseException miue)
            {
                Assert.AreEqual(ManufacturerInUseException.ManufacturerInUseCanNotBeDeletedCarMsg, miue.Message);
                throw miue;
            }
        }
    }
}
