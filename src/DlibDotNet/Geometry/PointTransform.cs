using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransform : PointTransformBase
    {

        #region Constructors

        public PointTransform()
        {
            this.NativePtr = NativeMethods.point_transform_new();
        }

        public PointTransform(double angle, DPoint vector)
        {
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            using (var native = vector.ToNative())
                this.NativePtr = NativeMethods.point_transform_new1(angle, native.NativePtr);
        }

        public PointTransform(double angle, double x, double y)
        {
            using (var native = new DPoint(x, y).ToNative())
                this.NativePtr = NativeMethods.point_transform_new1(angle, native.NativePtr);
        }

        #endregion

        #region Properties

        public DPoint B
        {
            get
            {
                var vector = NativeMethods.point_transform_get_b(this.NativePtr);
                return new DPoint(vector);
            }
        }

        public Matrix<double> M
        {
            get
            {
                var matrix = NativeMethods.point_transform_get_m(this.NativePtr);
                return new Matrix<double>(matrix);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = NativeMethods.point_transform_operator(this.NativePtr, native.NativePtr);
                return new DPoint(ptr);
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

            NativeMethods.point_transform_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
