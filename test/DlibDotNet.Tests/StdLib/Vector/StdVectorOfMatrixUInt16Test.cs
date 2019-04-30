using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.StdLib.Vector
{

    [TestClass]
    public class StdVectorOfMatrixUInt16Test : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVector<Matrix<ushort>>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Matrix<ushort>>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Matrix<ushort>(i, i));
            var vector = new StdVector<Matrix<ushort>>(source);
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