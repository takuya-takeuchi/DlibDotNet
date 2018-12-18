﻿// ReSharper disable once CheckNamespace

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

        internal static readonly IDictionary<Dlib.Native.MatrixElementType, int> ElementSizeDictionary;

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
                new {Type = typeof(RgbAlphaPixel),  ElementType = MatrixElementTypes.RgbAlphaPixel },
                new {Type = typeof(HsiPixel),       ElementType = MatrixElementTypes.HsiPixel }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);

            ElementSizeDictionary = new Dictionary<Dlib.Native.MatrixElementType, int>();
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt8, sizeof(byte));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt16, sizeof(ushort));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt32, sizeof(uint));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.UInt64, sizeof(ulong));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int8, sizeof(sbyte));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int16, sizeof(short));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int32, sizeof(int));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Int64, sizeof(long));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Float, sizeof(float));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.Double, sizeof(double));
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.RgbPixel, Marshal.SizeOf<RgbPixel>());
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.RgbAlphaPixel, Marshal.SizeOf<RgbAlphaPixel>());
            ElementSizeDictionary.Add(Dlib.Native.MatrixElementType.HsiPixel, Marshal.SizeOf<HsiPixel>());
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