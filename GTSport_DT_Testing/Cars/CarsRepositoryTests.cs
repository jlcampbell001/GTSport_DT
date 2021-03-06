﻿using GTSport_DT.Cars;
using GTSport_DT.Countries;
using GTSport_DT.Manufacturers;
using GTSport_DT.Regions;
using GTSport_DT_Testing.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;
using static GTSport_DT_Testing.Countries.CountriesForTesting;
using static GTSport_DT_Testing.Manufacturers.ManufacturersForTesting;
using static GTSport_DT_Testing.Cars.CarsForTesting;

namespace GTSport_DT_Testing.Cars
{
    [TestClass]
    public class CarsRepositoryTests : TestBase
    {
        private static RegionsRepository regionsRepository;
        private static CountriesRepository countriesRepository;
        private static ManufacturersRepository manufacturersRepository;
        private static CarsRepository carsRepository;

        private static readonly string expectedMaxKey = Car15.PrimaryKey;
        private static readonly CarCategory.Category categoryMinRange = CarCategory.N500;
        private static readonly CarCategory.Category categoryMaxRange = CarCategory.N700;
        private static readonly string driveTrainTest = DriveTrain.FR;
        private static readonly string manufacturerName = Manufacturer2.Name;
        private static readonly string countryDescription = Country2.Description;
        private static readonly string regionDescription = Region2.Description;
        private static readonly CarCategory.Category multipuleCriteriaCategoryMinRange = CarCategory.N100;
        private static readonly string multipuleCriteriaDriveTrainTest = DriveTrain.FF;

        private const string nameChange = "Name Changed";
        private const string badName = "BAD!Name";
        
        private const int numberOfCars = 15;
        private const int numberOfCarsForManufacturer = 2;
        private const int numberOfCarsCategoryStatRows = 14;
        private const int expectedN400Row = 3;
        private const int N400NumberOfCars = 2;
        private const int numberOfCategoryRangeRecords = 3;
        private const int numberOfCategoryMinRecords = 10;
        private const int numberOfCategoryMaxRecords = 8;
        private const int yearMinRange = 1972;
        private const int yearMaxRange = 2013;
        private const int numberOfYearRangeRecords = 4;
        private const int numberOfYearMinRecords = 11;
        private const int numberOfYearMaxRecords = 8;
        private const int maxPowerMinRange = 500;
        private const int maxPowerMaxRange = 900;
        private const int numberOfMaxPowerRangeRecords = 7;
        private const int numberOfMaxPowerMinRecords = 8;
        private const int numberOfMaxPowerMaxRecords = 14;
        private const int numberOfDriveTrainRecords = 3;
        private const int numberOfManufacturerRecords = 1;
        private const int numberOfCountryRecords = 3;
        private const int numberOfRegionRecords = 10;
        private const int numberOfMultipuleCriteriaRecords = 1;
        private const int multipuleCriteriaYearMaxRange = 2020;
        private const int multipuleCriteriaMaxPowerMinRange = 100;

        private const double N400AvgMaxPower = 394.5;
        private const double N400AvgPrice = 54155.00;
        private const double N400AvgMaxSpeed = 6.85;
        private const double N400AvgAcceleration = 4.3;
        private const double N400AvgBraking = 1.75;
        private const double N400AvgCornering = 1.5499999999999998;
        private const double N400AvgStability = 4.85;



        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            con = new NpgsqlConnection(cs);
            con.Open();

            regionsRepository = new RegionsRepository(con);
            countriesRepository = new CountriesRepository(con);
            manufacturersRepository = new ManufacturersRepository(con);
            carsRepository = new CarsRepository(con);

            regionsRepository.Save(Region1);
            regionsRepository.Save(Region2);
            regionsRepository.Save(Region3);
            regionsRepository.Flush();

            countriesRepository.Save(Country1);
            countriesRepository.Save(Country2);
            countriesRepository.Save(Country3);
            countriesRepository.Save(Country4);
            countriesRepository.Save(Country5);
            countriesRepository.Flush();

            manufacturersRepository.Save(Manufacturer1);
            manufacturersRepository.Save(Manufacturer2);
            manufacturersRepository.Save(Manufacturer3);
            manufacturersRepository.Save(Manufacturer4);
            manufacturersRepository.Save(Manufacturer5);
            manufacturersRepository.Save(Manufacturer6);
            manufacturersRepository.Save(Manufacturer7);
            manufacturersRepository.Save(Manufacturer8);
            manufacturersRepository.Save(Manufacturer9);
            manufacturersRepository.Flush();
        }

        [TestMethod]
        public void ZZZZ_ClassCleanUp()
        {
            if (con != null)
            {
                manufacturersRepository.Refresh();
                manufacturersRepository.Delete(Manufacturer1.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer2.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer3.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer4.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer5.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer6.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer7.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer8.PrimaryKey);
                manufacturersRepository.Delete(Manufacturer9.PrimaryKey);
                manufacturersRepository.Flush();

                countriesRepository.Refresh();
                countriesRepository.Delete(Country1.PrimaryKey);
                countriesRepository.Delete(Country2.PrimaryKey);
                countriesRepository.Delete(Country3.PrimaryKey);
                countriesRepository.Delete(Country4.PrimaryKey);
                countriesRepository.Delete(Country5.PrimaryKey);
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
        public void A010_SaveNewTest()
        {
            carsRepository.SaveAndFlush(Car1);

            Car carCheck = carsRepository.GetById(Car1.PrimaryKey);

            Assert.IsNotNull(carCheck);
            Assert.AreEqual(Car1.PrimaryKey, carCheck.PrimaryKey);
            Assert.AreEqual(Car1.Name, carCheck.Name);
            Assert.AreEqual(Car1.ManufacturerKey, carCheck.ManufacturerKey);
            Assert.AreEqual(Car1.Year, carCheck.Year);
            Assert.AreEqual(Car1.Category, carCheck.Category);
            Assert.AreEqual(Car1.Price, carCheck.Price);
            Assert.AreEqual(Car1.DisplacementCC, carCheck.DisplacementCC);
            Assert.AreEqual(Car1.MaxPower, carCheck.MaxPower);
            Assert.AreEqual(Car1.PowerRPM, carCheck.PowerRPM);
            Assert.AreEqual(Car1.TorqueFTLB, carCheck.TorqueFTLB);
            Assert.AreEqual(Car1.TorqueRPM, carCheck.TorqueRPM);
            Assert.AreEqual(Car1.DriveTrain, carCheck.DriveTrain);
            Assert.AreEqual(Car1.Aspiration, carCheck.Aspiration);
            Assert.AreEqual(Car1.Length, carCheck.Length);
            Assert.AreEqual(Car1.Width, carCheck.Width);
            Assert.AreEqual(Car1.Height, carCheck.Height);
            Assert.AreEqual(Car1.Weight, carCheck.Weight);
            Assert.AreEqual(Car1.MaxSpeed, carCheck.MaxSpeed);
            Assert.AreEqual(Car1.Acceleration, carCheck.Acceleration);
            Assert.AreEqual(Car1.Braking, carCheck.Braking);
            Assert.AreEqual(Car1.Cornering, carCheck.Cornering);
            Assert.AreEqual(Car1.Stability, carCheck.Stability);
        }

        [TestMethod]
        public void A020_SaveUpdateTest()
        {
            Car car = new Car(Car1.PrimaryKey, nameChange, Car1.ManufacturerKey, Car1.Year, Car1.Category, Car1.Price, Car1.DisplacementCC,
            Car1.MaxPower, Car1.PowerRPM, Car1.TorqueFTLB, Car1.TorqueRPM, Car1.DriveTrain, Car1.Aspiration, Car1.Length, Car1.Width,
            Car1.Height, Car1.Weight, Car1.MaxSpeed, Car1.Acceleration, Car1.Braking, Car1.Cornering, Car1.Stability);

            carsRepository.SaveAndFlush(car);

            Car carCheck = carsRepository.GetById(Car1.PrimaryKey);

            Assert.IsNotNull(carCheck);
            Assert.AreEqual(Car1.PrimaryKey, carCheck.PrimaryKey);
            Assert.AreEqual(nameChange, carCheck.Name);
            Assert.AreEqual(Car1.ManufacturerKey, carCheck.ManufacturerKey);
            Assert.AreEqual(Car1.Year, carCheck.Year);
            Assert.AreEqual(Car1.Category, carCheck.Category);
            Assert.AreEqual(Car1.Price, carCheck.Price);
            Assert.AreEqual(Car1.DisplacementCC, carCheck.DisplacementCC);
            Assert.AreEqual(Car1.MaxPower, carCheck.MaxPower);
            Assert.AreEqual(Car1.PowerRPM, carCheck.PowerRPM);
            Assert.AreEqual(Car1.TorqueFTLB, carCheck.TorqueFTLB);
            Assert.AreEqual(Car1.TorqueRPM, carCheck.TorqueRPM);
            Assert.AreEqual(Car1.DriveTrain, carCheck.DriveTrain);
            Assert.AreEqual(Car1.Aspiration, carCheck.Aspiration);
            Assert.AreEqual(Car1.Length, carCheck.Length);
            Assert.AreEqual(Car1.Width, carCheck.Width);
            Assert.AreEqual(Car1.Height, carCheck.Height);
            Assert.AreEqual(Car1.Weight, carCheck.Weight);
            Assert.AreEqual(Car1.MaxSpeed, carCheck.MaxSpeed);
            Assert.AreEqual(Car1.Acceleration, carCheck.Acceleration);
            Assert.AreEqual(Car1.Braking, carCheck.Braking);
            Assert.AreEqual(Car1.Cornering, carCheck.Cornering);
            Assert.AreEqual(Car1.Stability, carCheck.Stability);
        }

        [TestMethod]
        public void A030_Delete()
        {
            carsRepository.DeleteAndFlush(Car1.PrimaryKey);

            Car carCheck = carsRepository.GetById(Car1.PrimaryKey);

            Assert.IsNull(carCheck);
        }

        [TestMethod]
        public void A040_Add15Cars()
        {
            carsRepository.Save(Car1);
            carsRepository.Save(Car2);
            carsRepository.Save(Car3);
            carsRepository.Save(Car4);
            carsRepository.Save(Car5);
            carsRepository.Save(Car6);
            carsRepository.Save(Car7);
            carsRepository.Save(Car8);
            carsRepository.Save(Car9);
            carsRepository.Save(Car10);
            carsRepository.Save(Car11);
            carsRepository.Save(Car12);
            carsRepository.Save(Car13);
            carsRepository.Save(Car14);
            carsRepository.Save(Car15);
            carsRepository.Flush();
        }

        [TestMethod]
        public void A050_GetList()
        {
            List<Car> cars = carsRepository.GetList();

            Assert.AreEqual(numberOfCars, cars.Count);
        }

        [TestMethod]
        public void A060_GetListOrdered()
        {
            List<Car> cars = carsRepository.GetList(orderedList: true);

            Assert.AreEqual(numberOfCars, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A070_GetByName()
        {
            Car car = carsRepository.GetByName(Car3.Name);

            Assert.AreEqual(Car3.PrimaryKey, car.PrimaryKey);
        }

        [TestMethod]
        public void A080_GetByNameBadName()
        {
            Car car = carsRepository.GetByName(badName);

            Assert.IsNull(car);
        }

        [TestMethod]
        public void A090_GetMaxKey()
        {
            string maxKey = carsRepository.GetMaxKey();

            Assert.AreEqual(expectedMaxKey, maxKey);
        }

        [TestMethod]
        public void A100_GetListForManufacture()
        {
            List<Car> cars = carsRepository.GetListForManufacturerKey(Manufacturer1.PrimaryKey);

            Assert.AreEqual(numberOfCarsForManufacturer, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A110_GetListForManufactureOrdered()
        {
            List<Car> cars = carsRepository.GetListForManufacturerKey(Manufacturer1.PrimaryKey, orderedList: true);

            Assert.AreEqual(numberOfCarsForManufacturer, cars.Count);
            Assert.AreEqual(Car10.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A120_GetCarStatistics()
        {
            List<CarStatistic> carStatistics = carsRepository.GetCarStatistics();

            Assert.AreEqual(numberOfCarsCategoryStatRows, carStatistics.Count);
            Assert.AreEqual(CarCategory.N400, carStatistics[expectedN400Row].Category);
            Assert.AreEqual(N400NumberOfCars, carStatistics[expectedN400Row].NumberOfCars);
            Assert.AreEqual(N400AvgMaxPower, carStatistics[expectedN400Row].AvgMaxPower);
            Assert.AreEqual(N400AvgMaxSpeed, carStatistics[expectedN400Row].AvgMaxSpeed);
            Assert.AreEqual(N400AvgAcceleration, carStatistics[expectedN400Row].AvgAcceleration);
            Assert.AreEqual(N400AvgBraking, carStatistics[expectedN400Row].AvgBraking);
            Assert.AreEqual(N400AvgCornering, carStatistics[expectedN400Row].AvgCornering);
            Assert.AreEqual(N400AvgStability, carStatistics[expectedN400Row].AvgStability);
            Assert.AreEqual(N400AvgPrice, carStatistics[expectedN400Row].AvgPrice);
        }

        [TestMethod]
        public void A130_GetListByCriteriaNoCriteria()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfCars, cars.Count);
        }

        [TestMethod]
        public void A140_GetListByCriteriaNoCriteriaOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCars, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A150_GetListByCriteriaCategoryRange()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfCategoryRangeRecords, cars.Count);
            Assert.AreEqual(Car5.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A160_GetListByCriteriaCategoryRangeOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCategoryRangeRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A170_GetListByCriteriaCategoryMin()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfCategoryMinRecords, cars.Count);
            Assert.AreEqual(Car5.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A180_GetListByCriteriaCategoryMinOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = categoryMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCategoryMinRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A190_GetListByCriteriaCategoryMax()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryTo = categoryMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfCategoryMaxRecords, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A200_GetListByCriteriaCategoryMaxOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryTo = categoryMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCategoryMaxRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A210_GetListByCriteriaYearRange()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = yearMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfYearRangeRecords, cars.Count);
            Assert.AreEqual(Car3.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A220_GetListByCriteriaYearRangeOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = yearMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfYearRangeRecords, cars.Count);
            Assert.AreEqual(Car4.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A230_GetListByCriteriaYearMin()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearFrom = yearMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfYearMinRecords, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A240_GetListByCriteriaYearMinOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearFrom = yearMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfYearMinRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A250_GetListByCriteriaYearMax()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearTo = yearMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfYearMaxRecords, cars.Count);
            Assert.AreEqual(Car3.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A260_GetListByCriteriaYearMaxOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.YearTo = yearMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfYearMaxRecords, cars.Count);
            Assert.AreEqual(Car12.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A270_GetListByCriteriaMaxPowerRange()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfMaxPowerRangeRecords, cars.Count);
            Assert.AreEqual(Car6.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A280_GetListByCriteriaMaxPowerRangeOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfMaxPowerRangeRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A290_GetListByCriteriaMaxPowerMin()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfMaxPowerMinRecords, cars.Count);
            Assert.AreEqual(Car6.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A300_GetListByCriteriaMaxPowerMinOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerFrom = maxPowerMinRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfMaxPowerMinRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A310_GetListByCriteriaMaxPowerMax()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfMaxPowerMaxRecords, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A320_GetListByCriteriaMaxPowerMaxOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfMaxPowerMaxRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A330_GetListByCriteriaDriveTrain()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.DriveTrain = driveTrainTest;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfDriveTrainRecords, cars.Count);
            Assert.AreEqual(Car1.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A340_GetListByCriteriaDriveTrainOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.DriveTrain = driveTrainTest;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfDriveTrainRecords, cars.Count);
            Assert.AreEqual(Car6.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A350_GetListByCriteriaManufacturer()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.ManufacturerName = manufacturerName;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfManufacturerRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A360_GetListByCriteriaManufacturerOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.ManufacturerName = manufacturerName;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfManufacturerRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A370_GetListByCriteriaCountry()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CountryDescription = countryDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfCountryRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A380_GetListByCriteriaCountryOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CountryDescription = countryDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfCountryRecords, cars.Count);
            Assert.AreEqual(Car5.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A390_GetListByCriteriaRegion()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfRegionRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A400_GetListByCriteriaRegionOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfRegionRecords, cars.Count);
            Assert.AreEqual(Car7.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A410_GetListByCriteriaMultipuleCriteria()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = multipuleCriteriaCategoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = multipuleCriteriaYearMaxRange;
            carSearchCriteria.MaxPowerFrom = multipuleCriteriaMaxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;
            carSearchCriteria.DriveTrain = multipuleCriteriaDriveTrainTest;
            carSearchCriteria.ManufacturerName = manufacturerName;
            carSearchCriteria.CountryDescription = countryDescription;
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria);

            Assert.AreEqual(numberOfMultipuleCriteriaRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A420_GetListByCriteriaMultipuleCriteriaOrdered()
        {
            CarSearchCriteria carSearchCriteria = new CarSearchCriteria();
            carSearchCriteria.CategoryFrom = multipuleCriteriaCategoryMinRange;
            carSearchCriteria.CategoryTo = categoryMaxRange;
            carSearchCriteria.YearFrom = yearMinRange;
            carSearchCriteria.YearTo = multipuleCriteriaYearMaxRange;
            carSearchCriteria.MaxPowerFrom = multipuleCriteriaMaxPowerMinRange;
            carSearchCriteria.MaxPowerTo = maxPowerMaxRange;
            carSearchCriteria.DriveTrain = multipuleCriteriaDriveTrainTest;
            carSearchCriteria.ManufacturerName = manufacturerName;
            carSearchCriteria.CountryDescription = countryDescription;
            carSearchCriteria.RegionDescription = regionDescription;

            List<Car> cars = carsRepository.GetListByCriteria(carSearchCriteria, orderedList: true);

            Assert.AreEqual(numberOfMultipuleCriteriaRecords, cars.Count);
            Assert.AreEqual(Car2.PrimaryKey, cars[0].PrimaryKey);
        }

        [TestMethod]
        public void A430_Delete15Cars()
        {
            carsRepository.Refresh();
            carsRepository.Delete(Car1.PrimaryKey);
            carsRepository.Delete(Car2.PrimaryKey);
            carsRepository.Delete(Car3.PrimaryKey);
            carsRepository.Delete(Car4.PrimaryKey);
            carsRepository.Delete(Car5.PrimaryKey);
            carsRepository.Delete(Car6.PrimaryKey);
            carsRepository.Delete(Car7.PrimaryKey);
            carsRepository.Delete(Car8.PrimaryKey);
            carsRepository.Delete(Car9.PrimaryKey);
            carsRepository.Delete(Car10.PrimaryKey);
            carsRepository.Delete(Car11.PrimaryKey);
            carsRepository.Delete(Car12.PrimaryKey);
            carsRepository.Delete(Car13.PrimaryKey);
            carsRepository.Delete(Car14.PrimaryKey);
            carsRepository.Delete(Car15.PrimaryKey);
            carsRepository.Flush();
        }
    }
}
