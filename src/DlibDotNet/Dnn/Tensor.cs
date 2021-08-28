#if !LITE
using System;

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
                return NativeMethods.tensor_k(this.NativePtr);
            }
        }

        #endregion

    }

}

#endif
