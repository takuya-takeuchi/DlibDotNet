#if !LITE
using System;

namespace DlibDotNet.Dnn
{

    public sealed class Adam : Solver
    {

        #region Constructors

        public Adam()
            : this(0.0005f, 0.9f, 0.999f)
        {
        }

        public Adam(float weightDecay, float momentum1, float momentum2)
        {
            this.NativePtr = NativeMethods.adam_new(weightDecay, momentum1, momentum2);
        }

        #endregion

        #region Properties

        public override int SolverType
        {
            get
            {
                return (int)NativeMethods.OptimizerType.Adam;
            }
        }

        #endregion

        #region Methods

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.adam_delete(this.NativePtr);
        }

        #endregion

        #endregion

    }

}
#endif
