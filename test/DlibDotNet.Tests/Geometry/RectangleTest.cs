using System;
using Xunit;

namespace DlibDotNet.Tests.Geometry
{

    public class RectangleTest : TestBase
    {

        [Fact]
        public void Create1()
        {
            var rect = new Rectangle();
            Assert.Equal(0, rect.Left);
            Assert.Equal(0, rect.Top);
            Assert.Equal(1u, rect.Width);
            Assert.Equal(1u, rect.Height);
        }

        [Fact]
        public void Create2()
        {
            var width = (uint)this.NextRandom(1, 100);
            var height = (uint)this.NextRandom(1, 100);
            var rect = new Rectangle(width, height);
            Assert.Equal((uint)rect.Right, (width - 1));
            Assert.Equal((uint)rect.Bottom, (height - 1));
        }

        [Fact]
        public void Create3()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.Equal(rect.Left, left);
            Assert.Equal(rect.Top, top);
            Assert.Equal(rect.Right, right);
            Assert.Equal(rect.Bottom, bottom);
        }

        [Fact]
        public void Create4()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var p1 = new Point(left, top);
            var p2 = new Point(right, bottom);
            var rect = new Rectangle(p1, p2);
        }

        [Fact]
        public void Create5()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var p = new Point(left, top);
            var rect = new Rectangle(p);
        }

        [Fact]
        public void Left()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Left = value;
            Assert.Equal(rect.Left, value);
        }

        [Fact]
        public void Top()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Top = value;
            Assert.Equal(rect.Top, value);
        }

        [Fact]
        public void Right()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Right = value;
            Assert.Equal(rect.Right, value);
        }

        [Fact]
        public void Bottom()
        {
            var rect = new Rectangle();
            var value = this.NextRandom(0, 100);
            rect.Bottom = value;
            Assert.Equal(rect.Bottom, value);
        }

        [Fact]
        public void Width()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.Equal(rect.Width, width);
        }

        [Fact]
        public void Height()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.Equal(rect.Height, height);
        }

        [Fact]
        public void Area()
        {
            var width = (uint)this.NextRandom(0, 100);
            var height = (uint)this.NextRandom(0, 100);
            var rect = new Rectangle(width, height);
            Assert.Equal(rect.Area, width * height);
        }

        [Fact]
        public void IsEmpty()
        {
            var right = this.NextRandom(1, 100);
            var bottom = this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var rect1 = new Rectangle(left, top, right, bottom);
            Assert.True(rect1.IsEmpty);

            left = this.NextRandom(1, 100);
            top = this.NextRandom(1, 100);
            right = left * 2;
            bottom = top * 2;
            var rect2 = new Rectangle(left, top, right, bottom);
            Assert.False(rect2.IsEmpty);
        }

        [Fact]
        public void TopLeft()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var tl = rect.TopLeft;
            Assert.Equal(tl.X, left);
            Assert.Equal(tl.Y, top);
        }

        [Fact]
        public void TopRight()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var tr = rect.TopRight;
            Assert.Equal(tr.X, right);
            Assert.Equal(tr.Y, top);
        }

        [Fact]
        public void BottomLeft()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var bl = rect.BottomLeft;
            Assert.Equal(bl.X, left);
            Assert.Equal(bl.Y, bottom);
        }

        [Fact]
        public void BottomRight()
        {
            var left = this.NextRandom(0, 100);
            var top = this.NextRandom(0, 100);
            var right = this.NextRandom(0, 100);
            var bottom = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            var br = rect.BottomRight;
            Assert.Equal(br.X, right);
            Assert.Equal(br.Y, bottom);
        }

        [Fact]
        public void CenteredRect1()
        {
            const int x = 50;
            const int y = 60;
            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(x, y, width, height);

            var expected = new Rectangle();
            expected.Left = x - (width / 2);
            expected.Top = y - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.Equal(result.Left, expected.Left);
            Assert.Equal(result.Top, expected.Top);
            Assert.Equal(result.Right, expected.Right);
            Assert.Equal(result.Bottom, expected.Bottom);
        }

        [Fact]
        public void CenteredRect2()
        {
            const int x = 50;
            const int y = 60;
            var p = new Point(x, y);
            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(p, width, height);

            var expected = new Rectangle();
            expected.Left = x - (width / 2);
            expected.Top = y - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.Equal(result.Left, expected.Left);
            Assert.Equal(result.Top, expected.Top);
            Assert.Equal(result.Right, expected.Right);
            Assert.Equal(result.Bottom, expected.Bottom);
        }

        [Fact]
        public void CenteredRect3()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new Rectangle(left, top, right, bottom);

            const int width = 20;
            const int height = 30;

            var result = Rectangle.CenteredRect(rect, width, height);

            var expected = new Rectangle();
            expected.Left = ((left + right) / 2) - (width / 2);
            expected.Top = ((top + bottom) / 2) - (height / 2);
            expected.Right = expected.Left + width - 1;
            expected.Bottom = expected.Top + height - 1;

            Assert.Equal(result.Left, expected.Left);
            Assert.Equal(result.Top, expected.Top);
            Assert.Equal(result.Right, expected.Right);
            Assert.Equal(result.Bottom, expected.Bottom);
        }

        [Fact]
        public void Contains1()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var x = this.NextRandom(0, 100);
            var y = this.NextRandom(0, 100);
            var p = new Point(x, y);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.True(rect.Contains(p));
        }

        [Fact]
        public void Contains2()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var x = this.NextRandom(0, 100);
            var y = this.NextRandom(0, 100);
            var rect = new Rectangle(left, top, right, bottom);
            Assert.True(rect.Contains(x, y));
        }

        [Fact]
        public void Intersect()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new Rectangle(left, top, right, bottom);

            const int left2 = 10;
            const int top2 = 10;
            const int right2 = 30;
            const int bottom2 = 50;
            var rect2 = new Rectangle(left2, top2, right2, bottom2);

            var result = rect.Intersect(rect2);

            var left3 = Math.Max(rect.Left, rect2.Left);
            var top3 = Math.Max(rect.Top, rect2.Top);
            var right3 = Math.Min(rect.Right, rect2.Right);
            var bottom3 = Math.Min(rect.Bottom, rect2.Bottom);
            var expected = new Rectangle(left3, top3, right3, bottom3);

            Assert.Equal(result.Left, expected.Left);
            Assert.Equal(result.Top, expected.Top);
            Assert.Equal(result.Right, expected.Right);
            Assert.Equal(result.Bottom, expected.Bottom);
        }

        [Fact]
        public void Center()
        {
            var left = 0;
            var top = 0;
            var right = 59;
            var bottom = 99;
            var x = 30;
            var y = 50;
            var rect = new Rectangle(left, top, right, bottom);
            var center = rect.Center;
            Assert.Equal(center.X, x);
            Assert.Equal(center.Y, y);
        }

        [Fact]
        public void DCenter()
        {
            const int left = 0;
            const int top = 0;
            const int right = 61;
            const int bottom = 101;
            const double x = 30.5d;
            const double y = 50.5d;
            Rectangle rect = new Rectangle(left, top, right, bottom);
            var center = rect.DCenter;
            Assert.Equal(center.X, x);
            Assert.Equal(center.Y, y);
        }

        [Fact]
        public void SetAspectRatio()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new Rectangle(left, top, right, bottom);
            rect = Rectangle.SetAspectRatio(rect, 2);

            Assert.Equal(rect.Left, -21);
            Assert.Equal(rect.Top, 15);
            Assert.Equal(rect.Right, 120);
            Assert.Equal(rect.Bottom, 85);

            try
            {
                rect = Rectangle.SetAspectRatio(rect, 0);
                Assert.True(false, $"{nameof(Rectangle.SetAspectRatio)} should throw {nameof(ArgumentOutOfRangeException)} if ration is 0");
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        [Fact]
        public void Translate()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = rect.Translate(point);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void TranslateStatic()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = Rectangle.Translate(rect, point);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void TranslateXY()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);

            var ret = rect.Translate(x, y);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void TranslateStaticXY()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new Rectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = Rectangle.Translate(rect, x, y);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void OperatorAdd()
        {
            var lleft = this.NextRandom(0, 100);
            var ltop = this.NextRandom(0, 100);
            var lright = lleft * 2;
            var lbottom = ltop * 2;
            var rleft = this.NextRandom(0, 100);
            var rtop = this.NextRandom(0, 100);
            var rright = rleft * 2;
            var rbottom = rtop * 2;

            var r = new Rectangle(lleft, ltop, lright, lbottom);
            var l = new Rectangle(rleft, rtop, rright, rbottom);
            var rl = r + l;

            Assert.Equal(rl.Left, Math.Min(l.Left, r.Left));
            Assert.Equal(rl.Top, Math.Min(l.Top, r.Top));
            Assert.Equal(rl.Right, Math.Max(l.Right, r.Right));
            Assert.Equal(rl.Bottom, Math.Max(l.Bottom, r.Bottom));
        }

        [Fact]
        public void OperatorEqual()
        {
            for (var left = 1; left <= 100; left++)
                for (var top = 1; top <= 100; top++)
                {
                    var right = left * 2;
                    var bottom = top * 2;

                    var r = new Rectangle(left, top, right, bottom);
                    var l = new Rectangle(left, top, right, bottom);
                    var l1 = new Rectangle(left, top, right * 2, bottom);
                    var l2 = new Rectangle(left, top, right, bottom * 2);
                    var l3 = new Rectangle(left * 2, top, right, bottom);
                    var l4 = new Rectangle(left, top * 2, right, bottom);

                    Assert.True(r == l, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                    Assert.True(r != l1, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                    Assert.True(r != l2, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                    Assert.True(r != l3, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                    Assert.True(r != l4, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                }
        }

    }
}
