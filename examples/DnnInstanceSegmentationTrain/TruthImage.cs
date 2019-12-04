using System.Collections.Generic;
using DlibDotNet;

namespace DnnInstanceSegmentationTrain
{

    public sealed class TruthImage
    {

        public ImageInfo Info
        {
            get;
            set;
        }

        public List<TruthInstance> TruthInstances
        {
            get;
            set;
        }

    }

}