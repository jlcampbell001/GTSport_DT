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
    /// The owner name already exists exception.  Each owner name is expected to be unique.
    /// </summary>
    // --------------------------------------------------------------------------------
    [Serializable]
    public class OwnerNameAlreadyExistsException : Exception
    {
        public const string OwnerNameAlreadyExistsMsg = "An owner with the name already exists.";

        public string OwnerName { get; }

        public OwnerNameAlreadyExistsException()
        {
        }

        public OwnerNameAlreadyExistsException(string message) : base(message)
        {
        }

        public OwnerNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OwnerNameAlreadyExistsException(string message, string ownerName) : base(message)
        {
            OwnerName = ownerName;
        }

        protected OwnerNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }
}
