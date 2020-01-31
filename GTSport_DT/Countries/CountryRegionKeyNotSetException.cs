using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Countries
{
    [Serializable]
    public class CountryRegionKeyNotSetException : Exception
    {
        public const string CountryRegionKeyNotSetMsg = "The region key for the country was not filled.";

        public CountryRegionKeyNotSetException()
        {
        }

        public CountryRegionKeyNotSetException(string message) : base(message)
        {
        }

        public CountryRegionKeyNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CountryRegionKeyNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
