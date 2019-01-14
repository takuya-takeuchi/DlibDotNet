using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region Fields

        /// <summary>
        /// Native library file name.
        /// If Linux, it will be converted to  libDlibDotNetNative.so
        /// If MacOSX, it will be converted to  libDlibDotNetNative.dylib
        /// If Windows, it will be available after call LoadLibrary.
        /// And this file name must not contain period. If it does,
        /// CLR does not add extension (.dll) and CLR fails to load library
        /// </summary>
        public const string NativeLibrary = "DlibDotNetNative";

        public const string NativeDnnLibrary = "DlibDotNetNativeDnn";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;

        private static readonly WindowsLibraryLoader WindowsLibraryLoader = new WindowsLibraryLoader();

        #endregion

        #region Constructors

        static NativeMethods()
        {
            WindowsLibraryLoader.LoadLibraries(new[]
            {
                $"{NativeLibrary}",
                $"{NativeDnnLibrary}"
            });
        }

        #endregion

    }

}