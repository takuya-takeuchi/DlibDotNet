

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Drawable : DlibObject
    {

        #region Fields

        private readonly DrawableWindow _Window;

        #endregion

        #region Constructors

        protected Drawable(DrawableWindow window)
        {
            this._Window = window;
        }

        #endregion

        #region Properties

        public int Bottom
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_bottom(this.NativePtr);
            }
        }

        public uint Height
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_height(this.NativePtr);
            }
        }

        public int Left
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_left(this.NativePtr);
            }
        }

        public int Right
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_right(this.NativePtr);
            }
        }

        public int Top
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_top(this.NativePtr);
            }
        }

        public uint Width
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_width(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public void SetPos(int x, int y)
        {
            this.ThrowIfDisposed();
            NativeMethods.drawable_set_pos(this.NativePtr, x, y);
        }

        #endregion

    }

}