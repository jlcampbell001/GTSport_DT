using GTSport_DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Regions
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The entity for a region record.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class Region : Entity
    {
        public string Description { get; set; }

        public Region()
        {
        }

        public Region(string primaryKey, string description)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public override string ToString()
        {
            string line = "{PrimaryKey = '" + this.PrimaryKey + "', description = '" + this.Description + "'}";
            return line;
        }
    }
}
