using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType nearest_center(MatrixElementType type,
                                                      int templateRows,
                                                      int templateColumns,
                                                      IntPtr centers,
                                                      IntPtr sample,
                                                      out uint ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType find_clusters_using_angular_kmeans(MatrixElementType type,
                                                                          int templateRows,
                                                                          int templateColumns,
                                                                          IntPtr centers,
                                                                          IntPtr samples,
                                                                          uint max_iter,
                                                                          IntPtr result);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType pick_initial_centers(MatrixElementType elementType,
                                                            int templateRows,
                                                            int templateColumns,
                                                            long num_centers,
                                                            IntPtr centers,
                                                            IntPtr samples,
                                                            IntPtr k,
                                                            double percentile);

    }

}