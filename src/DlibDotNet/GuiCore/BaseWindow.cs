

// ReSharper disable once CheckNamespace

using System;

namespace DlibDotNet
{

    public abstract class BaseWindow : DlibObject
    {

        #region Constructors

        protected BaseWindow(bool isEnabledDispose = true) :
            base(isEnabledDispose)
        {
        }

        #endregion

        #region Properties

        public string Title
        {
            set
            {
                this.ThrowIfDisposed();
                var title = Dlib.Encoding.GetBytes(value ?? "");
                var strLength = title.Length;
                Array.Resize(ref title, strLength + 1);
                title[strLength] = (byte)'\0';
                NativeMethods.base_window_set_title(this.NativePtr, title);
            }
        }

        #endregion

        #region Methods

        public void CloseWindow()
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_close_window(this.NativePtr);
        }

        public void GetDisplaySize(out uint width, out uint height)
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_get_display_size(this.NativePtr, out width, out height);
        }

        public void GetSize(out uint width, out uint height)
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_get_size(this.NativePtr, out width, out height);
        }

        public void SetPos(int x, int y)
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_set_pos(this.NativePtr, x, y);
        }

        public void SetSize(int width, int height)
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_set_size(this.NativePtr, width, height);
        }

        public void Show()
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_show(this.NativePtr);
        }

        public void WaitUntilClosed()
        {
            this.ThrowIfDisposed();
            NativeMethods.base_window_wait_until_closed(this.NativePtr);
        }

        #endregion

    }

}