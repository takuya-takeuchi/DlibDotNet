using System.Runtime.InteropServices;

namespace DlibDotNet
{

    public static class Cuda
    {

        #region Methods

        public static bool TryGetDriverVersion(out int version)
        {
            return Native.cuda_cudaDriverGetVersion(out version);
        }

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
