using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class MatrixBase : TwoDimensionObjectBase
    {

        #region Fields

        private static readonly Dictionary<Type, MatrixElementTypes> SupportTypes = new Dictionary<Type, MatrixElementTypes>();

        internal static readonly IDictionary<NativeMethods.MatrixElementType, int> ElementSizeDictionary;

        #endregion

        #region Constructors

        static MatrixBase()
        {
            var types = new[]
            {
                new {Type = typeof(byte),           ElementType = MatrixElementTypes.UInt8 },
                new {Type = typeof(ushort),         ElementType = MatrixElementTypes.UInt16 },
                new {Type = typeof(uint),           ElementType = MatrixElementTypes.UInt32 },
                new {Type = typeof(ulong),          ElementType = MatrixElementTypes.UInt64 },
                new {Type = typeof(sbyte),          ElementType = MatrixElementTypes.Int8 },
                new {Type = typeof(short),          ElementType = MatrixElementTypes.Int16 },
                new {Type = typeof(int),            ElementType = MatrixElementTypes.Int32 },
                new {Type = typeof(long),           ElementType = MatrixElementTypes.Int64 },
                new {Type = typeof(float),          ElementType = MatrixElementTypes.Float },
                new {Type = typeof(double),         ElementType = MatrixElementTypes.Double },
                new {Type = typeof(RgbPixel),       ElementType = MatrixElementTypes.RgbPixel },
                new {Type = typeof(BgrPixel),       ElementType = MatrixElementTypes.BgrPixel },
                new {Type = typeof(RgbAlphaPixel),  ElementType = MatrixElementTypes.RgbAlphaPixel },
                new {Type = typeof(HsiPixel),       ElementType = MatrixElementTypes.HsiPixel },
                new {Type = typeof(LabPixel),       ElementType = MatrixElementTypes.LabPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);

            ElementSizeDictionary = new Dictionary<NativeMethods.MatrixElementType, int>();
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.UInt8, sizeof(byte));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.UInt16, sizeof(ushort));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.UInt32, sizeof(uint));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.UInt64, sizeof(ulong));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Int8, sizeof(sbyte));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Int16, sizeof(short));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Int32, sizeof(int));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Int64, sizeof(long));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Float, sizeof(float));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.Double, sizeof(double));
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.RgbPixel, Marshal.SizeOf<RgbPixel>());
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.BgrPixel, Marshal.SizeOf<BgrPixel>());
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.RgbAlphaPixel, Marshal.SizeOf<RgbAlphaPixel>());
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.HsiPixel, Marshal.SizeOf<HsiPixel>());
            ElementSizeDictionary.Add(NativeMethods.MatrixElementType.LabPixel, Marshal.SizeOf<LabPixel>());
        }

        protected MatrixBase(int templateRows = 0, int templateColumns = 0, bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
            this.TemplateRows = templateRows;
            this.TemplateColumns = templateColumns;
        }

        #endregion

        #region Properties

        public abstract MatrixElementTypes MatrixElementType
        {
            get;
        }

        internal int TemplateColumns
        {
            get;
        }

        internal int TemplateRows
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
