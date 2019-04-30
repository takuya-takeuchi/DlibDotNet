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
            if (menuItemText == null)
                throw new ArgumentNullException(nameof(menuItemText));

            this.ThrowIfDisposed();
            menuItemText.ThrowIfDisposed();

            return NativeMethods.popup_menu_add_menu_item_menu_item_text(this.NativePtr, menuItemText.NativePtr);
        }

        public uint AddMenuItem(MenuItemSeparator separator)
        {
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            this.ThrowIfDisposed();
            separator.ThrowIfDisposed();

            return NativeMethods.popup_menu_add_menu_item_menu_item_separator(this.NativePtr, separator.NativePtr);
        }
        
        #region Overrids
        #endregion

        #endregion

    }

}