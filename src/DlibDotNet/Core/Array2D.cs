using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class Array2D<T> : Array2DBase
        where T : struct 
    {

        #region Fields

        private readonly Dlib.Native.Array2DType _Array2DType;

        private static readonly Dictionary<Type, ImageTypes> SupportTypes = new Dictionary<Type, ImageTypes>();

        #endregion

        #region Constructors

        static Array2D()
        {
            var types = new[]
            {
                new { Type = typeof(byte),          ElementType = ImageTypes.UInt8  },
                new { Type = typeof(ushort),        ElementType = ImageTypes.UInt16 },
                new { Type = typeof(int),           ElementType = ImageTypes.Int32 },
                new { Type = typeof(float),         ElementType = ImageTypes.Float  },
                new { Type = typeof(double),        ElementType = ImageTypes.Double },
                new { Type = typeof(RgbPixel),      ElementType = ImageTypes.RgbPixel },
                new { Type = typeof(RgbAlphaPixel), ElementType = ImageTypes.RgbAlphaPixel },
                new { Type = typeof(HsiPixel),      ElementType = ImageTypes.HsiPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        public Array2D()
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = Dlib.Native.array2d_new(this._Array2DType);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        public Array2D(int rows, int columns)
        {
            if (!SupportTypes.TryGetValue(typeof(T), out var type))
                throw new NotSupportedException($"{typeof(T).Name} does not support");

            this._Array2DType = type.ToNativeArray2DType();

            this.NativePtr = Dlib.Native.array2d_new1(this._Array2DType, rows, columns);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this.ImageType = type;
        }

        #endregion

        #region Properties

        public override int Columns
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_nc(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override ImageTypes ImageType
        {
            get;
        }

        public override Rectangle Rect
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.rectangle_get_rect(this._Array2DType, this.NativePtr, out var ret);
                return new Rectangle(ret);
            }
        }

        public override int Rows
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_nr(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        public override int Size
        {
            get
            {
                this.ThrowIfDisposed();
                Dlib.Native.array2d_size(this._Array2DType, this.NativePtr, out var ret);
                return ret;
            }
        }

        #endregion

        #region Methods 

        #region Overrides 

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            Dlib.Native.array2d_delete(this._Array2DType, this.NativePtr);
        }

        #endregion

        #endregion

    }

}
