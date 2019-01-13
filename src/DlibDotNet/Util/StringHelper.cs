using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class StringHelper
    {

        #region Methods

        public static string FromStdString(IntPtr ptr)
        {
            // Need not to delete str
            // Because string.c_str returns inner memory of string instance.
            // This inner memory will be deleted when string instance is deleted.
            var str = NativeMethods.string_c_str(ptr);
            return Marshal.PtrToStringAnsi(str);
        }

        #endregion

    }

}
