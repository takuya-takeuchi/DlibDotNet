using System;

namespace DlibDotNet
{

    public interface IDlibObject : IDisposable
    {

        #region Properties

        IntPtr NativePtr
        {
            get;
        }

        #endregion

    }

}