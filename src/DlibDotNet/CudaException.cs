using System;

namespace DlibDotNet
{

    /// <summary>
    /// The exception is general exception for CUDA (Compute Unified Device Architecture). This class cannot be inherited.
    /// </summary>
    public sealed class CudaException : Exception
    {

        #region Constructors

        internal CudaException(int errorCode, string dllName, int driverVersion, int runtimeVersion)
        {
            this.ErrorCode = errorCode;
            this.DllName = dllName;
            this.DriverVersion = driverVersion;
            this.RuntimeVersion = runtimeVersion;
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
        /// Gets the CUDA runtime version.
        /// </summary>
        public int RuntimeVersion
        {
            get;
        }

        #endregion

    }

}
