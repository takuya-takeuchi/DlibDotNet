using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace DlibDotNet
{

    public sealed class ContainerBridgeRepository
    {

        #region Fields

        private static readonly ConcurrentDictionary<Type, IContainerBridge> SupportTypes = new ConcurrentDictionary<Type, IContainerBridge>();

        #endregion

        #region Methods

        public static void Add<T>(ContainerBridge<T> bridge)
        {
            if (bridge == null)
                throw new ArgumentNullException(nameof(bridge));

            var type = typeof(T);
            if (!SupportTypes.ContainsKey(type))
            {
                SupportTypes.AddOrUpdate(type, bridge, (key, value) => value);
            }
            else
            {
                SupportTypes[type] = bridge;
            }
        }

        public static ContainerBridge<T> Get<T>()
        {
            var type = typeof(T);
            if (!SupportTypes.TryGetValue(type, out var bridge))
                throw new NotSupportedException($"{type.FullName} is not supported.");

            return (ContainerBridge<T>)bridge;
        }

        #endregion

    }

}
