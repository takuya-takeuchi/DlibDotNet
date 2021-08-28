using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region Fields

        /// <summary>
        /// Native library file name.
        /// If Linux, it will be converted to  libDlibDotNetNative.so
        /// If MacOSX, it will be converted to  libDlibDotNetNative.dylib
        /// If Windows, it will be available after call LoadLibrary.
        /// And this file name must not contain period. If it does,
        /// CLR does not add extension (.dll) and CLR fails to load library
        /// </summary>
#if LIB_STATIC
        public const string NativeLibrary = "__Internal";

        public const string NativeDnnLibrary = "__Internal";
#elif LITE
        public const string NativeLibrary = "DlibDotNetNativeLite";

        public const string NativeDnnLibrary = "DlibDotNetNativeLite";
#else
        public const string NativeLibrary = "DlibDotNetNative";

        public const string NativeDnnLibrary = "DlibDotNetNativeDnn";
#endif

        public const CallingConvention CallingConvention = System.Runtime.InteropServices.CallingConvention.Cdecl;

        private static readonly WindowsLibraryLoader WindowsLibraryLoader = new WindowsLibraryLoader();

        #endregion

        #region Constructors

        static NativeMethods()
        {
            WindowsLibraryLoader.LoadLibraries(new[]
            {
                $"{NativeLibrary}",
                $"{NativeDnnLibrary}"
            });

#if !LITE
            foreach (var builder in new[] {
                LossMetric_anet_type_create(),
                LossMetric_metric_net_type_create()
            })
                LossMetricRegistry_add(builder);
            foreach (var builder in new[] {
                LossMmod_net_type_create(),
                LossMmod_net_type_1_create(),
                LossMmod_net_type_2_create(),
                LossMmod_net_type_3_create(),
                LossMmod_det_bnet_type_create(),
                LossMmod_det_anet_type_create()
            })
                LossMmodRegistry_add(builder);
            foreach (var builder in new[] {
                LossMulticlassLog_net_type_create(),
                LossMulticlassLog_net_1000_type_create(),
                LossMulticlassLog_anet_1000_type_create(),
                LossMulticlassLog_net_type2_create()
            })
                LossMulticlassLogRegistry_add(builder);
            foreach (var builder in new[] {
                LossMulticlassLogPerPixel_net_type_create(),
                LossMulticlassLogPerPixel_anet_type_create(),
                LossMulticlassLogPerPixel_ubnet_type_create(),
                LossMulticlassLogPerPixel_uanet_type_create(),
                LossMulticlassLogPerPixel_seg_bnet_type_create(),
                LossMulticlassLogPerPixel_seg_anet_type_create()
            })
                LossMulticlassLogPerPixelRegistry_add(builder);
#else
            foreach (var builder in new[] {
                LossMetric_anet_type_create(),
                LossMetric_metric_net_type_create()
            })
                LossMetricRegistry_add(builder);
            foreach (var builder in new[] {
                LossMmod_net_type_create(),
                LossMmod_net_type_1_create(),
                LossMmod_net_type_2_create(),
                LossMmod_net_type_3_create(),
                LossMmod_det_bnet_type_create(),
                LossMmod_det_anet_type_create()
            })
                LossMmodRegistry_add(builder);
            foreach (var builder in new[] {
                LossMulticlassLog_net_type_create(),
                LossMulticlassLog_net_1000_type_create(),
                LossMulticlassLog_anet_1000_type_create(),
                LossMulticlassLog_net_type2_create()
            })
                LossMulticlassLogRegistry_add(builder);
#endif
        }

        #endregion

    }

}
