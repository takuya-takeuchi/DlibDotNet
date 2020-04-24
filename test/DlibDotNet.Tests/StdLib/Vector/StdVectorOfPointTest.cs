using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfPointTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<Point>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Point>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Point(i, i));
            var vector = new StdVector<Point>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].X, i);
                Assert.Equal(ret[i].Y, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Point(i, i));
            var vector = new StdVector<Point>(source);
            Assert.Equal(vector.Size, size);
            var ret = new Point[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].X, i);
                Assert.Equal(ret[i + 5].Y, i);
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
