using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarCategoryNotValidException : Exception
    {
        public const string CarCategoryNotValidMsg = "The category is not a valid value.";

        public CarCategoryNotValidException()
        {
        }

        public CarCategoryNotValidException(string message) : base(message)
        {
        }

        public CarCategoryNotValidException(string message, CarCategory.Category category) : base(message)
        {
            Category = category;
        }

        public CarCategoryNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarCategoryNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public CarCategory.Category Category {get; set;}
    }
}
