using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region int32_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr queue_sort_1b_c_int32_t_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_delete(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_clear(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_reset(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_sort(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool queue_sort_1b_c_int32_t_move_next(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int queue_sort_1b_c_int32_t_size(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_enqueue(IntPtr q, int e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_int32_t_dequeue(IntPtr q, out int e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int queue_sort_1b_c_int32_t_element(IntPtr q);

        #endregion

        #region uint32_t

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr queue_sort_1b_c_uint32_t_new();

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_delete(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_clear(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_reset(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_sort(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool queue_sort_1b_c_uint32_t_move_next(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern int queue_sort_1b_c_uint32_t_size(IntPtr q);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_enqueue(IntPtr q, uint e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void queue_sort_1b_c_uint32_t_dequeue(IntPtr q, out uint e);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern uint queue_sort_1b_c_uint32_t_element(IntPtr q);

        #endregion

    }

}