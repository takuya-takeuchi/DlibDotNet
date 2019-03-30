using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MenuItemSeparator : MenuItem
    {

        #region Constructors

        public MenuItemSeparator()
        {
            this.NativePtr = NativeMethods.menu_item_separator_new();
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

            NativeMethods.menu_item_separator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}