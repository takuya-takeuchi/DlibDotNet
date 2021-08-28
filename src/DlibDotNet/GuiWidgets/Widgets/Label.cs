#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Label : Drawable
    {

        #region Constructors

        public Label(DrawableWindow window) :
            base(window)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.label_new(window.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        public void SetText(string name)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var str = Dlib.Encoding.GetBytes(name ?? "");
            NativeMethods.label_set_text(this.NativePtr, str, str.Length);
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

            NativeMethods.label_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}
#endif
