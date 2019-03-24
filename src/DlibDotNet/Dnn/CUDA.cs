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
            return NativeMethods.dnn_cuda_cudaDriverGetVersion(out version);
        }

        /// <summary>
        /// Gets the value of runtime version of CUDA.
        /// </summary>
        /// <param name="version">When this method returns, contains the value of runtime version of CUDA.</param>
        /// <returns><code>true</code> if CUDA runtime is installed and CUDA system returns valid value; otherwise, <code>false</code>.</returns>
        public static bool TryGetRuntimeVersion(out int version)
        {
            return NativeMethods.dnn_cuda_cudaRuntimeGetVersion(out version);
        }

        #region Helpers

        internal static void ThrowCudaException(NativeMethods.ErrorType error)
        {
            if (error == NativeMethods.ErrorType.OK)
                return;

            var tmp = -(int)error;
            if ((tmp & (int) NativeMethods.ErrorType.CudaError) != (int) NativeMethods.ErrorType.CudaError)
                return;

            tmp -= (int)NativeMethods.ErrorType.CudaError;

            NativeMethods.dnn_cuda_cudaDriverGetVersion(out var driverVersion);
            NativeMethods.dnn_cuda_cudaRuntimeGetVersion(out var runtimeVersion);
            throw new CudaException(tmp, NativeMethods.NativeDnnLibrary, driverVersion, runtimeVersion);
        }

        #endregion

        #endregion

    }

}
