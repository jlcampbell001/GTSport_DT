using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Cars
{
    /// <summary>
    /// The exception when the car can not be found in the table either by the key or name.
    /// </summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class CarNotFoundException : Exception
    {
        /// <summary>The car key not found message.</summary>
        public const string CarKeyNotFoundMsg = "The car can not be found by the key.";

        /// <summary>The car name not found message.</summary>
        public const string CarNameNotFoundMsg = "The car can not be found by the name.";

        /// <summary>Gets or sets the search parameter that caused the exception</summary>
        /// <value>The search parameter.</value>
        public string SearchParameter { get; set; }

        /// <summary>Initializes a new instance of the <see cref="CarNotFoundException"/> class.</summary>
        public CarNotFoundException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarNotFoundException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public CarNotFoundException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarNotFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="searchParameter">The search parameter.</param>
        public CarNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }

        /// <summary>Initializes a new instance of the <see cref="CarNotFoundException"/> class.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public CarNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="CarNotFoundException"/> class.</summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected CarNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}