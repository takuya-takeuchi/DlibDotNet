using System;
using System.Runtime.InteropServices;

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

            #region save_png

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdlib_free(IntPtr ptr);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdlib_malloc(IntPtr size);

            #endregion

        }

    }

}
