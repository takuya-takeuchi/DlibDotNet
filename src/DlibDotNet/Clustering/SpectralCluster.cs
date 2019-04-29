using System;
using System.Collections.Generic;
using System.Linq;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public static partial class Dlib
    {

        #region Methods

        public static IEnumerable<uint> SpectralCluster<TScalar, TKernel>(TKernel kernel, IEnumerable<Matrix<TScalar>> samples, uint clusters)
            where TScalar : struct
            where TKernel : KernelBase
        {
            if (kernel == null)
                throw new ArgumentNullException(nameof(kernel));
            if (samples == null)
                throw new ArgumentNullException(nameof(samples));

            if (!(clusters > 0))
                throw new ArgumentOutOfRangeException();

            kernel.ThrowIfDisposed();
            var samplesArray = samples.ToArray();
            samplesArray.ThrowIfDisposed();

            var templateRow = kernel.TemplateRows;
            var templateColumn = kernel.TemplateColumns;
            foreach (var sample in samplesArray)
            {
                if (sample == null)
                    throw new ArgumentException($"{nameof(samples)} contains null object", nameof(samples));
                if (sample.IsDisposed)
                    throw new ArgumentException($"{nameof(samples)} contains disposed object", nameof(samples));
                if (sample.TemplateRows != templateRow)
                    throw new ArgumentException($"{nameof(samples)} contains different {nameof(sample.TemplateRows)} of {typeof(Matrix<TScalar>).Name}", nameof(samples));
                if (sample.TemplateColumns != templateColumn)
                    throw new ArgumentException($"{nameof(samples)} contains different {nameof(sample.TemplateColumns)} of {typeof(Matrix<TScalar>).Name}", nameof(samples));
            }

            using (var samplesVector = new StdVector<Matrix<TScalar>>(samplesArray))
            {
                var err = NativeMethods.spectral_cluster(kernel.KernelType.ToNativeKernelType(),
                                                         kernel.SampleType.ToNativeMatrixElementType(),
                                                         kernel.TemplateRows,
                                                         kernel.TemplateColumns,
                                                         kernel.NativePtr,
                                                         samplesVector.NativePtr,
                                                         clusters,
                                                         out var ret);
                using (var vector = new StdVector<uint>(ret))
                    return vector.ToArray();
            }
        }

        #endregion

    }

}