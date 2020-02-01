using GTSport_DT.Manufacturers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static GTSport_DT_Testing.Countries.CountriesForTesting;

namespace GTSport_DT_Testing.Manufacturers
{
    public class ManufacturersForTesting
    {
        private const string manufacturer1PrimaryKey = "MAN900000001";
        private const string manufacturer1Name = "MAZDA";
        private static readonly string manufacturer1CountryKey = Country1.PrimaryKey;

        private const string manufacturer2PrimaryKey = "MAN900000002";
        private const string manufacturer2Name = "VOLKSWAGEN";
        private static readonly string manufacturer2CountryKey = Country2.PrimaryKey;

        private const string manufacturer3PrimaryKey = "MAN900000003";
        private const string manufacturer3Name = "FORD";
        private static readonly string manufacturer3CountryKey = Country3.PrimaryKey;

        private const string manufacturer4PrimaryKey = "MAN900000004";
        private const string manufacturer4Name = "MERCEDES-BENZ";
        private static readonly string manufacturer4CountryKey = Country2.PrimaryKey;

        private const string manufacturer5PrimaryKey = "MAN900000005";
        private const string manufacturer5Name = "PORSCHE";
        private static readonly string manufacturer5CountryKey = Country2.PrimaryKey;

        private const string manufacturer6PrimaryKey = "MAN900000006";
        private const string manufacturer6Name = "JAGUAR";
        private static readonly string manufacturer6CountryKey = Country4.PrimaryKey;

        private const string manufacturer7PrimaryKey = "MAN900000007";
        private const string manufacturer7Name = "MCLAREN";
        private static readonly string manufacturer7CountryKey = Country4.PrimaryKey;

        private const string manufacturer8PrimaryKey = "MAN900000008";
        private const string manufacturer8Name = "ASTON MARTIN";
        private static readonly string manufacturer8CountryKey = Country4.PrimaryKey;

        private const string manufacturer9PrimaryKey = "MAN900000009";
        private const string manufacturer9Name = "BUGATTI";
        private static readonly string manufacturer9CountryKey = Country5.PrimaryKey;

        public static Manufacturer Manufacturer1 = new Manufacturer(manufacturer1PrimaryKey, manufacturer1Name, manufacturer1CountryKey);
        public static Manufacturer Manufacturer2 = new Manufacturer(manufacturer2PrimaryKey, manufacturer2Name, manufacturer2CountryKey);
        public static Manufacturer Manufacturer3 = new Manufacturer(manufacturer3PrimaryKey, manufacturer3Name, manufacturer3CountryKey);
        public static Manufacturer Manufacturer4 = new Manufacturer(manufacturer4PrimaryKey, manufacturer4Name, manufacturer4CountryKey);
        public static Manufacturer Manufacturer5 = new Manufacturer(manufacturer5PrimaryKey, manufacturer5Name, manufacturer5CountryKey);
        public static Manufacturer Manufacturer6 = new Manufacturer(manufacturer6PrimaryKey, manufacturer6Name, manufacturer6CountryKey);
        public static Manufacturer Manufacturer7 = new Manufacturer(manufacturer7PrimaryKey, manufacturer7Name, manufacturer7CountryKey);
        public static Manufacturer Manufacturer8 = new Manufacturer(manufacturer8PrimaryKey, manufacturer8Name, manufacturer8CountryKey);
        public static Manufacturer Manufacturer9 = new Manufacturer(manufacturer9PrimaryKey, manufacturer9Name, manufacturer9CountryKey);
    }
}
