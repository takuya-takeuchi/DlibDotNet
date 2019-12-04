using DlibDotNet;

namespace DnnInstanceSegmentationTrain
{

    public sealed class TruthInstance
    {

        public RgbPixel RgbLabel
        {
            get;
            set;
        }

        public MModRect MmodRect
        {
            get;
            set;
        }

    }

}