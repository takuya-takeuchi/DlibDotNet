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
            this.NativePtr = NativeMethods.label_new(window.NativePtr);
        }

        #endregion

        #region Methods

        public void SetText(string name)
        {
            this.ThrowIfDisposed();
            var str = Dlib.Encoding.GetBytes(name ?? "");
            var strLength = str.Length;
            Array.Resize(ref str, strLength + 1);
            str[strLength] = (byte)'\0';
            NativeMethods.label_set_text(this.NativePtr, str);
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

            NativeMethods.label_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}