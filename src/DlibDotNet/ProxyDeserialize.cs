using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DlibDotNet
{

    /// <summary>
    /// Deserialize continuous objects from file.
    /// </summary>
    public sealed class ProxyDeserialize : DlibObject
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyDeserialize"/> class with the specified file path contains serialized data.
        /// </summary>
        /// <param name="path">The file path contains serialized data.</param>
        /// <exception cref="FileNotFoundException"><paramref name="path"/> is not found.</exception>
        public ProxyDeserialize(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Encoding.UTF8.GetBytes(path);
            this.NativePtr = Native.proxy_deserialize_new(str);
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.proxy_deserialize_delete(this.NativePtr);
        }

        #endregion

        #endregion

        private sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr proxy_deserialize_new(byte[] fileName);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void proxy_deserialize_delete(IntPtr deserialize);
            
        }

    }

}
