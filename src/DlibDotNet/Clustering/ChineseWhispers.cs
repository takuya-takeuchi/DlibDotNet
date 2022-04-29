using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static void ChineseWhispers(IEnumerable<SamplePair> edges, uint iterations, out uint clusters, out uint[] labels)
        {
            if (edges == null)
                throw new ArgumentNullException(nameof(edges));

            edges.ThrowIfDisposed();

            using (var e = new StdVector<SamplePair>(edges))
            using (var l = new StdVector<uint>())
            {
                clusters = (uint)NativeMethods.clustering_chinese_whispers(e.NativePtr, l.NativePtr, iterations);
                labels = l.ToArray();
            }
        }

        #endregion

    }

}