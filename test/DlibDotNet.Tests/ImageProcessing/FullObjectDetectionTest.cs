using Xunit;

namespace DlibDotNet.Tests.ImageProcessing
{

    public class FullObjectDetectionTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var rect = new Rectangle(10, 20, 40, 50);
            using (var detection = new FullObjectDetection(rect))
            {
                var r = detection.Rect;
                Assert.Equal(rect.Left, r.Left);
                Assert.Equal(rect.Right, r.Right);
                Assert.Equal(rect.Top, r.Top);
                Assert.Equal(rect.Bottom, r.Bottom);
            }
        }

        [Fact]
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
                Assert.Equal(rect.Left, r.Left);
                Assert.Equal(rect.Right, r.Right);
                Assert.Equal(rect.Top, r.Top);
                Assert.Equal(rect.Bottom, r.Bottom);

                Assert.Equal(detection.Parts, (uint)points.Length);

                for (var index = 0; index < points.Length; index++)
                {
                    var p = detection.GetPart((uint)index);
                    Assert.Equal(points[index].X, p.X);
                    Assert.Equal(points[index].Y, p.Y);
                }
            }
        }

    }

}
