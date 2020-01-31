using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Regions
{
    /// <summary>
    /// The region description already exists exception error. Each region has a unique description.
    /// </summary>
    [Serializable]
    public class RegionDescriptionAlreadyExistsException : Exception
    {
        /// <summary>The region description already exists message.</summary>
        public const string RegionDescriptionAlreadyExistsMsg = "A region with the description already exists.";

        /// <summary>Gets or sets the region description.</summary>
        /// <value>The region description.</value>
        public string RegionDescription { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionDescriptionAlreadyExistsException"/> class.
        /// </summary>
        public RegionDescriptionAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RegionDescriptionAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="regionDescription">The region description.</param>
        public RegionDescriptionAlreadyExistsException(string message, string regionDescription) : base(message)
        {
            RegionDescription = regionDescription;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public RegionDescriptionAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegionDescriptionAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected RegionDescriptionAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}