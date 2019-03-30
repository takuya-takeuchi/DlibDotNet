using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Image : DlibObject
    {

        #region Constructors

        public Image()
        {
            this.NativePtr = NativeMethods.image_dataset_metadata_image_new2();
        }

        public Image(string filename)
        {
            var str = Dlib.Encoding.GetBytes(filename);
            this.NativePtr = NativeMethods.image_dataset_metadata_image_new(str);
        }

        internal Image(IntPtr ptr, bool isDisposable = true) :
            base(isDisposable)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public BoxCollection Boxes
        {
            get
            {
                this.ThrowIfDisposed();
                return new BoxCollection(this);
            }
        }

        public string FileName
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = NativeMethods.image_dataset_metadata_image_get_filename(this.NativePtr);
                return StringHelper.FromStdString(stdstr, true);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.image_dataset_metadata_image_set_filename(this.NativePtr, str);
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

            NativeMethods.image_dataset_metadata_image_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
