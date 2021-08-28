#if !LITE
using System;

namespace DlibDotNet.Util
{

    internal static class GenericHelper
    {

        public static Type GetTypeParameter(Type type)
        {
            var types = type.GenericTypeArguments;
            if (types.Length != 1)
                return null;

            return type.GenericTypeArguments[0];
        }

    }

}

#endif
