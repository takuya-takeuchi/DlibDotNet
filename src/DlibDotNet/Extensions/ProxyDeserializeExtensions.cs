using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DlibDotNet.Dnn;

namespace DlibDotNet.Extensions
{

    public static class ProxyDeserializeExtensions
    {

        public static void Deserialize(this IDictionary<string, LossMulticlassLogPerPixel> maps,
                                            ProxyDeserialize deserialize,
                                            int networkType = 0)
        {
            if (deserialize == null)
                throw new ArgumentNullException(nameof(deserialize));

            deserialize.ThrowIfDisposed();

            var keys = IntPtr.Zero;
            var values = IntPtr.Zero;

            try
            {
                var error = NativeMethods.LossMulticlassLogPerPixel_deserialize_proxy_map(networkType,
                                                                                          deserialize.NativePtr,
                                                                                          out keys,
                                                                                          out values,
                                                                                          out var size,
                                                                                          out var errorMessage);
                Dnn.Cuda.ThrowCudaException(error);
                switch (error)
                {
                    case NativeMethods.ErrorType.DnnNotSupportNetworkType:
                        throw new NotSupportNetworkTypeException(networkType);
                    case NativeMethods.ErrorType.GeneralSerialization:
                        throw new SerializationException(StringHelper.FromStdString(errorMessage, true));
                }

                for (var i = 0; i < size; i++)
                {
                    var key = IntPtr.Add(keys, IntPtr.Size * i);
                    var value = IntPtr.Add(values, IntPtr.Size * i);
                    key = Marshal.ReadIntPtr(key);
                    value = Marshal.ReadIntPtr(value);
                    using (var stdString = new StdString(key))
                    {
                        var str = stdString.ToString(); 
                        var net = new LossMulticlassLogPerPixel(value, networkType);
                        maps.Add(str, net);
                    }
                }
            }
            finally
            {
                if (keys != IntPtr.Zero)
                    NativeMethods.stdlib_free(keys);
                if (values != IntPtr.Zero)
                    NativeMethods.stdlib_free(values);
            }
        }

    }

}
