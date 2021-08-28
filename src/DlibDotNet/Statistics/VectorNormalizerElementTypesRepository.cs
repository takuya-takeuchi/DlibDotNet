#if !LITE
using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class VectorNormalizerElementTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, MatrixElementTypes> Types = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors
        
        static VectorNormalizerElementTypesRepository()
        {
            var types = new[]
            {
                new {Type = typeof(Matrix<byte>),           ElementType = MatrixElementTypes.UInt8 },
                new {Type = typeof(Matrix<ushort>),         ElementType = MatrixElementTypes.UInt16 },
                new {Type = typeof(Matrix<uint>),           ElementType = MatrixElementTypes.UInt32 },
                new {Type = typeof(Matrix<ulong>),          ElementType = MatrixElementTypes.UInt64 },
                new {Type = typeof(Matrix<sbyte>),          ElementType = MatrixElementTypes.Int8 },
                new {Type = typeof(Matrix<short>),          ElementType = MatrixElementTypes.Int16 },
                new {Type = typeof(Matrix<int>),            ElementType = MatrixElementTypes.Int32 },
                new {Type = typeof(Matrix<long>),           ElementType = MatrixElementTypes.Int64 },
                new {Type = typeof(Matrix<float>),          ElementType = MatrixElementTypes.Float },
                new {Type = typeof(Matrix<double>),         ElementType = MatrixElementTypes.Double },
                new {Type = typeof(Matrix<RgbPixel>),       ElementType = MatrixElementTypes.RgbPixel },
                new {Type = typeof(Matrix<BgrPixel>),       ElementType = MatrixElementTypes.BgrPixel },
                new {Type = typeof(Matrix<RgbAlphaPixel>),  ElementType = MatrixElementTypes.RgbAlphaPixel },
                new {Type = typeof(Matrix<HsiPixel>),       ElementType = MatrixElementTypes.HsiPixel },
                new {Type = typeof(Matrix<LabPixel>),       ElementType = MatrixElementTypes.LabPixel }
            };

            foreach (var type in types)
                Types.Add(type.Type, type.ElementType);
        }

        #endregion

    }

}

#endif
