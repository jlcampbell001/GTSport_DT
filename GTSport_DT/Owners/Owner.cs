using GTSport_DT.General;
using System;

namespace GTSport_DT.Owners
{
    /// <summary>The owner entity.</summary>
    public class Owner : Entity
    {
        /// <summary>Gets or sets a value indicating whether this is the default owner.</summary>
        /// <value><c>true</c> if [default owner]; otherwise, <c>false</c>.</value>
        public Boolean DefaultOwner { get; set; }

        /// <summary>Gets or sets the name of the owner.</summary>
        /// <value>The name of the owner.</value>
        public string OwnerName { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Owner"/> class.</summary>
        public Owner()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Owner"/> class.</summary>
        /// <param name="primaryKey">The primary key.</param>
        /// <param name="ownerName">Name of the owner.</param>
        /// <param name="defaultOwner">If set to <c>true</c> it is the default owner.</param>
        /// <exception cref="ArgumentNullException">primaryKey or ownerName</exception>
        public Owner(string primaryKey, string ownerName, bool defaultOwner)
        {
            this.PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));
            this.OwnerName = ownerName ?? throw new ArgumentNullException(nameof(ownerName));
            this.DefaultOwner = defaultOwner;
        }

        /// <summary>Converts to string.</summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string line = "{PrimaryKey = '" + this.PrimaryKey + "', OwnerName = '" + this.OwnerName + "', DefaultOwner = " + this.DefaultOwner + "}";
            return line;
        }
    }
}