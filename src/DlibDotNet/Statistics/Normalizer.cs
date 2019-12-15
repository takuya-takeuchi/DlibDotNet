// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Normalizer : DlibObject
    {

        #region Constructors

        protected Normalizer(NormalizerType normalizerType, bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
            this.NormalizerType = normalizerType;
        }

        #endregion

        #region Properties

        public NormalizerType NormalizerType
        {
            get;
        }

        #endregion

    }

}