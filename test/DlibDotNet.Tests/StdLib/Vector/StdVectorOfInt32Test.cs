using System.Linq;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class StdVectorOfInt32Test : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVectorOfInt32();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVectorOfInt32(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).ToArray();
            var vector = new StdVectorOfInt32(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
                Assert.AreEqual(ret[i], i);
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
