using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Dataset : DlibObject
    {

        #region Constructors

        public Dataset()
        {
            this.NativePtr = NativeMethods.image_dataset_metadata_dataset_new();
        }

        #endregion

        #region Properties

        public string Comment
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = NativeMethods.image_dataset_metadata_dataset_get_comment(this.NativePtr);
                return StringHelper.FromStdString(stdstr, true);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.image_dataset_metadata_dataset_set_comment(this.NativePtr, str);
            }
        }

        public ImageCollection Images => new ImageCollection(this);

        public string Name
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = NativeMethods.image_dataset_metadata_dataset_get_name(this.NativePtr);
                return StringHelper.FromStdString(stdstr, true);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.image_dataset_metadata_dataset_set_name(this.NativePtr, str);
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

            NativeMethods.image_dataset_metadata_dataset_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
