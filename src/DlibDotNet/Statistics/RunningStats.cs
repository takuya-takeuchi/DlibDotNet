using System;
using System.Collections.Generic;
using DlibDotNet.Extensions;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    /// <summary>
    /// This class provides methods and properties to compute the running mean, variance, skewness, and excess kurtosis of a stream of real numbers.
    /// </summary>
    /// <typeparam name="TKernel">The type of real numbers.</typeparam>
    public sealed class RunningStats<TKernel> : DlibObject
        where TKernel : struct
    {

        #region Fields

        private readonly Dlib.Native.RunningStatsType _RunningStatsType;

        private readonly RunningStatsType _Type;

        private static readonly Dictionary<Type, RunningStatsType> SupportTypes = new Dictionary<Type, RunningStatsType>();

        private readonly RunningStatsImp<TKernel> _Imp;

        #endregion

        #region Constructors

        static RunningStats()
        {
            var types = new[]
            {
                new { Type = typeof(float),         ElementType = RunningStatsType.Float  },
                new { Type = typeof(double),        ElementType = RunningStatsType.Double }
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RunningStats{TTKernel}"/> class.
        /// </summary>
        public RunningStats()
        {
            if (!SupportTypes.TryGetValue(typeof(TKernel), out var type))
                throw new NotSupportedException($"{typeof(TKernel).Name} does not support");

            this._RunningStatsType = type.ToRunningStatsType();

            this.NativePtr = Dlib.Native.running_stats_new(this._RunningStatsType);
            if (this.NativePtr == IntPtr.Zero)
                throw new ArgumentException($"{type} is not supported.");

            this._Type = type;

            switch (this._Type)
            {
                case RunningStatsType.Float:
                    this._Imp = new RunningStatsFloatImp(this, this._RunningStatsType) as RunningStatsImp<TKernel>;
                    break;
                case RunningStatsType.Double:
                    this._Imp = new RunningStatsDoubleImp(this, this._RunningStatsType) as RunningStatsImp<TKernel>;
                    break;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of real numbers.
        /// </summary>
        public RunningStatsType RunningStatsType => this._Type;

        /// <summary>
        /// Gets the number of points given to this object so far.
        /// </summary>
        public TKernel CurrentN
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.CurrentN;
            }
        }

        /// <summary>
        /// Gets the unbiased sample kurtosis of all the values presented to this object so far.
        /// </summary>
        public TKernel ExcessKurtosis
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.ExcessKurtosis;
            }
        }

        /// <summary>
        /// Gets the largest value presented to this object so far.
        /// </summary>
        public TKernel Max
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Max;
            }
        }

        /// <summary>
        /// Gets the mean of all the values presented to this object so far.
        /// </summary>
        public TKernel Mean
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Mean;
            }
        }

        /// <summary>
        /// Gets the smallest value presented to this object so far.
        /// </summary>
        public TKernel Min
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Min;
            }
        }

        /// <summary>
        /// Gets the unbiased sample skewness of all the values presented to this object so far.
        /// </summary>
        public TKernel Skewness
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Skewness;
            }
        }

        /// <summary>
        /// Gets the unbiased sampled standard deviation of all the values presented to this object so far.
        /// </summary>
        public TKernel StdDev
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.StdDev;
            }
        }

        /// <summary>
        /// Gets the unbiased sample variance of all the values presented to this object so far.
        /// </summary>
        public TKernel Variance
        {
            get
            {
                this.ThrowIfDisposed();
                return this._Imp.Variance;
            }
        }

        #endregion

        #region Methods

        public void Add(TKernel value)
        {
            this.ThrowIfDisposed();
            this._Imp.Add(value);
        }

        /// <summary>
        /// Clears all points given to this object so far.
        /// </summary>
        public void Clear()
        {
            this.ThrowIfDisposed();
            Dlib.Native.running_stats_clear(this._RunningStatsType, this.NativePtr);
        }

        public TKernel Scale(TKernel scale)
        {
            this.ThrowIfDisposed();
            this._Imp.Scale(scale, out var value);
            return value;
        }

        #region Overrides

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            Dlib.Native.running_stats_delete(this._RunningStatsType, this.NativePtr);
        }

        #endregion

        #endregion

        private abstract class RunningStatsImp<T>
            where T : struct
        {

            #region Fields

            protected readonly DlibObject _Parent;

            protected readonly Dlib.Native.RunningStatsType _Type;

            #endregion

            #region Constructors

            protected RunningStatsImp(DlibObject parent, Dlib.Native.RunningStatsType type)
            {
                this._Parent = parent;
                this._Type = type;
            }

            #endregion

            #region Properties

            public abstract T CurrentN
            {
                get;
            }

            public abstract T ExcessKurtosis
            {
                get;
            }

            public abstract T Max
            {
                get;
            }

            public abstract T Mean
            {
                get;
            }

            public abstract T Min
            {
                get;
            }

            public abstract T Skewness
            {
                get;
            }

            public abstract T StdDev
            {
                get;
            }

            public abstract T Variance
            {
                get;
            }

            #endregion

            #region Methods

            public abstract void Add(T value);

            public abstract void Scale(T scale, out T ret);

            #endregion

        }

        private sealed class RunningStatsFloatImp : RunningStatsImp<float>
        {

            #region Constructors

            internal RunningStatsFloatImp(DlibObject parent, Dlib.Native.RunningStatsType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override float CurrentN
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_current_n(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float ExcessKurtosis
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_ex_kurtosis(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float Max
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_max(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float Mean
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_mean(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float Min
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_min(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float Skewness
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_skewness(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float StdDev
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_stddev(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override float Variance
            {
                get
                {
                    float value;
                    Dlib.Native.running_stats_variance(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            #endregion

            #region Methods

            public override void Add(float value)
            {
                Dlib.Native.running_stats_add(this._Type, this._Parent.NativePtr, ref value);
            }

            public override void Scale(float scale, out float ret)
            {
                Dlib.Native.running_stats_scale(this._Type, this._Parent.NativePtr, ref scale, out ret);
            }

            #endregion

        }

        private sealed class RunningStatsDoubleImp : RunningStatsImp<double>
        {

            #region Constructors

            internal RunningStatsDoubleImp(DlibObject parent, Dlib.Native.RunningStatsType type)
                : base(parent, type)
            {
            }

            #endregion

            #region Properties

            public override double CurrentN
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_current_n(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double ExcessKurtosis
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_ex_kurtosis(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double Max
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_max(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double Mean
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_mean(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double Min
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_min(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double Skewness
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_skewness(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double StdDev
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_stddev(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            public override double Variance
            {
                get
                {
                    double value;
                    Dlib.Native.running_stats_variance(this._Type, this._Parent.NativePtr, out value);
                    return value;
                }
            }

            #endregion

            #region Methods

            public override void Add(double value)
            {
                Dlib.Native.running_stats_add(this._Type, this._Parent.NativePtr, ref value);
            }

            public override void Scale(double scale, out double ret)
            {
                Dlib.Native.running_stats_scale(this._Type, this._Parent.NativePtr, ref scale, out ret);
            }

            #endregion

        }

    }

}