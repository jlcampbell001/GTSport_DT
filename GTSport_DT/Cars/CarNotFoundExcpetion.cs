using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarNotFoundExcpetion : Exception
    {
        public const string CarKeyNotFoundMsg = "The car can not be found by the key.";
        public const string CarNameNotFoundMsg = "The car can not be found by the name.";

        public CarNotFoundExcpetion()
        {
        }

        public CarNotFoundExcpetion(string message) : base(message)
        {
        }

        public CarNotFoundExcpetion(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }

        public CarNotFoundExcpetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarNotFoundExcpetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string SearchParameter { get; set; }
    }
}
