using System.Collections.Generic;
using System.Linq;
using DlibDotNet.ImageProcessing;
using DlibDotNet.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable once CheckNamespace
namespace DlibDotNet
{

    [TestClass]
    public class StdVectorOfVectorRectangleTest : TestBase
    {

        [TestMethod]
        public void Create()
        {
            var vector = new StdVectorOfVectorRectangle();
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVectorOfVectorRectangle(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [TestMethod]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new List<Rectangle>(Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i))));
            var vector = new StdVectorOfVectorRectangle(source);
            Assert.AreEqual(vector.Size, size);
            var ret = vector.ToArray();
            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.AreEqual(tmp[i].Left, i);
                    Assert.AreEqual(tmp[i].Top, i);
                    Assert.AreEqual(tmp[i].Right, i);
                    Assert.AreEqual(tmp[i].Bottom, i);
                }
            }
            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
