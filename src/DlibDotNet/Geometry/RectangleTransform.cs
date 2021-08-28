#if !LITE
using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class RectangleTransform : DlibObject
    {

        #region Constructors

        public RectangleTransform()
        {
            this.NativePtr = NativeMethods.rectangle_transform_new();
        }

        public RectangleTransform(PointTransformAffine transform)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            transform.ThrowIfDisposed();

            this.NativePtr = NativeMethods.rectangle_transform_new1(transform.NativePtr);
        }

        #endregion

        #region Properties

        public PointTransformAffine Transform
        {
            get
            {
                var matrix = NativeMethods.rectangle_transform_get_tform(this.NativePtr);
                return new PointTransformAffine(matrix);
            }
        }

        #endregion

        #region Methods

        public Rectangle Operator(Rectangle rectangle)
        {
            using (var native = rectangle.ToNative())
            {
                var ptr = NativeMethods.rectangle_transform_operator(this.NativePtr, native.NativePtr);
                return new Rectangle(ptr);
            }
        }

        public DRectangle Operator(DRectangle drectangle)
        {
            using (var native = drectangle.ToNative())
            {
                var ptr = NativeMethods.rectangle_transform_operator_d(this.NativePtr, native.NativePtr);
                return new DRectangle(ptr);
            }
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.rectangle_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
