using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointTransformAffine : PointTransformBase
    {

        #region Constructors

        public PointTransformAffine()
        {
            this.NativePtr = NativeMethods.point_transform_affine_new();
        }

        public PointTransformAffine(Matrix<double> matrix, DPoint vector)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (vector == null)
                throw new ArgumentNullException(nameof(vector));

            matrix.ThrowIfDisposed();

            if (matrix.Columns != 2 || matrix.Rows != 2)
                throw new ArgumentException($"{nameof(matrix)} should be 2x2 matrix");

            using (var native = vector.ToNative())
                this.NativePtr = NativeMethods.point_transform_affine_new1(matrix.NativePtr, native.NativePtr);
        }

        public PointTransformAffine(Matrix<double> matrix, double x, double y)
        {
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            matrix.ThrowIfDisposed();

            if (matrix.Columns != 2 || matrix.Rows != 2)
                throw new ArgumentException($"{nameof(matrix)} should be 2x2 matrix");

            using (var vector = new DPoint(x, y).ToNative())
                this.NativePtr = NativeMethods.point_transform_affine_new1(matrix.NativePtr, vector.NativePtr);
        }

        internal PointTransformAffine(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public DPoint B
        {
            get
            {
                var vector = NativeMethods.point_transform_affine_get_b(this.NativePtr);
                return new DPoint(vector);
            }
        }

        public Matrix<double> M
        {
            get
            {
                var matrix = NativeMethods.point_transform_affine_get_m(this.NativePtr);
                return new Matrix<double>(matrix);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = NativeMethods.point_transform_affine_operator(this.NativePtr, native.NativePtr);
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

            NativeMethods.point_transform_affine_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}