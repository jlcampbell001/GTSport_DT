using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Regions
{
    [Serializable]
    public class RegionInUseException : Exception
    {
        public const string RegionInUseCanNotBeDeletedCountryMsg = "Region is used in a country and can not be deleted.";

        public RegionInUseException()
        {
        }

        public RegionInUseException(string message) : base(message)
        {
        }

        public RegionInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegionInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
