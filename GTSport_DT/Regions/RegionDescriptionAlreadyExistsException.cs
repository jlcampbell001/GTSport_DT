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
    /// The region description already exists exception error.  Each region has a unique description.
    /// </summary>
    // --------------------------------------------------------------------------------
    [Serializable]
    public class RegionDescriptionAlreadyExistsException : Exception
    {
        public const string RegionDescriptionAlreadyExistsMsg = "A region with the description already exists.";

        public string RegionDescription { get; set; }

        public RegionDescriptionAlreadyExistsException()
        {
        }

        public RegionDescriptionAlreadyExistsException(string message) : base(message)
        {
        }

        public RegionDescriptionAlreadyExistsException(string message, string regionDescription) : base(message)
        {
            RegionDescription = regionDescription;
        }

        public RegionDescriptionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegionDescriptionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
