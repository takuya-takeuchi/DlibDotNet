#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct GenericElement
        {

            public IntPtr Ptr;

        }

        #region generic

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr pipe_generic_new(ulong mas_size);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void pipe_generic_delete(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool pipe_generic_dequeue(IntPtr q, out IntPtr e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void pipe_generic_disable(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void pipe_generic_enqueue(IntPtr q, IntPtr e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool pipe_generic_is_enabled(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void pipe_generic_wait_until_empty(IntPtr q);

        #endregion

    }

}
#endif
