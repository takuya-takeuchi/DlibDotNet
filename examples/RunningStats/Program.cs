/*
 * This sample program is ported by C# from examples\running_stats_ex.cpp.
*/

using System;
using DlibDotNet;

namespace RunningStats
{

    internal class Program
    {

        private static double Sinc(double x)
        {
            if (Math.Abs(x) < double.Epsilon)
                return 1;
            return Math.Sin(x) / x;
        }

        private static void Main()
        {
            using (var rs = new RunningStats<double>())
            {
                double tp1 = 0;
                double tp2 = 0;

                // We first generate the data and add it sequentially to our running_stats object.  We
                // then print every fifth data point.
                for (var x = 1; x <= 100; x++)
                {
                    tp1 = x / 100.0;
                    tp2 = Sinc(Math.PI * x / 100.0);
                    rs.Add(tp2);

                    if (x % 5 == 0)
                    {
                        Console.WriteLine($" x =  {tp1}  sinc(x) = {tp2}");
                    }
                }

                // Finally, we compute and print the mean, variance, skewness, and excess kurtosis of
                // our data.

                Console.WriteLine();
                Console.WriteLine($"Mean:           {rs.Mean}");
                Console.WriteLine($"Variance:       {rs.Variance}");
                Console.WriteLine($"Skewness:       {rs.Skewness}");
                Console.WriteLine($"Excess Kurtosis {rs.ExcessKurtosis}");
            }
        }

    }

}