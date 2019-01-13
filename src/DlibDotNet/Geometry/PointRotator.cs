using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PointRotator : PointTransformBase
    {

        #region Constructors

        public PointRotator()
        {
            this.NativePtr = NativeMethods.point_rotator_new();
        }

        public PointRotator(double angle)
        {
            this.NativePtr = NativeMethods.point_rotator_new1(angle);
        }

        #endregion

        #region Properties

        public Matrix<double> M
        {
            get
            {
                var matrix = NativeMethods.point_rotator_get_m(this.NativePtr);
                return new Matrix<double>(matrix);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = NativeMethods.point_rotator_operator(this.NativePtr, native.NativePtr);
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

            NativeMethods.point_rotator_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}