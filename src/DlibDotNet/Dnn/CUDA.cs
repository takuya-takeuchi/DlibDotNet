using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
{

    /// <summary>
    /// Provides the methods of CUDA (Compute Unified Device Architecture).
    /// </summary>
    public static class Cuda
    {

        #region Methods

        /// <summary>
        /// Gets the value of driver version of CUDA.
        /// </summary>
        /// <param name="version">When this method returns, contains the value of driver version of CUDA.</param>
        /// <returns><code>true</code> if CUDA driver is installed and CUDA system returns valid value; otherwise, <code>false</code>.</returns>
        public static bool TryGetDriverVersion(out int version)
        {
            return NativeMethods.cuda_dnn_cudaDriverGetVersion(out version);
        }

        /// <summary>
        /// Gets the value of runtime version of CUDA.
        /// </summary>
        /// <param name="version">When this method returns, contains the value of runtime version of CUDA.</param>
        /// <returns><code>true</code> if CUDA runtime is installed and CUDA system returns valid value; otherwise, <code>false</code>.</returns>
        public static bool TryGetRuntimeVersion(out int version)
        {
            return NativeMethods.cuda_dnn_cudaRuntimeGetVersion(out version);
        }

        #region Helpers

        internal static void ThrowCudaException(NativeMethods.ErrorType error)
        {
            if (error == NativeMethods.ErrorType.OK)
                return;

            var tmp = (int)error;
            var max = (int)NativeMethods.ErrorType.CudaError;                // 0x77000000
            var min = (int)NativeMethods.ErrorType.CudaErrorApiFailureBase;  // -(CudaError | 10000)
            if (!(min <= tmp && tmp < -max))
                return;

            tmp = -(tmp + (int)NativeMethods.ErrorType.CudaError);

            NativeMethods.cuda_dnn_cudaDriverGetVersion(out var driverVersion);
            NativeMethods.cuda_dnn_cudaRuntimeGetVersion(out var runtimeVersion);
            var namePtr = NativeMethods.cuda_dnn_cudaGetErrorName(tmp);
            var name = namePtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(namePtr) : null;
            var strPtr = NativeMethods.cuda_dnn_cudaGetErrorString(tmp);
            var message = strPtr != IntPtr.Zero ? Marshal.PtrToStringAnsi(strPtr) : null;
            throw new CudaException(tmp, NativeMethods.NativeDnnLibrary, driverVersion, runtimeVersion, name, message);
        }

        #endregion

        #endregion

    }

}
