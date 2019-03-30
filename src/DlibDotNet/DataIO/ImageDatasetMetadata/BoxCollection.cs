using System;
using System.Collections;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class BoxCollection : IEnumerable<Box>
    {

        #region Fields

        private readonly Image _Parent;

        #endregion

        #region Constructors

        internal BoxCollection(Image parent)
        {
            this._Parent = parent;
        }

        #endregion

        #region Properties

        public Box this[int index]
        {
            get
            {
                this._Parent.ThrowIfDisposed();

                var native = this._Parent.NativePtr;
                var box = NativeMethods.image_dataset_metadata_dataset_get_boxes_at(native, index);
                return new Box(box, false);
            }
        }

        public int Count
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_dataset_get_boxes_get_size(this._Parent.NativePtr);
            }
        }

        #endregion

        #region Methods

        public void Add(Box box)
        {
            if (box == null)
                throw new ArgumentNullException(nameof(box));

            this._Parent.ThrowIfDisposed();
            box.ThrowIfDisposed();

            NativeMethods.image_dataset_metadata_dataset_get_boxes_push_back(this._Parent.NativePtr, box.NativePtr);
        }

        public void Clear()
        {
            this._Parent.ThrowIfDisposed();
            NativeMethods.image_dataset_metadata_dataset_get_boxes_clear(this._Parent.NativePtr);
        }

        #endregion

        #region  Implements

        public IEnumerator<Box> GetEnumerator()
        {
            this._Parent.ThrowIfDisposed();

            var native = this._Parent.NativePtr;
            // avoid to StackOverflowException
            var count = NativeMethods.image_dataset_metadata_dataset_get_boxes_get_size(native);
            for (var index = 0; index < count; index++)
            {
                var box = NativeMethods.image_dataset_metadata_dataset_get_boxes_at(native, index);
                yield return new Box(box, false);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}