namespace GTSport_DT.Cars
{
    /// <summary>Values allowed for aspiration stat on a car.</summary>
    public static class Aspiration
    {
        /// <summary>An empty aspiration.</summary>
        public const string Empty = "";

        /// <summary>The EV aspiration.</summary>
        public const string EV = "EV";

        /// <summary>The NA aspiration.</summary>
        public const string NA = "NA";

        /// <summary>The SC aspiration.</summary>
        public const string SC = "SC";

        /// <summary>The TB aspiration.</summary>
        public const string TB = "TB";

        /// <summary>The list of aspirations for drop down lists.</summary>
        public static readonly string[] Aspirations = { Empty, EV, NA, SC, TB };
    }
}