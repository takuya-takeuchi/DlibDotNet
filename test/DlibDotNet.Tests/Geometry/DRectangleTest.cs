using System;
using Xunit;

namespace DlibDotNet.Tests.Geometry
{

    public class DRectangleTest : TestBase
    {

        [Fact]
        public void Create1()
        {
            var rect = new DRectangle();
            Assert.Equal(0d, rect.Left);
            Assert.Equal(0d, rect.Top);
            Assert.Equal(1d, rect.Width);
            Assert.Equal(1d, rect.Height);
        }

        [Fact]
        public void Create2()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var ret = new DRectangle(rect);
            Assert.Equal(rect.Left, left);
            Assert.Equal(rect.Top, top);
            Assert.Equal(rect.Right, right);
            Assert.Equal(rect.Bottom, bottom);
            Assert.Equal(ret.Left, left);
            Assert.Equal(ret.Top, top);
            Assert.Equal(ret.Right, right);
            Assert.Equal(ret.Bottom, bottom);
        }

        [Fact]
        public void Create3()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            Assert.Equal(rect.Left, left);
            Assert.Equal(rect.Top, top);
            Assert.Equal(rect.Right, right);
            Assert.Equal(rect.Bottom, bottom);
        }

        [Fact]
        public void Create4()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var p1 = new DPoint(left, top);
            var p2 = new DPoint(right, bottom);
            var rect = new DRectangle(p1, p2);
        }

        [Fact]
        public void Create5()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var p = new DPoint(left, top);
            var rect = new DRectangle(p);
        }

        [Fact]
        public void IsEmpty()
        {
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var rect1 = new DRectangle(left, top, right, bottom);
            Assert.True(rect1.IsEmpty);

            left = this.NextRandom(1, 100);
            top = this.NextRandom(1, 100);
            right = left * 2;
            bottom = top * 2;
            var rect2 = new DRectangle(left, top, right, bottom);
            Assert.False(rect2.IsEmpty);
        }

        [Fact]
        public void Cast()
        {
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var left = right * 2;
            var top = bottom * 2;
            var drect = new DRectangle(left, top, right, bottom);
            var rect = (Rectangle)drect;
            Assert.Equal(rect.Left, Math.Floor(drect.Left + 0.5d));
            Assert.Equal(rect.Top, Math.Floor(drect.Top + 0.5d));
            Assert.Equal(rect.Right, Math.Floor(drect.Right + 0.5d));
            Assert.Equal(rect.Bottom, Math.Floor(drect.Bottom + 0.5d));
        }

        [Fact]
        public void TopLeft()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var tl = rect.TopLeft;
            Assert.Equal(tl.X, left);
            Assert.Equal(tl.Y, top);
        }

        [Fact]
        public void TopRight()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var tr = rect.TopRight;
            Assert.Equal(tr.X, right);
            Assert.Equal(tr.Y, top);
        }

        [Fact]
        public void BottomLeft()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var bl = rect.BottomLeft;
            Assert.Equal(bl.X, left);
            Assert.Equal(bl.Y, bottom);
        }

        [Fact]
        public void BottomRight()
        {
            var left = (double)this.NextRandom(1, 100);
            var top = (double)this.NextRandom(1, 100);
            var right = (double)this.NextRandom(1, 100);
            var bottom = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(left, top, right, bottom);
            var br = rect.BottomRight;
            Assert.Equal(br.X, right);
            Assert.Equal(br.Y, bottom);
        }

        [Fact]
        public void Width()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.Equal(rect.Width, width + 1);
        }

        [Fact]
        public void Height()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.Equal(rect.Height, height + 1);
        }

        [Fact]
        public void Area()
        {
            var width = (double)this.NextRandom(1, 100);
            var height = (double)this.NextRandom(1, 100);
            var rect = new DRectangle(0, 0, width, height);
            Assert.Equal(rect.Area, (width + 1) * (height + 1));
        }

        [Fact]
        public void Intersect()
        {
            const double left = 0;
            const double top = 0;
            const double right = 100;
            const double bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);

            const double left2 = 10;
            const double top2 = 10;
            const double right2 = 30;
            const double bottom2 = 50;
            var rect2 = new DRectangle(left2, top2, right2, bottom2);

            var result = rect.Intersect(rect2);

            var left3 = Math.Max(rect.Left, rect2.Left);
            var top3 = Math.Max(rect.Top, rect2.Top);
            var right3 = Math.Min(rect.Right, rect2.Right);
            var bottom3 = Math.Min(rect.Bottom, rect2.Bottom);
            var expected = new DRectangle(left3, top3, right3, bottom3);

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
            var x = (double)this.NextRandom(1, 100);
            var y = (double)this.NextRandom(1, 100);
            var p = new DPoint(x, y);
            var rect = new DRectangle(left, top, right, bottom);
            Assert.True(rect.Contains(p));
        }

        [Fact]
        public void Contains2()
        {
            var left = 0;
            var top = 0;
            var right = 100;
            var bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);
            var rhs = new DRectangle(10, 10, 30, 30);
            Assert.True(rect.Contains(rhs));
        }

        [Fact]
        public void Center()
        {
            var left = 0;
            var top = 0;
            var right = 59;
            var bottom = 99;
            var x = 29.5;
            var y = 49.5;
            var rect = new DRectangle(left, top, right, bottom);
            var center = rect.Center;
            Assert.Equal(center.X, x);
            Assert.Equal(center.Y, y);
        }

        [Fact]
        public void DCenter()
        {
            const double left = 0;
            const double top = 0;
            const double right = 61;
            const double bottom = 101;
            const double x = 30.5d;
            const double y = 50.5d;
            DRectangle rect = new DRectangle(left, top, right, bottom);
            var center = rect.DCenter;
            Assert.Equal(center.X, x);
            Assert.Equal(center.Y, y);
        }

        [Fact]
        public void CenteredRect()
        {
            const int x = 50;
            const int y = 60;
            var p = new DPoint(x, y);
            double width = 20;
            double height = 30;

            var result = DRectangle.CenteredRect(p, width, height);

            width--;
            height--;

            var eLeft = x - width / 2;
            var eTop = y - height / 2;
            var eRight = eLeft + width - 1;
            var eBottom = eTop + height - 1;

            Assert.Equal(result.Left, eLeft);
            Assert.Equal(result.Top, eTop);
            Assert.Equal(result.Right, eRight + 1);
            Assert.Equal(result.Bottom, eBottom + 1);
        }

        [Fact]
        public void CenteredRect2()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);

            double width = 20;
            double height = 30;

            var result = DRectangle.CenteredRect(rect, width, height);

            width--;
            height--;

            var eLeft = ((left + right) / 2) - width / 2;
            var eTop = ((top + bottom) / 2) - height / 2;
            var eRight = eLeft + width - 1;
            var eBottom = eTop + height - 1;

            Assert.Equal(result.Left, eLeft);
            Assert.Equal(result.Top, eTop);
            Assert.Equal(result.Right, eRight + 1);
            Assert.Equal(result.Bottom, eBottom + 1);
        }

        [Fact]
        public void SetAspectRatio()
        {
            const double left = 0;
            const double top = 0;
            const double right = 100;
            const double bottom = 100;
            var rect = new DRectangle(left, top, right, bottom);
            rect = DRectangle.SetAspectRatio(rect, 2);

            Assert.Equal(rect.Left, -20.917784899841294);
            Assert.Equal(rect.Top, 14.791107550079353);
            Assert.Equal(rect.Right, 120.91778489984129);
            Assert.Equal(rect.Bottom, 85.208892449920654);

            try
            {
                rect = DRectangle.SetAspectRatio(rect, 0);
                Assert.True(false, $"{nameof(DRectangle.SetAspectRatio)} should throw {nameof(ArgumentOutOfRangeException)} if ration is 0");
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
            var rect = new DRectangle(left, top, right, bottom);
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
            var rect = new DRectangle(left, top, right, bottom);
            var point = new Point(x, y);

            var ret = DRectangle.Translate(rect, point);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void TranslateD()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new DRectangle(left, top, right, bottom);
            var point = new DPoint(x, y);

            var ret = rect.Translate(point);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void TranslateStaticD()
        {
            const int left = 0;
            const int top = 0;
            const int right = 100;
            const int bottom = 100;
            const int x = 10;
            const int y = 20;
            var rect = new DRectangle(left, top, right, bottom);
            var point = new DPoint(x, y);

            var ret = DRectangle.Translate(rect, point);

            Assert.Equal(ret.Left, left + x);
            Assert.Equal(ret.Top, top + y);
            Assert.Equal(ret.Right, right + x);
            Assert.Equal(ret.Bottom, bottom + y);
        }

        [Fact]
        public void OperatorAdd()
        {
            var lleft = (double)this.NextRandom(0, 100);
            var ltop = (double)this.NextRandom(0, 100);
            var lright = lleft * 2;
            var lbottom = ltop * 2;
            var rleft = (double)this.NextRandom(0, 100);
            var rtop = (double)this.NextRandom(0, 100);
            var rright = rleft * 2;
            var rbottom = rtop * 2;

            var r = new DRectangle(lleft, ltop, lright, lbottom);
            var l = new DRectangle(rleft, rtop, rright, rbottom);
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

                var r = new DRectangle(left, top, right, bottom);
                var l = new DRectangle(left, top, right, bottom);
                var l1 = new DRectangle(left, top, right * 2, bottom);
                var l2 = new DRectangle(left, top, right, bottom * 2);
                var l3 = new DRectangle(left * 2, top, right, bottom);
                var l4 = new DRectangle(left, top * 2, right, bottom);

                Assert.True(r == l, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                Assert.True(r != l1, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                Assert.True(r != l2, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                Assert.True(r != l3, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
                Assert.True(r != l4, $"left: {left}, top: {top}, right: {right}, bottom: {bottom}");
            }
        }

    }
}
