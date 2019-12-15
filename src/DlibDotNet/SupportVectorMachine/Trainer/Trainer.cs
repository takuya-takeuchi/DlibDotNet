// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public abstract class Trainer<TScalar> : DlibObject
        where TScalar : struct
    {

        protected Trainer(bool isEnabledDispose = true)
            : base(isEnabledDispose)
        {
        }

    }

}