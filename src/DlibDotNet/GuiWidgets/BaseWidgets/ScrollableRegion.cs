#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class ScrollableRegion : Drawable
    {

        #region Constructors

        protected ScrollableRegion(DrawableWindow window) :
            base(window)
        {
#if DLIB_NO_GUI_SUPPORT
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        public void SetSize(uint width, uint height)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.scrollable_region_set_size(this.NativePtr, width, height);
#else
            throw new NotSupportedException();
#endif
        }

        #region Overrides

        protected override void SetPosCore(int x, int y)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.scrollable_region_set_pos(this.NativePtr, x, y);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

    }

}
#endif
