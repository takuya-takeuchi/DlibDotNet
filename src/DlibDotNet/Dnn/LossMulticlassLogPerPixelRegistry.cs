using System;

namespace DlibDotNet.Dnn
{

    public static class LossMulticlassLogPerPixelRegistry
    {

        #region Methods

        public static bool Add(IntPtr builder)
        {
            return NativeMethods.LossMulticlassLogPerPixelRegistry_add(builder);
        }

        public static void Remove(IntPtr builder)
        {
            NativeMethods.LossMulticlassLogPerPixelRegistry_remove(builder);
        }

        public static bool Contains(int id)
        {
            return NativeMethods.LossMulticlassLogPerPixelRegistry_contains(id);
        }

        #endregion

    }

}