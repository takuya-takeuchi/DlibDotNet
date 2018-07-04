using System;
using System.Collections.Generic;
using System.Text;

namespace DlibDotNet
{
    public abstract class DNNFaceDetectorBase : DlibObject
    {
        #region Constructors

        protected DNNFaceDetectorBase(IntPtr ptr)
        {
            this.NativePtr = ptr;
        }

        #endregion

        #region Methods

        public abstract MModRect[] Detect(Array2DBase image, int upsampleNumTimes = 0);

        #region Overrides

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
        }

        #endregion

        #endregion
    }
}
