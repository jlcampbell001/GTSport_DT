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
    class Owner : Entity
    {
        public Owner()
        {
        }

        public Owner(string primaryKey, string ownerName, bool defaultOwner)
        {
            this.PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            this.OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
            this.DefaultOwner = defaultOwner;
        }

        public string OwnerName { get; set; }
        public Boolean DefaultOwner { get; set; }
    }
}
