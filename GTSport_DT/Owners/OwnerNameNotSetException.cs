using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Owners
{
    [Serializable]
    public class OwnerNameNotSetException : Exception
    {
        public const string OwnerNameNoSetMsg = "The owner name was not set.";

        public OwnerNameNotSetException()
        {
        }

        public OwnerNameNotSetException(string message) : base(message)
        {
        }

        public OwnerNameNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerNameNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
