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

            public FHogPyramidParameter(NativeMethods.PyramidType pyramidType, uint pyramidRate, NativeMethods.FHogFeatureExtractorType featureExtractorType)
            {
                this.PyramidType = pyramidType;
                this.PyramidRate = pyramidRate;
                this.FeatureExtractorType = featureExtractorType;
            }

            #endregion

            #region Properties

            public NativeMethods.FHogFeatureExtractorType FeatureExtractorType
            {
                get;
            }

            public NativeMethods.PyramidType PyramidType
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
