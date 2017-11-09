using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class VectorOfFullObjectDetection : StdVector<FullObjectDetection>
    {

        #region Constructors

        public VectorOfFullObjectDetection()
        {
            this.NativePtr = Native.stdvector_full_object_detection_new1();
        }

        public VectorOfFullObjectDetection(int size)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size));

            this.NativePtr = Native.stdvector_full_object_detection_new2(new IntPtr(size));
        }

        public VectorOfFullObjectDetection(IEnumerable<FullObjectDetection> data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
            this.NativePtr = Native.stdvector_full_object_detection_new3(array, new IntPtr(array.Length));
        }

        #endregion

        #region Properties

        public override IntPtr ElementPtr => Native.stdvector_full_object_detection_getPointer(this.NativePtr);

        public override int Size => Native.stdvector_full_object_detection_getSize(this.NativePtr).ToInt32();

        #endregion

        #region Methods

        public override FullObjectDetection[] ToArray()
        {
            var size = Size;
            if (size == 0)
                return new FullObjectDetection[0];

            var dst = new IntPtr[size];
            Native.stdvector_full_object_detection_copy(this.NativePtr, dst);
            return dst.Select(p=> new FullObjectDetection(p)).ToArray();
        }

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            // Do NOT dispose each item element except for std::vector
            //foreach (var item in this.ToArray())
            //    item?.Dispose();

            Native.stdvector_full_object_detection_delete(this.NativePtr);
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_new1();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_new2(IntPtr size);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_new3([In] IntPtr[] data, IntPtr dataLength);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_getSize(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_getPointer(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr stdvector_full_object_detection_at(IntPtr vector, int index);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_full_object_detection_delete(IntPtr vector);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void stdvector_full_object_detection_copy(IntPtr vector, IntPtr[] dst);

        }

    }

}