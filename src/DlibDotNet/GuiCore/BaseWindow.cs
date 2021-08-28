#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class BaseWindow : DlibObject
    {

        #region Constructors

        protected BaseWindow(bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
#if DLIB_NO_GUI_SUPPORT
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Properties

        public string Title
        {
            set
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                var title = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.base_window_set_title(this.NativePtr, title, title.Length);
#else
                throw new NotSupportedException();
#endif
            }
        }

        #endregion

        #region Methods

        public void CloseWindow()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_close_window(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
        }

        public void GetDisplaySize(out uint width, out uint height)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_get_display_size(this.NativePtr, out width, out height);
#else
            throw new NotSupportedException();
#endif
        }

        public void GetSize(out uint width, out uint height)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_get_size(this.NativePtr, out width, out height);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetPos(int x, int y)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_set_pos(this.NativePtr, x, y);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetSize(int width, int height)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_set_size(this.NativePtr, width, height);
#else
            throw new NotSupportedException();
#endif
        }

        public void Show()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_show(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void WaitUntilClosed()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.base_window_wait_until_closed(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}
#endif
