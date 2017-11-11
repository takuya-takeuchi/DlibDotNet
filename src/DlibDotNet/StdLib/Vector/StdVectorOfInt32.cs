using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdVectorOfInt32 : StdVector<int>
    {

        #region Constructors

        public StdVectorOfInt32()
        {
            this.NativePtr = Native.stdvector_int32_new1();
        }

        public StdVectorOfInt32(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.stdvector_int32_new2(new IntPtr(size));
        }

        public StdVectorOfInt32(IEnumerable<int> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.ToArray();
            this.NativePtr = Native.stdvector_int32_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.stdvector_int32_getPointer(this.NativePtr);

        public override int Size => Native.stdvector_int32_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override int[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new int[0];

            var dst = new int[size];
            Marshal.Copy(this.ElementPtr, dst, 0, dst.Length);
            return dst;
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            Native.stdvector_int32_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_int32_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_int32_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_int32_new3([In] int[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_int32_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_int32_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int stdvector_int32_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_int32_delete(IntPtr vector);

        }

    }

}