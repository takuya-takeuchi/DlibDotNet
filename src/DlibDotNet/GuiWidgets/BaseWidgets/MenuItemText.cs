using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MenuItemText : MenuItem
    {

        #region Delegates

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MenuItemTextEventDelegate();

        #endregion

        #region Fields

        private readonly IntPtr _Handle;

        #endregion

        #region Constructors

        public MenuItemText(string str, VoidActionMediator mediator, char hk)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            var s = Dlib.Encoding.GetBytes(str ?? "");
            this.NativePtr = NativeMethods.menu_item_text_new(s, mediator.NativePtr, hk);
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

            NativeMethods.menu_item_text_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}