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
#if !DLIB_NO_GUI_SUPPORT
            this._Window = window;
            this.NativePtr = NativeMethods.text_field_new(window.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Properties

        public bool HasInputFocus
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.text_field_has_input_focus(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public string Text
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                NativeMethods.text_field_get_text(this.NativePtr, out var str);
                return StringHelper.FromStdString(str, true);
#else
                throw new NotSupportedException();
#endif
            }
            set
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.text_field_set_text(this.NativePtr, str, str.Length);
#else
                throw new NotSupportedException();
#endif
            }
        }

        #endregion

        #region Methods

        public void GiveInputFocus()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.text_field_select_all_text(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SelectAllText()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.text_field_select_all_text(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetWidth(uint width)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.text_field_set_width(this.NativePtr, width);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetTextModifiedHandler(VoidActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.text_field_set_text_modified_handler(this.NativePtr, mediator.NativePtr);
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

            NativeMethods.text_field_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}