using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DlibDotNet
{

    public sealed class ProxyDeserialize : DlibObject
    {

        #region Constructors

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
