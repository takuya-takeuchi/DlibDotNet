using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class StdPairOfPointPointTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var first = new Point();
            var second = new Point();
            var pair = new StdPair<Point,Point>(first, second);
            this.DisposeAndCheckDisposedState(pair);
        }

        [TestMethod]
        public void CheckGetFirstSecond()
        {
            var fx = this.NextRandom(1, 100);
            var fy = this.NextRandom(1, 100);
            var sx = this.NextRandom(1, 100);
            var sy = this.NextRandom(1, 100);

            var first = new Point(fx, fy);
            var second = new Point(sx, sy);
            var pair1 = new StdPair<Point, Point>(first, second);

            Assert.AreEqual(pair1.First.X, fx);
            Assert.AreEqual(pair1.First.Y, fy);
            Assert.AreEqual(pair1.Second.X, sx);
            Assert.AreEqual(pair1.Second.Y, sy);
            Assert.AreEqual(pair1.First, first);
            Assert.AreEqual(pair1.Second, second);
            
            this.DisposeAndCheckDisposedState(pair1);
        }

        [TestMethod]
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

            Assert.AreEqual(pair.First.X, fx);
            Assert.AreEqual(pair.First.Y, fy);
            Assert.AreEqual(pair.Second.X, sx);
            Assert.AreEqual(pair.Second.Y, sy);

            this.DisposeAndCheckDisposedState(pair);
        }

    }

}
