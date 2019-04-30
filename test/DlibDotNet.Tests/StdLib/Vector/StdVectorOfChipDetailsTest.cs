using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    [TestClass]
    public class StdVectorOfChipDetailsTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVector<ChipDetails>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<ChipDetails>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new ChipDetails(new DRectangle(i, i, i, i), (uint)i));
            var vector = new StdVector<ChipDetails>(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.AreEqual(ret[i].Rect.Left, i);
                Assert.AreEqual(ret[i].Rect.Top, i);
                Assert.AreEqual(ret[i].Rect.Right, i);
                Assert.AreEqual(ret[i].Rect.Bottom, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
