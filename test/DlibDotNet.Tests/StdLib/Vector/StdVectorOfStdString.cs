using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    [TestClass]
    public class StdVectorOfStdString : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVector<StdString>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<StdString>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new StdString(i.ToString()));
            var vector = new StdVector<StdString>(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.AreEqual(ret[i].ToString(), i.ToString());
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
