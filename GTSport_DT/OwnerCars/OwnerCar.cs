using GTSport_DT.General;
using System;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The entity representation of an owner car record.</summary>
    /// <seealso cref="GTSport_DT.General.Entity"/>
    public class OwnerCar : Entity
    {
        /// <summary>Gets or sets the acquired date.</summary>
        /// <value>The acquired date.</value>
        public DateTime AcquiredDate { get; set; }

        /// <summary>Gets or sets the car identifier.</summary>
        /// <value>The car identifier.</value>
        public string CarID { get; set; }

        /// <summary>Gets or sets the car key.</summary>
        /// <value>The car key.</value>
        public string CarKey { get; set; }

        /// <summary>Gets or sets the maximum power.</summary>
        /// <value>The maximum power.</value>
        public int MaxPower { get; set; }

        /// <summary>Gets or sets the owner key.</summary>
        /// <value>The owner key.</value>
        public string OwnerKey { get; set; }

        /// <summary>Gets or sets the paint job.</summary>
        /// <value>The paint job.</value>
        public string PaintJob { get; set; }

        /// <summary>Gets or sets the power level.</summary>
        /// <value>The power level.</value>
        public int PowerLevel { get; set; }

        /// <summary>Gets or sets the weight reduction level.</summary>
        /// <value>The weight reduction level.</value>
        public int WeightReductionLevel { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OwnerCar"/> class.</summary>
        public OwnerCar()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerCar"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="ownerKey">The owner key.</param>
        /// <param name="carKey">The car key.</param>
        /// <param name="carID">The car identifier.</param>
        /// <param name="paintJob">The paint job.</param>
        /// <param name="maxPower">The maximum power.</param>
        /// <param name="powerLevel">The power level.</param>
        /// <param name="weightReductionLevel">The weight reduction level.</param>
        /// <param name="acquiredDate">The acquired date.</param>
        /// <exception cref="ArgumentNullException">
        /// primaryKey or ownerKey or carKey or carID or paintJob
        /// </exception>
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

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Owner Key = '" + OwnerKey + "', Car Key = '" + CarKey
                            + "', Car ID = '" + CarID + "', Paint Job = '" + PaintJob + "', Max Power = " + MaxPower + ", Power Level = " + PowerLevel
                            + "', Weight Reduction Level = " + WeightReductionLevel + ", Date Acquired = '" + AcquiredDate + "}";
            return line;
        }
    }
}