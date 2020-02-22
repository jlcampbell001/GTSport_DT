using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.OwnerCars
{
    [Serializable]
    public class OwnerCarCarKeyNotSetException : Exception
    {
        public const string OwnerCarCarKeyNotSetMsg = "The car key was not filled.";

        public OwnerCarCarKeyNotSetException()
        {
        }

        public OwnerCarCarKeyNotSetException(string message) : base(message)
        {
        }

        public OwnerCarCarKeyNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OwnerCarCarKeyNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
