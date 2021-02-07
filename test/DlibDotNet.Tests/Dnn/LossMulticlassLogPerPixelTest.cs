using System.IO;
using System.Linq;
using DlibDotNet.Dnn;
using Xunit;

namespace DlibDotNet.Tests.Dnn
{

    public class LossMulticlassLogPerPixelTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var networkIds = Enumerable.Range(0, 4);
            foreach (var networkId in networkIds)
                using (var loss = new LossMulticlassLogPerPixel(networkId))
                    Assert.True(!loss.IsDisposed);
        }

        [Fact]
        public void Deserialize()
        {
            var path = Path.Combine(this.ModelDirectory, "semantic_segmentation_voc2012net.dnn");
            using (var loss = LossMulticlassLogPerPixel.Deserialize(path))
                Assert.Equal(220, loss.NumLayers);
        }

        [Fact]
        public void Deserialize2()
        {
            var path = Path.Combine(this.ModelDirectory, "semantic_segmentation_voc2012net.dnn");
            using (var loss = LossMulticlassLogPerPixel.Deserialize(File.ReadAllBytes(path)))
                Assert.Equal(220, loss.NumLayers);
        }
        
        [Fact]
        public void Operator()
        {
            var image = this.GetDataFile("2008_001322.jpg");
            var path = Path.Combine(this.ModelDirectory, "semantic_segmentation_voc2012net.dnn");
            using (var loss1 = LossMulticlassLogPerPixel.Deserialize(path))
            using (var loss2 = LossMulticlassLogPerPixel.Deserialize(File.ReadAllBytes(path)))
            using (var matrix = Dlib.LoadImageAsMatrix<RgbPixel>(image.FullName))
            using (var ret1 = loss1.Operator(matrix))
            using (var ret2 = loss2.Operator(matrix))
            {
                Assert.Equal(1, ret1.Count);
                Assert.Equal(1, ret2.Count);

                var r1 = ret1[0];
                var r2 = ret2[0];

                Assert.Equal(r1.Rows, r2.Rows);
                Assert.Equal(r1.Columns, r2.Columns);

                for (var c = 0; c < r1.Columns; c++)
                for (var r = 0; r < r1.Rows; r++)
                    Assert.Equal(r1[r, c], r2[r, c]);
            }
        }

    }

}
