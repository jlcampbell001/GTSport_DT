using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarNameAlreadyExistsException : Exception
    {
        public const string CarNameAlreadyExistsMsg = "The car name already exists for another car.";

        public CarNameAlreadyExistsException()
        {
        }

        public CarNameAlreadyExistsException(string message) : base(message)
        {
        }

        public CarNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
