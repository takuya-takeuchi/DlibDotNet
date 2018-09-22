using System;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.ImageDatasetMetadata
{

    public sealed class Box : DlibObject
    {

        #region Constructors

        public Box()
        {
            this.NativePtr = Native.image_dataset_metadata_box_new();
        }

        internal Box(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public double Age
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_age(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_age(this.NativePtr, value);
            }
        }

        public double Angle
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_angle(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_angle(this.NativePtr, value);
            }
        }

        public double DetectionScore
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_detection_score(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_detection_score(this.NativePtr, value);
            }
        }

        public bool Difficult
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_difficult(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_difficult(this.NativePtr, value);
            }
        }

        public Gender Gender
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_gender(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_gender(this.NativePtr, value);
            }
        }

        public bool HasLabel
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_has_label(this.NativePtr);
            }
        }

        public bool Ignore
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_ignore(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_ignore(this.NativePtr, value);
            }
        }

        public string Label
        {
            get
            {
                this.ThrowIfDisposed();
                var stdstr = Native.image_dataset_metadata_box_get_label(this.NativePtr);
                return StringHelper.FromStdString(stdstr);
            }
            set
            {
                this.ThrowIfDisposed();
                var str = Encoding.UTF8.GetBytes(value ?? "");
                Native.image_dataset_metadata_box_set_label(this.NativePtr, str);
            }
        }

        public bool Occluded
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_occluded(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_occluded(this.NativePtr, value);
            }
        }

        public double Pose
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_pose(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_pose(this.NativePtr, value);
            }
        }
        
        public Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                var ptr = Native.image_dataset_metadata_box_get_rect(this.NativePtr);
                return new Rectangle(ptr);
            }
            set
            {
                this.ThrowIfDisposed();
                using (var ptr = value.ToNative())
                    Native.image_dataset_metadata_box_set_rect(this.NativePtr, ptr.NativePtr);
            }
        }

        public bool Truncated
        {
            get
            {
                this.ThrowIfDisposed();
                return Native.image_dataset_metadata_box_get_truncated(this.NativePtr);
            }
            set
            {
                this.ThrowIfDisposed();
                Native.image_dataset_metadata_box_set_truncated(this.NativePtr, value);
            }
        }

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Native.image_dataset_metadata_box_delete(this.NativePtr);
        }

        #endregion

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_box_new();

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double image_dataset_metadata_box_get_age(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_age(IntPtr dataset, double age);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double image_dataset_metadata_box_get_angle(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_angle(IntPtr dataset, double angle);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double image_dataset_metadata_box_get_detection_score(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_detection_score(IntPtr dataset, double detectionScore);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern bool image_dataset_metadata_box_get_difficult(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_difficult(IntPtr dataset, bool difficult);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern Gender image_dataset_metadata_box_get_gender(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_gender(IntPtr dataset, Gender gender);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern bool image_dataset_metadata_box_has_label(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern bool image_dataset_metadata_box_get_ignore(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_ignore(IntPtr dataset, bool ignore);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_box_get_label(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_label(IntPtr dataset, byte[] label);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern bool image_dataset_metadata_box_get_occluded(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_occluded(IntPtr dataset, bool occluded);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern double image_dataset_metadata_box_get_pose(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_dataset_metadata_box_get_rect(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_rect(IntPtr dataset, IntPtr rect);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_pose(IntPtr dataset, double pose);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern bool image_dataset_metadata_box_get_truncated(IntPtr dataset);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_set_truncated(IntPtr dataset, bool truncated);

            [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern void image_dataset_metadata_box_delete(IntPtr dataset);

        }

    }

}
