using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_dims_new(uint rows, uint cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_dims_get_cols(IntPtr chip, out uint cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void chip_dims_set_cols(IntPtr chip, uint cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_dims_get_rows(IntPtr chip, out uint rows);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void chip_dims_set_rows(IntPtr chip, uint rows);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void chip_dims_delete(IntPtr obj);

    }

}
