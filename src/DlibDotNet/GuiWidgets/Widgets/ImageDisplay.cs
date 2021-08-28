#if !LITE
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
#if !DLIB_NO_GUI_SUPPORT
            this.NativePtr = NativeMethods.image_display_new(window.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #region Methods

        public void AddLabelablePartName(string name)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var str = Dlib.Encoding.GetBytes(name);
            NativeMethods.image_display_add_labelable_part_name(this.NativePtr, str, str.Length);
#else
            throw new NotSupportedException();
#endif
        }

        public void AddOverlay(IEnumerable<OverlayRect> rects)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            if (rects == null)
                throw new ArgumentNullException(nameof(rects));

            using (var vector = new StdVector<OverlayRect>(rects))
                NativeMethods.image_display_add_overlay(this.NativePtr, vector.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void ClearOverlay()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.image_display_clear_overlay(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public string GetDefaultOverlayRectLabel()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var ret = NativeMethods.image_display_get_default_overlay_rect_label(this.NativePtr);
            return StringHelper.FromStdString(ret, true);
#else
            throw new NotSupportedException();
#endif
        }

        public IEnumerable<OverlayRect> GetOverlayRects()
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            var rects = NativeMethods.image_display_get_overlay_rects(this.NativePtr);
            using (var vector = new StdVector<OverlayRect>(rects))
                return vector.ToArray();
#else
            throw new NotSupportedException();
#endif
        }

        public void SetImage<T>(Array2D<T> image)
            where T : struct
        {
#if !DLIB_NO_GUI_SUPPORT
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
#else
            throw new NotSupportedException();
#endif
        }

        public void SetImageClickedHandler(ClickActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_image_clicked_handler(this.NativePtr, mediator.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetOverlayRectsChangedHandler(VoidActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_overlay_rects_changed_handler(this.NativePtr, mediator.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetOverlayRectSelectedHandler(ImageDisplayOverlayRectActionMediator mediator)
        {
#if !DLIB_NO_GUI_SUPPORT
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.ThrowIfDisposed();
            mediator.ThrowIfDisposed();

            NativeMethods.image_display_set_overlay_rect_selected_handler(this.NativePtr, mediator.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetDefaultOverlayRectColor(RgbAlphaPixel color)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();
            NativeMethods.image_display_set_default_overlay_rect_color(this.NativePtr, color);
#else
            throw new NotSupportedException();
#endif
        }

        public void SetDefaultOverlayRectLabel(string label)
        {
#if !DLIB_NO_GUI_SUPPORT
            this.ThrowIfDisposed();

            var labelStr = Dlib.Encoding.GetBytes(label ?? "");
            NativeMethods.image_display_set_default_overlay_rect_label(this.NativePtr, labelStr, labelStr.Length);
#else
            throw new NotSupportedException();
#endif
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
#if !DLIB_NO_GUI_SUPPORT
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.image_display_delete(this.NativePtr);
#else
            throw new NotSupportedException();
#endif
        }

        #endregion

        #endregion

        public sealed class OverlayRect : DlibObject
        {

            #region Constructors

            public OverlayRect()
            {
#if !DLIB_NO_GUI_SUPPORT
                this.NativePtr = NativeMethods.image_display_overlay_rect_new();
#else
            throw new NotSupportedException();
#endif
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
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    var color = new RgbAlphaPixel();
                    NativeMethods.image_display_overlay_rect_get_color(this.NativePtr, ref color);
                    return color;
#else
                    throw new NotSupportedException();
#endif
                }
                set
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    NativeMethods.image_display_overlay_rect_set_color(this.NativePtr, value);
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public bool CrossedOut
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    return NativeMethods.image_display_overlay_rect_get_crossed_out(this.NativePtr);
#else
                    throw new NotSupportedException();
#endif
                }
                set
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    NativeMethods.image_display_overlay_rect_set_crossed_out(this.NativePtr, value);
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public string Label
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    var stdstr = NativeMethods.image_display_overlay_rect_get_label(this.NativePtr);
                    return StringHelper.FromStdString(stdstr, true);
#else
                    throw new NotSupportedException();
#endif
                }
                set
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    var str = Dlib.Encoding.GetBytes(value ?? "");
                    NativeMethods.image_display_overlay_rect_set_label(this.NativePtr, str, str.Length);
#else
                    throw new NotSupportedException();
#endif
                }
            }

            public PartCollection Parts
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    return new PartCollection(new InternalPartCollection(this));
#else
                    throw new NotSupportedException();
#endif
                }
                set
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();

                    var collection = new InternalPartCollection(this);
                    collection.Clear();
                    if (value == null)
                        return;

                    foreach (var kvp in value)
                        collection[kvp.Key] = kvp.Value;
#else
            throw new NotSupportedException();
#endif
                }
            }

            public Rectangle Rect
            {
                get
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    var ptr = NativeMethods.image_display_overlay_rect_get_rect(this.NativePtr);
                    return new Rectangle(ptr);
#else
                    throw new NotSupportedException();
#endif
                }
                set
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.ThrowIfDisposed();
                    using (var ptr = value.ToNative())
                        NativeMethods.image_display_overlay_rect_set_rect(this.NativePtr, ptr.NativePtr);
#else
                    throw new NotSupportedException();
#endif
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
#if !DLIB_NO_GUI_SUPPORT
                base.DisposeUnmanaged();

                if (this.NativePtr == IntPtr.Zero)
                    return;

                NativeMethods.image_display_overlay_rect_delete(this.NativePtr);
#else
                throw new NotSupportedException();
#endif
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
#if !DLIB_NO_GUI_SUPPORT
                        this.Parent.ThrowIfDisposed();
                        return NativeMethods.image_display_overlay_rect_get_parts_get_size(this.Parent.NativePtr);
#else
                        throw new NotSupportedException();
#endif
                    }
                }

                public override Point this[string key]
                {
                    get
                    {
#if !DLIB_NO_GUI_SUPPORT
                        this.Parent.ThrowIfDisposed();

                        var str = Dlib.Encoding.GetBytes(key ?? "");
                        var native = this.Parent.NativePtr;
                        if (!NativeMethods.image_display_overlay_rect_get_parts_get_value(native, str, str.Length, out var p))
                            throw new KeyNotFoundException();

                        return new Point(p);
#else
                        throw new NotSupportedException();
#endif
                    }
                    set
                    {
#if !DLIB_NO_GUI_SUPPORT
                        this.Parent.ThrowIfDisposed();

                        var str = Dlib.Encoding.GetBytes(key ?? "");
                        var native = this.Parent.NativePtr;
                        using (var pp = value.ToNative())
                            NativeMethods.image_display_overlay_rect_get_parts_set_value(native, str, str.Length, pp.NativePtr);
#else
                        throw new NotSupportedException();
#endif
                    }
                }

                #endregion

                #region Methods

                public override void Clear()
                {
#if !DLIB_NO_GUI_SUPPORT
                    this.Parent.ThrowIfDisposed();

                    NativeMethods.image_display_overlay_rect_get_parts_clear(this.Parent.NativePtr);
#else
                    throw new NotSupportedException();
#endif
                }

                #endregion

                #region  Implements

                public override IEnumerator<KeyValuePair<string, Point>> GetEnumerator()
                {
#if !DLIB_NO_GUI_SUPPORT
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
#else
                    throw new NotSupportedException();
#endif
                }

                #endregion

            }

        }

    }

}
#endif
