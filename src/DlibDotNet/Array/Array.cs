using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Array<T> : DlibObject, IEnumerable<T>
        where T : DlibObject, new()
    {

        #region Fields

        private readonly Dlib.Native.Array2DType _Array2DType;

        private static readonly Dictionary<Type, ItemTypes> SupportItemTypes = new Dictionary<Type, ItemTypes>();

        private static readonly Dictionary<Type, ImageTypes> SupportArray2DElementTypes = new Dictionary<Type, ImageTypes>();

        private static readonly Dictionary<Type, MatrixElementTypes> SupportMatrixElementTypes = new Dictionary<Type, MatrixElementTypes>();

        private readonly ItemTypes _ItemType;

        private ImageTypes _ArrayElementType;

        private MatrixElementTypes _MatrixElementType;

        #endregion

        #region Constructors

        static Array()
        {
            var itemTypes = new[]
            {
                new { Type = typeof(byte),                   ItemType = ItemTypes.PixelType },
                new { Type = typeof(ushort),                 ItemType = ItemTypes.PixelType },
                new { Type = typeof(float),                  ItemType = ItemTypes.PixelType },
                new { Type = typeof(double),                 ItemType = ItemTypes.PixelType },
                new { Type = typeof(RgbPixel),               ItemType = ItemTypes.PixelType },
                new { Type = typeof(RgbAlphaPixel),          ItemType = ItemTypes.PixelType },
                new { Type = typeof(HsiPixel),               ItemType = ItemTypes.PixelType },

                new { Type = typeof(Array2D<byte>),          ItemType = ItemTypes.Array2D },
                new { Type = typeof(Array2D<ushort>),        ItemType = ItemTypes.Array2D },
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

        public Array()
            : this(false, 0)
        {
        }

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
                    }
                    break;
                case ItemTypes.Matrix:
                    {
                        var types = typeof(T).GenericTypeArguments;
                        if (types.Length != 1)
                            throw new NotSupportedException($"{typeof(T).Name} does not support");
                        if (!SupportMatrixElementTypes.TryGetValue(typeof(T), out this._MatrixElementType))
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

        #region Overrides 

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

        #endregion

        private enum ItemTypes
        {

            PixelType,

            Array2D,

            Matrix

        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            //for (int i = 0, count = this.Size; i < count; i++)
            //{

            //}
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

    }

}
