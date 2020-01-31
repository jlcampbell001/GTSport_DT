using GTSport_DT.General;
using System;

namespace GTSport_DT.Regions
{
    /// <summary>The entity for a region record.</summary>
    public class Region : Entity
    {
        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Region"/> class.</summary>
        public Region()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Region"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="description">The description.</param>
        /// <exception cref="ArgumentNullException">primaryKey or description</exception>
        public Region(string primaryKey, string description)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string line = "{PrimaryKey = '" + this.PrimaryKey + "', description = '" + this.Description + "'}";
            return line;
        }
    }
}