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
            var pair = new StdPairOfPointPoint(first, second);
            this.DisposeAndCheckDisposedState(pair);
            this.DisposeAndCheckDisposedState(second);
            this.DisposeAndCheckDisposedState(first);
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
            var pair1 = new StdPairOfPointPoint(first, second);

            Assert.AreEqual(pair1.First.X, fx);
            Assert.AreEqual(pair1.First.Y, fy);
            Assert.AreEqual(pair1.Second.X, sx);
            Assert.AreEqual(pair1.Second.Y, sy);
            Assert.AreEqual(pair1.First, first);
            Assert.AreEqual(pair1.Second, second);

            var pair2 = new StdPairOfPointPoint(null, null);
            Assert.AreEqual(pair2.First, null);
            Assert.AreEqual(pair2.Second, null);

            this.DisposeAndCheckDisposedState(pair2);
            this.DisposeAndCheckDisposedState(pair1);
            this.DisposeAndCheckDisposedState(second);
            this.DisposeAndCheckDisposedState(first);
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

            var pair = new StdPairOfPointPoint(null, null);
            pair.First = first;
            pair.Second = second;

            Assert.AreEqual(pair.First.X, fx);
            Assert.AreEqual(pair.First.Y, fy);
            Assert.AreEqual(pair.Second.X, sx);
            Assert.AreEqual(pair.Second.Y, sy);
            Assert.AreEqual(pair.First, first);
            Assert.AreEqual(pair.Second, second);

            this.DisposeAndCheckDisposedState(pair);
            this.DisposeAndCheckDisposedState(second);
            this.DisposeAndCheckDisposedState(first);
        }

    }

}
