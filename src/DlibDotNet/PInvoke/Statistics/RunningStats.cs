using System;
using System.Runtime.InteropServices;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    internal sealed partial class NativeMethods
    {

        #region running_stats

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr running_stats_new(RunningStatsType type);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref float val);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_add(RunningStatsType type, IntPtr stats, ref double val);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_clear(RunningStatsType type, IntPtr stats);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out float n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_current_n(RunningStatsType type, IntPtr stats, out double n);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out float ex_kurtosis);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_ex_kurtosis(RunningStatsType type, IntPtr stats, out double ex_kurtosis);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out float max);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_max(RunningStatsType type, IntPtr stats, out double max);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out float mean);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_mean(RunningStatsType type, IntPtr stats, out double mean);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out float min);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_min(RunningStatsType type, IntPtr stats, out double min);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref float scale, out float ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_scale(RunningStatsType type, IntPtr stats, ref double scale, out double ret);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out float skewness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_skewness(RunningStatsType type, IntPtr stats, out double skewness);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out float stddev);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_stddev(RunningStatsType type, IntPtr stats, out double stddev);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out float variance);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern ErrorType running_stats_variance(RunningStatsType type, IntPtr stats, out double variance);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern void running_stats_delete(RunningStatsType type, IntPtr stats);

        [DllImport(NativeLibrary, CallingConvention = CallingConvention)]
        public static extern IntPtr running_stats_operator_add(RunningStatsType type, IntPtr left, IntPtr right);

        #endregion

    }

}