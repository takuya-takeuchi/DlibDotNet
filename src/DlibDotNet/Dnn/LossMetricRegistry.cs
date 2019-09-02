using System;

namespace DlibDotNet.Dnn
{

    public static class LossMetricRegistry
    {

        #region Methods

        public static bool Add(IntPtr builder)
        {
            return NativeMethods.LossMetricRegistry_add(builder);
        }

        public static void Remove(IntPtr builder)
        {
            NativeMethods.LossMetricRegistry_remove(builder);
        }

        #endregion

    }

}