// ReSharper disable once CheckNamespace

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class MatrixBase : TwoDimentionObjectBase
    {

        #region Fields

        private static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors

        static MatrixBase()
        {
            var types = new[]
            {
                new {Type = typeof(byte),           ElementType = MatrixElementTypes.UInt8 },
                new {Type = typeof(ushort),         ElementType = MatrixElementTypes.UInt16 },
                new {Type = typeof(uint),           ElementType = MatrixElementTypes.UInt32 },
                new {Type = typeof(sbyte),          ElementType = MatrixElementTypes.Int8 },
                new {Type = typeof(short),          ElementType = MatrixElementTypes.Int16 },
                new {Type = typeof(int),            ElementType = MatrixElementTypes.Int32 },
                new {Type = typeof(float),          ElementType = MatrixElementTypes.Float },
                new {Type = typeof(double),         ElementType = MatrixElementTypes.Double },
                new {Type = typeof(RgbPixel),       ElementType = MatrixElementTypes.RgbPixel },
                new {Type = typeof(RgbAlphaPixel),  ElementType = MatrixElementTypes.RgbAlphaPixel },
                new {Type = typeof(HsiPixel),       ElementType = MatrixElementTypes.HsiPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion

        #region Properties

        public abstract MatrixElementTypes MatrixElementType
        {
            get;
        }

        #endregion

        #region Methods

        internal static bool TryParse(Type type, out MatrixElementTypes result)
        {
            return SupportTypes.TryGetValue(type, out result);
        }

        #endregion

    }

}