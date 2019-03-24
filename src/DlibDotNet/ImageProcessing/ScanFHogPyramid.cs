using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ScanFHogPyramid<T, U> : ImageScanner
        where T : class
        where U : class
    {

        #region Fields

        private readonly NativeMethods.FHogFeatureExtractorType _FeatureExtractorType;

        private readonly NativeMethods.PyramidType _PyramidType;

        private readonly uint _PyramidRate;

        private static readonly Dictionary<Type, NativeMethods.PyramidType> SupportPyramidType = new Dictionary<Type, NativeMethods.PyramidType>();

        private static readonly Dictionary<Type, NativeMethods.FHogFeatureExtractorType> SupportFeatureExtractorType = new Dictionary<Type, NativeMethods.FHogFeatureExtractorType>();

        #endregion

        #region Constructors

        static ScanFHogPyramid()
        {
            SupportPyramidType.Add(typeof(PyramidDown), NativeMethods.PyramidType.Down);

            SupportFeatureExtractorType.Add(typeof(DefaultFHogFeatureExtractor), NativeMethods.FHogFeatureExtractorType.Default);
        }

        public ScanFHogPyramid(uint pyramidRate)
        {
            if (!SupportPyramidType.TryGetValue(typeof(T), out var pyramidType))
                throw new NotSupportedException();
            if (!SupportFeatureExtractorType.TryGetValue(typeof(U), out var featureExtractorType))
                throw new NotSupportedException();

            this._PyramidRate = pyramidRate;
            this._PyramidType = pyramidType;
            this._FeatureExtractorType = featureExtractorType;

            var ret = NativeMethods.scan_fhog_pyramid_new(this._PyramidType,
                                                          this._PyramidRate,
                                                          this._FeatureExtractorType,
                                                          out var pyramid);
            switch (ret)
            {
                case NativeMethods.ErrorType.PyramidNotSupportType:
                case NativeMethods.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
                case NativeMethods.ErrorType.FHogNotSupportExtractor:
                    throw new NotSupportedException();
            }

            this.NativePtr = pyramid;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        public void SetDetectionWindowSize(uint width, uint height)
        {
            NativeMethods.scan_fhog_pyramid_set_detection_window_size(this._PyramidType,
                                                                      this._PyramidRate,
                                                                      this._FeatureExtractorType,
                                                                      this.NativePtr,
                                                                      width,
                                                                      height);
        }

        public void SetNuclearNormRegularizationStrength(double strength)
        {
            if (strength < 0)
                throw new ArgumentOutOfRangeException();

            NativeMethods.scan_fhog_pyramid_set_nuclear_norm_regularization_strength(this._PyramidType,
                                                                                     this._PyramidRate,
                                                                                     this._FeatureExtractorType,
                                                                                     this.NativePtr,
                                                                                     strength);
        }

        #region Overrids

        /// <summary>
        /// Releases all unmanaged resources.
        /// </summary>
        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();

            if (this.NativePtr == IntPtr.Zero)
                return;

            NativeMethods.scan_fhog_pyramid_delete(this._PyramidType, 
                                                   this._PyramidRate,
                                                   this._FeatureExtractorType,
                                                   this.NativePtr);
        }

        internal override FHogPyramidParameter GetFHogPyramidParameter()
        {
            return new FHogPyramidParameter(this._PyramidType, this._PyramidRate, this._FeatureExtractorType);
        }

        #endregion

        #endregion

        #region ImageScanner Members

        public override ImageScannerType ScannerType => ImageScannerType.FHogPyramid;

        #endregion

    }

}
