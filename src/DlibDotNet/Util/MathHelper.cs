#if !LITE
using System;

namespace DlibDotNet.Util
{

    internal static class MathHelper
    {

        #region Methods

        public static double ConvertToRadian(double angle)
        {
            return angle * Math.PI / 180d;
        }

        #region Helpers
        #endregion

        #endregion

    }

}

#endif
