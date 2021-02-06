using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Dnn;
using Xunit;

namespace DlibDotNet.Tests.Dnn
{

    public class LossMmodTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var networkIds = Enumerable.Range(0, 4);
            foreach (var networkId in networkIds)
                using (var loss = new LossMmod(networkId))
                    Assert.True(!loss.IsDisposed);
        }

        [Fact]
        public void Deserialize()
        {
            var path = Path.Combine(this.ModelDirectory, "mmod_human_face_detector.dat");
            using (var loss = LossMmod.Deserialize(path))
                Assert.Equal(21, loss.NumLayers);
        }

        [Fact]
        public void Deserialize2()
        {
            var path = Path.Combine(this.ModelDirectory, "mmod_human_face_detector.dat");
            using (var loss = LossMmod.Deserialize(File.ReadAllBytes(path)))
                Assert.Equal(21, loss.NumLayers);
        }
        
        [Fact]
        public void Operator()
        {
            var image = this.GetDataFile("Lenna.jpg");
            var path = Path.Combine(this.ModelDirectory, "mmod_human_face_detector.dat");
            using (var net1 = LossMmod.Deserialize(path))
            using (var net2 = LossMmod.Deserialize(File.ReadAllBytes(path)))
            using (var matrix = Dlib.LoadImageAsMatrix<RgbPixel>(image.FullName))
            using (var ret1 = net1.Operator(matrix))
            using (var ret2 = net2.Operator(matrix))
            {
                Assert.Equal(1, ret1.Count);
                Assert.Equal(1, ret2.Count);

                var r1 = ret1[0].ToArray();
                var r2 = ret2[0].ToArray();

                Assert.Equal(r1.Length, r2.Length);
                Assert.Equal(r1[0].Rect.Left, r2[0].Rect.Left);
                Assert.Equal(r1[0].Rect.Right, r2[0].Rect.Right);
                Assert.Equal(r1[0].Rect.Top, r2[0].Rect.Top);
                Assert.Equal(r1[0].Rect.Bottom, r2[0].Rect.Bottom);
            }
        }

    }

}
