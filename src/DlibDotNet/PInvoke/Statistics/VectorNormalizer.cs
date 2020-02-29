using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region running_stats

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_normalizer_new(MatrixElementType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_normalizer_delete(MatrixElementType type, IntPtr normalizer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType vector_normalizer_operator(MatrixElementType type,
                                                                  IntPtr normalizer,
                                                                  IntPtr matrix,
                                                                  out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_normalizer_train(MatrixElementType type,
                                                          IntPtr normalizer,
                                                          IntPtr samples);

        #endregion

    }

}