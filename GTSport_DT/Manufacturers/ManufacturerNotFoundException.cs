using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Manufacturers
{
    /// <summary>The exception when the manufacturer is not found in the table.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class ManufacturerNotFoundException : Exception
    {
        /// <summary>The manufacturer key not found message.</summary>
        public const string ManufacturerKeyNotFoundMsg = "The manufacturer could not be found by key.";

        /// <summary>The manufacturer name not found message.</summary>
        public const string ManufacturerNameNotFoundMsg = "The manufacturer could not be found by name.";

        /// <summary>Gets or sets the search parameter.</summary>
        /// <value>The search parameter that caused the exception.</value>
        public string SearchParameter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerNotFoundException"/> class.
        /// </summary>
        public ManufacturerNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ManufacturerNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="searchParameter">The search parameter.</param>
        public ManufacturerNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public ManufacturerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufacturerNotFoundException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected ManufacturerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}