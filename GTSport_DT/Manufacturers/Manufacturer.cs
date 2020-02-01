using GTSport_DT.General;
using System;

namespace GTSport_DT.Manufacturers
{
    public class Manufacturer : Entity
    {
        public string CountryKey { get; set; }

        public string Name { get; set; }

        public Manufacturer()
        {
        }

        public Manufacturer(string primaryKey, string name, string countryKey)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CountryKey = countryKey ?? throw new ArgumentNullException(nameof(countryKey));
        }

        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Name = '" + Name + "', Country Key = '" + CountryKey + "'}";
            return line;
        }
    }
}