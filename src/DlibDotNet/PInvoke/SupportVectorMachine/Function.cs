using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region decision_function

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void decision_function_delete(SvmKernelType kernelType,
                                                           MatrixElementType type,
                                                           int templateRows,
                                                           int templateColumns,
                                                           IntPtr function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_decision_function(SvmKernelType kernelType,
                                                                   MatrixElementType type,
                                                                   int templateRows,
                                                                   int templateColumns,
                                                                   IntPtr function, 
                                                                   byte[] fileName,
                                                                   int fileNameLength,
                                                                   out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_decision_function(byte[] fileName,
                                                                     int fileNameLength,
                                                                     SvmKernelType kernelType,
                                                                     MatrixElementType matrixElementType,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     out IntPtr function, 
                                                                     out IntPtr errorMessage);

        #region double
        
        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType decision_function_operator_double(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         int templateRows,
                                                                         int templateColumns,
                                                                         IntPtr function,
                                                                         IntPtr sample,
                                                                         out double ret);

        #endregion

        #region float

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType decision_function_operator_float(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        int templateRows,
                                                                        int templateColumns,
                                                                        IntPtr function,
                                                                        IntPtr sample,
                                                                        out float ret);

        #endregion

        #endregion

        #region probabilistic_decision_function

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void probabilistic_decision_function_delete(SvmKernelType kernelType,
                                                                         MatrixElementType type,
                                                                         int templateRows,
                                                                         int templateColumns,
                                                                         IntPtr function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_probabilistic_decision_function(SvmKernelType kernelType,
                                                                                 MatrixElementType type,
                                                                                 int templateRows,
                                                                                 int templateColumns,
                                                                                 IntPtr function,
                                                                                 byte[] fileName,
                                                                                 int fileNameLength,
                                                                                 out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_probabilistic_decision_function(byte[] fileName,
                                                                                   int fileNameLength,
                                                                                   SvmKernelType kernelType,
                                                                                   MatrixElementType matrixElementType,
                                                                                   int templateRows,
                                                                                   int templateColumns,
                                                                                   out IntPtr function,
                                                                                   out IntPtr errorMessage);

        #endregion

        #region projection_function

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void projection_function_delete(SvmKernelType kernelType,
                                                             MatrixElementType type,
                                                             int templateRows,
                                                             int templateColumns,
                                                             IntPtr function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType serialize_projection_function(SvmKernelType kernelType,
                                                                     MatrixElementType type,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     IntPtr function,
                                                                     byte[] fileName,
                                                                     int fileNameLength,
                                                                     out IntPtr errorMessage);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType deserialize_projection_function(byte[] fileName,
                                                                       int fileNameLength,
                                                                       SvmKernelType kernelType,
                                                                       MatrixElementType matrixElementType,
                                                                       int templateRows,
                                                                       int templateColumns,
                                                                       out IntPtr function,
                                                                       out IntPtr errorMessage);

        #endregion

        #region normalized_function

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr normalized_function_new(SvmKernelType kernelType,
                                                            MatrixElementType type,
                                                            int templateRows,
                                                            int templateColumns,
                                                            SvmFunctionType functionType);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void normalized_function_delete(SvmKernelType kernelType,
                                                             MatrixElementType type,
                                                             int templateRows,
                                                             int templateColumns,
                                                             SvmFunctionType functionType,
                                                             IntPtr function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_get_normalizer(SvmKernelType kernelType,
                                                                          MatrixElementType type,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          SvmFunctionType function_type,
                                                                          IntPtr function,
                                                                          out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_set_normalizer(SvmKernelType kernelType,
                                                                          MatrixElementType type,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          SvmFunctionType function_type,
                                                                          IntPtr function,
                                                                          NormalizerType normalizer_type,
                                                                          IntPtr normalizer);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_get_function(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        int templateRows,
                                                                        int templateColumns,
                                                                        SvmFunctionType function_type,
                                                                        IntPtr function,
                                                                        out IntPtr ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_set_function(SvmKernelType kernelType,
                                                                        MatrixElementType type,
                                                                        int templateRows,
                                                                        int templateColumns,
                                                                        SvmFunctionType function_type,
                                                                        IntPtr function,
                                                                        IntPtr sub_function);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_deserialize(SvmKernelType kernel_type,
                                                                       MatrixElementType type,
                                                                       int templateRows,
                                                                       int templateColumns,
                                                                       SvmFunctionType function_type,
                                                                       byte[] file_name,
                                                                       int file_name_length,
                                                                       out IntPtr ret,
                                                                       out IntPtr error_message);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_serialize(SvmKernelType kernel_type,
                                                                     MatrixElementType type,
                                                                     int templateRows,
                                                                     int templateColumns,
                                                                     SvmFunctionType function_type,
                                                                     IntPtr function,
                                                                     byte[] file_name,
                                                                     int file_name_length,
                                                                     out IntPtr error_message);

        #region operator

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_int8_t(SvmKernelType kernel_type,
                                                                           MatrixElementType type,
                                                                           int templateRows,
                                                                           int templateColumns,
                                                                           SvmFunctionType function_type,
                                                                           IntPtr function,
                                                                           IntPtr sample,
                                                                           out sbyte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_uint8_t(SvmKernelType kernel_type,
                                                                            MatrixElementType type,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            SvmFunctionType function_type,
                                                                            IntPtr function,
                                                                            IntPtr sample,
                                                                            out byte ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_int16_t(SvmKernelType kernel_type,
                                                                            MatrixElementType type,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            SvmFunctionType function_type,
                                                                            IntPtr function,
                                                                            IntPtr sample,
                                                                            out short ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_uint16_t(SvmKernelType kernel_type,
                                                                             MatrixElementType type,
                                                                             int templateRows,
                                                                             int templateColumns,
                                                                             SvmFunctionType function_type,
                                                                             IntPtr function,
                                                                             IntPtr sample,
                                                                             out ushort ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_int32_t(SvmKernelType kernel_type,
                                                                            MatrixElementType type,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            SvmFunctionType function_type,
                                                                            IntPtr function,
                                                                            IntPtr sample,
                                                                            out int ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_uint32_t(SvmKernelType kernel_type,
                                                                             MatrixElementType type,
                                                                             int templateRows,
                                                                             int templateColumns,
                                                                             SvmFunctionType function_type,
                                                                             IntPtr function,
                                                                             IntPtr sample,
                                                                             out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_int64_t(SvmKernelType kernel_type,
                                                                            MatrixElementType type,
                                                                            int templateRows,
                                                                            int templateColumns,
                                                                            SvmFunctionType function_type,
                                                                            IntPtr function,
                                                                            IntPtr sample,
                                                                            out long ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_uint64_t(SvmKernelType kernel_type,
                                                                             MatrixElementType type,
                                                                             int templateRows,
                                                                             int templateColumns,
                                                                             SvmFunctionType function_type,
                                                                             IntPtr function,
                                                                             IntPtr sample,
                                                                             out ulong ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_double(SvmKernelType kernel_type,
                                                                           MatrixElementType type,
                                                                           int templateRows,
                                                                           int templateColumns,
                                                                           SvmFunctionType function_type,
                                                                           IntPtr function,
                                                                           IntPtr sample,
                                                                           out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType normalized_function_operator_float(SvmKernelType kernel_type,
                                                                          MatrixElementType type,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          SvmFunctionType function_type,
                                                                          IntPtr function,
                                                                          IntPtr sample,
                                                                          out float ret);

        #endregion

        #endregion

    }

}