using GTSport_DT.Cars;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The owner car statistic entity.</summary>
    public class OwnerCarsStatistic
    {
        /// <summary>Gets or sets the cars owned. Includes duplicates.</summary>
        /// <value>The cars owned.</value>
        public int carsOwned { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        public CarCategory.Category Category { get; set; }

        /// <summary>Gets or sets the unique cars owned.</summary>
        /// <value>The unique cars owned.</value>
        public int uniqueCarsOwned { get; set; }
    }
}