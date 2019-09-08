using System;

namespace DlibDotNet.Dnn
{

    public static class LossMulticlassLogRegistry
    {

        #region Methods

        public static bool Add(IntPtr builder)
        {
            return NativeMethods.LossMulticlassLogRegistry_add(builder);
        }

        public static void Remove(IntPtr builder)
        {
            NativeMethods.LossMulticlassLogRegistry_remove(builder);
        }

        public static bool Contains(int id)
        {
            return NativeMethods.LossMulticlassLogRegistry_contains(id);
        }

        #endregion

    }

}