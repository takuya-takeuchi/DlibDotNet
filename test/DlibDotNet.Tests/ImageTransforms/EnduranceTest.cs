using System;
using Xunit;

namespace DlibDotNet.Tests.ImageTransforms
{

    public class EnduranceTest : TestBase
    {

        private const string LoadTarget = "Lenna";

        [Fact]
        public void ExtractImageChip()
        {
            var path = this.GetDataFile($"{LoadTarget}.bmp");

            const int loop = 1000;
            var sizeArray = new long[loop];
            var first = GetCurrentMemory();

            var start = GetCurrentMemory() - first;
            using (var image = DlibTest.LoadImageHelp(ImageTypes.RgbPixel, path))
            using (var dims = new ChipDims(227, 227))
            using (var chip = new ChipDetails(new Rectangle(0, 0, 100, 100), dims))
                for (var count = 0; count < loop; count++)
                    using (Dlib.ExtractImageChip<RgbPixel>(image, chip))
                        sizeArray[count] = GetCurrentMemory();

            // Important!!
            GC.Collect(2, GCCollectionMode.Forced, true);

            var end = GetCurrentMemory() - first;
            Console.WriteLine("        Start Total Memory = {0} KB", start / 1024);
            Console.WriteLine("          End Total Memory = {0} KB", end / 1024);
            Console.WriteLine("Delta (End - Start) Memory = {0} KB", (end - start) / 1024);

            // Rough estimate whether occur memory leak (less than 10240KB)
            Assert.True((end - start) / 1024 < 10240);
        }

    }

}
