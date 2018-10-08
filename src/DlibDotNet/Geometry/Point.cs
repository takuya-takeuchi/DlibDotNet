using System;
using System.Runtime.InteropServices;
using DlibDotNet.Util;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public struct Point : IEquatable<Point>
    {

        #region Constructors

        public Point(int x, int y)
        {
            this._X = x;
            this._Y = y;
        }

        internal Point(IntPtr nativePtr)
        {
            using (var native = new NativePoint(nativePtr))
            {
                this._X = native.X;
                this._Y = native.Y;
            }
        }

        internal Point(IntPtr ptr, bool isEnabledDispose = true)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            using (var native = new NativePoint(ptr, isEnabledDispose))
            {
                this._X = native.X;
                this._Y = native.Y;
            }
        }

        #endregion

        #region Properties

        public double Length
        {
            get
            {
                using (var point = this.ToNative())
                    return point.Length;
            }
        }

        public double LengthSquared
        {
            get
            {
                using (var point = this.ToNative())
                    return point.LengthSquared;
            }
        }

        private int _X;

        public int X
        {
            get => this._X;
            set => this._X = value;
        }

        private int _Y;

        public int Y
        {
            get => this._Y;
            set => this._Y = value;
        }

        #endregion

        #region Methods

        public bool Equals(Point other)
        {
            return this._X == other._X && this._Y == other._Y;
        }

        public Point Rotate(Point point, double angle)
        {
            using (var src = this.ToNative())
            using (var np = point.ToNative())
            using (var ret = src.Rotate(np, angle))
                return ret.ToManaged();
        }

        public static Point Rotate(Point center, Point point, double angle)
        {
            return center.Rotate(point, angle);
        }

        internal NativePoint ToNative()
        {
            return new NativePoint(this._X, this._Y);
        }

        #region Overrids

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this._X * 397) ^ this._Y;
            }
        }

        public override string ToString()
        {
            using (var point = this.ToNative())
                return point.ToString();
        }

        public static Point operator +(Point point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static Point operator -(Point point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left - right)
                return ret.ToManaged();
        }

        public static Point operator *(Point point, int rhs)
        {
            using (var left = point.ToNative())
            using (var ret = left * rhs)
                return ret.ToManaged();
        }

        public static Point operator *(int lhs, Point point)
        {
            using (var right = point.ToNative())
            using (var ret = lhs * right)
                return ret.ToManaged();
        }

        public static Point operator *(double lhs, Point point)
        {
            using (var right = point.ToNative())
            using (var ret = lhs * right)
                return ret.ToManaged();
        }

        public static Point operator /(Point point, int rhs)
        {
            using (var left = point.ToNative())
            using (var ret = left / rhs)
                return ret.ToManaged();
        }

        public static bool operator ==(Point point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
                return left == right;
        }

        public static bool operator !=(Point point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
                return left != right;
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

#pragma warning disable CS0660, CS0661
        internal sealed class NativePoint : VectorBase<int>
        {

            #region Constructors

            public NativePoint(int x, int y)
            {
                this.NativePtr = Native.point_new1(x, y);
            }

            public NativePoint()
            {
                this.NativePtr = Native.point_new();
            }

            internal NativePoint(IntPtr ptr, bool isEnabledDispose = true) :
                base(isEnabledDispose)
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

            public NativePoint Rotate(NativePoint point, double angle)
            {
                this.ThrowIfDisposed();

                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ret = Native.rotate_point(this.NativePtr, point.NativePtr, MathHelper.ConvertToRadian(angle));
                return new NativePoint(ret);
            }

            public static NativePoint Rotate(NativePoint center, NativePoint point, double angle)
            {
                return center.Rotate(point, angle);
            }

            public Point ToManaged()
            {
                return new Point(this.X, this.Y);
            }

            #region Overrides

            public static NativePoint operator +(NativePoint point, NativePoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = Native.point_operator_add(point.NativePtr, rhs.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator -(NativePoint point, NativePoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = Native.point_operator_sub(point.NativePtr, rhs.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(NativePoint point, int rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = Native.point_operator_mul_point_int32(point.NativePtr, rhs);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(int lhs, NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = Native.point_operator_mul_int32_point(lhs, point.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(double lhs, NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = Native.point_operator_mul_double_point(lhs, point.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator /(NativePoint point, int rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                if (rhs == 0)
                    throw new DivideByZeroException();

                var ptr = Native.point_operator_div(point.NativePtr, rhs);
                return new NativePoint(ptr);
            }

            public static bool operator ==(NativePoint point, NativePoint rhs)
            {
                if (ReferenceEquals(point, rhs))
                    return true;
                if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                    return false;

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return Native.point_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            public static bool operator !=(NativePoint point, NativePoint rhs)
            {
                if (ReferenceEquals(point, rhs))
                    return false;
                if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                    return true;

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return !Native.point_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            public override string ToString()
            {
                var ofstream = IntPtr.Zero;
                var stdstr = IntPtr.Zero;
                var str = "";

                try
                {
                    ofstream = Dlib.Native.ostringstream_new();
                    Native.point_operator_left_shift(this.NativePtr, ofstream);
                    stdstr = Dlib.Native.ostringstream_str(ofstream);
                    str = StringHelper.FromStdString(stdstr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    if (stdstr != IntPtr.Zero)
                        Dlib.Native.string_delete(stdstr);
                    if (ofstream != IntPtr.Zero)
                        Dlib.Native.ostringstream_delete(ofstream);
                }

                return str;
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                Native.point_delete(this.NativePtr);
            }

            #endregion

            #endregion

            private sealed class Native
            {

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr point_new();

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr point_new1(int x, int y);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void point_delete(IntPtr point);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern void point_operator_left_shift(IntPtr point, IntPtr ofstream);

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
                public static extern IntPtr point_operator_mul_point_int32(IntPtr point, int rhs);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr point_operator_mul_int32_point(int lhs, IntPtr point);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr point_operator_mul_double_point(double lhs, IntPtr point);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr point_operator_div(IntPtr point, int rhs);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                [return: MarshalAs(UnmanagedType.U1)]
                public static extern bool point_operator_equal(IntPtr point, IntPtr rhs);

                [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
                public static extern IntPtr rotate_point(IntPtr center, IntPtr p, double rhs);

            }

        }
#pragma warning restore CS0660, CS0661

    }

}
