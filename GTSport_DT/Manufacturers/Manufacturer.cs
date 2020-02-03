using GTSport_DT.General;
using System;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The manufacturer entity.</summary>
    /// <seealso cref="GTSport_DT.General.Entity"/>
    public class Manufacturer : Entity
    {
        /// <summary>Gets or sets the country key.</summary>
        /// <value>The country key.</value>
        public string CountryKey { get; set; }

        /// <summary>Gets or sets the manufacturer name.</summary>
        /// <value>The manufacturer name.</value>
        public string Name { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Manufacturer"/> class.</summary>
        public Manufacturer()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Manufacturer"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="name">The name.</param>
        /// <param name="countryKey">The country key.</param>
        /// <exception cref="ArgumentNullException">primaryKey or name or countryKey</exception>
        public Manufacturer(string primaryKey, string name, string countryKey)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            CountryKey = countryKey ?? throw new ArgumentNullException(nameof(countryKey));
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Name = '" + Name + "', Country Key = '" + CountryKey + "'}";
            return line;
        }
    }
}