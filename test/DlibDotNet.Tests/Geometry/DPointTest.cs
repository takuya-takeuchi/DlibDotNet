using System;
using Xunit;

namespace DlibDotNet.Tests.Geometry
{

    public class DPointTest : TestBase
    {

        [Fact]
        public void Create1()
        {
            var point = new DPoint();
            Assert.Equal(point.X, 0d);
            Assert.Equal(point.Y, 0d);
        }

        [Fact]
        public void Create2()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            Assert.Equal(point.X, x);
            Assert.Equal(point.Y, y);
        }

        [Fact]
        public void Length()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            var length = Math.Sqrt(x * x + y * y);
            Assert.Equal(point.Length, length);
        }

        [Fact]
        public void LengthSquared()
        {
            var x = this.NextDoubleRandom();
            var y = this.NextDoubleRandom();
            var point = new DPoint(x, y);
            var lengthSquared = x * x + y * y;
            Assert.Equal(point.LengthSquared, lengthSquared);
        }

        [Fact]
        public void Rotate()
        {
            const int away = 90;
            var x = 10;
            var y = 20;
            var point = new DPoint(x, y);
            var move = new DPoint(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = point.Rotate(p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.True(Math.Round(rotated.X) == x, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.True(Math.Round(rotated.X) == x + away, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.True(Math.Round(rotated.X) == x, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y + away, $"Check Y and Rotate: {90 * i}");
                        break;
                }
            }
        }

        [Fact]
        public void Rotate2()
        {
            const int away = 90;
            var x = 10;
            var y = 20;
            var point = new DPoint(x, y);
            var move = new DPoint(-away, 0);
            var p = point + move;

            for (var i = 1; i <= 3; i++)
            {
                var rotated = DPoint.Rotate(point, p, 90 * i);

                switch (i)
                {
                    case 1:
                        Assert.True(Math.Round(rotated.X) == x, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y - away, $"Check Y Rotate: {90 * i}");
                        break;
                    case 2:
                        Assert.True(Math.Round(rotated.X) == x + away, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y, $"Check Y Rotate: {90 * i}");
                        break;
                    case 3:
                        Assert.True(Math.Round(rotated.X) == x, $"Check X and Rotate: {90 * i}");
                        Assert.True(Math.Round(rotated.Y) == y + away, $"Check Y and Rotate: {90 * i}");
                        break;
                }
            }
        }

        [Fact]
        public void OperatorAdd()
        {
            var lx = (double)this.NextRandom(0, 100);
            var ly = (double)this.NextRandom(0, 100);
            var rx = (double)this.NextRandom(0, 100);
            var ry = (double)this.NextRandom(0, 100);

            var r = new DPoint(lx, ly);
            var l = new DPoint(rx, ry);
            var rl = r + l;

            Assert.Equal(rl.X, lx + rx);
            Assert.Equal(rl.Y, ly + ry);
        }

        [Fact]
        public void OperatorSub()
        {
            var lx = (double)this.NextRandom(0, 100);
            var ly = (double)this.NextRandom(0, 100);
            var rx = (double)this.NextRandom(0, 100);
            var ry = (double)this.NextRandom(0, 100);

            var r = new DPoint(lx, ly);
            var l = new DPoint(rx, ry);
            var rl = r - l;

            Assert.Equal(rl.X, lx - rx);
            Assert.Equal(rl.Y, ly - ry);
        }

        [Fact]
        public void OperatorMul_DPoint_Int32()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var l = new DPoint(lx, ly);
            var r = 2;
            var lr = l * r;

            Assert.Equal(lr.X, lx * r);
            Assert.Equal(lr.Y, ly * r);
        }

        [Fact]
        public void OperatorMul_DPoint_Double()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var l = new DPoint(lx, ly);
            var r = 2.5d;
            var lr = l * r;

            Assert.Equal(lr.X, lx * r);
            Assert.Equal(lr.Y, ly * r);
        }

        [Fact]
        public void OperatorMul_Int32_DPoint()
        {
            var rx = (double)this.NextByteRandom();
            var ry = (double)this.NextByteRandom();

            var r = new DPoint(rx, ry);
            var l = 2;
            var lr = l * r;

            Assert.Equal(lr.X, l * rx);
            Assert.Equal(lr.Y, l * ry);
        }

        [Fact]
        public void OperatorMul_Double_DPoint()
        {
            var rx = (double)this.NextByteRandom();
            var ry = (double)this.NextByteRandom();

            var r = new DPoint(rx, ry);
            var l = 2.5d;
            var lr = l * r;

            Assert.Equal(lr.X, l * rx);
            Assert.Equal(lr.Y, l * ry);
        }

        [Fact]
        public void OperatorDiv()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            var r = new DPoint(lx, ly);
            var l = 2;
            var rl = r / l;

            Assert.Equal(rl.X, lx / l);
            Assert.Equal(rl.Y, ly / l);
        }

        [Fact]
        public void OperatorDivByZero()
        {
            var lx = (double)this.NextRandom(10, 100);
            var ly = (double)this.NextRandom(10, 100);

            try
            {
                var r = new DPoint(lx, ly);
                var rl = r / 0;
                Assert.True(false, "Should throw DivideByZeroException when DPoint was divided by 0");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("OK");
            }
        }

        [Fact]
        public void OperatorEqual()
        {
            var x = (double)this.NextRandom(1, 100);
            var y = (double)this.NextRandom(1, 100);

            var r = new DPoint(x, y);
            var l = new DPoint(x, y);
            var l1 = new DPoint(x * 2, y);
            var l2 = new DPoint(x, y * 2);

            Assert.True(r == l, $"1 - RX: {r.X}, RY: {r.Y}\nLX: {l.X}, LY: {l.Y}");
            Assert.True(r != l1, $"2 - RX: {r.X}, RY: {r.Y}\nLX: {l1.X}, LY: {l1.Y}");
            Assert.True(r != l2, $"3 - RX: {r.X}, RY: {r.Y}\nLX: {l2.X}, LY: {l2.Y}");
        }

    }
}
