using System;
using System.IO;

namespace DlibDotNet
{

    /// <summary>
    /// Serialize continuous objects to file.
    /// </summary>
    public sealed class ProxySerialize : DlibObject
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxySerialize"/> class with the specified file path contains serialized data.
        /// </summary>
        /// <param name="path">The file path contains serialized data.</param>
        /// <exception cref="FileNotFoundException"><paramref name="path"/> is not found.</exception>
        public ProxySerialize(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} is not found", path);

            var str = Dlib.Encoding.GetBytes(path);
            this.NativePtr = NativeMethods.proxy_serialize_new(str, str.Length);
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

            NativeMethods.proxy_serialize_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
