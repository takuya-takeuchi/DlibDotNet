using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdString : DlibObject
    {

        #region Constructors

        internal StdString(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            if (this.IsDisposed)
                return;

            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.string_delete(this.NativePtr);
            this.NativePtr = IntPtr.Zero;
        }

        #endregion

        #endregion

    }

}