using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DlibDotNet.ImageProcessing;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdVectorOfVectorMModRect : StdVector<StdVectorOfMModRect>
    {

        #region Constructors

        public StdVectorOfVectorMModRect()
        {
            this.NativePtr = Native.stdvector_vector_mmod_rect_new1();
        }

        public StdVectorOfVectorMModRect(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.stdvector_vector_mmod_rect_new2(new IntPtr(size));
        }

        public StdVectorOfVectorMModRect(IEnumerable<IEnumerable<MModRect>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rects => new StdVectorOfMModRect(rects.Select(r => r)).NativePtr).ToArray();
            this.NativePtr = Native.stdvector_vector_mmod_rect_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.stdvector_vector_mmod_rect_getPointer(this.NativePtr);

        public override int Size => Native.stdvector_vector_mmod_rect_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override StdVectorOfMModRect[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new StdVectorOfMModRect[0];

            var dst = new IntPtr[size];
            Native.stdvector_vector_mmod_rect_copy(this.NativePtr, dst);
            return dst.Select(p => p != IntPtr.Zero ? new StdVectorOfMModRect(p) : null).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            foreach (var item in this.ToArray())
                item?.Dispose();

            Native.stdvector_vector_mmod_rect_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_mmod_rect_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_vector_mmod_rect_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_vector_mmod_rect_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}