using System.Linq;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class StdVectorOfMatrixTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVector<Matrix<int>>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Matrix<int>>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Matrix<int>(i, i));
            var vector = new StdVector<Matrix<int>>(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.AreEqual(ret[i].Rows, i);
                Assert.AreEqual(ret[i].Columns, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
