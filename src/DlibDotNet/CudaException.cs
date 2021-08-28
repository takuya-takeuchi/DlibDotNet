using System;

namespace DlibDotNet
{

    /// <summary>
    /// The exception is general exception for CUDA (Compute Unified Device Architecture). This class cannot be inherited.
    /// </summary>
    public sealed class CudaException : Exception
    {

        #region Constructors

        internal CudaException(int errorCode, string dllName, int driverVersion, int runtimeVersion, string errorName, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.DllName = dllName;
            this.DriverVersion = driverVersion;
            this.RuntimeVersion = runtimeVersion;
            this.ErrorName = errorName;
            this.ErrorMessage = errorMessage;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the library name which throw <see cref="CudaException"/>.
        /// </summary>
        public string DllName
        {
            get;
        }

        /// <summary>
        /// Gets the CUDA driver version.
        /// </summary>
        public int DriverVersion
        {
            get;
        }

        /// <summary>
        /// Gets the CUDA error code.
        /// </summary>
        public int ErrorCode
        {
            get;
        }

        /// <summary>
        /// Gets the CUDA error name.
        /// </summary>
        public string ErrorName
        {
            get;
        }

        /// <summary>
        /// Gets the CUDA error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
        }

        /// <summary>
        /// Gets the CUDA runtime version.
        /// </summary>
        public int RuntimeVersion
        {
            get;
        }

        #endregion

        #region Methods

        #region Overrids

        public override string ToString()
        {
            return $"{typeof(CudaException).FullName}: {this.ErrorMessage}.";
        }

        #endregion

        #endregion

    }

}
