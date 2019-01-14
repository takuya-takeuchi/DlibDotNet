using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        [DllImport(NativeMethods.NativeLibrary, CallingConvention = NativeMethods.CallingConvention)]
        public static extern ErrorType find_candidate_object_locations(Array2DType type,
                                                                       IntPtr img,
                                                                       IntPtr rect,
                                                                       MatrixElementType matrixElementType,
                                                                       IntPtr kvals,
                                                                       uint minSize,
                                                                       uint maxMergingIterations);

    }

}