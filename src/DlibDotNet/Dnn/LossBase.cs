using System;

namespace DlibDotNet.Dnn
{

    public static partial class Dlib
    {

        #region Methods

        public static int GetBuilderId(IntPtr builder)
        {
            return NativeMethods.LossBase_get_id(builder);
        }

        #endregion

    }

}