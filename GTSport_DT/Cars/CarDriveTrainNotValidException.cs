using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GTSport_DT.Cars
{
    [Serializable]
    public class CarDriveTrainNotValidException : Exception
    {
        public const string CarDriveTrainNotValidMsg = "The drive train value is not valid.";

        public string DriveTrain { get; set; }

        public CarDriveTrainNotValidException()
        {
        }

        public CarDriveTrainNotValidException(string message) : base(message)
        {
        }

        public CarDriveTrainNotValidException(string message, string driveTrain) : base(message)
        {
            DriveTrain = driveTrain;
        }

        public CarDriveTrainNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CarDriveTrainNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
