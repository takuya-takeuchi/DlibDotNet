using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType assignment_cost(MatrixElementType elementType, IntPtr cost, IntPtr assignments, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType max_cost_assignment(MatrixElementType elementType, IntPtr cost, IntPtr assignments);

    }

}