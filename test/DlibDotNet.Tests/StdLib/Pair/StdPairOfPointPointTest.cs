using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Pair
{

    public class StdPairOfPointPointTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var first = new Point();
            var second = new Point();
            var pair = new StdPair<Point,Point>(first, second);
            this.DisposeAndCheckDisposedState(pair);
        }

        [Fact]
        public void CheckGetFirstSecond()
        {
            var fx = this.NextRandom(1, 100);
            var fy = this.NextRandom(1, 100);
            var sx = this.NextRandom(1, 100);
            var sy = this.NextRandom(1, 100);

            var first = new Point(fx, fy);
            var second = new Point(sx, sy);
            var pair1 = new StdPair<Point, Point>(first, second);

            Assert.Equal(pair1.First.X, fx);
            Assert.Equal(pair1.First.Y, fy);
            Assert.Equal(pair1.Second.X, sx);
            Assert.Equal(pair1.Second.Y, sy);
            Assert.Equal(pair1.First, first);
            Assert.Equal(pair1.Second, second);
            
            this.DisposeAndCheckDisposedState(pair1);
        }

        [Fact]
        public void CheckSetFirstSecond()
        {
            var fx = this.NextRandom(1, 100);
            var fy = this.NextRandom(1, 100);
            var sx = this.NextRandom(1, 100);
            var sy = this.NextRandom(1, 100);

            var first = new Point(fx, fy);
            var second = new Point(sx, sy);
            var first1 = new Point(fx, fy);
            var second2 = new Point(sx, sy);

            var pair = new StdPair<Point, Point>(first, second);
            pair.First = first1;
            pair.Second = second2;

            Assert.Equal(pair.First.X, fx);
            Assert.Equal(pair.First.Y, fy);
            Assert.Equal(pair.Second.X, sx);
            Assert.Equal(pair.Second.Y, sy);

            this.DisposeAndCheckDisposedState(pair);
        }

    }

}
