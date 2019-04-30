using System;
using System.Linq;
using DlibDotNet.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.StdLib.Vector
{

    [TestClass]
    public class EnduranceTest : TestBase
    {

        [TestMethod]
        public void Primitive()
        {
            const int size = 100000;
            const int loop = 10000;
            var first = GetCurrentMemory();
            var source = Enumerable.Range(0, size).ToArray();

            // Avoid effect of StdVectorElementTypesRepository
            var tmp = new StdVector<int>();
            tmp.Dispose();

            var start = GetCurrentMemory() - first;
            for (var count = 0; count < loop; count++)
            {
                var vector = new StdVector<int>(source);
                vector.Dispose();
                this.DisposeAndCheckDisposedState(vector);
            }

            // Important!!
            GC.Collect(2, GCCollectionMode.Forced, true);

            var end = GetCurrentMemory() - first;
            Console.WriteLine("        Start Total Memory = {0} KB", start / 1024);
            Console.WriteLine("          End Total Memory = {0} KB", end / 1024);
            Console.WriteLine("Delta (End - Start) Memory = {0} KB", (end - start) / 1024);

            // Rough estimate whether occur memory leak (less than 1024KB)
            Assert.IsTrue((end - start) / 1024 < 1024);
        }

        [TestMethod]
        public void NonPrimitive()
        {
            const int size = 10000;
            const int loop = 100000;
            var first = GetCurrentMemory();

            // Avoid effect of StdVectorElementTypesRepository
            var tmp = new StdVector<StdString>();
            tmp.Dispose();

            var start = GetCurrentMemory() - first;
            var source = Enumerable.Range(0, size).Select(i => new StdString(i.ToString())).ToArray();
            for (var count = 0; count < loop; count++)
            {
                var vector = new StdVector<StdString>(source);
                vector.Dispose();
            }

            source.DisposeElement();

            // Important!!
            GC.Collect(2, GCCollectionMode.Forced, true);

            var end = GetCurrentMemory() - first;
            Console.WriteLine("        Start Total Memory = {0} KB", start / 1024);
            Console.WriteLine("          End Total Memory = {0} KB", end / 1024);
            Console.WriteLine("Delta (End - Start) Memory = {0} KB", (end - start) / 1024);

            // Rough estimate whether occur memory leak (less than 10240KB)
            Assert.IsTrue((end - start) / 1024 < 10240);
        }

    }

}
