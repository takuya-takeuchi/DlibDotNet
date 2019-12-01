using System;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    /// <summary>
    /// Represents an annotated image. This class cannot be inherited.
    /// </summary>
    public sealed class Image : DlibObject
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            this.NativePtr = NativeMethods.image_dataset_metadata_image_new2();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class with a specified file name of image.
        /// </summary>
        public Image(string filename)
        {
            var str = Dlib.Encoding.GetBytes(filename);
            this.NativePtr = NativeMethods.image_dataset_metadata_image_new(str, str.Length);
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

        /// <summary>
        /// Gets a collection of annotated rectangular area of an image.
        /// </summary>
        public BoxCollection Boxes
        {
            get
            {
                this.ThrowIfDisposed();
                return new BoxCollection(this);
            }
        }

        /// <summary>
        /// Gets or sets the file name of image.
        /// </summary>
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
                NativeMethods.image_dataset_metadata_image_set_filename(this.NativePtr, str, str.Length);
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
