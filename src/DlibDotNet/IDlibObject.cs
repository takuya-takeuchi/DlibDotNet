using System;

namespace DlibDotNet
{

    /// <summary>
    /// Defines methods and properties to represent the dlib objects.
    /// </summary>
    public interface IDlibObject : IDisposable
    {

        #region Properties

        /// <summary>
        /// Gets a pointer of native structure.
        /// </summary>>
        IntPtr NativePtr
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// If this object is disposed, then <see cref="System.ObjectDisposedException"/> is thrown.
        /// </summary>
        void ThrowIfDisposed();

        #endregion

    }

}