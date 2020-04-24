using Xunit;

namespace DlibDotNet.Tests.Queue
{

    public class Kernel1AOfUInt32Test : TestBase
    {

        [Fact]
        public void Create()
        {
            var queue = new Queue<uint>.Kernel1A();
            this.DisposeAndCheckDisposedState(queue);
        }

        [Fact]
        public void Clear()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            queue.Clear();
            Assert.Equal(0, queue.Count);

            this.DisposeAndCheckDisposedState(queue);
        }

        [Fact]
        public void Enqueue()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
            {
                queue.Enqueue((uint)i);
                Assert.Equal(queue.Count, i + 1);
            }
            this.DisposeAndCheckDisposedState(queue);
        }

        [Fact]
        public void Dequeue()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            var cur = 0u;
            while (queue.Count > 0)
            {
                queue.Dequeue(out var item);
                Assert.Equal(cur, item);
                cur++;
            }

            this.DisposeAndCheckDisposedState(queue);
        }

        [Fact]
        public void ElementAndMoveNext()
        {
            const int size = 100;
            var queue = new Queue<uint>.Kernel1A();
            for (var i = 0; i < size; i++)
                queue.Enqueue((uint)i);

            var cur = 0u;
            queue.Reset();
            while (queue.MoveNext)
            {
                Assert.Equal(cur, queue.Element());
                cur++;
            }

            this.DisposeAndCheckDisposedState(queue);
        }

    }

}