using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MenuItemText : MenuItem
    {

        #region Fields

        private readonly IntPtr _Handle;

        #endregion

        #region Constructors

        public MenuItemText(string str, DrawableWindow window, DrawableWindow.EventDelegate handler, char hk)
        {
            var s = Dlib.Encoding.GetBytes(str ?? "");
            this._Handle = Marshal.GetFunctionPointerForDelegate(handler);
            this.NativePtr = NativeMethods.menu_item_text_new(s, window.NativePtr, this._Handle, hk);
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