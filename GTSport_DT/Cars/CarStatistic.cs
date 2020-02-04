using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    public class CarStatistic
    {
        public CarCategory.Category Category { get; set;}
        public int NumberOfCars { get; set; }
        public double AvgMaxPower { get; set; }
        public double AvgPrice { get; set; }
        public double AvgMaxSpeed { get; set; }
        public double AvgAcceleration { get; set; }
        public double AvgBraking { get; set; }
        public double AvgCornering { get; set; }
        public double AvgStability { get; set; }
    }
}
