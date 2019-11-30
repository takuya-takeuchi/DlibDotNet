using System;
using DlibDotNet;

namespace DnnInstanceSegmentationTrain
{

    internal sealed class SegTrainingSampleContainerBridge : ContainerBridge<SegTrainingSample>
    {

        public override SegTrainingSample Create(IntPtr ptr, IParameter parameter = null)
        {
            return new SegTrainingSample(ptr);
        }

        public override IntPtr GetPtr(SegTrainingSample item)
        {
            return item.NativePtr;
        }

    }

}