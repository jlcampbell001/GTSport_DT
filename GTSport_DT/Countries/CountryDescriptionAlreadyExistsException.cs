using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    [Serializable]
    public class CountryDescriptionAlreadyExistsException : Exception
    {
        public const string CountryDesciprtionAlreadyExistsMsg = "The country description already exists and must be unique.";

        public string CountryDescription { get; set; }

        public CountryDescriptionAlreadyExistsException()
        {
        }

        public CountryDescriptionAlreadyExistsException(string message) : base(message)
        {
        }

        public CountryDescriptionAlreadyExistsException(string message, string description) : base(message)
        {
            CountryDescription = description;
        }

        public CountryDescriptionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryDescriptionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
