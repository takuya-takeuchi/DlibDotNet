using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.ImageProcessing
{

    [TestClass]
    public class FullObjectDetectionTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var rect = new Rectangle(10, 20, 40, 50);
            using (var detection = new FullObjectDetection(rect))
            {
                var r = detection.Rect;
                Assert.AreEqual(rect.Left, r.Left);
                Assert.AreEqual(rect.Right, r.Right);
                Assert.AreEqual(rect.Top, r.Top);
                Assert.AreEqual(rect.Bottom, r.Bottom);
            }
        }

        [TestMethod]
        public void Create2()
        {
            var rect = new Rectangle(10, 20, 40, 50);
            var points = new []
            {
                new Point(10, 50),
                new Point(20, 40),
                new Point(30, 30),
                new Point(40, 20),
                new Point(50, 10),
            };

            using (var detection = new FullObjectDetection(rect, points))
            {
                var r = detection.Rect;
                Assert.AreEqual(rect.Left, r.Left);
                Assert.AreEqual(rect.Right, r.Right);
                Assert.AreEqual(rect.Top, r.Top);
                Assert.AreEqual(rect.Bottom, r.Bottom);

                Assert.AreEqual(detection.Parts, (uint)points.Length);

                for (var index = 0; index < points.Length; index++)
                {
                    var p = detection.GetPart((uint)index);
                    Assert.AreEqual(points[index].X, p.X);
                    Assert.AreEqual(points[index].Y, p.Y);
                }
            }
        }

    }

}
