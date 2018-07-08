using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    public sealed class ScanFHogPyramid<T, U> : ImageScanner
        where T : class
        where U : class
    {

        #region Events
        #endregion

        #region Fields

        private readonly Dlib.Native.FHogFeatureExtractorType _FeatureExtractorType;

        private readonly Dlib.Native.PyramidType _PyramidType;

        private readonly uint _PyramidRate;

        private static readonly Dictionary<Type, Dlib.Native.PyramidType> SupportPyramidType = new Dictionary<Type, Dlib.Native.PyramidType>();

        private static readonly Dictionary<Type, Dlib.Native.FHogFeatureExtractorType> SupportFeatureExtractorType = new Dictionary<Type, Dlib.Native.FHogFeatureExtractorType>();

        #endregion

        #region Constructors

        static ScanFHogPyramid()
        {
            SupportPyramidType.Add(typeof(PyramidDown), Dlib.Native.PyramidType.Down);

            SupportFeatureExtractorType.Add(typeof(DefaultFHogFeatureExtractor), Dlib.Native.FHogFeatureExtractorType.Default);
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

            var ret = Dlib.Native.scan_fhog_pyramid_new(this._PyramidType,
                                                        this._PyramidRate,
                                                        this._FeatureExtractorType,
                                                        out var pyramid);
            switch (ret)
            {
                case Dlib.Native.ErrorType.PyramidNotSupportType:
                case Dlib.Native.ErrorType.PyramidNotSupportRate:
                    throw new NotSupportedException();
                case Dlib.Native.ErrorType.FHogNotSupportExtractor:
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
            Dlib.Native.scan_fhog_pyramid_set_detection_window_size(this._PyramidType,
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

            Dlib.Native.scan_fhog_pyramid_set_nuclear_norm_regularization_strength(this._PyramidType,
                                                                                   this._PyramidRate,
                                                                                   this._FeatureExtractorType,
                                                                                   this.NativePtr,
                                                                                   strength);
        }

        #region Overrids

        protected override void DisposeUnmanaged()
        {
            base.DisposeUnmanaged();
            if (!this.IsDisposed)
                Dlib.Native.scan_fhog_pyramid_delete(this._PyramidType, this._PyramidRate, this._FeatureExtractorType, this.NativePtr);
        }

        internal override FHogPyramidParameter GetFHogPyramidParameter()
        {
            return new FHogPyramidParameter(this._PyramidType, this._PyramidRate, this._FeatureExtractorType);
        }

        #endregion

        #region Event Handlers
        #endregion

        #region Helpers
        #endregion

        #endregion

        #region ImageScanner Members

        public override ImageScannerType ScannerType => ImageScannerType.FHogPyramid;

        #endregion

    }

}
