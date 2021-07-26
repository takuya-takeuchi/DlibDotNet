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
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.menu_bar_new(window.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        public PopupMenu Menu(uint index)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var ret = NativeMethods.menu_bar_menu(this.NativePtr, index);
            return new PopupMenu(ret, false);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetNumberOfMenus(uint num)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.menu_bar_set_number_of_menus(this.NativePtr, num);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetMenuName(uint index, string name, char underline)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var str = Dlib.Encoding.GetBytes(name ?? "");
            NativeMethods.menu_bar_set_menu_name(this.NativePtr, index, str, str.Length, underline);
#else
            throw new NotSupportedException();
#endif
        }

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

            NativeMethods.menu_bar_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}