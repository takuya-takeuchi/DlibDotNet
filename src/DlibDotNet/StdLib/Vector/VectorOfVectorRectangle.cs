using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorOfVectorRectangle : StdVector<VectorOfRectangle>
    {

        #region Constructors

        public VectorOfVectorRectangle()
        {
            this.NativePtr = Native.stdvector_vector_rectangle_new1();
        }

        public VectorOfVectorRectangle(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.stdvector_vector_rectangle_new2(new IntPtr(size));
        }

        public VectorOfVectorRectangle(IEnumerable<IEnumerable<Rectangle>> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rects => new VectorOfRectangle(rects.Select(r => r)).NativePtr).ToArray();
            this.NativePtr = Native.stdvector_vector_rectangle_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.stdvector_vector_rectangle_getPointer(this.NativePtr);

        public override int Size => Native.stdvector_vector_rectangle_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override VectorOfRectangle[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new VectorOfRectangle[0];

            var dst = new IntPtr[size];
            Native.stdvector_vector_rectangle_copy(this.NativePtr, dst);
            return dst.Select(p => p != IntPtr.Zero ? new VectorOfRectangle(p) : null).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            foreach (var item in this.ToArray())
                item?.Dispose();

            Native.stdvector_vector_rectangle_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_vector_rectangle_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_vector_rectangle_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_vector_rectangle_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}