using System;
using System.IO;

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

            var str = Dlib.Encoding.GetBytes(path);
            var strLength = str.Length;
            Array.Resize(ref str, strLength + 1);
            str[strLength] = (byte)'\0';
            this.NativePtr = NativeMethods.proxy_deserialize_new(str);
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

            NativeMethods.proxy_deserialize_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
