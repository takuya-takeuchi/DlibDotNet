using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ListBox : ScrollableRegion
    {

        #region Constructors

        public ListBox(DrawableWindow window) :
            base(window)
        {
            this.NativePtr = NativeMethods.list_box_new(window.NativePtr);
        }

        #endregion

        #region Properties
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

            NativeMethods.list_box_delete(this.NativePtr);
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}