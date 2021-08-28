using System;
using DlibDotNet.Util;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an ordered pair of integer x- and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct Point : IEquatable<Point>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Point"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">The horizontal position of the point.</param>
        /// <param name="y">The vertical position of the point.</param>
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

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="Point"/>.
        /// </summary>
        /// <value>The x-coordinate of this <see cref="Point"/>.</value>
        public int X
        {
            get => this._X;
            set => this._X = value;
        }

        private int _Y;

        /// <summary>
        /// Gets or sets the y-coordinate of this <see cref="Point"/>.
        /// </summary>
        /// <value>The y-coordinate of this <see cref="Point"/>.</value>
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

        /// <summary>
        /// Specifies whether this <see cref="Point"/> contains the same coordinates as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="Point"/> and has the same coordinates as this <see cref="Point"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Point && Equals((Point)obj);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Point"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="Point"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this._X * 397) ^ this._Y;
            }
        }

        /// <summary>
        /// Converts this <see cref="Point"/> to a human-readable string.
        /// </summary>
        /// <returns>A string that represents this <see cref="Point"/>.</returns>
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

        /// <summary>
        /// Compares two <see cref="Point"/> objects. The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> properties of the two <see cref="Point"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="Point"/> to compare.</param>
        /// <param name="right">A <see cref="Point"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="X"/> and <see cref="Y"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(Point left, Point right)
        {
            using (var l = left.ToNative())
            using (var r = right.ToNative())
                return l == r;
        }

        /// <summary>
        /// Compares two <see cref="Point"/> objects. The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> properties of the two <see cref="Point"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="Point"/> to compare.</param>
        /// <param name="right">A <see cref="Point"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="X"/> properties or the <see cref="Y"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(Point left, Point right)
        {
            using (var l = left.ToNative())
            using (var r = right.ToNative())
                return l != r;
        }

        #endregion

        #endregion

#pragma warning disable CS0660, CS0661
        internal sealed class NativePoint : VectorBase<int>
        {

            #region Constructors

            public NativePoint(int x, int y)
            {
                this.NativePtr = NativeMethods.point_new1(x, y);
            }

            public NativePoint()
            {
                this.NativePtr = NativeMethods.point_new();
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
                    return NativeMethods.point_length(this.NativePtr);
                }
            }

            public override double LengthSquared
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.point_length_squared(this.NativePtr);
                }
            }

            public override int X
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.point_x(this.NativePtr);
                }
            }

            public override int Y
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.point_y(this.NativePtr);
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

                var ret = NativeMethods.rotate_point(this.NativePtr, point.NativePtr, MathHelper.ConvertToRadian(angle));
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

                var ptr = NativeMethods.point_operator_add(point.NativePtr, rhs.NativePtr);
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

                var ptr = NativeMethods.point_operator_sub(point.NativePtr, rhs.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(NativePoint point, int rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.point_operator_mul_point_int32(point.NativePtr, rhs);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(int lhs, NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.point_operator_mul_int32_point(lhs, point.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator *(double lhs, NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.point_operator_mul_double_point(lhs, point.NativePtr);
                return new NativePoint(ptr);
            }

            public static NativePoint operator /(NativePoint point, int rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                if (rhs == 0)
                    throw new DivideByZeroException();

                var ptr = NativeMethods.point_operator_div(point.NativePtr, rhs);
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

                return NativeMethods.point_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            public static bool operator !=(NativePoint point, NativePoint rhs)
            {
                if (ReferenceEquals(point, rhs))
                    return false;
                if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                    return true;

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return !NativeMethods.point_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            public override string ToString()
            {
                var ofstream = IntPtr.Zero;
                var stdstr = IntPtr.Zero;
                var str = "";

                try
                {
                    ofstream = NativeMethods.ostringstream_new();
                    NativeMethods.point_operator_left_shift(this.NativePtr, ofstream);
                    stdstr = NativeMethods.ostringstream_str(ofstream);
                    str = StringHelper.FromStdString(stdstr);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    if (stdstr != IntPtr.Zero)
                        NativeMethods.string_delete(stdstr);
                    if (ofstream != IntPtr.Zero)
                        NativeMethods.ostringstream_delete(ofstream);
                }

                return str;
            }

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.point_delete(this.NativePtr);
            }

            #endregion

            #endregion

        }
#pragma warning restore CS0660, CS0661

    }

}
