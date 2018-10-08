using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Image : DlibObject
    {

        #region Constructors

        public Image()
        {
            this.NativePtr = Native.image_dataset_metadata_image_new2();
        }

        public Image(string filename)
        {
            var str = Encoding.UTF8.GetBytes(filename);
            this.NativePtr = Native.image_dataset_metadata_image_new(str);
        }

        internal Image(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public Box[] Boxes
        {
            get
            {
                this.ThrowIfDisposed();
                var boxes = Native.image_dataset_metadata_dataset_get_boxes(this.NativePtr);
                using (var vector = new StdVector<Box>(boxes))
                    return vector.ToArray();
            }
            set
            {
                using (var vector = value != null ? new StdVector<Box>(value, null) : new StdVector<Box>())
                    Native.image_dataset_metadata_dataset_set_boxes(this.NativePtr, vector.NativePtr);
            }
        }

        public string FileName
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = Native.image_dataset_metadata_image_get_filename(this.NativePtr);
                return StringHelper.FromStdString(stdstr);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Encoding.UTF8.GetBytes(value ?? "");
                Native.image_dataset_metadata_image_set_filename(this.NativePtr, str);
            }
        }

        #endregion

        #region Methods

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.image_dataset_metadata_image_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_image_new(byte[] filename);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_image_new2();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_dataset_get_boxes(IntPtr image);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_dataset_set_boxes(IntPtr image, IntPtr boxes);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_image_get_filename(IntPtr image);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_image_set_filename(IntPtr image, byte[] filename);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_image_delete(IntPtr image);

        }

    }

}
