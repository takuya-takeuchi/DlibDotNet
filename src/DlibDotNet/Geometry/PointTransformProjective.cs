using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransformProjective : PointTransformBase
    {

        #region Constructors

        public PointTransformProjective()
        {
            this.NativePtr = NativeMethods.point_transform_projective_new();
        }

        public PointTransformProjective(Matrix<double> matrix)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (matrix.Columns != 3 || matrix.Rows != 3)
                throw new ArgumentException($"{nameof(matrix)} should be 3x3 matrix");

            this.NativePtr = NativeMethods.point_transform_projective_new1(matrix.NativePtr);
        }

        #endregion

        #region Properties

        public Matrix<double> M
        {
            get
            {
                var matrix = NativeMethods.point_transform_projective_get_m(this.NativePtr);
                return new Matrix<double>(matrix);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = NativeMethods.point_transform_projective_operator(this.NativePtr, native.NativePtr);
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

            NativeMethods.point_transform_projective_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}