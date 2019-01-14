using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out byte ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out ushort ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out uint ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out sbyte ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out short ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out int ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out double ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out float ret);

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType max_cost_assignment(MatrixElementType elementType, IntPtr cost, IntPtr assignments);

    }

}