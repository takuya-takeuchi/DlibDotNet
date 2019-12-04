using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;
using DlibDotNet.ImageDatasetMetadata;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ImageDisplay : ScrollableRegion
    {

        #region Constructors

        public ImageDisplay(DrawableWindow window) :
            base(window)
        {
            this.NativePtr = NativeMethods.image_display_new(window.NativePtr);
        }

        #endregion

        #region Methods

        public void AddLabelablePartName(string name)
        {
            this.ThrowIfDisposed();

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var str = Dlib.Encoding.GetBytes(name);
            NativeMethods.image_display_add_labelable_part_name(this.NativePtr, str, str.Length);
        }

        public void AddOverlay(IEnumerable<OverlayRect> rects)
        {
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<OverlayRect>(rects))
                NativeMethods.image_display_add_overlay(this.NativePtr, vector.NativePtr);
        }

        public void ClearOverlay()
        {
            this.ThrowIfDisposed();
            NativeMethods.image_display_clear_overlay(this.NativePtr);
        }

        public string GetDefaultOverlayRectLabel()
        {
            this.ThrowIfDisposed();
            var ret = NativeMethods.image_display_get_default_overlay_rect_label(this.NativePtr);
            return StringHelper.FromStdString(ret, true);
        }

        public IEnumerable<OverlayRect> GetOverlayRects()
        {
            this.ThrowIfDisposed();
            var rects = NativeMethods.image_display_get_overlay_rects(this.NativePtr);
            using (var vector = new StdVector<OverlayRect>(rects))
                return vector.ToArray();
        }

        public void SetImage<T>(Array2D<T> image)
            where T : struct
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            this.ThrowIfDisposed();
            image.ThrowIfDisposed();

            var inType = image.ImageType.ToNativeArray2DType();
            var ret = NativeMethods.image_display_set_image(this.NativePtr, inType, image.NativePtr);
            switch (ret)
            {
                case NativeMethods.ErrorType.Array2DTypeTypeNotSupport:
                    throw new ArgumentException($"Input {inType} is not supported.");
            }
        }

        public void SetImageClickedHandler(ClickActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_image_clicked_handler(this.NativePtr, mediator.NativePtr);
        }

        public void SetOverlayRectsChangedHandler(VoidActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_overlay_rects_changed_handler(this.NativePtr, mediator.NativePtr);
        }

        public void SetOverlayRectSelectedHandler(ImageDisplayOverlayRectActionMediator mediator)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_overlay_rect_selected_handler(this.NativePtr, mediator.NativePtr);
        }

        public void SetDefaultOverlayRectColor(RgbAlphaPixel color)
        {
            this.ThrowIfDisposed();
            NativeMethods.image_display_set_default_overlay_rect_color(this.NativePtr, color);
        }

        public void SetDefaultOverlayRectLabel(string label)
        {
            this.ThrowIfDisposed();

            var labelStr = Dlib.Encoding.GetBytes(label ?? "");
            NativeMethods.image_display_set_default_overlay_rect_label(this.NativePtr, labelStr, labelStr.Length);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.image_display_delete(this.NativePtr);
        }

        #endregion

        #endregion

        public sealed class OverlayRect : DlibObject
        {

            #region Constructors

            public OverlayRect()
            {
                this.NativePtr = NativeMethods.image_display_overlay_rect_new();
            }

            internal OverlayRect(IntPtr ptr, bool isEnabledDispose = true) :
                base(isEnabledDispose)
            {
                this.NativePtr = ptr;
            }

            #endregion

            #region Properties

            public RgbAlphaPixel Color
            {
                get
                {
                    this.ThrowIfDisposed();
                    var color = new RgbAlphaPixel();
                    NativeMethods.image_display_overlay_rect_get_color(this.NativePtr, ref color);
                    return color;
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.image_display_overlay_rect_set_color(this.NativePtr, value);
                }
            }

            public bool CrossedOut
            {
                get
                {
                    this.ThrowIfDisposed();
                    return NativeMethods.image_display_overlay_rect_get_crossed_out(this.NativePtr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    NativeMethods.image_display_overlay_rect_set_crossed_out(this.NativePtr, value);
                }
            }

            public string Label
            {
                get
                {
                    this.ThrowIfDisposed();
                    var stdstr = NativeMethods.image_display_overlay_rect_get_label(this.NativePtr);
                    return StringHelper.FromStdString(stdstr, true);
                }
                set
                {
                    this.ThrowIfDisposed();
                    var str = Dlib.Encoding.GetBytes(value ?? "");
                    NativeMethods.image_display_overlay_rect_set_label(this.NativePtr, str, str.Length);
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

            public Rectangle Rect
            {
                get
                {
                    this.ThrowIfDisposed();
                    var ptr = NativeMethods.image_display_overlay_rect_get_rect(this.NativePtr);
                    return new Rectangle(ptr);
                }
                set
                {
                    this.ThrowIfDisposed();
                    using (var ptr = value.ToNative())
                        NativeMethods.image_display_overlay_rect_set_rect(this.NativePtr, ptr.NativePtr);
                }
            }

            #endregion

            #region Methods

            #region Overrids

            /// <summary>
            /// Releases all unmanaged resources.
            /// </summary>
            protected override void DisposeUnmanaged()
            {
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.image_display_overlay_rect_delete(this.NativePtr);
            }

            #endregion

            #endregion

            internal sealed class InternalPartCollection : PartCollection.PartCollectionBridge
            {

                #region Constructors

                internal InternalPartCollection(OverlayRect parent) :
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
                        return NativeMethods.image_display_overlay_rect_get_parts_get_size(this.Parent.NativePtr);
                    }
                }

                public override Point this[string key]
                {
                    get
                    {
                        this.Parent.ThrowIfDisposed();

                        var str = Dlib.Encoding.GetBytes(key ?? "");
                        var native = this.Parent.NativePtr;
                        if (!NativeMethods.image_display_overlay_rect_get_parts_get_value(native, str, str.Length, out var p))
                            throw new KeyNotFoundException();

                        return new Point(p);
                    }
                    set
                    {
                        this.Parent.ThrowIfDisposed();

                        var str = Dlib.Encoding.GetBytes(key ?? "");
                        var native = this.Parent.NativePtr;
                        using (var pp = value.ToNative())
                            NativeMethods.image_display_overlay_rect_get_parts_set_value(native, str, str.Length, pp.NativePtr);
                    }
                }

                #endregion

                #region Methods

                public override void Clear()
                {
                    this.Parent.ThrowIfDisposed();

                    NativeMethods.image_display_overlay_rect_get_parts_clear(this.Parent.NativePtr);
                }

                #endregion

                #region  Implements

                public override IEnumerator<KeyValuePair<string, Point>> GetEnumerator()
                {
                    this.Parent.ThrowIfDisposed();

                    using (var strings = new StdVector<StdString>())
                    using (var points = new StdVector<Point>())
                    {
                        NativeMethods.image_display_overlay_rect_get_parts_get_all(this.Parent.NativePtr, strings.NativePtr, points.NativePtr);

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

}