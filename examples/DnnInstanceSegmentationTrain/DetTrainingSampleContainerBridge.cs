using System;
using DlibDotNet;

namespace DnnInstanceSegmentationTrain
{

    internal sealed class DetTrainingSampleContainerBridge : ContainerBridge<DetTrainingSample>
    {

        public override DetTrainingSample Create(IntPtr ptr, IParameter parameter = null)
        {
            return new DetTrainingSample(ptr);
        }

        public override IntPtr GetPtr(DetTrainingSample item)
        {
            return item.NativePtr;
        }

    }

}