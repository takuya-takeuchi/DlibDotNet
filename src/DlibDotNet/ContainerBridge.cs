#if !LITE
using System;

namespace DlibDotNet
{

    public abstract class ContainerBridge<T> : IContainerBridge
    {

        public abstract T Create(IntPtr ptr, IParameter parameter = null);

        public abstract IntPtr GetPtr(T item);

    }

}
#endif
