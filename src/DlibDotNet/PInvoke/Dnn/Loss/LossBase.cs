using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern int LossBase_get_id(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern MatrixElementType LossBase_get_data_type(IntPtr builder);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern MatrixElementType LossBase_get_label_type();

    }

}