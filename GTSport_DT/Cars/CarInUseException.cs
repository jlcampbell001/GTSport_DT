using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Cars
{
    /// <summary>The exception when the car is in use.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class CarInUseException : Exception
    {
        /// <summary>The car in use owned car message.</summary>
        public const string CarInUseOwnedCarMsg = "The car can not be deleted as it is in use in an owned car record.";

        /// <summary>Initializes a new instance of the <see cref="CarInUseException"/> class.</summary>
        public CarInUseException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarInUseException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public CarInUseException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarInUseException"/> class.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public CarInUseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarInUseException"/> class.</summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected CarInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}