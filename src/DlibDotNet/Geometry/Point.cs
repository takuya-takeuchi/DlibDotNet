using System;
using System.Runtime.InteropServices;
using DlibDotNet.Util;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Point : Vector<int>
    {

        #region Constructors

        public Point(int x, int y)
        {
            this.NativePtr = Native.point_new1(x, y);
        }

        public Point()
        {
            this.NativePtr = Native.point_new();
        }

        internal Point(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public override double Length
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.point_length(this.NativePtr);
            }
        }

        public override double LengthSquared
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.point_length_squared(this.NativePtr);
            }
        }

        public override int X
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.point_x(this.NativePtr);
            }
        }

        public override int Y
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.point_y(this.NativePtr);
            }
        }

        public override int Z
        {
            get
            {
                this.ThrowIfDisposed();
                return 0;
            }
        }

        #endregion

        #region Methods

        public Point Rotate(Point point, double angle)
        {
            this.ThrowIfDisposed();

            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            var ret = Native.rotate_point(this.NativePtr, point.NativePtr, MathHelper.ConvertToRadian(angle));
            return new Point(ret);
        }

        public static Point Rotate(Point center, Point point, double angle)
        {
            return center.Rotate(point, angle);
        }

        #region Overrides

        public static Point operator +(Point point, Point rhs)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            point.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var ptr = Native.point_operator_add(point.NativePtr, rhs.NativePtr);
            return new Point(ptr);
        }

        public static Point operator -(Point point, Point rhs)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            point.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var ptr = Native.point_operator_sub(point.NativePtr, rhs.NativePtr);
            return new Point(ptr);
        }

        public static Point operator *(Point point, int rhs)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            var ptr = Native.point_operator_mul(point.NativePtr, rhs);
            return new Point(ptr);
        }

        public static Point operator /(Point point, int rhs)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            if (rhs == 0)
                throw new DivideByZeroException();

            var ptr = Native.point_operator_div(point.NativePtr, rhs);
            return new Point(ptr);
        }

        public static bool operator ==(Point point, Point rhs)
        {
            if (ReferenceEquals(point, rhs))
                return true;
            if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                return false;

            point.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return Native.point_operator_equal(point.NativePtr, rhs.NativePtr);
        }

        public static bool operator !=(Point point, Point rhs)
        {
            if (ReferenceEquals(point, rhs))
                return false;
            if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                return true;

            point.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return !Native.point_operator_equal(point.NativePtr, rhs.NativePtr);
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.point_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_new1(int x, int y);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void point_delete(IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double point_length(IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double point_length_squared(IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int point_x(IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int point_y(IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_operator_add(IntPtr point, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_operator_sub(IntPtr point, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_operator_mul(IntPtr point, int rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr point_operator_div(IntPtr point, int rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool point_operator_equal(IntPtr point, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rotate_point(IntPtr center, IntPtr p, double rhs);

        }

    }

}
