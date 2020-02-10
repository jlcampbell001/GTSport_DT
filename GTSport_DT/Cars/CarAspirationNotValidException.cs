using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarAspirationNotValidException : Exception
    {
        public const string CarAspirationNotValidMsg = "The aspiration is not a valid value.";

        public CarAspirationNotValidException()
        {
        }

        public CarAspirationNotValidException(string message) : base(message)
        {
        }

        public CarAspirationNotValidException(string message, string aspiration) : base(message)
        {
            Aspiration = aspiration;
        }

        public CarAspirationNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarAspirationNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string Aspiration { get; set; }
    }
}
