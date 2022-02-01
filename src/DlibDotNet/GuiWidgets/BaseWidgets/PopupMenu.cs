using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PopupMenu : BaseWindow
    {

        #region Constructors

        internal PopupMenu(IntPtr ptr, bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public uint AddMenuItem(MenuItemText menuItemText)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (menuItemText == null)
                throw new ArgumentNullException(nameof(menuItemText));

            this.ThrowIfDisposed();
            menuItemText.ThrowIfDisposed();

            return NativeMethods.popup_menu_add_menu_item_menu_item_text(this.NativePtr, menuItemText.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public uint AddMenuItem(MenuItemSeparator separator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            this.ThrowIfDisposed();
            separator.ThrowIfDisposed();

            return NativeMethods.popup_menu_add_menu_item_menu_item_separator(this.NativePtr, separator.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}