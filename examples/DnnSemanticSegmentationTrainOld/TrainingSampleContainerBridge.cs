using System;
using DlibDotNet;
using DnnSemanticSegmentation;

namespace DnnSemanticSegmentationTrain
{

    public sealed class TrainingSampleContainerBridge : ContainerBridge<TrainingSample>
    {

        public override TrainingSample Create(IntPtr ptr, IParameter parameter = null)
        {
            return new TrainingSample(ptr);
        }

        public override IntPtr GetPtr(TrainingSample item)
        {
            return item.NativePtr;
        }

    }

}