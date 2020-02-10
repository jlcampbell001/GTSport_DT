using GTSport_DT.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarNameNotSetException : Exception
    {
        public const string CarNameNotSetMsg = "The car name is not filled.";

        public CarNameNotSetException()
        {
        }

        public CarNameNotSetException(string message) : base(message)
        {
        }

        public CarNameNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarNameNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
