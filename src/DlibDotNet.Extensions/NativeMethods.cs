using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace DlibDotNet.Extensions
{

    internal sealed partial class NativeMethods
    {

        #region P/Invoke

        [DllImport("kernel32", CallingConvention = System.Runtime.InteropServices.CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern IntPtr LoadLibrary(string dllPath);

        #endregion

        #region Fields

#if LINUX
        public const string NativeLibrary = "libDlibDotNetNative.so";

        public const string NativeDnnLibrary = "libDlibDotNetNative.Dnn.so";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#elif MAC
        public const string NativeLibrary = "libDlibDotNet.Native.dylib";
        public const string NativeDnnLibrary = "libDlibDotNet.Native.Dnn.dylib";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#else
        /// <summary>
        /// Native library file name.
        /// If Linux, it will be converted to  libOpenPoseDotNetNative.so
        /// If MacOSX, it will be converted to  libOpenPoseDotNetNative.dylib
        /// If Windows, it will be available after call LoadLibrary.
        /// And this file name must not contain period. If it does,
        /// CLR does not add extension (.dll) and CLR fails to load library
        /// </summary>
        public const string NativeLibrary = "DlibDotNetNative.dll";

        public const string NativeDnnLibrary = "DlibDotNetNative.Dnn.dll";

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;
#endif

        /// <summary>
        /// Native library file name.
        /// If Linux, it will be converted to  libOpenPoseDotNetNative.so
        /// If MacOSX, it will be converted to  libOpenPoseDotNetNative.dylib
        /// If Windows, it will be available after call LoadLibrary.
        /// And this file name must not contain period. If it does,
        /// CLR does not add extension (.dll) and CLR fails to load library
        /// </summary>
        private static readonly IDictionary<string, IntPtr> LoadedLibraries = new Dictionary<string, IntPtr>();

        #endregion

        #region Constructors

        static NativeMethods()
        {
            if (!IsWindows())
                return;

            var fileName = $"{NativeLibrary}.dll";
            if (LoadedLibraries.ContainsKey(fileName))
                return;

            var ret = LoadLibrary(fileName);
            if (ret != IntPtr.Zero)
            {
                LoadedLibraries.Add(fileName, ret);
                return;
            }

            var executingAssembly = typeof(NativeMethods).GetTypeInfo().Assembly;
            var baseDirectory = Path.GetDirectoryName(executingAssembly.Location);
            ret = LoadLibrary(Path.Combine(baseDirectory, fileName));
            if (ret != IntPtr.Zero)
            {
                LoadedLibraries.Add(fileName, ret);
                return;
            }
        }

        #endregion

        #region Properties

        public static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT ||
                   Environment.OSVersion.Platform == PlatformID.Win32S ||
                   Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                   Environment.OSVersion.Platform == PlatformID.WinCE;
        }

        #endregion

    }

}
