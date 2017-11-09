using System.Collections.Generic;

namespace DlibDotNet.Extensions
{

    internal static class EnumerableExtensions
    {

        internal static void ThrowIfDisposed<T>(this IEnumerable<T> items)
            where T : DlibObject
        {
            foreach (var item in items)
                item.ThrowIfDisposed();
        }

    }

}
