using System.Linq;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DlibDotNet.Tests.StdLib.Vector
{

    public class StdVectorOfChipDetailsTest : TestBase
    {

        [Fact]
        public void Create()
        {
            var vector = new StdVector<ChipDetails>();
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithSize()
        {
            const int size = 10;
            var vector = new StdVector<ChipDetails>(size);
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CreateWithCollection()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new ChipDetails(new DRectangle(i, i, i, i), (uint)i));
            var vector = new StdVector<ChipDetails>(source);
            Assert.Equal(vector.Size, size);
            var ret = vector.ToArray();
            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i].Rect.Left, i);
                Assert.Equal(ret[i].Rect.Top, i);
                Assert.Equal(ret[i].Rect.Right, i);
                Assert.Equal(ret[i].Rect.Bottom, i);
            }
            this.DisposeAndCheckDisposedState(vector);
        }

        [Fact]
        public void CopyTo()
        {
            const int size = 10;
            var source = Enumerable.Range(0, size).Select(i => new ChipDetails(new DRectangle(i, i, i, i), (uint)i));
            var vector = new StdVector<ChipDetails>(source);
            Assert.Equal(vector.Size, size);
            var ret = new ChipDetails[15];
            vector.CopyTo(ret, 5);

            for (var i = 0; i < size; i++)
            {
                Assert.Equal(ret[i + 5].Rect.Left, i);
                Assert.Equal(ret[i + 5].Rect.Top, i);
                Assert.Equal(ret[i + 5].Rect.Right, i);
                Assert.Equal(ret[i + 5].Rect.Bottom, i);
            }

            this.DisposeAndCheckDisposedState(vector);
        }

    }

}
