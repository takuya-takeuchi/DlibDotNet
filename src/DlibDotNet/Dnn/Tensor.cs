using System;
using System.Runtime.InteropServices;

namespace DlibDotNet.Dnn
{

    public class Tensor : DlibObject
    {

        #region Constructors

        internal Tensor(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                throw new ArgumentException("Can not pass IntPtr.Zero", nameof(ptr));

            this.NativePtr = ptr;
        }

        #endregion

        #region Properties

        public int K
        {
            get
            {
                return Native.tensor_k(this.NativePtr);
            }
        }

        #endregion

        internal sealed class Native
        {

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern int tensor_k(IntPtr tensor);

            [DllImport(NativeMethods.NativeDnnLibrary, CallingConvention = NativeMethods.CallingConvention)]
            public static extern IntPtr image_plane(IntPtr tensor, int sample, int k);

        }

    }

}
