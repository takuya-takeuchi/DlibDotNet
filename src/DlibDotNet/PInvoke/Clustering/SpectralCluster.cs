using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType spectral_cluster(KernelType kernelType,
                                                        MatrixElementType type,
                                                        int templateRows,
                                                        int templateColumns,
                                                        IntPtr kernel,
                                                        IntPtr samples,
                                                        uint numClusters,
                                                        out IntPtr ret);

    }

}
