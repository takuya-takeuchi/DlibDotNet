using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Dataset : DlibObject
    {

        #region Constructors

        public Dataset()
        {
            this.NativePtr = Native.image_dataset_metadata_dataset_new();
        }

        #endregion

        #region Properties

        public string Comment
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = Native.image_dataset_metadata_dataset_get_comment(this.NativePtr);
                return StringHelper.FromStdString(stdstr);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Encoding.UTF8.GetBytes(value ?? "");
                Native.image_dataset_metadata_dataset_set_comment(this.NativePtr, str);
            }
        }

        public Image[] Images
        {
            get
            {
                this.ThrowIfDisposed();
                var images = Native.image_dataset_metadata_dataset_get_images(this.NativePtr);
                using (var vector = new StdVector<Image>(images))
                    return vector.ToArray();
            }
            set
            {
                using (var vector = value != null ? new StdVector<Image>(value, null) : new StdVector<Image>())
                    Native.image_dataset_metadata_dataset_set_images(this.NativePtr, vector.NativePtr);
            }
        }

        public string Name
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = Native.image_dataset_metadata_dataset_get_name(this.NativePtr);
                return StringHelper.FromStdString(stdstr);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Encoding.UTF8.GetBytes(value ?? "");
                Native.image_dataset_metadata_dataset_set_name(this.NativePtr, str);
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

            Native.image_dataset_metadata_dataset_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_dataset_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_dataset_get_comment(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_dataset_set_comment(IntPtr dataset, byte[] comment);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_dataset_get_images(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_dataset_set_images(IntPtr dataset, IntPtr images);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_dataset_get_name(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_dataset_set_name(IntPtr dataset, byte[] name);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_dataset_delete(IntPtr dataset);

        }

    }

}
