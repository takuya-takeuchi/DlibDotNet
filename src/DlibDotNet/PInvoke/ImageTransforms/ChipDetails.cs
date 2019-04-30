using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_details_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_details_new2(IntPtr drect, IntPtr dims);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_details_new3(IntPtr rect, IntPtr dims);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_details_new4(IntPtr rect, uint size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr chip_details_new5(IntPtr rect, uint size, double angle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_details_angle(IntPtr chip, out double angle);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_details_cols(IntPtr chip, out uint cols);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_details_rect(IntPtr chip, out IntPtr rect);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool chip_details_rows(IntPtr chip, out uint rows);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void chip_details_delete(IntPtr obj);

    }

}