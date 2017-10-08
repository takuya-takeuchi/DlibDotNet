using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorOfRectangle : StdVector<Rectangle>
    {

        #region Constructors

        public VectorOfRectangle()
        {
            this.NativePtr = Native.vector_rectangle_new1();
        }

        public VectorOfRectangle(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.vector_rectangle_new2(new IntPtr(size));
        }

        public VectorOfRectangle(IEnumerable<Rectangle> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
            this.NativePtr = Native.vector_rectangle_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.vector_rectangle_getPointer(this.NativePtr);

        public override int Size => Native.vector_rectangle_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override Rectangle[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new Rectangle[0];

            var dst = new IntPtr[size];
            Native.vector_rectangle_copy(this.NativePtr, dst);
            return dst.Select(p=> new Rectangle(p)).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            Native.vector_rectangle_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr vector_rectangle_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_rectangle_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void vector_rectangle_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}