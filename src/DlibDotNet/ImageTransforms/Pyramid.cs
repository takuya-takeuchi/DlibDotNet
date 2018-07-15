using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Pyramid
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

        internal static bool TryGetSupportPyramidType<T>(out Dlib.Native.PyramidType type)
        {
            return SupportPyramidType.TryGetValue(typeof(T), out type);
        }

        #region Helpers
        #endregion

        #endregion

    }

}
