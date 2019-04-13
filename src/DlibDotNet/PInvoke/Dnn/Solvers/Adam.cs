using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr adam_new(float weight_decay, float momentum1, float momentum2);

        [DllImport(NativeDnnLibrary, CallingConvention = CallingConvention)]
        public static extern void adam_delete(IntPtr adam);

    }

}