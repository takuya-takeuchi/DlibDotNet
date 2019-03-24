// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class ScrollableRegion : Drawable
    {

        #region Events
        #endregion

        #region Fields
        #endregion

        #region Constructors

        protected ScrollableRegion(DrawableWindow window) :
            base(window)
        {
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void SetPos(int x, int y)
        {
            this.ThrowIfDisposed();
            NativeMethods.scrollable_region_set_pos(this.NativePtr, x, y);
        }

        public void SetSize(uint width, uint height)
        {
            this.ThrowIfDisposed();
            NativeMethods.scrollable_region_set_size(this.NativePtr, width, height);
        }

        #region Overrids
        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

    }

}