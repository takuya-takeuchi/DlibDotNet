#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MenuItemSeparator : MenuItem
    {

        #region Constructors

        public MenuItemSeparator()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.menu_item_separator_new();
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.menu_item_separator_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}
#endif
