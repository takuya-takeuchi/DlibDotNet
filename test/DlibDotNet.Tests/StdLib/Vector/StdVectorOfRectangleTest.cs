using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfRectangleTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<Rectangle>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<Rectangle>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i));
            var vector = new StdVector<Rectangle>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].Left, i);
                Assert.Equal(ret[i].Top, i);
                Assert.Equal(ret[i].Right, i);
                Assert.Equal(ret[i].Bottom, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i));
            var vector = new StdVector<Rectangle>(source);
            Assert.Equal(vector.Size, size);
            var ret = new Rectangle[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].Left, i);
                Assert.Equal(ret[i + 5].Top, i);
                Assert.Equal(ret[i + 5].Right, i);
                Assert.Equal(ret[i + 5].Bottom, i);
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
