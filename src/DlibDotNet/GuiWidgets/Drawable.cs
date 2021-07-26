using System;

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
#if DLIB_NO_GUI_SUPPORT
            throw new NotSupportedException();
#endif
            this._Window = window;
        }

        #endregion

        #region Properties

        public int Bottom
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_bottom(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public uint Height
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_height(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public int Left
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_left(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public int Right
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_right(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public int Top
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_top(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        public uint Width
        {
            get
            {
#if !DLIB_NO_GUI_SUPPORT
                this.ThrowIfDisposed();
                return NativeMethods.drawable_get_width(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
            }
        }

        #endregion

        #region Methods

        public void SetPos(int x, int y)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.drawable_set_pos(this.NativePtr, x, y);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

    }

}