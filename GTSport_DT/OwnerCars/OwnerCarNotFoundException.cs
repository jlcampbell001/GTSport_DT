using System;
using System.Runtime.Serialization;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The exception when the owner car can not be found.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class OwnerCarNotFoundException : Exception
    {
        /// <summary>The owner car identifier not found message.</summary>
        public const string OwnerCarIDNotFoundMsg = "The owner car can not be found by the car id.";

        /// <summary>The owner car key not found message.</summary>
        public const string OwnerCarKeyNotFoundMsg = "The owner car can not be found by key.";

        /// <summary>Gets or sets the search parameter.</summary>
        /// <value>The search parameter.</value>
        public string SearchParameter { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarNotFoundException"/> class.
        /// </summary>
        public OwnerCarNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OwnerCarNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public OwnerCarNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="searchParameter">The search parameter.</param>
        public OwnerCarNotFoundException(string message, string searchParameter) : base(message)
        {
            SearchParameter = searchParameter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarNotFoundException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected OwnerCarNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}