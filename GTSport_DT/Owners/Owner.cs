using GTSport_DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Owners
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The owner entity.
    /// </summary>
    // --------------------------------------------------------------------------------
    public class Owner : Entity
    {
        public string OwnerName { get; set; }
        public Boolean DefaultOwner { get; set; }

        public Owner()
        {
        }

        public Owner(string primaryKey, string ownerName, bool defaultOwner)
        {
            this.PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            this.OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
            this.DefaultOwner = defaultOwner;
        }

        public override string ToString()
        {
            string line = "{PrimaryKey = '" + this.PrimaryKey + "', OwnerName = '" + this.OwnerName + "', DefaultOwner = " + this.DefaultOwner + "}";
            return line;
        }
    }
}
