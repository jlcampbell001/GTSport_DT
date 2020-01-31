using GTSport_DT.Countries;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Regions.RegionsForTesting;

namespace GTSport_DT_Testing.Countries
{
    public class CountriesForTesting
    {
        private const string country1Key = "COU900000001";
        private const string country1Description = "JAPAN";
        private static string country1RegionKey = Region3.PrimaryKey;

        private const string country2Key = "COU900000002";
        private const string country2Description = "GERMANY";
        private static string country2RegionKey = Region2.PrimaryKey;

        private const string country3Key = "COU900000003";
        private const string country3Description = "USA";
        private static string country3RegionKey = Region1.PrimaryKey;

        private const string country4Key = "COU900000004";
        private const string country4Description = "UNITED KINGDOM";
        private static string country4RegionKey = Region2.PrimaryKey;

        private const string country5Key = "COU900000005";
        private const string country5Description = "FRANCE";
        private static string country5RegionKey = Region2.PrimaryKey;

        public static Country Country1 = new Country(country1Key, country1Description, country1RegionKey);
        public static Country Country2 = new Country(country2Key, country2Description, country2RegionKey);
        public static Country Country3 = new Country(country3Key, country3Description, country3RegionKey);
        public static Country Country4 = new Country(country4Key, country4Description, country4RegionKey);
        public static Country Country5 = new Country(country5Key, country5Description, country5RegionKey);
    }
}
