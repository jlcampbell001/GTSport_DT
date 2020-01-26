using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Owners
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The owner not found exception.  When deleting an owner the owner must exists first.
    /// </summary>
    // --------------------------------------------------------------------------------
    [Serializable]
    public class OwnerNotFoundException : Exception
    {
        public const string OwnerKeyNotFoundMsg = "Could not find owner by key.";
        public const string OwnerNameNotFoundMsg = "Could not find owner by name.";

        public string SearchParmater{ get; set; }

        public OwnerNotFoundException()
        {
        }

        public OwnerNotFoundException(string message) : base(message)
        {
        }

        public OwnerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OwnerNotFoundException(string message, string searchParameter) : this(message)
        {
            SearchParmater = searchParameter;
        }

    }
}
