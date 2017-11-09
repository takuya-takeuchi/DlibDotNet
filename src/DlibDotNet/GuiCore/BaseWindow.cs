using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class BaseWindow : DlibObject
    {

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Constructors
        #endregion

        #region Properties

        public string Title
        {
            set
            {
                this.ThrowIfDisposed();
                var title = Encoding.UTF8.GetBytes(value ?? "");
                Native.base_window_set_title(this.NativePtr, title);
            }
        }

        #endregion

        #region Methods

        public void WaitUntilClosed()
        {
            this.ThrowIfDisposed();
            Native.base_window_wait_until_closed(this.NativePtr);
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            // Do not delete here!!
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void base_window_set_title(IntPtr window, byte[] title);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void base_window_wait_until_closed(IntPtr window);

        }

    }

}
