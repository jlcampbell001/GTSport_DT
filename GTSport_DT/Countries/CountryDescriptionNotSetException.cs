using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    [Serializable]
    public class CountryDescriptionNotSetException :Exception
    {
        public const string CountryDescriptionNotSetMsg = "The country description was not filled.";

        public CountryDescriptionNotSetException()
        {
        }

        public CountryDescriptionNotSetException(string message) : base(message)
        {
        }

        public CountryDescriptionNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryDescriptionNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
