using System;
using System.Collections;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// This object represents a 1-Dimensional array of objects. 
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    public sealed class Array<T> : DlibObject, IEnumerable<T>
    {

        #region Fields

        private readonly Dlib.Native.Array2DType _Array2DType;

        private static readonly Dictionary<Type, ItemTypes> SupportItemTypes = new Dictionary<Type, ItemTypes>();

        private static readonly Dictionary<Type, ImageTypes> SupportArray2DElementTypes = new Dictionary<Type, ImageTypes>();

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixElementTypes = new Dictionary<Type, MatrixElementTypes>();

        private readonly ItemTypes _ItemType;

        private readonly ImageTypes _ArrayElementType;

        private readonly MatrixElementTypes _MatrixElementType;

        #endregion

        #region Constructors

        static Array()
        {
            var itemTypes = new[]
            {
                new { Type = typeof(byte),                   ItemType = ItemTypes.PixelType },
                new { Type = typeof(ushort),                 ItemType = ItemTypes.PixelType },
                new { Type = typeof(uint),                   ItemType = ItemTypes.PixelType },
                new { Type = typeof(sbyte),                  ItemType = ItemTypes.PixelType },
                new { Type = typeof(short),                  ItemType = ItemTypes.PixelType },
                new { Type = typeof(int),                    ItemType = ItemTypes.PixelType },
                new { Type = typeof(float),                  ItemType = ItemTypes.PixelType },
                new { Type = typeof(double),                 ItemType = ItemTypes.PixelType },
                new { Type = typeof(RgbPixel),               ItemType = ItemTypes.PixelType },
                new { Type = typeof(RgbAlphaPixel),          ItemType = ItemTypes.PixelType },
                new { Type = typeof(HsiPixel),               ItemType = ItemTypes.PixelType },

                new { Type = typeof(Array2D<byte>),          ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<ushort>),        ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<uint>),          ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<sbyte>),         ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<short>),         ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<int>),           ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<float>),         ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<double>),        ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<RgbPixel>),      ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<RgbAlphaPixel>), ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<HsiPixel>),      ItemType = ItemTypes.Array2D },

                new { Type = typeof(Matrix<byte>),           ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<ushort>),         ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<uint>),           ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<sbyte>),          ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<short>),          ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<int>),            ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<float>),          ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<double>),         ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<RgbPixel>),       ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<HsiPixel>),       ItemType = ItemTypes.Matrix },
                new { Type = typeof(Matrix<RgbAlphaPixel>),  ItemType = ItemTypes.Matrix }
            };

            foreach (var type in itemTypes)
                SupportItemTypes.Add(type.Type, type.ItemType);

            var elementTypes = new[]
            {
                new { Type = typeof(byte),          ElementType = ImageTypes.UInt8  },
                new { Type = typeof(ushort),        ElementType = ImageTypes.UInt16 },
                new { Type = typeof(uint),          ElementType = ImageTypes.UInt32  },
                new { Type = typeof(sbyte),         ElementType = ImageTypes.Int8  },
                new { Type = typeof(short),         ElementType = ImageTypes.Int16 },
                new { Type = typeof(int),           ElementType = ImageTypes.Int32  },
                new { Type = typeof(float),         ElementType = ImageTypes.Float  },
                new { Type = typeof(double),        ElementType = ImageTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = ImageTypes.RgbPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = ImageTypes.RgbAlphaPixel },
                new { Type = typeof(HsiPixel),      ElementType = ImageTypes.HsiPixel }
            };

            foreach (var type in elementTypes)
                SupportArray2DElementTypes.Add(type.Type, type.ElementType);

            var matrixTypes = new[]
            {
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8 },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16 },
                new { Type = typeof(uint),          ElementType = MatrixElementTypes.UInt32 },
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8 },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16 },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32 },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = MatrixElementTypes.RgbPixel },
                new { Type = typeof(HsiPixel),      ElementType = MatrixElementTypes.HsiPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = MatrixElementTypes.RgbAlphaPixel }
            };

            foreach (var type in matrixTypes)
                SupportMatrixElementTypes.Add(type.Type, type.ElementType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array{T}"/> class.
        /// </summary>
        public Array()
            : this(false, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Array{T}"/> class with the specified size of array.
        /// </summary>
        /// <param name="size">The number of elements which have initial values for their type.</param>
        public Array(uint size)
            : this(true, size)
        {
        }

        private Array(bool specifySize, uint size)
        {
            if (!SupportItemTypes.TryGetValue(typeof(T), out this._ItemType))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            switch (this._ItemType)
            {
                case ItemTypes.PixelType:
                    {
                        if (!SupportArray2DElementTypes.TryGetValue(typeof(T), out this._ArrayElementType))
                            throw new NotSupportedException($"{typeof(T).Name} does not support");

                        var type = this._ArrayElementType.ToNativeArray2DType();
                        this.NativePtr = specifySize ? Dlib.Native.array_new1(type, size) : Dlib.Native.array_new(type);
                    }
                    break;
                case ItemTypes.Array2D:
                    {
                        var types = typeof(T).GenericTypeArguments;
                        if (types.Length != 1)
                            throw new NotSupportedException($"{typeof(T).Name} does not support");
                        if (!SupportArray2DElementTypes.TryGetValue(types[0], out this._ArrayElementType))
                            throw new NotSupportedException($"{types[0].Name} does not support");

                        var type = this._ArrayElementType.ToNativeArray2DType();
                        this.NativePtr = specifySize ? Dlib.Native.array_array2d_new1(type, size) : Dlib.Native.array_array2d_new(type);
                        this._Array2DType = this._ArrayElementType.ToNativeArray2DType();
                    }
                    break;
                case ItemTypes.Matrix:
                    {
                        var types = typeof(T).GenericTypeArguments;
                        if (types.Length != 1)
                            throw new NotSupportedException($"{typeof(T).Name} does not support");
                        if (!SupportMatrixElementTypes.TryGetValue(types[0], out this._MatrixElementType))
                            throw new NotSupportedException($"{typeof(T).Name} does not support");

                        var type = this._MatrixElementType.ToNativeMatrixElementType();
                        this.NativePtr = specifySize ? Dlib.Native.array_matrix_new1(type, size) : Dlib.Native.array_matrix_new(type);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        #region Properties

        public ImageTypes ImageType => this._ArrayElementType;

        public MatrixElementTypes MatrixElementTypes => this._MatrixElementType;

        public T this[int index]
        {
            get
            {
                this.ThrowIfDisposed();

                switch (this._ItemType)
                {
                    case ItemTypes.PixelType:
                        return (T)this.GetPixelItem(this._ArrayElementType, (uint)index);
                    case ItemTypes.Array2D:
                        return (T)this.GetArray2DItem(this._Array2DType, (uint)index);
                    case ItemTypes.Matrix:
                        return (T)this.GetMatrixItem(this._MatrixElementType, (uint)index);
                }

                return default(T);
            }
        }

        public int Size
        {
            get
            {
                this.ThrowIfDisposed();

                uint size = 0;
                var error = Dlib.Native.ErrorType.OK;
                switch (this._ItemType)
                {
                    case ItemTypes.PixelType:
                        error = Dlib.Native.array_pixel_size(this._Array2DType, this.NativePtr, out size);
                        break;
                    case ItemTypes.Array2D:
                        error = Dlib.Native.array_array2d_size(this._Array2DType, this.NativePtr, out size);
                        break;
                    case ItemTypes.Matrix:
                        error = Dlib.Native.array_matrix_size(this._MatrixElementType.ToNativeMatrixElementType(), this.NativePtr, out size);
                        break;
                }

                return (int)size;
            }
        }

        #endregion

        #region Methods

        public void PushBack(T item)
        {
            this.ThrowIfDisposed();

            switch (this._ItemType)
            {
                case ItemTypes.PixelType:
                    this.PushBackPixelItem(this._ArrayElementType, item);
                    break;
                case ItemTypes.Array2D:
                    // NOTE
                    // item is moved. So item content get to be undefined value
                    this.PushBackArray2DItem(this._Array2DType, item as DlibObject);
                    break;
                case ItemTypes.Matrix:
                    // NOTE
                    // item is moved. So item content get to be undefined value
                    this.PushBackMatrixItem(this._MatrixElementType.ToNativeMatrixElementType(), item as DlibObject);
                    break;
            }
        }

        #region Overrides 

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            switch (this._ItemType)
            {
                case ItemTypes.PixelType:
                    Dlib.Native.array_delete_pixel(this._Array2DType, this.NativePtr);
                    break;
                case ItemTypes.Array2D:
                    Dlib.Native.array_delete_array2d(this._Array2DType, this.NativePtr);
                    break;
                case ItemTypes.Matrix:
                    Dlib.Native.array_delete_matrix(this._MatrixElementType.ToNativeMatrixElementType(), this.NativePtr);
                    break;
            }
        }

        #endregion

        #region Helpers 

        private object GetPixelItem(ImageTypes type, uint index)
        {
            switch (type)
            {
                case ImageTypes.UInt8:
                    Dlib.Native.array_pixel_getitem_uint8(type.ToNativeArray2DType(), this.NativePtr, index, out var b8);
                    return b8;
                case ImageTypes.UInt16:
                    Dlib.Native.array_pixel_getitem_uint16(type.ToNativeArray2DType(), this.NativePtr, index, out var b16);
                    return b16;
                case ImageTypes.UInt32:
                    Dlib.Native.array_pixel_getitem_uint32(type.ToNativeArray2DType(), this.NativePtr, index, out var b32);
                    return b32;
                case ImageTypes.Int8:
                    Dlib.Native.array_pixel_getitem_int8(type.ToNativeArray2DType(), this.NativePtr, index, out var u8);
                    return u8;
                case ImageTypes.Int16:
                    Dlib.Native.array_pixel_getitem_int16(type.ToNativeArray2DType(), this.NativePtr, index, out var u16);
                    return u16;
                case ImageTypes.Int32:
                    Dlib.Native.array_pixel_getitem_int32(type.ToNativeArray2DType(), this.NativePtr, index, out var u32);
                    return u32;
                case ImageTypes.Float:
                    Dlib.Native.array_pixel_getitem_float(type.ToNativeArray2DType(), this.NativePtr, index, out var f);
                    return f;
                case ImageTypes.Double:
                    Dlib.Native.array_pixel_getitem_double(type.ToNativeArray2DType(), this.NativePtr, index, out var d);
                    return d;
                case ImageTypes.RgbPixel:
                    Dlib.Native.array_pixel_getitem_rgb_pixel(type.ToNativeArray2DType(), this.NativePtr, index, out var rgb);
                    return rgb;
                case ImageTypes.RgbAlphaPixel:
                    Dlib.Native.array_pixel_getitem_rgb_alpha_pixel(type.ToNativeArray2DType(), this.NativePtr, index, out var rgba);
                    return rgba;
                case ImageTypes.HsiPixel:
                    Dlib.Native.array_pixel_getitem_hsi_pixel(type.ToNativeArray2DType(), this.NativePtr, index, out var hsi);
                    return hsi;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private object GetArray2DItem(Dlib.Native.Array2DType type, uint index)
        {
            var err = Dlib.Native.array_array2d_getitem(type,
                                                        this.NativePtr, index,
                                                        out var array);

            switch (type)
            {
                case Dlib.Native.Array2DType.UInt8:
                    return new Array2D<byte>(array, ImageTypes.UInt8, false);
                case Dlib.Native.Array2DType.UInt16:
                    return new Array2D<ushort>(array, ImageTypes.UInt16, false);
                case Dlib.Native.Array2DType.UInt32:
                    return new Array2D<uint>(array, ImageTypes.UInt32, false);
                case Dlib.Native.Array2DType.Int8:
                    return new Array2D<sbyte>(array, ImageTypes.Int8, false);
                case Dlib.Native.Array2DType.Int16:
                    return new Array2D<short>(array, ImageTypes.Int16, false);
                case Dlib.Native.Array2DType.Int32:
                    return new Array2D<int>(array, ImageTypes.Int32, false);
                case Dlib.Native.Array2DType.Float:
                    return new Array2D<float>(array, ImageTypes.Float, false);
                case Dlib.Native.Array2DType.Double:
                    return new Array2D<double>(array, ImageTypes.Double, false);
                case Dlib.Native.Array2DType.RgbPixel:
                    return new Array2D<RgbPixel>(array, ImageTypes.RgbPixel, false);
                case Dlib.Native.Array2DType.RgbAlphaPixel:
                    return new Array2D<RgbAlphaPixel>(array, ImageTypes.RgbAlphaPixel, false);
                case Dlib.Native.Array2DType.HsiPixel:
                    return new Array2D<HsiPixel>(array, ImageTypes.HsiPixel, false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private object GetMatrixItem(MatrixElementTypes type, uint index)
        {
            var err = Dlib.Native.array_matrix_getitem(this._MatrixElementType.ToNativeMatrixElementType(),
                                                       this.NativePtr, index,
                                                       out var mat);

            var templateRow = 0;
            var templateColumn = 0;
            switch (type)
            {
                case MatrixElementTypes.UInt8:
                    return new Matrix<byte>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.UInt16:
                    return new Matrix<ushort>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.UInt32:
                    return new Matrix<uint>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.Int8:
                    return new Matrix<sbyte>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.Int16:
                    return new Matrix<short>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.Int32:
                    return new Matrix<int>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.Float:
                    return new Matrix<float>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.Double:
                    return new Matrix<double>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.RgbPixel:
                    return new Matrix<RgbPixel>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.RgbAlphaPixel:
                    return new Matrix<RgbAlphaPixel>(mat, templateRow, templateColumn, false);
                case MatrixElementTypes.HsiPixel:
                    return new Matrix<HsiPixel>(mat, templateRow, templateColumn, false);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void PushBackArray2DItem(Dlib.Native.Array2DType type, IDlibObject item)
        {
            item.ThrowIfDisposed();
            Dlib.Native.array_array2d_pushback(type, this.NativePtr, item.NativePtr);
        }
        
        private void PushBackMatrixItem(Dlib.Native.MatrixElementType type, IDlibObject item)
        {
            item.ThrowIfDisposed();
            Dlib.Native.array_matrix_pushback(type, this.NativePtr, item.NativePtr);
        }

        private void PushBackPixelItem(ImageTypes type, object item)
        {
            switch (type)
            {
                case ImageTypes.UInt8:
                    Dlib.Native.array_pixel_pushback_uint8(type.ToNativeArray2DType(), this.NativePtr, (byte)item);
                    break;
                case ImageTypes.UInt16:
                    Dlib.Native.array_pixel_pushback_uint16(type.ToNativeArray2DType(), this.NativePtr, (ushort)item);
                    break;
                case ImageTypes.UInt32:
                    Dlib.Native.array_pixel_pushback_uint32(type.ToNativeArray2DType(), this.NativePtr, (uint)item);
                    break;
                case ImageTypes.Int8:
                    Dlib.Native.array_pixel_pushback_int8(type.ToNativeArray2DType(), this.NativePtr, (sbyte)item);
                    break;
                case ImageTypes.Int16:
                    Dlib.Native.array_pixel_pushback_int16(type.ToNativeArray2DType(), this.NativePtr, (short)item);
                    break;
                case ImageTypes.Int32:
                    Dlib.Native.array_pixel_pushback_int32(type.ToNativeArray2DType(), this.NativePtr, (int)item);
                    break;
                case ImageTypes.Float:
                    Dlib.Native.array_pixel_pushback_float(type.ToNativeArray2DType(), this.NativePtr, (float)item);
                    break;
                case ImageTypes.Double:
                    Dlib.Native.array_pixel_pushback_double(type.ToNativeArray2DType(), this.NativePtr, (double)item);
                    break;
                case ImageTypes.RgbPixel:
                    Dlib.Native.array_pixel_pushback_rgb_pixel(type.ToNativeArray2DType(), this.NativePtr, (RgbPixel)item);
                    break;
                case ImageTypes.RgbAlphaPixel:
                    Dlib.Native.array_pixel_pushback_rgb_alpha_pixel(type.ToNativeArray2DType(), this.NativePtr, (RgbAlphaPixel)item);
                    break;
                case ImageTypes.HsiPixel:
                    Dlib.Native.array_pixel_pushback_hsi_pixel(type.ToNativeArray2DType(), this.NativePtr, (HsiPixel)item);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        #endregion

        #endregion

        private enum ItemTypes
        {

            PixelType,

            Array2D,

            Matrix

        }

        #region IEnumerable<T> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            this.ThrowIfDisposed();

            switch (this._ItemType)
            {
                case ItemTypes.PixelType:
                    for (int i = 0, count = this.Size; i < count; i++)
                        yield return (T)this.GetPixelItem(this._ArrayElementType, (uint)i);
                    break;
                case ItemTypes.Array2D:
                    for (int i = 0, count = this.Size; i < count; i++)
                        yield return (T)this.GetArray2DItem(this._Array2DType, (uint)i);
                    break;
                case ItemTypes.Matrix:
                    for (int i = 0, count = this.Size; i < count; i++)
                        yield return (T)this.GetMatrixItem(this._MatrixElementType, (uint)i);
                    break;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}
