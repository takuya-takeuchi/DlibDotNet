#if !LITE
using System;

namespace DlibDotNet.Dnn
{

    public static class LossMmodRegistry
    {

        #region Methods

        public static bool Add(IntPtr builder)
        {
            return NativeMethods.LossMmodRegistry_add(builder);
        }

        public static void Remove(IntPtr builder)
        {
            NativeMethods.LossMmodRegistry_remove(builder);
        }

        public static bool Contains(int id)
        {
            return NativeMethods.LossMmodRegistry_contains(id);
        }

        public static int GetId(IntPtr builder)
        {
            return NativeMethods.LossBase_get_id(builder);
        }

        #endregion

    }

}
#endif
