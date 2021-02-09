using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet.Dnn;
using Xunit;

namespace DlibDotNet.Tests.Dnn
{

    public class LossMetricTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var networkIds = Enumerable.Range(0, 2);
            foreach (var networkId in networkIds)
                using (var loss = new LossMetric(networkId))
                    Assert.True(!loss.IsDisposed);
        }

        [Fact]
        public void Deserialize()
        {
            var path = Path.Combine(this.ModelDirectory, "dlib_face_recognition_resnet_model_v1.dat");
            using (var loss = LossMetric.Deserialize(path))
                Assert.Equal(132, loss.NumLayers);
        }

        [Fact]
        public void Deserialize2()
        {
            var path = Path.Combine(this.ModelDirectory, "dlib_face_recognition_resnet_model_v1.dat");
            using (var loss = LossMetric.Deserialize(File.ReadAllBytes(path)))
                Assert.Equal(132, loss.NumLayers);
        }
        
        [Fact]
        public void Operator()
        {
            var image = this.GetDataFile("Lenna.jpg");
            var path1 = Path.Combine(this.ModelDirectory, "dlib_face_recognition_resnet_model_v1.dat");
            var path2 = Path.Combine(this.ModelDirectory, "shape_predictor_5_face_landmarks.dat");
            using (var net1 = LossMetric.Deserialize(path1))
            using (var net2 = LossMetric.Deserialize(File.ReadAllBytes(path1)))
            using (var sp = ShapePredictor.Deserialize(path2))
            using (var matrix = Dlib.LoadImageAsMatrix<RgbPixel>(image.FullName))
            using (var detector = Dlib.GetFrontalFaceDetector())
            {
                var faces = new List<Matrix<RgbPixel>>();
                foreach (var face in detector.Operator(matrix))
                {
                    var shape = sp.Detect(matrix, face);
                    var faceChipDetail = Dlib.GetFaceChipDetails(shape, 150, 0.25);
                    var faceChip = Dlib.ExtractImageChip<RgbPixel>(matrix, faceChipDetail);
                    faces.Add(faceChip);
                }

                foreach (var face in faces)
                {
                    using (var ret1 = net1.Operator(face))
                    using (var ret2 = net2.Operator(face))
                    {
                        Assert.Equal(1, ret1.Count);
                        Assert.Equal(1, ret2.Count);

                        var r1 = ret1[0];
                        var r2 = ret2[0];

                        Assert.Equal(r1.Columns, r2.Columns);
                        Assert.Equal(r1.Rows, r2.Rows);

                        for (var c = 0; c < r1.Columns; c++)
                        for (var r = 0; r < r1.Rows; r++)
                            Assert.Equal(r1[r, c], r2[r, c]);
                    }

                    face.Dispose();
                }
            }
        }

    }

}
