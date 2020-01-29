using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Regions
{
    // --------------------------------------------------------------------------------
    /// <summary>
    /// The region description not set exception.  The description must be filled.
    /// </summary>
    // --------------------------------------------------------------------------------
    [Serializable]
    public class RegionDescriptionNotSetException : Exception
    {
        public const string regionDescriptionNotSet = "The region description was not set.";

        public RegionDescriptionNotSetException()
        {
        }

        public RegionDescriptionNotSetException(string message) : base(message)
        {
        }

        public RegionDescriptionNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegionDescriptionNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
