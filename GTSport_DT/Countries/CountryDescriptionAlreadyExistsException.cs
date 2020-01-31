using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Countries
{
    /// <summary>The exception when the description already exists when saving / updating a country.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class CountryDescriptionAlreadyExistsException : Exception
    {
        /// <summary>The country description already exists message</summary>
        public const string CountryDescriptionAlreadyExistsMsg = "The country description already exists and must be unique.";

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDescriptionAlreadyExistsException"/> class.
        /// </summary>
        public CountryDescriptionAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CountryDescriptionAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="description">The description.</param>
        public CountryDescriptionAlreadyExistsException(string message, string description) : base(message)
        {
            CountryDescription = description;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public CountryDescriptionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CountryDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected CountryDescriptionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>Gets or sets the country description.</summary>
        /// <value>The country description.</value>
        public string CountryDescription { get; set; }
    }
}