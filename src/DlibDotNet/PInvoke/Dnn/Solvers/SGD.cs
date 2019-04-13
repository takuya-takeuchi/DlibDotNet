using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr sgd_new(float weight_decay, float momentum);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void sgd_delete(IntPtr adam);

    }

}