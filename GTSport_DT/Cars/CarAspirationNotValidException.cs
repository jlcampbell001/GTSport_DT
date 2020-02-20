using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Cars
{
    /// <summary>The exception when the car aspiration is not valid.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class CarAspirationNotValidException : Exception
    {
        /// <summary>The car aspiration not valid message.</summary>
        public const string CarAspirationNotValidMsg = "The aspiration is not a valid value.";

        /// <summary>Gets or sets the aspiration that caused the exception.</summary>
        /// <value>The aspiration.</value>
        public string Aspiration { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAspirationNotValidException"/> class.
        /// </summary>
        public CarAspirationNotValidException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAspirationNotValidException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CarAspirationNotValidException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAspirationNotValidException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="aspiration">The aspiration.</param>
        public CarAspirationNotValidException(string message, string aspiration) : base(message)
        {
            Aspiration = aspiration;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAspirationNotValidException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public CarAspirationNotValidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarAspirationNotValidException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected CarAspirationNotValidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}