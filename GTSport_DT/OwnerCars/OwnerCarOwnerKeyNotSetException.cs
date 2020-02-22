using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    [Serializable]
    public class OwnerCarOwnerKeyNotSetException : Exception
    {
        public const string OwnerCarOwnerKeyNotSetMsg = "The owner key was not filled.";

        public OwnerCarOwnerKeyNotSetException()
        {
        }

        public OwnerCarOwnerKeyNotSetException(string message) : base(message)
        {
        }

        public OwnerCarOwnerKeyNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerCarOwnerKeyNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
