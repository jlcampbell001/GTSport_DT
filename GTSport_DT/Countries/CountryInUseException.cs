using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    [Serializable]
    public class CountryInUseException : Exception
    {
        public const string CountryInUseCanNotBeDeletedManufacturerMsg = "Country is in use with a manufacturer and can not be delete.";

        public CountryInUseException()
        {
        }

        public CountryInUseException(string message) : base(message)
        {
        }

        public CountryInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
