#if !LITE
using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Dnn;

namespace DlibDotNet.Extensions
{

    public static class ProxySerializeExtensions
    {

        public static void Serialize(this IDictionary<string, LossMulticlassLogPerPixel> maps,
                                          ProxySerialize serialize,
                                          int networkType = 0)
        {
            if (serialize == null)
                throw new ArgumentNullException(nameof(serialize));

            serialize.ThrowIfDisposed();

            StdString[] keys = null;

            try
            {
                keys = new StdString[maps.Count];
                var values = new IntPtr[maps.Count];

                var index = 0;
                foreach (var kvp in maps)
                {
                    keys[index] = new StdString(kvp.Key);
                    values[index] = kvp.Value.NativePtr;
                    index++;
                }

                var keysArray = keys.Select(s => s.NativePtr).ToArray();
                var error = NativeMethods.LossMulticlassLogPerPixel_serialize_proxy_map(networkType,
                                                                                        serialize.NativePtr,
                                                                                        keysArray,
                                                                                        values,
                                                                                        maps.Count,
                                                                                        out var errorMessage);
                Dnn.Cuda.ThrowCudaException(error);
                switch (error)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(networkType);
                    case NativeMethods.ErrorType.GeneralSerialization:
                        throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
                }
            }
            finally
            {
                keys?.DisposeElement();
            }
        }

    }

}

#endif
