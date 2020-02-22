using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    [Serializable]
    public class OwnerCarIDNotSetException : Exception
    {
        public const string OwnerCarIDNotSetMsg = "The car id was not filled.";

        public OwnerCarIDNotSetException()
        {
        }

        public OwnerCarIDNotSetException(string message) : base(message)
        {
        }

        public OwnerCarIDNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerCarIDNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
