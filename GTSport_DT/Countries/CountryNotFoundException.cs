using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    [Serializable]
    public class CountryNotFoundException : Exception
    {
        public const string CountryKeyNotFoundMsg = "Could not find country by key.";
        public const string CountryDescriptionNotFoundMsg = "Could not find country by description.";

        public string SearchParameter { get; set; }

        public CountryNotFoundException()
        {
        }

        public CountryNotFoundException(string message) : base(message)
        {
        }

        public CountryNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }

        public CountryNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
