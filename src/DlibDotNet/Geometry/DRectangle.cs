using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public struct DRectangle : IEquatable<DRectangle>
    {

        #region Constructors

        public DRectangle(Rectangle rect)
        {
            using (var native = new NativeDRectangle(rect.Left, rect.Top, rect.Right, rect.Bottom))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public DRectangle(double left, double top, double right, double bottom)
        {
            using (var native = new NativeDRectangle(left, top, right, bottom))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public DRectangle(DPoint point)
        {
            using (var np = point.ToNative())
            using (var native = new NativeDRectangle(np))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public DRectangle(DPoint p1, DPoint p2)
        {
            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            using (var native = new NativeDRectangle(np1, np2))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public DRectangle(DRectangle drect)
        {
            using (var src = drect.ToNative())
            using (var native = new NativeDRectangle(src))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        internal DRectangle(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            using (var native = new NativeDRectangle(ptr))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        #endregion

        #region Properties

        public double Area
        {
            get
            {
                using (var native = this.ToNative())
                    return native.Area;
            }
        }

        private double _Bottom;

        public double Bottom
        {
            get => this._Bottom;
            set => this._Bottom = value;
        }

        public DPoint BottomLeft
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.BottomLeft)
                    return point.ToManaged();
            }
        }

        public DPoint BottomRight
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.BottomRight)
                    return point.ToManaged();
            }
        }

        public DPoint Center
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.Center)
                    return point.ToManaged();
            }
        }

        public DPoint DCenter
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.DCenter)
                    return point.ToManaged();
            }
        }

        public double Height
        {
            get
            {
                using (var native = this.ToNative())
                    return native.Height;
            }
        }

        public bool IsEmpty
        {
            get
            {
                using (var native = this.ToNative())
                    return native.IsEmpty;
            }
        }

        private double _Left;

        public double Left
        {
            get => this._Left;
            set => this._Left = value;
        }

        private double _Right;

        public double Right
        {
            get => this._Right;
            set => this._Right = value;
        }

        private double _Top;

        public double Top
        {
            get => this._Top;
            set => this._Top = value;
        }

        public DPoint TopLeft
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.TopLeft)
                    return point.ToManaged();
            }
        }

        public DPoint TopRight
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.TopRight)
                    return point.ToManaged();
            }
        }

        public double Width
        {
            get
            {
                using (var native = this.ToNative())
                    return native.Width;
            }
        }

        #endregion

        #region Methods

        public static DRectangle CenteredRect(double x, double y, double width, double height)
        {
            var p = new DPoint(x, y);
            return CenteredRect(p, width, height);
        }

        public static DRectangle CenteredRect(DPoint p, double width, double height)
        {
            using (var np = p.ToNative())
            using (var ret = NativeDRectangle.CenteredRect(np, width, height))
                return ret.ToManaged();
        }

        public static DRectangle CenteredRect(DRectangle drect, double width, double height)
        {
            using (var nr = drect.ToNative())
            using (var ret = NativeDRectangle.CenteredRect(nr, width, height))
                return ret.ToManaged();
        }

        public bool Contains(DPoint point)
        {
            using (var np = point.ToNative())
            using (var nr = this.ToNative())
                return nr.Contains(np);
        }

        public bool Contains(DRectangle drect)
        {
            using (var np = drect.ToNative())
            using (var nr = this.ToNative())
                return nr.Contains(np);
        }

        public bool Equals(DRectangle other)
        {
            return this._Bottom.Equals(other._Bottom) && this._Left.Equals(other._Left) && this._Right.Equals(other._Right) && this._Top.Equals(other._Top);
        }

        public static DRectangle SetAspectRatio(DRectangle rect, double ratio)
        {
            if(!(ratio > 0))
                throw new ArgumentOutOfRangeException(nameof(ratio));

            using (var nr = rect.ToNative())
            using (var result = nr.SetAspectRatio(ratio))
                return result.ToManaged();
        }

        public DRectangle Intersect(DRectangle drect)
        {
            using (var np = drect.ToNative())
            using (var nr = this.ToNative())
            using (var ret = nr.Intersect(np))
                return ret.ToManaged();
        }

        public DRectangle Translate(Point point)
        {
            return Translate(this, point);
        }

        public static DRectangle Translate(DRectangle drect, Point point)
        {
            using (var np = point.ToNative())
            using (var nr = drect.ToNative())
            using (var ret = NativeDRectangle.Translate(nr, np))
                return ret.ToManaged();
        }

        public DRectangle Translate(DPoint point)
        {
            return Translate(this, point);
        }

        public static DRectangle Translate(DRectangle drect, DPoint point)
        {
            using (var np = point.ToNative())
            using (var nr = drect.ToNative())
            using (var ret = NativeDRectangle.Translate(nr, np))
                return ret.ToManaged();
        }

        internal NativeDRectangle ToNative()
        {
            return new NativeDRectangle(this._Left, this._Top, this._Right, this._Bottom);
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is DRectangle && Equals((DRectangle)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this._Bottom.GetHashCode();
                hashCode = (hashCode * 397) ^ this._Left.GetHashCode();
                hashCode = (hashCode * 397) ^ this._Right.GetHashCode();
                hashCode = (hashCode * 397) ^ this._Top.GetHashCode();
                return hashCode;
            }
        }

        public static explicit operator Rectangle(DRectangle drect)
        {
            using (var native = drect.ToNative())
            using (var ret = (Rectangle.NativeRectangle)native)
                return ret.ToManaged();
        }

        public static DRectangle operator +(DRectangle drect, DRectangle rhs)
        {
            using (var left = drect.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static DRectangle operator *(DRectangle drect, double rhs)
        {
            using (var left = drect.ToNative())
            using (var ret = left * rhs)
                return ret.ToManaged();
        }

        public static DRectangle operator /(DRectangle drect, double rhs)
        {
            using (var left = drect.ToNative())
            using (var ret = left / rhs)
                return ret.ToManaged();
        }

        public static bool operator ==(DRectangle rect, DRectangle rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
                return left == right;
        }

        public static bool operator !=(DRectangle rect, DRectangle rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
                return left != right;
        }

        #endregion

        #endregion

#pragma warning disable CS0660, CS0661
        internal sealed class NativeDRectangle : DlibObject
        {

            #region Constructors

            public NativeDRectangle()
            {
                this.NativePtr = NativeMethods.drectangle_new();
            }

            public NativeDRectangle(double left, double top, double right, double bottom)
            {
                this.NativePtr = NativeMethods.drectangle_new1(left, top, right, bottom);
            }

            public NativeDRectangle(DPoint.NativeDPoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                this.NativePtr = NativeMethods.drectangle_new2(point.NativePtr);
            }

            public NativeDRectangle(DPoint.NativeDPoint p1, DPoint.NativeDPoint p2)
            {
                if (p1 == null)
                    throw new ArgumentNullException(nameof(p1));
                if (p2 == null)
                    throw new ArgumentNullException(nameof(p2));

                p1.ThrowIfDisposed();
                p2.ThrowIfDisposed();

                this.NativePtr = NativeMethods.drectangle_new3(p1.NativePtr, p2.NativePtr);
            }

            public NativeDRectangle(NativeDRectangle drect)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                this.NativePtr = NativeMethods.drectangle_new4(drect.NativePtr);
            }

            internal NativeDRectangle(IntPtr ptr)
            {
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public double Area
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_area(this.NativePtr);
                }
            }

            public double Bottom
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_bottom(this.NativePtr);
                }
            }

            public DPoint.NativeDPoint BottomLeft
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_bl_corner(this.NativePtr));
                }
            }

            public DPoint.NativeDPoint BottomRight
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_br_corner(this.NativePtr));
                }
            }

            public DPoint.NativeDPoint Center
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_center(this.NativePtr));
                }
            }

            public DPoint.NativeDPoint DCenter
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_dcenter(this.NativePtr));
                }
            }

            public double Height
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_height(this.NativePtr);
                }
            }

            public bool IsEmpty
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_is_empty(this.NativePtr);
                }
            }

            public double Left
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_left(this.NativePtr);
                }
            }

            public double Right
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_right(this.NativePtr);
                }
            }

            public double Top
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_top(this.NativePtr);
                }
            }

            public DPoint.NativeDPoint TopLeft
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_tl_corner(this.NativePtr));
                }
            }

            public DPoint.NativeDPoint TopRight
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.drectangle_tr_corner(this.NativePtr));
                }
            }

            public double Width
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.drectangle_width(this.NativePtr);
                }
            }

            #endregion

            #region Methods

            public static NativeDRectangle CenteredRect(double x, double y, double width, double height)
            {
                using (var p = new DPoint.NativeDPoint(x, y))
                    return CenteredRect(p, width, height);
            }

            public static NativeDRectangle CenteredRect(DPoint.NativeDPoint p, double width, double height)
            {
                if (p == null)
                    throw new ArgumentNullException(nameof(p));

                p.ThrowIfDisposed();

                var result = NativeMethods.drectangle_centered_rect1(p.NativePtr, width, height);
                return new NativeDRectangle(result);
            }

            public static NativeDRectangle CenteredRect(NativeDRectangle drect, double width, double height)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                var result = NativeMethods.drectangle_centered_rect(drect.NativePtr, width, height);
                return new NativeDRectangle(result);
            }

            public bool Contains(DPoint.NativeDPoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                return NativeMethods.drectangle_contains(this.NativePtr, point.NativePtr);
            }

            public bool Contains(NativeDRectangle drect)
            {
                return NativeMethods.drectangle_contains2(this.NativePtr, drect.NativePtr);
            }

            public NativeDRectangle Intersect(NativeDRectangle drect)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                var result = NativeMethods.drectangle_intersect(this.NativePtr, drect.NativePtr);
                return new NativeDRectangle(result);
            }

            public NativeDRectangle SetAspectRatio(double ratio)
            {
                var result = NativeMethods.drectangle_set_aspect_ratio(this.NativePtr, ratio);
                return new NativeDRectangle(result);
            }

            public NativeDRectangle Translate(Point.NativePoint point)
            {
                return Translate(this, point);
            }

            public static NativeDRectangle Translate(NativeDRectangle drect, Point.NativePoint point)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                drect.ThrowIfDisposed();
                point.ThrowIfDisposed();

                var result = NativeMethods.drectangle_translate_rect(drect.NativePtr, point.NativePtr);
                return new NativeDRectangle(result);
            }

            public NativeDRectangle Translate(DPoint.NativeDPoint point)
            {
                return Translate(this, point);
            }

            public static NativeDRectangle Translate(NativeDRectangle drect, DPoint.NativeDPoint point)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                drect.ThrowIfDisposed();
                point.ThrowIfDisposed();

                var result = NativeMethods.drectangle_translate_rect_d(drect.NativePtr, point.NativePtr);
                return new NativeDRectangle(result);
            }

            internal DRectangle ToManaged()
            {
                return new DRectangle(this.Left, this.Top, this.Right, this.Bottom);
            }

            #region Overrides

            public static explicit operator Rectangle.NativeRectangle(NativeDRectangle drect)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                var ptr = NativeMethods.drectangle_operator(drect.NativePtr);
                return new Rectangle.NativeRectangle(ptr);
            }

            public static NativeDRectangle operator +(NativeDRectangle drect, NativeDRectangle rhs)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                drect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.drectangle_operator_add(drect.NativePtr, rhs.NativePtr);
                return new NativeDRectangle(ptr);
            }

            public static NativeDRectangle operator *(NativeDRectangle drect, double rhs)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                var ptr = NativeMethods.drectangle_operator_mul(drect.NativePtr, rhs);
                return new NativeDRectangle(ptr);
            }

            public static NativeDRectangle operator /(NativeDRectangle drect, double rhs)
            {
                if (drect == null)
                    throw new ArgumentNullException(nameof(drect));

                drect.ThrowIfDisposed();

                if (Math.Abs(rhs) < double.Epsilon)
                    throw new DivideByZeroException();

                var ptr = NativeMethods.drectangle_operator_div(drect.NativePtr, rhs);
                return new NativeDRectangle(ptr);
            }

            public static bool operator ==(NativeDRectangle rect, NativeDRectangle rhs)
            {
                if (ReferenceEquals(rect, rhs))
                    return true;
                if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                    return false;

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return NativeMethods.drectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
            }

            public static bool operator !=(NativeDRectangle rect, NativeDRectangle rhs)
            {
                if (ReferenceEquals(rect, rhs))
                    return false;
                if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                    return true;

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return !NativeMethods.drectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.drectangle_delete(this.NativePtr);
            }

            #endregion

            #endregion
            
        }
#pragma warning restore CS0660, CS0661

    }

}
