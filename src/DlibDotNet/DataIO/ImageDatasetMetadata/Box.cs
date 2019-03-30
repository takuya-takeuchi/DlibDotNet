using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Box : DlibObject
    {

        #region Constructors

        public Box()
        {
            this.NativePtr = NativeMethods.image_dataset_metadata_box_new();
        }

        internal Box(IntPtr ptr, bool isDisposable = true) :
            base(isDisposable)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public double Age
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_age(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_age(this.NativePtr, value);
            }
        }

        public double Angle
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_angle(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_angle(this.NativePtr, value);
            }
        }

        public double DetectionScore
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_detection_score(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_detection_score(this.NativePtr, value);
            }
        }

        public bool Difficult
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_difficult(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_difficult(this.NativePtr, value);
            }
        }

        public Gender Gender
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_gender(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_gender(this.NativePtr, value);
            }
        }

        public bool HasLabel
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_has_label(this.NativePtr);
            }
        }

        public bool Ignore
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_ignore(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_ignore(this.NativePtr, value);
            }
        }

        public string Label
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = NativeMethods.image_dataset_metadata_box_get_label(this.NativePtr);
                return StringHelper.FromStdString(stdstr, true);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Dlib.Encoding.GetBytes(value ?? "");
                NativeMethods.image_dataset_metadata_box_set_label(this.NativePtr, str);
            }
        }

        public bool Occluded
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_occluded(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_occluded(this.NativePtr, value);
            }
        }

        public PartCollection Parts
        {
            get => new PartCollection(new InternalPartCollection(this));
            set
            {
                this.ThrowIfDisposed();

                var collection = new InternalPartCollection(this);
                collection.Clear();
                if (value == null)
                    return;

                foreach (var kvp in value)
                    collection[kvp.Key] = kvp.Value;
            }
        }

        public double Pose
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_pose(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_pose(this.NativePtr, value);
            }
        }

        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var ptr = NativeMethods.image_dataset_metadata_box_get_rect(this.NativePtr);
                return new Rectangle(ptr);
            }
            set
            {
                this.ThrowIfDisposed();
                using (var ptr = value.ToNative())
                    NativeMethods.image_dataset_metadata_box_set_rect(this.NativePtr, ptr.NativePtr);
            }
        }

        public bool Truncated
        {
            get
            {
                this.ThrowIfDisposed();
                return NativeMethods.image_dataset_metadata_box_get_truncated(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                NativeMethods.image_dataset_metadata_box_set_truncated(this.NativePtr, value);
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

            NativeMethods.image_dataset_metadata_box_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class InternalPartCollection : PartCollection.PartCollectionBridge
        {

            #region Constructors

            internal InternalPartCollection(Box parent) :
                base(parent)
            {
            }

            #endregion

            #region Properties

            public override int Count
            {
                get
                {
                    this.Parent.ThrowIfDisposed();
                    return NativeMethods.image_dataset_metadata_box_get_parts_get_size(this.Parent.NativePtr);
                }
            }

            public override Point this[string key]
            {
                get
                {
                    this.Parent.ThrowIfDisposed();

                    var str = Dlib.Encoding.GetBytes(key ?? "");
                    var native = this.Parent.NativePtr;
                    if (!NativeMethods.image_dataset_metadata_box_get_parts_get_value(native, str, out var p))
                        throw new KeyNotFoundException();

                    return new Point(p);
                }
                set
                {
                    this.Parent.ThrowIfDisposed();

                    var str = Dlib.Encoding.GetBytes(key ?? "");
                    var native = this.Parent.NativePtr;
                    using (var pp = value.ToNative())
                        NativeMethods.image_dataset_metadata_box_get_parts_set_value(native, str, pp.NativePtr);
                }
            }

            #endregion

            #region Methods

            public override void Clear()
            {
                this.Parent.ThrowIfDisposed();

                NativeMethods.image_dataset_metadata_box_parts_clear(this.Parent.NativePtr);
            }

            #endregion

            #region  Implements

            public override IEnumerator<KeyValuePair<string, Point>> GetEnumerator()
            {
                this.Parent.ThrowIfDisposed();

                using (var strings = new StdVector<StdString>())
                using (var points = new StdVector<Point>())
                {
                    NativeMethods.image_dataset_metadata_box_get_parts_get_all(this.Parent.NativePtr, strings.NativePtr, points.NativePtr);

                    var stdStrings = strings.ToArray();
                    var stringArray = stdStrings.Select(stdstr => StringHelper.FromStdString(stdstr.NativePtr)).ToArray();
                    var pointArray = points.ToArray();

                    foreach (var stdString in stdStrings)
                        stdString.Dispose();

                    for (var index = 0; index < stringArray.Length; index++)
                    {
                        var s = stringArray[index];
                        var p = pointArray[index];
                        yield return new KeyValuePair<string, Point>(s, p);
                    }
                }
            }

            #endregion

        }

    }

}
