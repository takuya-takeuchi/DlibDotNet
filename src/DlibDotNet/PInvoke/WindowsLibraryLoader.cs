using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed class WindowsLibraryLoader
    {

        #region Fields

        private const string ProcessorArchitecture = "PROCESSOR_ARCHITECTURE";

        private const string DllFileExtension = ".dll";

        private const string DllDirectory = "dll";

        private readonly Dictionary<string, int> _ProcessorArchitectureAddressWidthPlatforms = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
                                                                                               {
                                                                                                   {"x86", 4},
                                                                                                   {"AMD64", 8},
                                                                                                   {"IA64", 8},
                                                                                                   {"ARM", 4}
                                                                                               };

        private readonly Dictionary<string, string> _ProcessorArchitecturePlatforms = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                                                                                      {
                                                                                          {"x86", "x86"},
                                                                                          {"AMD64", "x64"},
                                                                                          {"IA64", "Itanium"},
                                                                                          {"ARM", "WinCE"}
                                                                                      };

        private readonly object _SyncLock = new object();

        /// <summary>
        /// Native library file name.
        /// If Linux, it will be converted to  libDlibDotNetNative.so
        /// If MacOSX, it will be converted to  libDlibDotNetNative.dylib
        /// If Windows, it will be available after call LoadLibrary.
        /// And this file name must not contain period. If it does,
        /// CLR does not add extension (.dll) and CLR fails to load library
        /// </summary>
        private static readonly IDictionary<string, IntPtr> LoadedLibraries = new Dictionary<string, IntPtr>();

        [DllImport("kernel32", CallingConvention = CallingConvention.Winapi, SetLastError = true, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern IntPtr Win32LoadLibrary(string dllPath);

        #endregion

        #region Properties

        public static bool IsCurrentPlatformSupported()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT ||
                   Environment.OSVersion.Platform == PlatformID.Win32S ||
                   Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                   Environment.OSVersion.Platform == PlatformID.WinCE;
        }

        #endregion

        #region Methods

        #region Helpers

        private static string FixUpDllFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return fileName;

            if (!fileName.EndsWith(DllFileExtension, StringComparison.OrdinalIgnoreCase))
                return $"{fileName}{DllFileExtension}";

            return fileName;
        }

        private ProcessArchitectureInfo GetProcessArchitecture()
        {
            // BUG: Will this always be reliable?
            var processArchitecture = Environment.GetEnvironmentVariable(ProcessorArchitecture);
            var processInfo = new ProcessArchitectureInfo();
            if (!string.IsNullOrEmpty(processArchitecture))
            {
                // Sanity check
                processInfo.Architecture = processArchitecture;
            }
            else
            {
                processInfo.AddWarning("Failed to detect processor architecture, falling back to x86.");
                processInfo.Architecture = (IntPtr.Size == 8) ? "x64" : "x86";
            }

            var addressWidth = this._ProcessorArchitectureAddressWidthPlatforms[processInfo.Architecture];
            if (addressWidth != IntPtr.Size)
            {
                if (String.Equals(processInfo.Architecture, "AMD64", StringComparison.OrdinalIgnoreCase) && IntPtr.Size == 4)
                {
                    // fall back to x86 if detected x64 but has an address width of 32 bits.
                    processInfo.Architecture = "x86";
                    processInfo.AddWarning("Expected the detected processing architecture of {0} to have an address width of {1} Bytes but was {2} Bytes, falling back to x86.", processInfo.Architecture, addressWidth, IntPtr.Size);
                }
                else
                {
                    // no fallback possible
                    processInfo.AddWarning("Expected the detected processing architecture of {0} to have an address width of {1} Bytes but was {2} Bytes.", processInfo.Architecture, addressWidth, IntPtr.Size);

                }
            }

            return processInfo;
        }

        private string GetPlatformName(string processorArchitecture)
        {
            if (String.IsNullOrEmpty(processorArchitecture))
                return null;

            string platformName;
            if (this._ProcessorArchitecturePlatforms.TryGetValue(processorArchitecture, out platformName))
            {
                return platformName;
            }

            return null;
        }

        public void LoadLibraries(IEnumerable<string> dlls)
        {
            if (!IsWindows())
                return;

            foreach (var dll in dlls)
                LoadLibrary(dll);
        }

        private void LoadLibrary(string dllName)
        {
            if (!IsCurrentPlatformSupported())
                return;

            try
            {
                lock (this._SyncLock)
                {
                    if (LoadedLibraries.ContainsKey(dllName))
                        return;

                    var processArch = GetProcessArchitecture();
                    IntPtr dllHandle;

                    // Try loading from executing assembly domain
                    var executingAssembly = GetType().GetTypeInfo().Assembly;
                    var baseDirectory = Path.GetDirectoryName(executingAssembly.Location);
                    dllHandle = LoadLibraryInternal(dllName, baseDirectory, processArch);
                    if (dllHandle != IntPtr.Zero) return;

                    // Gets the pathname of the base directory that the assembly resolver uses to probe for assemblies.
                    // https://github.com/dotnet/corefx/issues/2221
                    baseDirectory = AppContext.BaseDirectory;
                    dllHandle = LoadLibraryInternal(dllName, baseDirectory, processArch);
                    if (dllHandle != IntPtr.Zero) return;

                    // Finally try the working directory
                    baseDirectory = Path.GetFullPath(Directory.GetCurrentDirectory());
                    dllHandle = LoadLibraryInternal(dllName, baseDirectory, processArch);
                    if (dllHandle != IntPtr.Zero) return;

                    var errorMessage = new StringBuilder();
                    errorMessage.Append($"Failed to find dll \"{dllName}\", for processor architecture {processArch.Architecture}.");
                    if (processArch.HasWarnings)
                    {
                        // include process detection warnings
                        errorMessage.Append($"\r\nWarnings: \r\n{processArch.WarningText()}");
                    }

                    throw new Exception(errorMessage.ToString());
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        private IntPtr LoadLibraryInternal(string dllName, string baseDirectory, ProcessArchitectureInfo processArchInfo)
        {
            var platformName = GetPlatformName(processArchInfo.Architecture);
            var expectedDllDirectory = Path.Combine(
                Path.Combine(baseDirectory, DllDirectory), platformName);
            return this.LoadLibraryRaw(dllName, expectedDllDirectory);
        }

        private IntPtr LoadLibraryRaw(string dllName, string baseDirectory)
        {
            var libraryHandle = IntPtr.Zero;
            var fileName = FixUpDllFileName(Path.Combine(baseDirectory, dllName));

            // Show where we're trying to load the file from
            Debug.WriteLine($"Trying to load native library \"{fileName}\"...");

            if (File.Exists(fileName))
            {
                // Attempt to load dll
                try
                {
                    libraryHandle = Win32LoadLibrary(fileName);
                    if (libraryHandle != IntPtr.Zero)
                    {
                        // library has been loaded
                        Debug.WriteLine($"Successfully loaded native library \"{fileName}\".");
                        LoadedLibraries.Add(dllName, libraryHandle);
                    }
                    else
                    {
                        Debug.WriteLine($"Failed to load native library \"{fileName}\".\r\nCheck windows event log.");
                    }
                }
                catch (Exception e)
                {
                    var lastError = Marshal.GetLastWin32Error();
                    Debug.WriteLine($"Failed to load native library \"{fileName}\".\r\nLast Error:{lastError}\r\nCheck inner exception and\\or windows event log.\r\nInner Exception: {e}");
                }
            }
            else
            {
                Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "The native library \"{0}\" does not exist.", fileName));
            }

            return libraryHandle;
        }

        #endregion

        #endregion

        private class ProcessArchitectureInfo
        {

            #region Constructors

            public ProcessArchitectureInfo()
            {
                this.Warnings = new List<string>();
            }

            #endregion

            #region Properties

            public string Architecture
            {
                get; set;
            }

            private List<string> Warnings
            {
                get;
            }

            #endregion

            #region Methods

            public void AddWarning(string format, params object[] args)
            {
                Warnings.Add(String.Format(format, args));
            }

            public bool HasWarnings => Warnings.Count > 0;

            public string WarningText()
            {
                return string.Join("\r\n", Warnings.ToArray());
            }

            #endregion

        }

    }

}
