using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Owners
{
    [Serializable]
    public class OwnerNotFoundException : Exception
    {
        public static readonly string OwnerKeyNotFoundMsg = "Could not find owner by key.";
        public static readonly string OwnerNameNotFoundMsg = "Could not find owner by name.";

        public string SearchParameter { get; }

        public OwnerNotFoundException()
        {
        }

        public OwnerNotFoundException(string message) : base(message)
        {
        }

        public OwnerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OwnerNotFoundException(string message, string searchParameter) : this(message)
        {
            SearchParameter = searchParameter;
        }

        protected OwnerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }


    }
}
