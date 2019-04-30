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

        public bool HasInputFocus
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.text_field_has_input_focus(this.NativePtr);
            }
        }

        public string Text
        {
            get
            {
                this.ThrowIfDisposed();
                NativeMethods.text_field_get_text(this.NativePtr, out var str);
                return StringHelper.FromStdString(str, true);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.text_field_set_text(this.NativePtr, str);
            }
        }

        #endregion

        #region Methods

        public void GiveInputFocus()
        {
            this.ThrowIfDisposed();
            NativeMethods.text_field_select_all_text(this.NativePtr);
        }

        public void SelectAllText()
        {
            this.ThrowIfDisposed();
            NativeMethods.text_field_select_all_text(this.NativePtr);
        }

        public void SetWidth(uint width)
        {
            this.ThrowIfDisposed();
            NativeMethods.text_field_set_width(this.NativePtr, width);
        }

        public void SetTextModifiedHandler(VoidActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            mediator.ThrowIfDisposed();

            NativeMethods.text_field_set_text_modified_handler(this.NativePtr, mediator.NativePtr);
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