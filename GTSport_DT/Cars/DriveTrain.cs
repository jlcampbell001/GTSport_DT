namespace GTSport_DT.Cars
{
    /// <summary>The valid drive train values.</summary>
    public static class DriveTrain
    {
        /// <summary>The empty value for a drive train.</summary>
        public const string Empty = " ";

        /// <summary>The FF drive train.</summary>
        public const string FF = "FF";

        /// <summary>The 4 wheel drive drive train.</summary>
        public const string FourWD = "4WD";

        /// <summary>The FR drive train.</summary>
        public const string FR = "FR";

        /// <summary>The MR drive train.</summary>
        public const string MR = "MR";

        /// <summary>The RR drive train.</summary>
        public const string RR = "RR";

        /// <summary>The list of valid drive trains for drop down lists.</summary>
        public static readonly string[] DriveTrains = { Empty, FourWD, FF, FR, MR, RR };
    }
}