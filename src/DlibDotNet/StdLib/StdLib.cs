using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        private static void CopyFromHeapMemory(IntPtr ptr, int count, out long[] array)
        {
            array = new long[count];
            Marshal.Copy(ptr, array, 0, count);
            //Native.stdlib_free(ptr);
        }

        #endregion

        internal sealed partial class Native
        {

            #region stdlib

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdlib_free(IntPtr ptr);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdlib_malloc(IntPtr size);

            #endregion

            #region string

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr string_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void string_append(IntPtr str, StringBuilder c_str, int len);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr string_c_str(IntPtr str);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void string_delete(IntPtr str);

            #endregion

            #region ostringstream

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr ostringstream_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr ostringstream_str(IntPtr str);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void ostringstream_delete(IntPtr str);

            #endregion

        }

    }

}
