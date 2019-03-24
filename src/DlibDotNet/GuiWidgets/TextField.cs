using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class TextField : Drawable
    {

        #region Fields

        private readonly DrawableWindow _Window;

        #endregion

        #region Constructors

        public TextField(DrawableWindow window) :
            base(window)
        {
            this._Window = window;
            this.NativePtr = NativeMethods.text_field_new(window.NativePtr);
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void SetText(string name)
        {
            this.ThrowIfDisposed();
            var str = Dlib.Encoding.GetBytes(name ?? "");
            NativeMethods.text_field_set_text(this.NativePtr, str);
        }

        public void SetWidth(uint width)
        {
            this.ThrowIfDisposed();
            NativeMethods.text_field_set_width(this.NativePtr, width);
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

            NativeMethods.text_field_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}