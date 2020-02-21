using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Owners.OwnersForTesting;
using static GTSport_DT_Testing.Cars.CarsForTesting;
using GTSport_DT.OwnerCars;

namespace GTSport_DT_Testing.OwnerCars
{
    public class OwnerCarsForTesting
    {
        private const string ownerCar1Key = "OWC900000001";
        private static readonly string ownerCar1OwnerKey = Owner1.PrimaryKey;
        private static readonly string ownerCar1CarKey = Car1.PrimaryKey;
        private static readonly string ownerCar1CarID = Car1.Name + "_01";
        private const string ownerCar1PaintJob = "Red";
        private static readonly int ownerCar1MaxPower = Car1.MaxPower;
        private const int ownerCar1PowerLevel = 0;
        private const int ownerCar1WeightReductionLevel = 0;
        private static readonly DateTime ownerCar1AcquiredDate = new DateTime(2017, 08, 05);

        private const string ownerCar2Key = "OWC900000002";
        private static readonly string ownerCar2OwnerKey = Owner2.PrimaryKey;
        private static readonly string ownerCar2CarKey = Car2.PrimaryKey;
        private static readonly string ownerCar2CarID = Car2.Name + "_02";
        private const string ownerCar2PaintJob = "Blue";
        private static readonly int ownerCar2MaxPower = Car2.MaxPower + 50;
        private const int ownerCar2PowerLevel = 1;
        private const int ownerCar2WeightReductionLevel = 2;
        private static readonly DateTime ownerCar2AcquiredDate = new DateTime(2017, 07, 15);

        private const string ownerCar3Key = "OWC900000003";
        private static readonly string ownerCar3OwnerKey = Owner1.PrimaryKey;
        private static readonly string ownerCar3CarKey = Car3.PrimaryKey;
        private static readonly string ownerCar3CarID = Car3.Name + "_03";
        private const string ownerCar3PaintJob = "Silver";
        private static readonly int ownerCar3MaxPower = Car3.MaxPower;
        private const int ownerCar3PowerLevel = 0;
        private const int ownerCar3WeightReductionLevel = 1;
        private static readonly DateTime ownerCar3AcquiredDate = new DateTime(2016, 12, 25);

        public static readonly OwnerCar OwnerCar1 = new OwnerCar(ownerCar1Key, ownerCar1OwnerKey, ownerCar1CarKey,
            ownerCar1CarID, ownerCar1PaintJob, ownerCar1MaxPower, ownerCar1PowerLevel, ownerCar1WeightReductionLevel, ownerCar1AcquiredDate);

        public static readonly OwnerCar OwnerCar2 = new OwnerCar(ownerCar2Key, ownerCar2OwnerKey, ownerCar2CarKey,
            ownerCar2CarID, ownerCar2PaintJob, ownerCar2MaxPower, ownerCar2PowerLevel, ownerCar2WeightReductionLevel, ownerCar2AcquiredDate);

        public static readonly OwnerCar OwnerCar3 = new OwnerCar(ownerCar3Key, ownerCar3OwnerKey, ownerCar3CarKey,
            ownerCar3CarID, ownerCar3PaintJob, ownerCar3MaxPower, ownerCar3PowerLevel, ownerCar3WeightReductionLevel, ownerCar3AcquiredDate);
    }
}
