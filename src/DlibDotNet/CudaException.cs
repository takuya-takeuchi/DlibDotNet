using System;

namespace DlibDotNet
{

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

        public string DllName
        {
            get;
        }

        public int DriverVersion
        {
            get;
        }

        public int ErrorCode
        {
            get;
        }

        public int RuntimeVersion
        {
            get;
        }

        #endregion

    }

}
