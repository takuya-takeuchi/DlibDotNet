using System;
using System.Collections;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class EnumerableDisposer<T> : IDisposable{

        #region Fields

        private readonly bool _DisposeElement;

        #endregion

        #region Constructors

        public EnumerableDisposer(IEnumerable<T> collection, bool disposeElement = false)
        {
            this.Collection = collection;
            this._DisposeElement = disposeElement;
        }

        #endregion

        #region Finalizer

        /// <summary>
        /// Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.
        /// </summary>
        ~EnumerableDisposer()
        {
            this.Dispose(false);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance has been disposed.
        /// </summary>
        /// <returns>true if this instance has been disposed; otherwise, false.</returns>
        public bool IsDisposed
        {
            get;
            private set;
        }

        public IEnumerable<T> Collection
        {
            get;
        }

        #endregion

        #region Methods

        #region Helpers

        private static void RecursiveDispose(IEnumerable elements, bool disposeElement)
        {
            foreach (var element in elements)
            {
                if (element is IEnumerable<IDisposable> tmp)
                    RecursiveDispose(tmp, disposeElement);
                else
                    if (disposeElement && element is IDisposable disposable)
                        disposable.Dispose();
            }
        }

        #endregion

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases all resources used by this <see cref="EnumerableDisposer{T}"/>.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by this <see cref="EnumerableDisposer{T}"/>.
        /// </summary>
        /// <param name="disposing">Indicate value whether <see cref="IDisposable.Dispose"/> method was called.</param>
        private void Dispose(bool disposing)
        {
            if (this.IsDisposed)
            {
                return;
            }

            this.IsDisposed = true;

            if (disposing)
            {
                RecursiveDispose(this.Collection, this._DisposeElement);
            }
        }

        #endregion

    }

}
