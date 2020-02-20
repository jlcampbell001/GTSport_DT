using System;

namespace GTSport_DT.Cars
{
    /// <summary>The object for filtering the car list.</summary>
    public class CarSearchCriteria
    {
        /// <summary>Gets or sets the category from.</summary>
        /// <value>The category from.</value>
        public CarCategory.Category CategoryFrom { get; set; }

        /// <summary>Gets or sets the category to.</summary>
        /// <value>The category to.</value>
        public CarCategory.Category CategoryTo { get; set; }

        /// <summary>Gets or sets the country description.</summary>
        /// <value>The country description.</value>
        public string CountryDescription { get; set; }

        /// <summary>Gets or sets the drive train.</summary>
        /// <value>The drive train.</value>
        public string DriveTrain { get; set; }

        /// <summary>Gets or sets the name of the manufacturer.</summary>
        /// <value>The name of the manufacturer.</value>
        public string ManufacturerName { get; set; }

        /// <summary>Gets or sets the maximum power from.</summary>
        /// <value>The maximum power from.</value>
        public int MaxPowerFrom { get; set; }

        /// <summary>Gets or sets the maximum power to.</summary>
        /// <value>The maximum power to.</value>
        public int MaxPowerTo { get; set; }

        /// <summary>Gets or sets the region description.</summary>
        /// <value>The region description.</value>
        public string RegionDescription { get; set; }

        /// <summary>Gets or sets the year from.</summary>
        /// <value>The year from.</value>
        public int YearFrom { get; set; }

        /// <summary>Gets or sets the year to.</summary>
        /// <value>The year to.</value>
        public int YearTo { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CarSearchCriteria"/> class.</summary>
        public CarSearchCriteria()
        {
            CategoryFrom = null;
            CategoryTo = null;
            YearFrom = -1;
            YearTo = -1;
            MaxPowerFrom = -1;
            MaxPowerTo = -1;
            DriveTrain = null;
            ManufacturerName = null;
            CountryDescription = null;
            RegionDescription = null;
        }

        /// <summary>Initializes a new instance of the <see cref="CarSearchCriteria"/> class.</summary>
        /// <param name="categoryFrom">The category from.</param>
        /// <param name="categoryTo">The category to.</param>
        /// <param name="yearFrom">The year from.</param>
        /// <param name="yearTo">The year to.</param>
        /// <param name="maxPowerFrom">The maximum power from.</param>
        /// <param name="maxPowerTo">The maximum power to.</param>
        /// <param name="driveTrain">The drive train.</param>
        /// <param name="manufacturerName">Name of the manufacturer.</param>
        /// <param name="countryDescription">The country description.</param>
        /// <param name="regionDescription">The region description.</param>
        /// <exception cref="ArgumentNullException">
        /// categoryFrom or categoryTo or driveTrain or manufacturerName or countryDescription or regionDescription
        /// </exception>
        public CarSearchCriteria(CarCategory.Category categoryFrom, CarCategory.Category categoryTo, int yearFrom, int yearTo, int maxPowerFrom,
            int maxPowerTo, string driveTrain, string manufacturerName, string countryDescription, string regionDescription)
        {
            CategoryFrom = categoryFrom ?? throw new ArgumentNullException(nameof(categoryFrom));
            CategoryTo = categoryTo ?? throw new ArgumentNullException(nameof(categoryTo));
            YearFrom = yearFrom;
            YearTo = yearTo;
            MaxPowerFrom = maxPowerFrom;
            MaxPowerTo = maxPowerTo;
            DriveTrain = driveTrain ?? throw new ArgumentNullException(nameof(driveTrain));
            ManufacturerName = manufacturerName ?? throw new ArgumentNullException(nameof(manufacturerName));
            CountryDescription = countryDescription ?? throw new ArgumentNullException(nameof(countryDescription));
            RegionDescription = regionDescription ?? throw new ArgumentNullException(nameof(regionDescription));
        }
    }
}