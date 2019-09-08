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

        public static bool Contains(int id)
        {
            return NativeMethods.LossMetricRegistry_contains(id);
        }

        #endregion

    }

}