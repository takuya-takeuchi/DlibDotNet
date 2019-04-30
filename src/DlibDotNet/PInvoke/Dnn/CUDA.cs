using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention, EntryPoint = nameof(cuda_cudaRuntimeGetVersion))]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool dnn_cuda_cudaRuntimeGetVersion(out int version);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention, EntryPoint = nameof(cuda_cudaDriverGetVersion))]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool dnn_cuda_cudaDriverGetVersion(out int version);

    }

}