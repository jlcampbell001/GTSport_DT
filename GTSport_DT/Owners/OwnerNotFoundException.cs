using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Owners
{
    /// <summary>
    /// The owner not found exception. When deleting an owner the owner must exists first.
    /// </summary>
    [Serializable]
    public class OwnerNotFoundException : Exception
    {
        /// <summary>The owner key not found message.</summary>
        public const string OwnerKeyNotFoundMsg = "Could not find owner by key.";

        /// <summary>The owner name not found message.</summary>
        public const string OwnerNameNotFoundMsg = "Could not find owner by name.";

        /// <summary>Gets or sets the search parameter.</summary>
        /// <value>The search parameter that caused the exception.</value>
        public string SearchParameter { get; set; }

        /// <summary>Initializes a new instance of the <see cref="OwnerNotFoundException"/> class.</summary>
        public OwnerNotFoundException()
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerNotFoundException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public OwnerNotFoundException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerNotFoundException"/> class.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public OwnerNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerNotFoundException"/> class.</summary>
        /// <param name="message">The message.</param>
        /// <param name="searchParameter">The search parameter.</param>
        public OwnerNotFoundException(string message, string searchParameter) : this(message)
        {
            SearchParameter = searchParameter;
        }

        /// <summary>Initializes a new instance of the <see cref="OwnerNotFoundException"/> class.</summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected OwnerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}