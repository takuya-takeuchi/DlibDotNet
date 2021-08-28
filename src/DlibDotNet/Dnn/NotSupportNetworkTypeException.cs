using System;

namespace DlibDotNet.Dnn
{

    /// <summary>
    /// The exception that is thrown when an specified network type is not supported. This class cannot be inherited.
    /// </summary>
    public sealed class NotSupportNetworkTypeException : Exception
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportNetworkTypeException"/> class with a specified network type that is the cause of this exception.
        /// </summary>
        /// <param name="networkType">The network type.</param>
        public NotSupportNetworkTypeException(int networkType)
        {
            this.NetworkType = networkType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotSupportNetworkTypeException"/> class with a specified network type, error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="networkType">The network type.</param>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The name of the parameter that caused the current exception.</param>
        public NotSupportNetworkTypeException(int networkType, string message, Exception innerException)
            : base(message, innerException)
        {
            this.NetworkType = networkType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the the network type that is the cause of this exception.
        /// </summary>
        public int NetworkType
        {
            get;
        }

        #endregion

    }

}
