using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class DRectangle : DlibObject
    {

        #region Constructors

        public DRectangle()
        {
            this.NativePtr = Native.drectangle_new();
        }

        public DRectangle(double left, double top, double right, double bottom)
        {
            this.NativePtr = Native.drectangle_new1(left, top, right, bottom);
        }

        public DRectangle(DPoint point)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            this.NativePtr = Native.drectangle_new2(point.NativePtr);
        }

        public DRectangle(DPoint p1, DPoint p2)
        {
            if (p1 == null)
                throw new ArgumentNullException(nameof(p1));
            if (p2 == null)
                throw new ArgumentNullException(nameof(p2));

            p1.ThrowIfDisposed();
            p2.ThrowIfDisposed();

            this.NativePtr = Native.drectangle_new3(p1.NativePtr, p2.NativePtr);
        }

        public DRectangle(DRectangle drect)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            this.NativePtr = Native.drectangle_new4(drect.NativePtr);
        }

        internal DRectangle(IntPtr ptr)
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
                return Native.drectangle_area(this.NativePtr);
            }
        }

        public double Bottom
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_bottom(this.NativePtr);
            }
        }

        public DPoint BottomLeft
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_bl_corner(this.NativePtr));
            }
        }

        public DPoint BottomRight
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_br_corner(this.NativePtr));
            }
        }

        public DPoint Center
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_center(this.NativePtr));
            }
        }

        public DPoint DCenter
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_dcenter(this.NativePtr));
            }
        }

        public double Height
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_height(this.NativePtr);
            }
        }

        public bool IsEmpty
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_is_empty(this.NativePtr);
            }
        }

        public double Left
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_left(this.NativePtr);
            }
        }

        public double Right
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_right(this.NativePtr);
            }
        }

        public double Top
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_top(this.NativePtr);
            }
        }

        public DPoint TopLeft
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_tl_corner(this.NativePtr));
            }
        }

        public DPoint TopRight
        {
            get
            {
                this.ThrowIfDisposed();
                return new DPoint(Native.drectangle_tr_corner(this.NativePtr));
            }
        }

        public double Width
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.drectangle_width(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public static DRectangle CenteredRect(double x, double y, double width, double height)
        {
            using (var p = new DPoint(x, y))
                return CenteredRect(p, width, height);
        }

        public static DRectangle CenteredRect(DPoint p, double width, double height)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));

            p.ThrowIfDisposed();

            var result = Native.drectangle_centered_rect1(p.NativePtr, width, height);
            return new DRectangle(result);
        }

        public static DRectangle CenteredRect(DRectangle drect, double width, double height)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            var result = Native.drectangle_centered_rect(drect.NativePtr, width, height);
            return new DRectangle(result);
        }

        public bool Contains(DPoint point)
        {
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            point.ThrowIfDisposed();

            return Native.drectangle_contains(this.NativePtr, point.NativePtr);
        }

        public bool Contains(DRectangle drect)
        {
            return Native.drectangle_contains2(this.NativePtr, drect.NativePtr);
        }

        public DRectangle Intersect(DRectangle drect)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            var result = Native.drectangle_intersect(this.NativePtr, drect.NativePtr);
            return new DRectangle(result);
        }

        public DRectangle Translate(Point point)
        {
            return Translate(this, point);
        }

        public static DRectangle Translate(DRectangle drect, Point point)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            drect.ThrowIfDisposed();
            point.ThrowIfDisposed();

            var result = Native.drectangle_translate_rect(drect.NativePtr, point.NativePtr);
            return new DRectangle(result);
        }

        public DRectangle Translate(DPoint point)
        {
            return Translate(this, point);
        }

        public static DRectangle Translate(DRectangle drect, DPoint point)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));
            if (point == null)
                throw new ArgumentNullException(nameof(point));

            drect.ThrowIfDisposed();
            point.ThrowIfDisposed();

            var result = Native.drectangle_translate_rect_d(drect.NativePtr, point.NativePtr);
            return new DRectangle(result);
        }

        #region Overrides

        public static explicit operator Rectangle(DRectangle drect)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            var ptr = Native.drectangle_operator(drect.NativePtr);
            return new Rectangle(ptr);
        }

        public static DRectangle operator +(DRectangle drect, DRectangle rhs)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));
            if (rhs == null)
                throw new ArgumentNullException(nameof(rhs));

            drect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            var ptr = Native.drectangle_operator_add(drect.NativePtr, rhs.NativePtr);
            return new DRectangle(ptr);
        }

        public static DRectangle operator *(DRectangle drect, double rhs)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            var ptr = Native.drectangle_operator_mul(drect.NativePtr, rhs);
            return new DRectangle(ptr);
        }

        public static DRectangle operator /(DRectangle drect, double rhs)
        {
            if (drect == null)
                throw new ArgumentNullException(nameof(drect));

            drect.ThrowIfDisposed();

            if (Math.Abs(rhs) < double.Epsilon)
                throw new DivideByZeroException();

            var ptr = Native.drectangle_operator_div(drect.NativePtr, rhs);
            return new DRectangle(ptr);
        }

        public static bool operator ==(DRectangle rect, DRectangle rhs)
        {
            if (ReferenceEquals(rect, rhs))
                return true;
            if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                return false;

            rect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return Native.drectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
        }

        public static bool operator !=(DRectangle rect, DRectangle rhs)
        {
            if (ReferenceEquals(rect, rhs))
                return false;
            if (ReferenceEquals(rect, null) || ReferenceEquals(rhs, null))
                return true;

            rect.ThrowIfDisposed();
            rhs.ThrowIfDisposed();

            return !Native.drectangle_operator_equal(rect.NativePtr, rhs.NativePtr);
        }

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Native.drectangle_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_new1(double left, double top, double right, double bottom);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_new2(IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_new3(IntPtr p1, IntPtr p2);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_new4(IntPtr drect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_operator(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void drectangle_delete(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_bottom(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_left(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_right(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_top(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool drectangle_is_empty(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_bl_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_br_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_tl_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_tr_corner(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_area(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_height(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double drectangle_width(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_intersect(IntPtr rect, IntPtr target);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool drectangle_contains(IntPtr rect, IntPtr point);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool drectangle_contains2(IntPtr rect, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_center(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_dcenter(IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_centered_rect(IntPtr dpoint, double width, double height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_centered_rect1(IntPtr drect, double width, double height);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_translate_rect(IntPtr rect, IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_translate_rect_d(IntPtr rect, IntPtr p);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_operator_add(IntPtr drect, IntPtr rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_operator_mul(IntPtr drect, double rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr drectangle_operator_div(IntPtr drect, double rhs);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool drectangle_operator_equal(IntPtr drect, IntPtr rhs);

        }

    }

}
