using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    public class CarSearchCriteria
    {
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

        public CarCategory.Category CategoryFrom { get; set; }
        public CarCategory.Category CategoryTo { get; set; }
        public int YearFrom { get; set; }
        public int YearTo { get; set; }
        public int MaxPowerFrom { get; set; }
        public int MaxPowerTo { get; set; }
        public string DriveTrain { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryDescription { get; set; }
        public string RegionDescription { get; set; }


    }
}
