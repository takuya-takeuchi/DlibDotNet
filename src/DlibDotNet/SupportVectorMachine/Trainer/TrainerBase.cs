#if !LITE
// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class TrainerBase : DlibObject
    {

        protected TrainerBase(bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
        }

    }

}
#endif
