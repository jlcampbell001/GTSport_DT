using System;
using System.Runtime.Serialization;

namespace GTSport_DT.Owners
{
    /// <summary>The owner name already exists exception. Each owner name is expected to be unique.</summary>
    [Serializable]
    public class OwnerNameAlreadyExistsException : Exception
    {
        /// <summary>The owner name already exists message.</summary>
        public const string OwnerNameAlreadyExistsMsg = "An owner with the name already exists.";

        /// <summary>Gets the name of the owner.</summary>
        /// <value>The name of the owner.</value>
        public string OwnerName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerNameAlreadyExistsException"/> class.
        /// </summary>
        public OwnerNameAlreadyExistsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerNameAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OwnerNameAlreadyExistsException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerNameAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public OwnerNameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerNameAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ownerName">Name of the owner.</param>
        public OwnerNameAlreadyExistsException(string message, string ownerName) : base(message)
        {
            OwnerName = ownerName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerNameAlreadyExistsException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected OwnerNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}