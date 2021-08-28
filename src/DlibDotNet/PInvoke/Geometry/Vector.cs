#if !LITE
using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_new(VectorElementType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_delete(VectorElementType type, IntPtr vector);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType vector_operator_left_shift(VectorElementType type, IntPtr vector, IntPtr ofstream);

        #region vector_get_xyz

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_uint8_t(IntPtr vector, out byte x, out byte y, out byte z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_uint16_t(IntPtr vector, out ushort x, out ushort y, out ushort z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_uint32_t(IntPtr vector, out uint x, out uint y, out uint z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_int8_t(IntPtr vector, out sbyte x, out sbyte y, out sbyte z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_int16_t(IntPtr vector, out short x, out short y, out short z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_int32_t(IntPtr vector, out int x, out int y, out int z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_float(IntPtr vector, out float x, out float y, out float z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_get_xyz_double(IntPtr vector, out double x, out double y, out double z);

        #endregion

        #region vector_set_xyz

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_uint8_t(IntPtr vector, byte x, byte y, byte z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_uint16_t(IntPtr vector, ushort x, ushort y, ushort z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_uint32_t(IntPtr vector, uint x, uint y, uint z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_int8_t(IntPtr vector, sbyte x, sbyte y, sbyte z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_int16_t(IntPtr vector, short x, short y, short z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_int32_t(IntPtr vector, int x, int y, int z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_float(IntPtr vector, float x, float y, float z);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void vector_set_xyz_double(IntPtr vector, double x, double y, double z);

        #endregion

        #region vector_operator_add

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_uint8_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_uint16_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_uint32_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_int8_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_int16_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_int32_t(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_float(IntPtr left, IntPtr right, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_add_double(IntPtr left, IntPtr right, out IntPtr ret);

        #endregion

        #region vector_operator_div

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_uint8_t(IntPtr vector, byte value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_uint16_t(IntPtr vector, ushort value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_uint32_t(IntPtr vector, uint value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_int8_t(IntPtr vector, sbyte value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_int16_t(IntPtr vector, short value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_int32_t(IntPtr vector, int value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_float(IntPtr vector, float value, out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr vector_operator_div_double(IntPtr vector, double value, out IntPtr ret);

        #endregion

    }

}
#endif
