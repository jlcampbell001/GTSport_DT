using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Owners
{
    class Owner
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

        public string PrimaryKey { get; set; }
        public string OwnerName { get; set; }
        public Boolean DefaultOwner { get; set; }
    }
}
