using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    public static class DriveTrain
    {
        public const string FourWD = "4WD";
        public const string FF = "FF";
        public const string FR = "FR";
        public const string MR = "MR";
        public const string RR = "RR";
        public const string Empty = " ";

        public static readonly string[] DriveTrains = { Empty, FourWD, FF, FR, MR, RR };
    }
}
