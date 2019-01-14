using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public struct Rectangle : IEquatable<Rectangle>
    {

        #region Fields

        public static readonly Rectangle Empty = new Rectangle(0, 0, -1, -1);

        #endregion

        #region Constructors

        public Rectangle(int left, int top, int right, int bottom)
        {
            using (var native = new NativeRectangle(left, top, right, bottom))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public Rectangle(uint width, uint height)
        {
            using (var native = new NativeRectangle(width, height))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public Rectangle(Point p1, Point p2)
        {
            using (var np1 = p1.ToNative())
            using (var np2 = p2.ToNative())
            using (var native = new NativeRectangle(np1, np2))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        public Rectangle(Point point)
        {
            using (var np = point.ToNative())
            using (var native = new NativeRectangle(np))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        internal Rectangle(IntPtr ptr, bool isEnabledDispose = true)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            using (var native = new NativeRectangle(ptr, isEnabledDispose))
            {
                this._Left = native.Left;
                this._Top = native.Top;
                this._Right = native.Right;
                this._Bottom = native.Bottom;
            }
        }

        #endregion

        #region Properties

        public uint Area
        {
            get
            {
                using (var native = this.ToNative())
                    return native.Area;
            }
        }

        private int _Bottom;

        public int Bottom
        {
            get => this._Bottom;
            set => this._Bottom = value;
        }

        public Point BottomLeft
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.BottomLeft)
                    return point.ToManaged();
            }
        }

        public Point BottomRight
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.BottomRight)
                    return point.ToManaged();
            }
        }

        public Point Center
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

        public uint Height
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

        private int _Left;

        public int Left
        {
            get => this._Left;
            set => this._Left = value;
        }

        private int _Right;

        public int Right
        {
            get => this._Right;
            set => this._Right = value;
        }

        private int _Top;

        public int Top
        {
            get => this._Top;
            set => this._Top = value;
        }

        public Point TopLeft
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.TopLeft)
                    return point.ToManaged();
            }
        }

        public Point TopRight
        {
            get
            {
                using (var native = this.ToNative())
                using (var point = native.TopRight)
                    return point.ToManaged();
            }
        }

        public uint Width
        {
            get
            {
                using (var native = this.ToNative())
                    return native.Width;
            }
        }

        #endregion

        #region Methods

        public static Rectangle CenteredRect(int x, int y, uint width, uint height)
        {
            using (var result = NativeRectangle.CenteredRect(x, y, width, height))
                return result.ToManaged();
        }

        public static Rectangle CenteredRect(Point p, uint width, uint height)
        {
            using (var np = p.ToNative())
            using (var result = NativeRectangle.CenteredRect(np, width, height))
                return result.ToManaged();
        }

        public static Rectangle CenteredRect(Rectangle rect, uint width, uint height)
        {
            using (var nr = rect.ToNative())
            using (var result = NativeRectangle.CenteredRect(nr, width, height))
                return result.ToManaged();
        }

        public bool Contains(Point point)
        {
            using (var np = point.ToNative())
            using (var nr = this.ToNative())
                return nr.Contains(np);
        }

        public bool Contains(int x, int y)
        {
            using (var nr = this.ToNative())
                return nr.Contains(x, y);
        }

        public bool Equals(Rectangle other)
        {
            return this._Bottom == other._Bottom && this._Left == other._Left && this._Right == other._Right && this._Top == other._Top;
        }

        public Rectangle Intersect(Rectangle rect)
        {
            using (var nrt = rect.ToNative())
            using (var nrs = this.ToNative())
            using (var result = nrs.Intersect(nrt))
                return result.ToManaged();
        }

        public static Rectangle SetAspectRatio(Rectangle rect, double ratio)
        {
            if (!(ratio > 0))
                throw new ArgumentOutOfRangeException(nameof(ratio));

            using (var nr = rect.ToNative())
            using (var result = nr.SetAspectRatio(ratio))
                return result.ToManaged();
        }

        public Rectangle Translate(int x, int y)
        {
            using (var nr = this.ToNative())
            using (var result = nr.Translate(x, y))
                return result.ToManaged();
        }

        public static Rectangle Translate(Rectangle rect, int x, int y)
        {
            using (var nr = rect.ToNative())
            using (var result = nr.Translate(x, y))
                return result.ToManaged();
        }

        public Rectangle Translate(Point point)
        {
            return Translate(this, point);
        }

        public static Rectangle Translate(Rectangle rect, Point point)
        {
            using (var np = point.ToNative())
            using (var nr = rect.ToNative())
            using (var result = nr.Translate(np))
                return result.ToManaged();
        }

        internal NativeRectangle ToNative()
        {
            return new NativeRectangle(this._Left, this._Top, this._Right, this._Bottom);
        }

        #region Overrides

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Rectangle && Equals((Rectangle)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this._Bottom;
                hashCode = (hashCode * 397) ^ this._Left;
                hashCode = (hashCode * 397) ^ this._Right;
                hashCode = (hashCode * 397) ^ this._Top;
                return hashCode;
            }
        }

        public static Rectangle operator +(Rectangle rect, Rectangle rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static Rectangle operator +(Rectangle rect, Point rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
            using (var ret = left + right)
                return ret.ToManaged();
        }

        public static bool operator ==(Rectangle rect, Rectangle rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
                return left == right;
        }

        public static bool operator !=(Rectangle rect, Rectangle rhs)
        {
            using (var left = rect.ToNative())
            using (var right = rhs.ToNative())
                return left != right;
        }

        #endregion

        #endregion

#pragma warning disable CS0660, CS0661
        internal sealed class NativeRectangle : DlibObject
        {

            #region Constructors

            public NativeRectangle(int left, int top, int right, int bottom)
            {
                this.NativePtr = NativeMethods.rectangle_new1(left, top, right, bottom);
            }

            public NativeRectangle(uint width, uint height)
            {
                this.NativePtr = NativeMethods.rectangle_new2(width, height);
            }

            public NativeRectangle()
            {
                this.NativePtr = NativeMethods.rectangle_new();
            }

            public NativeRectangle(Point.NativePoint p1, Point.NativePoint p2)
            {
                if (p1 == null)
                    throw new ArgumentNullException(nameof(p1));
                if (p2 == null)
                    throw new ArgumentNullException(nameof(p2));

                p1.ThrowIfDisposed();
                p2.ThrowIfDisposed();

                this.NativePtr = NativeMethods.rectangle_new4(p1.NativePtr, p2.NativePtr);
            }

            public NativeRectangle(Point.NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                this.NativePtr = NativeMethods.rectangle_new3(point.NativePtr);
            }

            internal NativeRectangle(IntPtr ptr, bool isEnabledDispose = true) :
                base(isEnabledDispose)
            {
                if (ptr == IntPtr.Zero)
                    throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public uint Area
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_area(this.NativePtr);
                }
            }

            public int Bottom
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_bottom(this.NativePtr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.rectangle_set_bottom(this.NativePtr, value);
                }
            }

            public Point.NativePoint BottomLeft
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new Point.NativePoint(NativeMethods.rectangle_bl_corner(this.NativePtr));
                }
            }

            public Point.NativePoint BottomRight
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new Point.NativePoint(NativeMethods.rectangle_br_corner(this.NativePtr));
                }
            }

            public Point.NativePoint Center
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new Point.NativePoint(NativeMethods.rectangle_center(this.NativePtr));
                }
            }

            public DPoint.NativeDPoint DCenter
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new DPoint.NativeDPoint(NativeMethods.rectangle_dcenter(this.NativePtr));
                }
            }

            public uint Height
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_height(this.NativePtr);
                }
            }

            public bool IsEmpty
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_is_empty(this.NativePtr);
                }
            }

            public int Left
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_left(this.NativePtr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.rectangle_set_left(this.NativePtr, value);
                }
            }

            public int Right
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_right(this.NativePtr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.rectangle_set_right(this.NativePtr, value);
                }
            }

            public int Top
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_top(this.NativePtr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.rectangle_set_top(this.NativePtr, value);
                }
            }

            public Point.NativePoint TopLeft
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new Point.NativePoint(NativeMethods.rectangle_tl_corner(this.NativePtr));
                }
            }

            public Point.NativePoint TopRight
            {
                get
                {
                    this.ThrowIfDisposed();
                    return new Point.NativePoint(NativeMethods.rectangle_tr_corner(this.NativePtr));
                }
            }

            public uint Width
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.rectangle_width(this.NativePtr);
                }
            }

            #endregion

            #region Methods

            public static NativeRectangle CenteredRect(int x, int y, uint width, uint height)
            {
                var result = NativeMethods.rectangle_centered_rect(x, y, width, height);
                return new NativeRectangle(result);
            }

            public static NativeRectangle CenteredRect(Point.NativePoint p, uint width, uint height)
            {
                if (p == null)
                    throw new ArgumentNullException(nameof(p));

                p.ThrowIfDisposed();

                var result = NativeMethods.rectangle_centered_rect1(p.NativePtr, width, height);
                return new NativeRectangle(result);
            }

            public static NativeRectangle CenteredRect(NativeRectangle rect, uint width, uint height)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));

                rect.ThrowIfDisposed();

                var result = NativeMethods.rectangle_centered_rect2(rect.NativePtr, width, height);
                return new NativeRectangle(result);
            }

            public bool Contains(Point.NativePoint point)
            {
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                point.ThrowIfDisposed();

                return NativeMethods.rectangle_contains(this.NativePtr, point.NativePtr);
            }

            public bool Contains(int x, int y)
            {
                return NativeMethods.rectangle_contains1(this.NativePtr, x, y);
            }

            public NativeRectangle Intersect(NativeRectangle rect)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));

                rect.ThrowIfDisposed();

                var result = NativeMethods.rectangle_intersect(this.NativePtr, rect.NativePtr);
                return new NativeRectangle(result);
            }

            public NativeRectangle SetAspectRatio(double ratio)
            {
                var result = NativeMethods.rectangle_set_aspect_ratio(this.NativePtr, ratio);
                return new NativeRectangle(result);
            }

            public NativeRectangle Translate(int x, int y)
            {
                return Translate(this, x, y);
            }

            public static NativeRectangle Translate(NativeRectangle rect, int x, int y)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));

                rect.ThrowIfDisposed();

                var result = NativeMethods.rectangle_translate_rect_xy(rect.NativePtr, x, y);
                return new NativeRectangle(result);
            }

            public NativeRectangle Translate(Point.NativePoint point)
            {
                return Translate(this, point);
            }

            public static NativeRectangle Translate(NativeRectangle rect, Point.NativePoint point)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));
                if (point == null)
                    throw new ArgumentNullException(nameof(point));

                rect.ThrowIfDisposed();
                point.ThrowIfDisposed();

                var result = NativeMethods.rectangle_translate_rect(rect.NativePtr, point.NativePtr);
                return new NativeRectangle(result);
            }

            internal Rectangle ToManaged()
            {
                return new Rectangle(this.Left, this.Top, this.Right, this.Bottom);
            }

            #region Overrides

            public static NativeRectangle operator +(NativeRectangle rect, NativeRectangle rhs)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.rectangle_operator_add(rect.NativePtr, rhs.NativePtr);
                return new NativeRectangle(ptr);
            }

            public static NativeRectangle operator +(NativeRectangle rect, Point.NativePoint rhs)
            {
                if (rect == null)
                    throw new ArgumentNullException(nameof(rect));
                if (rhs == null)
                    throw new ArgumentNullException(nameof(rhs));

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                var ptr = NativeMethods.rectangle_operator_add_point(rect.NativePtr, rhs.NativePtr);
                return new NativeRectangle(ptr);
            }

            public static bool operator ==(NativeRectangle rect, NativeRectangle rhs)
            {
                if (ReferenceEquals(rect, rhs))
                    return true;
                if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                    return false;

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return NativeMethods.rectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
            }

            public static bool operator !=(NativeRectangle rect, NativeRectangle rhs)
            {
                if (ReferenceEquals(rect, rhs))
                    return false;
                if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                    return true;

                rect.ThrowIfDisposed();
                rhs.ThrowIfDisposed();

                return !NativeMethods.rectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
            }

            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.rectangle_delete(this.NativePtr);
            }

            #endregion

            #endregion

        }
#pragma warning restore CS0660, CS0661

    }

}
