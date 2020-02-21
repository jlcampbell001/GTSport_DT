using GTSport_DT.General;
using System;

namespace GTSport_DT.OwnerCars
{
    public class OwnerCar : Entity
    {
        public DateTime AcquiredDate { get; set; }
        public string CarID { get; set; }
        public string CarKey { get; set; }
        public string PaintJob { get; set; }
        public int MaxPower { get; set; }
        public string OwnerKey { get; set; }
        public int PowerLevel { get; set; }
        public int WeightReductionLevel { get; set; }

        public OwnerCar()
        {
        }

        public OwnerCar(string primaryKey, string ownerKey, string carKey, string carID, string paintJob, int maxPower,
            int powerLevel, int weightReductionLevel, DateTime acquiredDate)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            OwnerKey = ownerKey ?? throw new ArgumentNullException(nameof(ownerKey));
            CarKey = carKey ?? throw new ArgumentNullException(nameof(carKey));
            CarID = carID ?? throw new ArgumentNullException(nameof(carID));
            PaintJob = paintJob ?? throw new ArgumentNullException(nameof(paintJob));
            MaxPower = maxPower;
            PowerLevel = powerLevel;
            WeightReductionLevel = weightReductionLevel;
            AcquiredDate = acquiredDate;
        }

        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Owner Key = '" + OwnerKey + "', Car Key = '" + CarKey
                            + "', Car ID = '" + CarID + "', Paint Job = '" + PaintJob+ "', Max Power = " + MaxPower + ", Power Level = " + PowerLevel
                            + "', Weight Reduction Level = " + WeightReductionLevel + ", Date Acquired = '" + AcquiredDate + "}";
            return line;
        }
    }
}