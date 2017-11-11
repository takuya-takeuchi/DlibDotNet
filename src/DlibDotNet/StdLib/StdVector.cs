using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using DlibDotNet.Extensions;
using DlibDotNet.ImageProcessing;
using DlibDotNet.Util;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class StdVector<T> : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        private readonly StdVectorImp<T> _Imp;

        #endregion

        #region Constructors

        static StdVector()
        {
            var types = new[]
            {
                new { Type = typeof(int),                          ElementType = ElementTypes.Int32 },
                new { Type = typeof(long),                         ElementType = ElementTypes.Long  },
                new { Type = typeof(Rectangle),                    ElementType = ElementTypes.Rectangle },
                new { Type = typeof(ChipDetails),                  ElementType = ElementTypes.ChipDetails  },
                new { Type = typeof(FullObjectDetection),          ElementType = ElementTypes.FullObjectDetection  },
                new { Type = typeof(ImageWindow.OverlayLine),      ElementType = ElementTypes.ImageWindowOverlayLine  },
                new { Type = typeof(PerspectiveWindow.OverlayDot), ElementType = ElementTypes.PerspectiveWindowOverlayDot  },
                new { Type = typeof(MModRect),                     ElementType = ElementTypes.MModRect  },
                new { Type = typeof(Vector<double>),               ElementType = ElementTypes.VectorDouble       },
                new { Type = typeof(StdVector<Rectangle>),         ElementType = ElementTypes.StdVectorRectangle },
                new { Type = typeof(StdVector<MModRect>),         ElementType = ElementTypes.StdVectorMModRect  },
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public StdVector()
        {
            this._Imp = Create<T>();
            this.NativePtr = this._Imp.Create();
        }

        public StdVector(int size)
        {
            this._Imp = Create<T>();
            this.NativePtr = this._Imp.Create(size);
        }

        public StdVector(IEnumerable<T> data)
        {
            this._Imp = Create<T>();
            this.NativePtr = this._Imp.Create(data);
        }

        internal StdVector(IntPtr ptr)
        {
            this._Imp = Create<T>();
            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public IntPtr ElementPtr
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.GetElementPtr(this.NativePtr);
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.GetSize(this.NativePtr);
            }
        }

        #endregion

        #region Methods

        public T[] ToArray()
        {
            this.ThrowIfDisposed();
            return this._Imp.ToArray(this.NativePtr);
        }

        #region Helpers

        private static StdVectorImp<T> Create<T>()
        {
            if (SupportTypes.TryGetValue(typeof(T), out var type))
            {
                switch (type)
                {
                    case ElementTypes.Int32:
                        return new StdVectorInt32Imp() as StdVectorImp<T>;
                    case ElementTypes.Long:
                        return new StdVectorLongImp() as StdVectorImp<T>;
                    case ElementTypes.VectorDouble:
                        return new StdVectorVectorDoubleImp() as StdVectorImp<T>;
                    case ElementTypes.Rectangle:
                        return new StdVectorRectangleImp() as StdVectorImp<T>;
                    case ElementTypes.ChipDetails:
                        return new StdVectorChipDetailsImp() as StdVectorImp<T>;
                    case ElementTypes.FullObjectDetection:
                        return new StdVectorFullObjectDetectionImp() as StdVectorImp<T>;
                    case ElementTypes.ImageWindowOverlayLine:
                        return new StdVectorImageWindowOverlayLineImp() as StdVectorImp<T>;
                    case ElementTypes.PerspectiveWindowOverlayDot:
                        return new StdVectorPerspectiveWindowOverlayDotImp() as StdVectorImp<T>;
                    case ElementTypes.MModRect:
                        return new StdVectorMModRectImp() as StdVectorImp<T>;
                    case ElementTypes.StdVectorRectangle:
                        return new StdVectorStdVectorRectangleImp() as StdVectorImp<T>;
                    case ElementTypes.StdVectorMModRect:
                        return new StdVectorStdVectorMModRectImp() as StdVectorImp<T>;
                }
            }
            else
            {
                var t = typeof(T);
                var matrix = typeof(MatrixBase);
                if (matrix.IsAssignableFrom(t))
                {
                    var arg = GenericHelper.GetTypeParameter(t);
                    if (MatrixBase.TryParse(arg, out var r))
                    {
                        switch (r)
                        {
                            case MatrixElementTypes.UInt8:
                                return new StdVectorMatrixImp<byte>() as StdVectorImp<T>;
                            case MatrixElementTypes.UInt16:
                                return new StdVectorMatrixImp<ushort>() as StdVectorImp<T>;
                            case MatrixElementTypes.UInt32:
                                return new StdVectorMatrixImp<uint>() as StdVectorImp<T>;
                            case MatrixElementTypes.Int8:
                                return new StdVectorMatrixImp<sbyte>() as StdVectorImp<T>;
                            case MatrixElementTypes.Int16:
                                return new StdVectorMatrixImp<short>() as StdVectorImp<T>;
                            case MatrixElementTypes.Int32:
                                return new StdVectorMatrixImp<int>() as StdVectorImp<T>;
                            case MatrixElementTypes.Float:
                                return new StdVectorMatrixImp<float>() as StdVectorImp<T>;
                            case MatrixElementTypes.Double:
                                return new StdVectorMatrixImp<double>() as StdVectorImp<T>;
                            case MatrixElementTypes.RgbPixel:
                                return new StdVectorMatrixImp<RgbPixel>() as StdVectorImp<T>;
                            case MatrixElementTypes.RgbAlphaPixel:
                                return new StdVectorMatrixImp<RgbAlphaPixel>() as StdVectorImp<T>;
                            case MatrixElementTypes.HsiPixel:
                                return new StdVectorMatrixImp<HsiPixel>() as StdVectorImp<T>;
                        }
                    }
                }
            }

            throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        #endregion

        #endregion

        private enum ElementTypes
        {

            Int32,

            Long,

            Rectangle,

            PerspectiveWindowOverlayDot,

            ImageWindowOverlayLine,

            FullObjectDetection,

            ChipDetails,

            Matrix,

            MModRect,

            VectorDouble,

            StdVectorRectangle,

            StdVectorMModRect,

        }

        #region StdVectorImp

        private abstract class StdVectorImp<T>
        {

            #region Methods

            public abstract IntPtr Create();

            public abstract IntPtr Create(int size);

            public abstract IntPtr Create(IEnumerable<T> data);

            public abstract void Dispose(IntPtr ptr);

            public abstract IntPtr GetElementPtr(IntPtr ptr);

            public abstract int GetSize(IntPtr ptr);

            public abstract T[] ToArray(IntPtr ptr);

            #endregion

        }

        private sealed class StdVectorInt32Imp : StdVectorImp<int>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_int32_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_int32_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<int> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return Dlib.Native.stdvector_int32_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_int32_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_int32_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_int32_getSize(ptr).ToInt32();
            }

            public override int[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new int[0];

                var dst = new int[size];
                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, dst, 0, dst.Length);
                return dst;
            }

            #endregion

        }

        private sealed class StdVectorLongImp : StdVectorImp<long>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_long_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_long_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<long> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.ToArray();
                return Dlib.Native.stdvector_long_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_long_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_long_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_long_getSize(ptr).ToInt32();
            }

            public override long[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new long[0];

                var dst = new long[size];
                var elementPtr = this.GetElementPtr(ptr);
                Marshal.Copy(elementPtr, dst, 0, dst.Length);
                return dst;
            }

            #endregion

        }

        private sealed class StdVectorChipDetailsImp : StdVectorImp<ChipDetails>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_chip_details_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_chip_details_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<ChipDetails> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_chip_details_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_chip_details_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_chip_details_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_chip_details_getSize(ptr).ToInt32();
            }

            public override ChipDetails[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new ChipDetails[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_chip_details_copy(ptr, dst);
                return dst.Select(p => new ChipDetails(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorFullObjectDetectionImp : StdVectorImp<FullObjectDetection>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_full_object_detection_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_full_object_detection_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<FullObjectDetection> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_full_object_detection_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_full_object_detection_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_full_object_detection_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_full_object_detection_getSize(ptr).ToInt32();
            }

            public override FullObjectDetection[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new FullObjectDetection[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_full_object_detection_copy(ptr, dst);
                return dst.Select(p => new FullObjectDetection(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorImageWindowOverlayLineImp : StdVectorImp<ImageWindow.OverlayLine>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_image_window_overlay_line_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_image_window_overlay_line_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<ImageWindow.OverlayLine> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_image_window_overlay_line_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_image_window_overlay_line_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_image_window_overlay_line_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_image_window_overlay_line_getSize(ptr).ToInt32();
            }

            public override ImageWindow.OverlayLine[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new ImageWindow.OverlayLine[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_image_window_overlay_line_copy(ptr, dst);
                return dst.Select(p => new ImageWindow.OverlayLine(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorMModRectImp : StdVectorImp<MModRect>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_mmod_rect_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_mmod_rect_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<MModRect> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_mmod_rect_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_mmod_rect_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_mmod_rect_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_mmod_rect_getSize(ptr).ToInt32();
            }

            public override MModRect[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new MModRect[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_mmod_rect_copy(ptr, dst);
                return dst.Select(p => new MModRect(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorMatrixImp<U> : StdVectorImp<Matrix<U>>
            where U : struct 
        {

            #region Fields

            private readonly MatrixElementTypes _Type;

            private readonly Dlib.Native.MatrixElementType _NativeType;

            #endregion

            #region Constructors

            public StdVectorMatrixImp()
            {
                Matrix<U>.TryParse<U>(out var type);
                this._Type = type;
                this._NativeType = this._Type.ToNativeMatrixElementType();
            }

            #endregion

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_matrix_new1(this._NativeType);
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_matrix_new2(this._NativeType, new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<Matrix<U>> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_matrix_new3(this._NativeType, array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_matrix_delete(this._NativeType, ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_matrix_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_matrix_getSize(ptr).ToInt32();
            }

            public override Matrix<U>[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new Matrix<U>[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_matrix_copy(ptr, dst);
                return dst.Select(p => p != IntPtr.Zero ? new Matrix<U>(p, this._Type) : null).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorPerspectiveWindowOverlayDotImp : StdVectorImp<PerspectiveWindow.OverlayDot>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_perspective_window_overlay_dot_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_perspective_window_overlay_dot_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<PerspectiveWindow.OverlayDot> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_perspective_window_overlay_dot_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_perspective_window_overlay_dot_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_perspective_window_overlay_dot_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_perspective_window_overlay_dot_getSize(ptr).ToInt32();
            }

            public override PerspectiveWindow.OverlayDot[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new PerspectiveWindow.OverlayDot[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_perspective_window_overlay_dot_copy(ptr, dst);
                return dst.Select(p => new PerspectiveWindow.OverlayDot(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorRectangleImp : StdVectorImp<Rectangle>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_rectangle_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_rectangle_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<Rectangle> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_rectangle_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_rectangle_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_rectangle_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_rectangle_getSize(ptr).ToInt32();
            }

            public override Rectangle[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new Rectangle[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_rectangle_copy(ptr, dst);
                return dst.Select(p => new Rectangle(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorVectorDoubleImp : StdVectorImp<Vector<double>>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_vector_double_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_vector_double_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<Vector<double>> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_vector_double_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_vector_double_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_vector_double_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_vector_double_getSize(ptr).ToInt32();
            }

            public override Vector<double>[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new Vector<double>[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_vector_double_copy(ptr, dst);
                return dst.Select(p => new Vector<double>(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorStdVectorRectangleImp : StdVectorImp<StdVector<Rectangle>>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_stdvector_rectangle_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_stdvector_rectangle_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<StdVector<Rectangle>> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_stdvector_rectangle_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_stdvector_rectangle_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_stdvector_rectangle_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_stdvector_rectangle_getSize(ptr).ToInt32();
            }

            public override StdVector<Rectangle>[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new StdVector<Rectangle>[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_stdvector_rectangle_copy(ptr, dst);
                return dst.Select(p => new StdVector<Rectangle>(p)).ToArray();
            }

            #endregion

        }

        private sealed class StdVectorStdVectorMModRectImp : StdVectorImp<StdVector<MModRect>>
        {

            #region Methods

            public override IntPtr Create()
            {
                return Dlib.Native.stdvector_stdvector_mmod_rect_new1();
            }

            public override IntPtr Create(int size)
            {
                if (size < 0)
                    throw new ArgumentOutOfRangeException(nameof(size));

                return Dlib.Native.stdvector_stdvector_mmod_rect_new2(new IntPtr(size));
            }

            public override IntPtr Create(IEnumerable<StdVector<MModRect>> data)
            {
                if (data == null)
                    throw new ArgumentNullException(nameof(data));

                var array = data.Select(rectangle => rectangle.NativePtr).ToArray();
                return Dlib.Native.stdvector_stdvector_mmod_rect_new3(array, new IntPtr(array.Length));
            }

            public override void Dispose(IntPtr ptr)
            {
                Dlib.Native.stdvector_stdvector_mmod_rect_delete(ptr);
            }

            public override IntPtr GetElementPtr(IntPtr ptr)
            {
                return Dlib.Native.stdvector_stdvector_mmod_rect_getPointer(ptr);
            }

            public override int GetSize(IntPtr ptr)
            {
                return Dlib.Native.stdvector_stdvector_mmod_rect_getSize(ptr).ToInt32();
            }

            public override StdVector<MModRect>[] ToArray(IntPtr ptr)
            {
                var size = this.GetSize(ptr);
                if (size == 0)
                    return new StdVector<MModRect>[0];

                var dst = new IntPtr[size];
                Dlib.Native.stdvector_stdvector_mmod_rect_copy(ptr, dst);
                return dst.Select(p => new StdVector<MModRect>(p)).ToArray();
            }

            #endregion

        }

        #endregion

    }

}