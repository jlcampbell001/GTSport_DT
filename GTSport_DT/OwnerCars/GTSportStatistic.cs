using GTSport_DT.Cars;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The class for all the statistic data for GT Sport DT.</summary>
    public class GTSportStatistic
    {
        /// <summary>Gets or sets the average acceleration.</summary>
        /// <value>The average acceleration.</value>
        public double AvgAcceleration { get; set; }

        /// <summary>Gets or sets the average braking.</summary>
        /// <value>The average braking.</value>
        public double AvgBraking { get; set; }

        /// <summary>Gets or sets the average cornering.</summary>
        /// <value>The average cornering.</value>
        public double AvgCornering { get; set; }

        /// <summary>Gets or sets the average maximum power.</summary>
        /// <value>The average maximum power.</value>
        public double AvgMaxPower { get; set; }

        /// <summary>Gets or sets the average maximum speed.</summary>
        /// <value>The average maximum speed.</value>
        public double AvgMaxSpeed { get; set; }

        /// <summary>Gets or sets the average price.</summary>
        /// <value>The average price.</value>
        public double AvgPrice { get; set; }

        /// <summary>Gets or sets the average stability.</summary>
        /// <value>The average stability.</value>
        public double AvgStability { get; set; }

        /// <summary>Gets or sets the cars owned.</summary>
        /// <value>The cars owned.</value>
        public int carsOwned { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public CarCategory.Category Category { get; set; }

        /// <summary>Gets or sets the number of cars.</summary>
        /// <value>The number of cars.</value>
        public int NumberOfCars { get; set; }

        /// <summary>Gets or sets the percent owned.</summary>
        /// <value>The percent owned.</value>
        public double percentOwned { get; set; }

        /// <summary>Gets or sets the unique cars owned.</summary>
        /// <value>The unique cars owned.</value>
        public int uniqueCarsOwned { get; set; }
    }
}