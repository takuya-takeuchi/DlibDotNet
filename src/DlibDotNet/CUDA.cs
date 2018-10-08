using System.Runtime.InteropServices;

namespace DlibDotNet
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
            return Native.cuda_cudaDriverGetVersion(out version);
        }

        /// <summary>
        /// Gets the value of runtime version of CUDA.
        /// </summary>
        /// <param name="version">When this method returns, contains the value of runtime version of CUDA.</param>
        /// <returns><code>true</code> if CUDA runtime is installed and CUDA system returns valid value; otherwise, <code>false</code>.</returns>
        public static bool TryGetRuntimeVersion(out int version)
        {
            return Native.cuda_cudaRuntimeGetVersion(out version);
        }

        #endregion

        internal sealed class Native
        {

            #region CUDA

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool cuda_cudaRuntimeGetVersion(out int version);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool cuda_cudaDriverGetVersion(out int version);

            #endregion}

        }

    }

}
