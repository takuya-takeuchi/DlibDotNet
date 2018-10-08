using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
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

        #region Helpers

        internal static void ThrowCudaException(Dlib.Native.ErrorType error)
        {
            if (error == Dlib.Native.ErrorType.OK)
                return;

            var tmp = -(int)error;
            if ((tmp & (int) Dlib.Native.ErrorType.CudaError) != (int) Dlib.Native.ErrorType.CudaError)
                return;

            tmp -= (int)Dlib.Native.ErrorType.CudaError;

            Native.cuda_cudaDriverGetVersion(out var driverVersion);
            Native.cuda_cudaRuntimeGetVersion(out var runtimeVersion);
            throw new CudaException(tmp, NativeMethods.NativeDnnLibrary, driverVersion, runtimeVersion);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            #region CUDA

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool cuda_cudaRuntimeGetVersion(out int version);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool cuda_cudaDriverGetVersion(out int version);

            #endregion}

        }

    }

}
