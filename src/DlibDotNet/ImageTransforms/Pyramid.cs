using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Pyramid : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, NativeMethods.PyramidType> SupportPyramidType = new Dictionary<Type, NativeMethods.PyramidType>();

        #endregion

        #region Constructors

        static Pyramid()
        {
            SupportPyramidType.Add(typeof(PyramidDown), NativeMethods.PyramidType.Down);
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

        internal static bool TryGetSupportPyramidType<T>(out NativeMethods.PyramidType type)
        {
            return SupportPyramidType.TryGetValue(typeof(T), out type);
        }

        #endregion

    }

}
