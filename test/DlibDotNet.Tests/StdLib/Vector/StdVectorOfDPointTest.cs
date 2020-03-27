using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfDPointTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<DPoint>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<DPoint>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new DPoint(i, i));
            var vector = new StdVector<DPoint>(source);
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
            var source = Enumerable.Range(0, size).Select(i => new DPoint(i, i));
            var vector = new StdVector<DPoint>(source);
            Assert.Equal(vector.Size, size);
            var ret = new DPoint[15];
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
