using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class MenuBar : Drawable
    {

        #region Constructors

        public MenuBar(DrawableWindow window) :
            base(window)
        {
            this.NativePtr = NativeMethods.menu_bar_new(window.NativePtr);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void SetNumberOfMenus(uint num)
        {
            this.ThrowIfDisposed();
            NativeMethods.menu_bar_set_number_of_menus(this.NativePtr, num);
        }

        public void SetMenuName(uint index, string name, char underline)
        {
            this.ThrowIfDisposed();
            var str = Dlib.Encoding.GetBytes(name ?? "");
            NativeMethods.menu_bar_set_menu_name(this.NativePtr, index, str, underline);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.menu_bar_delete(this.NativePtr);
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}