using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfVectorRectangleTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<StdVector<Rectangle>>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<StdVector<Rectangle>>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new StdVector<Rectangle>(Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i))));
            var vector = new StdVector<StdVector<Rectangle>>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.Equal(tmp[i].Left, i);
                    Assert.Equal(tmp[i].Top, i);
                    Assert.Equal(tmp[i].Right, i);
                    Assert.Equal(tmp[i].Bottom, i);
                }
            }

            this.DisposeAndCheckDisposedState(vector);
            foreach (var s in source)
                s.Dispose();
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(j => new StdVector<Rectangle>(Enumerable.Range(0, size).Select(i => new Rectangle(i, i, i, i))));
            var vector = new StdVector<StdVector<Rectangle>>(source);
            Assert.Equal(vector.Size, size);
            var ret = new StdVector<Rectangle>[15];
            vector.CopyTo(ret, 5);

            for (var j = 0; j < size; j++)
            {
                var tmp = ret[j + 5].ToArray();
                for (var i = 0; i < size; i++)
                {
                    Assert.Equal(tmp[i].Left, i);
                    Assert.Equal(tmp[i].Top, i);
                    Assert.Equal(tmp[i].Right, i);
                    Assert.Equal(tmp[i].Bottom, i);
                }
            }

            this.DisposeAndCheckDisposedState(vector);
            foreach (var s in source)
                s.Dispose();
        }

    }

}
