using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal static class NumericKernelTypesRepository
    {

        #region Fields

        public static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        #endregion

        #region Constructors
        
        static NumericKernelTypesRepository()
        {
            var types = new[]
            {
                new { Type = typeof(sbyte),         ElementType = MatrixElementTypes.Int8   },
                new { Type = typeof(short),         ElementType = MatrixElementTypes.Int16  },
                new { Type = typeof(int),           ElementType = MatrixElementTypes.Int32  },
                new { Type = typeof(byte),          ElementType = MatrixElementTypes.UInt8  },
                new { Type = typeof(ushort),        ElementType = MatrixElementTypes.UInt16 },
                new { Type = typeof(uint),          ElementType = MatrixElementTypes.UInt32 },
                new { Type = typeof(float),         ElementType = MatrixElementTypes.Float  },
                new { Type = typeof(double),        ElementType = MatrixElementTypes.Double }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion

    }

}
