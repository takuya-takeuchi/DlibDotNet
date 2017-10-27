using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class PairOfPointPoint : StdPair<Point, Point>
    {

        #region Constructors

        public PairOfPointPoint(Point first, Point second)
        {
            if (first != null)
                first.ThrowIfDisposed();
            if (second != null)
                second.ThrowIfDisposed();

            var firstPtr = first == null ? IntPtr.Zero : first.NativePtr;
            var secondPtr = second == null ? IntPtr.Zero : second.NativePtr;
            this.NativePtr = Native.pair_point_point_new(firstPtr, secondPtr);
            this._Second = second;
            this._First = first;
        }

        internal PairOfPointPoint(IntPtr first, IntPtr second)
        {
            if (first == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(first));
            if (second == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(second));

            this.NativePtr = Native.pair_point_point_new(first, second);
            this._First = new Point(first);
            this._Second = new Point(second);
        }

        #endregion

        #region Properties

        private Point _First;

        public override Point First
        {
            get
            {
                this.ThrowIfDisposed();
                return this._First;
            }
            set
            {
                this.ThrowIfDisposed();
                this._First = value;
                Native.pair_point_point_set_first(this.NativePtr, value == null ? IntPtr.Zero : value.NativePtr);
            }
        }

        private Point _Second;

        public override Point Second
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Second;
            }
            set
            {
                this.ThrowIfDisposed();
                this._Second = value;
                Native.pair_point_point_set_second(this.NativePtr, value == null ? IntPtr.Zero : value.NativePtr);
            }
        }

        #endregion

        #region Methods

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            Native.pair_point_point_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr pair_point_point_new(IntPtr first, IntPtr second);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr pair_point_point_get_first(IntPtr pair);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void pair_point_point_set_first(IntPtr pair, IntPtr first);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr pair_point_point_get_second(IntPtr pair);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void pair_point_point_set_second(IntPtr pair, IntPtr second);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void pair_point_point_delete(IntPtr pair);

        }

    }

}
