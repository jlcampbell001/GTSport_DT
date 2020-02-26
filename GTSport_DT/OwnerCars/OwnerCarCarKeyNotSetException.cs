﻿using System;
using System.Runtime.Serialization;

namespace GTSport_DT.OwnerCars
{
    /// <summary>The exception for when an owner car car key is not filled.</summary>
    /// <seealso cref="System.Exception"/>
    [Serializable]
    public class OwnerCarCarKeyNotSetException : Exception
    {
        /// <summary>The owner car car key not set message.</summary>
        public const string OwnerCarCarKeyNotSetMsg = "The car key was not filled.";

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarCarKeyNotSetException"/> class.
        /// </summary>
        public OwnerCarCarKeyNotSetException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarCarKeyNotSetException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public OwnerCarCarKeyNotSetException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarCarKeyNotSetException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference ( <span
        /// class="keyword"><span class="languageSpecificText"><span class="cs">null</span><span
        /// class="vb">Nothing</span><span class="cpp">nullptr</span></span></span><span
        /// class="nu">a null reference ( <span class="keyword">Nothing</span> in Visual
        /// Basic)</span> in Visual Basic) if no inner exception is specified.
        /// </param>
        public OwnerCarCarKeyNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerCarCarKeyNotSetException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the
        /// serialized object data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains
        /// contextual information about the source or destination.
        /// </param>
        protected OwnerCarCarKeyNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}