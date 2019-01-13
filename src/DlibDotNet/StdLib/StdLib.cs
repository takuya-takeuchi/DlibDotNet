using System;
using System.Runtime.InteropServices;
using System.Text;

using ErrorType = DlibDotNet.NativeMethods.ErrorType;
using MatrixElementType = DlibDotNet.NativeMethods.MatrixElementType;

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

    }

}
