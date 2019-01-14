using System;
using System.Collections.Generic;
using PyramidType = DlibDotNet.NativeMethods.PyramidType;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Pyramid : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, PyramidType> SupportPyramidType = new Dictionary<Type, PyramidType>();

        #endregion

        #region Constructors

        static Pyramid()
        {
            SupportPyramidType.Add(typeof(PyramidDown), PyramidType.Down);
        }

        #endregion

        #region Methods

        public abstract Rectangle RectDown(Rectangle rect);

        public abstract Rectangle RectDown(Rectangle rect, uint levels);

        public abstract DRectangle RectDown(DRectangle rect);

        public abstract DRectangle RectDown(DRectangle rect, uint levels);

        public abstract Rectangle RectUp(Rectangle rect);

        public abstract Rectangle RectUp(Rectangle rect, uint levels);

        public abstract DRectangle RectUp(DRectangle rect);

        public abstract DRectangle RectUp(DRectangle rect, uint levels);

        internal static bool TryGetSupportPyramidType<T>(out PyramidType type)
        {
            return SupportPyramidType.TryGetValue(typeof(T), out type);
        }

        #endregion

    }

}
