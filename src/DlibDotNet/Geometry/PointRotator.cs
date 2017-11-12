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
            this.NativePtr = Native.point_rotator_new();
        }

        public PointRotator(double angle)
        {
            this.NativePtr = Native.point_rotator_new1(angle);
        }

        #endregion

        #region Properties

        public Matrix<double> M
        {
            get
            {
                var matrix = Native.point_rotator_get_m(this.NativePtr);
                return new Matrix<double>(matrix, MatrixElementTypes.Double);
            }
        }

        #endregion

        #region Methods

        public override DPoint Operator(DPoint point)
        {
            using (var native = point.ToNative())
            {
                var ptr = Native.point_rotator_operator(this.NativePtr, native.NativePtr);
                return new DPoint(ptr);
            }
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.point_rotator_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_rotator_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_rotator_new1(double angle);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_rotator_get_m(IntPtr obj);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_rotator_operator(IntPtr obj, IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void point_rotator_delete(IntPtr obj);

        }

    }

}