using System.Collections.Generic;

namespace DlibDotNet.Extensions
{

    public static class EnumerableExtensions
    {

        internal static void ThrowIfDisposed<T>(this IEnumerable<T> items)
            where T : DlibObject
        {
            foreach (var item in items)
                item.ThrowIfDisposed();
        }

        internal static void ThrowIfDisposed<T>(this IEnumerable<IEnumerable<T>> items)
            where T : DlibObject
        {
            foreach (var elements in items)
                foreach (var item in elements)
                    item.ThrowIfDisposed();
        }

        public static void DisposeElement<T>(this IEnumerable<T> items)
            where T : DlibObject
        {
            foreach (var item in items)
                item.Dispose();
        }

        public static void DisposeElement<T>(this IEnumerable<IEnumerable<T>> items)
            where T : DlibObject
        {
            foreach (var elements in items)
                foreach (var item in elements)
                    item.Dispose();
        }

    }

}
