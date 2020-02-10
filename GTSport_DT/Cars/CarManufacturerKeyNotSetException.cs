using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarManufacturerKeyNotSetException : Exception
    {
        public const string CarManufacturerKeyNotSetMsg = "The manufacturer key is not filled.";

        public CarManufacturerKeyNotSetException()
        {
        }

        public CarManufacturerKeyNotSetException(string message) : base(message)
        {
        }

        public CarManufacturerKeyNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarManufacturerKeyNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
