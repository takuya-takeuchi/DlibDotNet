using System;
using System.Collections;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class ImageCollection : IEnumerable<Image>
    {

        #region Fields

        private readonly Dataset _Parent;

        #endregion

        #region Constructors

        internal ImageCollection(Dataset parent)
        {
            this._Parent = parent;
        }

        #endregion

        #region Properties

        public Image this[int index]
        {
            get
            {
                this._Parent.ThrowIfDisposed();

                var native = this._Parent.NativePtr;
                var image = NativeMethods.image_dataset_metadata_dataset_get_images_at(native, index);
                return new Image(image, false);
            }
        }

        public int Count
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_dataset_get_images_get_size(this._Parent.NativePtr);
            }
        }

        #endregion

        #region Methods

        public void Add(Image image)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            this._Parent.ThrowIfDisposed();
            image.ThrowIfDisposed();

            NativeMethods.image_dataset_metadata_dataset_get_images_push_back(this._Parent.NativePtr, image.NativePtr);
        }

        public void Clear()
        {
            this._Parent.ThrowIfDisposed();
            NativeMethods.image_dataset_metadata_dataset_get_images_clear(this._Parent.NativePtr);
        }

        public void RemoveAt(int index)
        {
            if (!(0 <= index && index < this.Count))
                throw new ArgumentOutOfRangeException();

            this._Parent.ThrowIfDisposed();
            NativeMethods.image_dataset_metadata_dataset_get_images_remove_at(this._Parent.NativePtr, index);
        }

        #endregion

        #region  Implements

        public IEnumerator<Image> GetEnumerator()
        {
            this._Parent.ThrowIfDisposed();
            var native = this._Parent.NativePtr;
            // avoid to StackOverflowException
            var count = NativeMethods.image_dataset_metadata_dataset_get_images_get_size(native);
            for (var index = 0; index < count; index++)
            {
                var box = NativeMethods.image_dataset_metadata_dataset_get_images_at(native, index);
                yield return new Image(box, false);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}