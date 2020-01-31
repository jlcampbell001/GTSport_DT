using GTSport_DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    public class Country : Entity
    {
        public string Description { get; set; }
        public string RegionKey { get; set; }

        public Country()
        {
        }

        public Country(string primaryKey, string description, string regionKey)
        {
            PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            RegionKey = regionKey ?? throw new ArgumentNullException(nameof(regionKey));
        }

        public override string ToString()
        {
            string line = "{Primary Key = '" + PrimaryKey + "', Description ='" + Description + "', Region Key = '" + RegionKey + "'";
            return base.ToString();
        }
    }
}
