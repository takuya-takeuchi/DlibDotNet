using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Pyramid : DlibObject
    {

        #region Fields

        private static readonly Dictionary<Type, Dlib.Native.PyramidType> SupportPyramidType = new Dictionary<Type, Dlib.Native.PyramidType>();

        #endregion

        #region Constructors

        static Pyramid()
        {
            SupportPyramidType.Add(typeof(PyramidDown), Dlib.Native.PyramidType.Down);
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

        internal static bool TryGetSupportPyramidType<T>(out Dlib.Native.PyramidType type)
        {
            return SupportPyramidType.TryGetValue(typeof(T), out type);
        }

        #endregion

    }

}
