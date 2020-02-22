using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    [Serializable]
    public class OwnerCarNotFoundException : Exception
    {
        public const string OwnerCarKeyNotFoundMsg = "The owner car can not be found by key.";
        public const string OwnerCarIDNotFoundMsg = "The owner car can not be found by the car id.";

        public string SearchParameter { get; set; }

        public OwnerCarNotFoundException()
        {
        }

        public OwnerCarNotFoundException(string message) : base(message)
        {
        }

        public OwnerCarNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerCarNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OwnerCarNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }
    }
}
