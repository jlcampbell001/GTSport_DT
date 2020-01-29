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
    /// The region not found exception.  When deleting the region it must exist.
    /// </summary>
    // --------------------------------------------------------------------------------
    [Serializable]
    public class RegionNotFoundException : Exception
    {
        public const string RegionKeyNotFoundMsg = "Could not find region by key.";
        public const string RegionDescriptionNotFoundMsg = "Could not find region by name.";

        public string SearchParameter { get; set; }

        public RegionNotFoundException()
        {
        }

        public RegionNotFoundException(string message) : base(message)
        {
        }

        public RegionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        public RegionNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }
    }
}
