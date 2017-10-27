using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Rectangle : DlibObject
    {

        #region Constructors

        public Rectangle(int left, int top, int right, int bottom)
        {
            this.NativePtr = Native.rectangle_new1(left, top, right, bottom);
        }

        public Rectangle(uint width, uint height)
        {
            this.NativePtr = Native.rectangle_new2(width, height);
        }

        public Rectangle()
        {
            this.NativePtr = Native.rectangle_new();
        }

        public Rectangle(Point p1, Point p2)
        {
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            this.NativePtr = Native.rectangle_new4(p1.NativePtr, p2.NativePtr);
        }

        public Rectangle(Point point)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            this.NativePtr = Native.rectangle_new3(point.NativePtr);
        }

        internal Rectangle(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public uint Area
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_area(this.NativePtr);
            }
        }

        public int Bottom
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_bottom(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rectangle_set_bottom(this.NativePtr, value);
            }
        }

        public Point BottomLeft
        {
            get
            {
                this.ThrowIfDisposed();
                return new Point(Native.rectangle_bl_corner(this.NativePtr));
            }
        }

        public Point BottomRight
        {
            get
            {
                this.ThrowIfDisposed();
                return new Point(Native.rectangle_br_corner(this.NativePtr));
            }
        }

        public Point Center
        {
            get
            {
                this.ThrowIfDisposed();
                return new Point(Native.rectangle_center(this.NativePtr));
            }
        }

        public DPoint DCenter
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.rectangle_dcenter(this.NativePtr));
            }
        }

        public uint Height
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_height(this.NativePtr);
            }
        }

        public bool IsEmpty
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_is_empty(this.NativePtr);
            }
        }

        public int Left
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_left(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rectangle_set_left(this.NativePtr, value);
            }
        }

        public int Right
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_right(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rectangle_set_right(this.NativePtr, value);
            }
        }

        public int Top
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_top(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.rectangle_set_top(this.NativePtr, value);
            }
        }

        public Point TopLeft
        {
            get
            {
                this.ThrowIfDisposed();
                return new Point(Native.rectangle_tl_corner(this.NativePtr));
            }
        }

        public Point TopRight
        {
            get
            {
                this.ThrowIfDisposed();
                return new Point(Native.rectangle_tr_corner(this.NativePtr));
            }
        }

        public uint Width
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.rectangle_width(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public static Rectangle CenteredRect(int x, int y, uint width, uint height)
        {
            var result = Native.rectangle_centered_rect(x, y, width, height);
            return new Rectangle(result);
        }

        public static Rectangle CenteredRect(Point p, uint width, uint height)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            p.ThrowIfDisposed();

            var result = Native.rectangle_centered_rect1(p.NativePtr, width, height);
            return new Rectangle(result);
        }

        public static Rectangle CenteredRect(Rectangle rect, uint width, uint height)
        {
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));

            rect.ThrowIfDisposed();

            var result = Native.rectangle_centered_rect2(rect.NativePtr, width, height);
            return new Rectangle(result);
        }

        public bool Contains(Point point)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            return Native.rectangle_contains(this.NativePtr, point.NativePtr);
        }

        public bool Contains(int x, int y)
        {
            return Native.rectangle_contains1(this.NativePtr, x, y);
        }

        public Rectangle Intersect(Rectangle rect)
        {
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));

            rect.ThrowIfDisposed();

            var result = Native.rectangle_intersect(this.NativePtr, rect.NativePtr);
            return new Rectangle(result);
        }

        public Rectangle Translate(int x, int y)
        {
            return Translate(this, x, y);
        }

        public static Rectangle Translate(Rectangle rect, int x, int y)
        {
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));

            rect.ThrowIfDisposed();

            var result = Native.rectangle_translate_rect_xy(rect.NativePtr, x, y);
            return new Rectangle(result);
        }

        public Rectangle Translate(Point point)
        {
            return Translate(this, point);
        }

        public static Rectangle Translate(Rectangle rect, Point point)
        {
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            rect.ThrowIfDisposed();
            point.ThrowIfDisposed();

            var result = Native.rectangle_translate_rect(rect.NativePtr, point.NativePtr);
            return new Rectangle(result);
        }

        #region Overrides

        public static Rectangle operator +(Rectangle rect, Rectangle rhs)
        {
            if (rect == null)
                throw new ArgumentNullException(nameof(rect));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            rect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var ptr = Native.rectangle_operator_add(rect.NativePtr, rhs.NativePtr);
            return new Rectangle(ptr);
        }

        public static bool operator ==(Rectangle rect, Rectangle rhs)
        {
            if (ReferenceEquals(rect, rhs))
                return true;
            if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                return false;

            rect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return Native.rectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
        }

        public static bool operator !=(Rectangle rect, Rectangle rhs)
        {
            if (ReferenceEquals(rect, rhs))
                return true;
            if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                return false;

            rect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return !Native.rectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.rectangle_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_new1(int left, int top, int right, int bottom);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_new2(uint width, uint height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_new3(IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_new4(IntPtr p1, IntPtr p2);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_delete(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int rectangle_bottom(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int rectangle_left(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int rectangle_right(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int rectangle_top(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_set_bottom(IntPtr rect, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_set_left(IntPtr rect, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_set_right(IntPtr rect, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void rectangle_set_top(IntPtr rect, int value);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool rectangle_is_empty(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_bl_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_br_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_tl_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_tr_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint rectangle_area(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint rectangle_height(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern uint rectangle_width(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool rectangle_contains(IntPtr rect, IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool rectangle_contains1(IntPtr rect, int x, int y);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_centered_rect(int x, int y, uint width, uint height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_centered_rect1(IntPtr point, uint width, uint height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_centered_rect2(IntPtr rect, uint width, uint height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_intersect(IntPtr rect, IntPtr target);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_center(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_dcenter(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_translate_rect(IntPtr rect, IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_translate_rect_xy(IntPtr rect, int x, int y);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr rectangle_operator_add(IntPtr rect, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool rectangle_operator_equal(IntPtr drect, IntPtr rhs);

        }

    }

}
