using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarInUseException : Exception
    {
        public const string CarInUseOwnedCarMsg = "The car can not be deleted as it is in use in an owned car record.";

        public CarInUseException()
        {
        }

        public CarInUseException(string message) : base(message)
        {
        }

        public CarInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
