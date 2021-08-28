#if !LITE
using System;
using DlibDotNet.Util;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// Represents an ordered pair of double-precision floating-point x- and y-coordinates that defines a point in a two-dimensional plane.
    /// </summary>
    public struct DPoint : IEquatable<DPoint>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DPoint"/> class with the specified coordinates.
        /// </summary>
        /// <param name="x">The horizontal position of the point.</param>
        /// <param name="y">The vertical position of the point.</param>
        public DPoint(double x, double y)
        {
            this._X = x;
            this._Y = y;
        }

        internal DPoint(IntPtr ptr, bool isEnabledDispose = true)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            using (var native = new NativeDPoint(ptr, isEnabledDispose))
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

        private double _X;

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="DPoint"/>.
        /// </summary>
        /// <value>The x-coordinate of this <see cref="DPoint"/>.</value>
        public double X
        {
            get => this._X;
            set => this._X = value;
        }

        private double _Y;

        /// <summary>
        /// Gets or sets the x-coordinate of this <see cref="DPoint"/>.
        /// </summary>
        /// <value>The x-coordinate of this <see cref="DPoint"/>.</value>
        public double Y
        {
            get => this._Y;
            set => this._Y = value;
        }

        #endregion

        #region Methods

        public bool Equals(DPoint other)
        {
            return this._X.Equals(other._X) && this._Y.Equals(other._Y);
        }

        public DPoint Rotate(DPoint point, double angle)
        {
            using (var src = this.ToNative())
            using (var np = point.ToNative())
            using (var ret = src.Rotate(np, angle))
                return ret.ToManaged();
        }

        public static DPoint Rotate(DPoint center, DPoint point, double angle)
        {
            return center.Rotate(point, angle);
        }

        internal NativeDPoint ToNative()
        {
            return new NativeDPoint(this._X, this._Y);
        }

        #region Overrids

        /// <summary>
        /// Specifies whether this <see cref="DPoint"/> contains the same coordinates as the specified <see cref="Object"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to test.</param>
        /// <returns><code>true</code> if <paramref name="obj"/> is a <see cref="DPoint"/> and has the same coordinates as this <see cref="DPoint"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DPoint && Equals((DPoint)obj);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="DPoint"/>.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="DPoint"/>.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this._X.GetHashCode() * 397) ^ this._Y.GetHashCode();
            }
        }

        public static DPoint operator +(DPoint point, DPoint rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static DPoint operator +(DPoint point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static DPoint operator -(DPoint point, DPoint rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left - right)
                return ret.ToManaged();
        }

        public static DPoint operator -(DPoint point, Point rhs)
        {
            using (var left = point.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left - right)
                return ret.ToManaged();
        }

        public static DPoint operator *(DPoint point, double rhs)
        {
            using (var left = point.ToNative())
            using (var ret = left * rhs)
                return ret.ToManaged();
        }

        public static DPoint operator *(int lhs, DPoint point)
        {
            using (var right = point.ToNative())
            using (var ret = lhs * right)
                return ret.ToManaged();
        }

        public static DPoint operator *(double lhs, DPoint point)
        {
            using (var right = point.ToNative())
            using (var ret = lhs * right)
                return ret.ToManaged();
        }

        public static DPoint operator /(DPoint point, double rhs)
        {
            using (var left = point.ToNative())
            using (var ret = left / rhs)
                return ret.ToManaged();
        }

        /// <summary>
        /// Compares two <see cref="DPoint"/> objects. The result specifies whether the values of the <see cref="X"/> and <see cref="Y"/> properties of the two <see cref="DPoint"/> objects are equal.
        /// </summary>
        /// <param name="left">A <see cref="DPoint"/> to compare.</param>
        /// <param name="right">A <see cref="DPoint"/> to compare.</param>
        /// <returns><code>true</code> if the <see cref="X"/> and <see cref="Y"/> values of <paramref name="left"/> and <paramref name="right"/> are equal; otherwise, <code>false</code>.</returns>
        public static bool operator ==(DPoint left, DPoint right)
        {
            using (var l = left.ToNative())
            using (var r = right.ToNative())
                return l == r;
        }

        /// <summary>
        /// Compares two <see cref="DPoint"/> objects. The result specifies whether the values of the <see cref="X"/> or <see cref="Y"/> properties of the two <see cref="DPoint"/> objects are unequal.
        /// </summary>
        /// <param name="left">A <see cref="DPoint"/> to compare.</param>
        /// <param name="right">A <see cref="DPoint"/> to compare.</param>
        /// <returns><code>true</code> if the values of either the <see cref="X"/> properties or the <see cref="Y"/> properties of <paramref name="left"/> and <paramref name="right"/> differ; otherwise, <code>false</code>.</returns>
        public static bool operator !=(DPoint left, DPoint right)
        {
            using (var l = left.ToNative())
            using (var r = right.ToNative())
                return l != r;
        }

        public static explicit operator Point(DPoint point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        #endregion

        #endregion

#pragma warning disable CS0660, CS0661
        internal sealed class NativeDPoint : VectorBase<double>
        {

            #region Constructors

            public NativeDPoint(double x, double y)
            {
                this.NativePtr = NativeMethods.dpoint_new1(x, y);
            }

            public NativeDPoint()
            {
                this.NativePtr = NativeMethods.dpoint_new();
            }

            internal NativeDPoint(IntPtr ptr, bool isEnabledDispose = true) :
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
                    return NativeMethods.dpoint_length(this.NativePtr);
                }
            }

            public override double LengthSquared
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.dpoint_length_squared(this.NativePtr);
                }
            }

            public override double X
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.dpoint_x(this.NativePtr);
                }
            }

            public override double Y
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.dpoint_y(this.NativePtr);
                }
            }

            public override double Z
            {
                get
                {
                    this.ThrowIfDisposed();
                    return 0;
                }
            }

            #endregion

            #region Methods

            public NativeDPoint Rotate(NativeDPoint point, double angle)
            {
                this.ThrowIfDisposed();

                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ret = NativeMethods.rotate_dpoint(this.NativePtr, point.NativePtr, MathHelper.ConvertToRadian(angle));
                return new NativeDPoint(ret);
            }

            public static NativeDPoint Rotate(NativeDPoint center, NativeDPoint point, double angle)
            {
                return center.Rotate(point, angle);
            }

            public DPoint ToManaged()
            {
                return new DPoint(this.X, this.Y);
            }

            #region Overrides

            public static NativeDPoint operator +(NativeDPoint point, NativeDPoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_add(point.NativePtr, rhs.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator +(NativeDPoint point, Point.NativePoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_add2(point.NativePtr, rhs.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator -(NativeDPoint point, NativeDPoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_sub(point.NativePtr, rhs.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator -(NativeDPoint point, Point.NativePoint rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_sub2(point.NativePtr, rhs.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator *(NativeDPoint point, double rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_mul_dpoint_double(point.NativePtr, rhs);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator *(int lhs, NativeDPoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_mul_int32_dpoint(lhs, point.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator *(double lhs, NativeDPoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                var ptr = NativeMethods.dpoint_operator_mul_double_dpoint(lhs, point.NativePtr);
                return new NativeDPoint(ptr);
            }

            public static NativeDPoint operator /(NativeDPoint point, double rhs)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                if (Math.Abs(rhs) < double.Epsilon)
                    throw new DivideByZeroException();

                var ptr = NativeMethods.dpoint_operator_div(point.NativePtr, rhs);
                return new NativeDPoint(ptr);
            }

            public static bool operator ==(NativeDPoint point, NativeDPoint rhs)
            {
                if (ReferenceEquals(point, rhs))
                    return true;
                if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                    return false;

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return NativeMethods.dpoint_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            public static bool operator !=(NativeDPoint point, NativeDPoint rhs)
            {
                if (ReferenceEquals(point, rhs))
                    return false;
                if (ReferenceEquals(point, null) || ReferenceEquals(rhs, null))
                    return true;

                point.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return !NativeMethods.dpoint_operator_equal(point.NativePtr, rhs.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.dpoint_delete(this.NativePtr);
            }

            #endregion

            #endregion

        }
#pragma warning restore CS0660, CS0661

    }

}
#endif
