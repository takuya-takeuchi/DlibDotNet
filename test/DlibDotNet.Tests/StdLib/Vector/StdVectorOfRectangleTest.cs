using System.Linq;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class StdVectorOfRectangleTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVector<Rectangle>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Rectangle>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i));
            var vector = new StdVector<Rectangle>(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.AreEqual(ret[i].Left, i);
                Assert.AreEqual(ret[i].Top, i);
                Assert.AreEqual(ret[i].Right, i);
                Assert.AreEqual(ret[i].Bottom, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
