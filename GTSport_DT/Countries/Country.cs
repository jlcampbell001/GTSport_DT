using GTSport_DT.General;
using System;

namespace GTSport_DT.Countries
{
    /// <summary>The country entity.</summary>
    /// <seealso cref="GTSport_DT.General.Entity"/>
    public class Country : Entity
    {
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Gets or sets the region key.</summary>
        /// <value>The region key the country is in.</value>
        public string RegionKey { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Country"/> class.</summary>
        public Country()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Country"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="description">The description.</param>
        /// <param name="regionKey">The region key.</param>
        /// <exception cref="ArgumentNullException">primaryKey or description or regionKey</exception>
        public Country(string primaryKey, string description, string regionKey)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            RegionKey = regionKey ?? throw new ArgumentNullException(nameof(regionKey));
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Description ='" + Description + "', Region Key = '" + RegionKey + "'";
            return line;
        }
    }
}