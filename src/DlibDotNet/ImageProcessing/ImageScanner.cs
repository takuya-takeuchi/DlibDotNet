// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class ImageScanner : DlibObject
    {

        #region Properties

        public abstract ImageScannerType ScannerType
        {
            get;
        }

        #endregion

        #region Methods

        internal virtual FHogPyramidParameter GetFHogPyramidParameter()
        {
            return null;
        }

        #endregion

        internal sealed class FHogPyramidParameter
        {

            #region Constructors

            public FHogPyramidParameter(Dlib.Native.PyramidType pyramidType, uint pyramidRate, Dlib.Native.FHogFeatureExtractorType featureExtractorType)
            {
                this.PyramidType = pyramidType;
                this.PyramidRate = pyramidRate;
                this.FeatureExtractorType = featureExtractorType;
            }

            #endregion

            #region Properties

            public Dlib.Native.FHogFeatureExtractorType FeatureExtractorType
            {
                get;
            }

            public Dlib.Native.PyramidType PyramidType
            {
                get;
            }

            public uint PyramidRate
            {
                get;
            }

            #endregion

        }

    }

}