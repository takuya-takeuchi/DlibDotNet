using System.Collections.Generic;

namespace DlibDotNet
{

    public interface IUndisposableElementCollection<T> : IEnumerable<T>, IDlibObject
    {

        int Count
        {
            get;
        }

        T this[int index]
        {
            get;
        }

        T this[uint index]
        {
            get;
        }

    }

}