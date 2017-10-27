using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorOfLong : StdVector<long>
    {

        #region Constructors

        public VectorOfLong()
        {
            this.NativePtr = Native.vector_long_new1();
        }

        public VectorOfLong(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.vector_long_new2(new IntPtr(size));
        }

        public VectorOfLong(IEnumerable<long> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.ToArray();
            this.NativePtr = Native.vector_long_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.vector_long_getPointer(this.NativePtr);

        public override int Size => Native.vector_long_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override long[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new long[0];

            var dst = new long[size];
            Marshal.Copy(this.ElementPtr, dst, 0, dst.Length);
            return dst;
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            Native.vector_long_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_long_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_long_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_long_new3([In] long[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_long_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_long_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern long vector_long_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_long_delete(IntPtr vector);

        }

    }

}