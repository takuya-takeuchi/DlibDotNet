using System;
using Xunit;

namespace DlibDotNet.Tests.Geometry
{

    public class PointTransformsTest : TestBase
    {

        #region PointRotator

        [Fact]
        public void PointRotatorCreate()
        {
            var rotator = new PointRotator();
            this.DisposeAndCheckDisposedState(rotator);
        }

        [Fact]
        public void PointRotatorCreate1()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
            {
                var rotator = new PointRotator(angle);
                this.DisposeAndCheckDisposedState(rotator);
            }
        }

        [Fact]
        public void PointRotatorM()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
            {
                var rotator = new PointRotator(angle);
                var m = rotator.M;
                Assert.Equal(m.MatrixElementType, MatrixElementTypes.Double);
                Assert.Equal(m.Rows, 2);
                Assert.Equal(m.Columns, 2);
                this.DisposeAndCheckDisposedState(m);
                this.DisposeAndCheckDisposedState(rotator);
            }
        }

        [Fact]
        public void PointRotatorOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var vector = new DPoint(x, y);
                    var rotator = new PointRotator(15);
                    var ret = rotator.Operator(vector);
                    this.DisposeAndCheckDisposedState(rotator);
                }
        }

        #endregion

        #region PointTransform

        [Fact]
        public void PointTransformCreate()
        {
            var transform = new PointTransform();
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void PointTransformCreate1()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        this.DisposeAndCheckDisposedState(transform);
                    }
        }

        [Fact]
        public void PointTransformB()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        var b = transform.B;
                        Assert.Equal(b.X, x);
                        Assert.Equal(b.Y, y);
                        this.DisposeAndCheckDisposedState(transform);
                    }
        }

        [Fact]
        public void PointTransformM()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        var m = transform.M;
                        Assert.Equal(m.MatrixElementType, MatrixElementTypes.Double);
                        Assert.Equal(m.Rows, 2);
                        Assert.Equal(m.Columns, 2);
                        this.DisposeAndCheckDisposedState(m);
                        this.DisposeAndCheckDisposedState(transform);
                    }
        }

        [Fact]
        public void PointTransformOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var vector = new DPoint(x, y);
                    var vector2 = new DPoint(x * 2, y * 2);
                    var transform = new PointTransform(15, vector);
                    var ret = transform.Operator(vector2);
                    this.DisposeAndCheckDisposedState(transform);
                }
        }

        #endregion

        #region PointTransformAffine

        [Fact]
        public void PointTransformAffineCreate()
        {
            var transform = new PointTransformAffine();
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void PointTransformAffineCreateException()
        {
            PointTransformAffine transform = null;
            DPoint vector;
            Matrix<double> matrix = null;
            
            try
            {
                matrix = new Matrix<double>();
                vector = new DPoint();
                transform = new PointTransformAffine(matrix, vector);
                Assert.True(false, "PointTransformAffine should not accept not 2x2 matrix");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (matrix != null)
                    this.DisposeAndCheckDisposedState(matrix);
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }
        }

        [Fact]
        public void PointTransformAffineCreate1()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    this.DisposeAndCheckDisposedState(matrix);
                    this.DisposeAndCheckDisposedState(transform);
                }
        }

        [Fact]
        public void PointTransformAffineB()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    var b = transform.B;
                    Assert.Equal(b.X, x);
                    Assert.Equal(b.Y, y);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        [Fact]
        public void PointTransformAffineM()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    var m = transform.M;
                    Assert.Equal(m.MatrixElementType, MatrixElementTypes.Double);
                    Assert.Equal(m.Rows, 2);
                    Assert.Equal(m.Columns, 2);
                    this.DisposeAndCheckDisposedState(m);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        [Fact]
        public void PointTransformAffineOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    var vector2 = new DPoint(x * 2, y * 2);
                    var ret = transform.Operator(vector2);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        #endregion

        #region PointTransformProjective

        [Fact]
        public void PointTransformProjectiveCreate()
        {
            var transform = new PointTransformProjective();
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void PointTransformProjectiveCreateException()
        {
            PointTransformProjective transform = null;
            Matrix<double> matrix = null;

            try
            {
                transform = new PointTransformProjective(null);
                Assert.True(false, "PointTransformProjective should not accept null object for 1st argument");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }

            try
            {
                matrix = new Matrix<double>();
                transform = new PointTransformProjective(matrix);
                Assert.True(false, "PointTransformProjective should not accept not 3x3 matrix");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (matrix != null)
                    this.DisposeAndCheckDisposedState(matrix);
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }
        }

        [Fact]
        public void PointTransformProjectiveCreate1()
        {
            var matrix = new Matrix<double>(3, 3);
            var transform = new PointTransformProjective(matrix);
            this.DisposeAndCheckDisposedState(matrix);
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void PointTransformProjectiveM()
        {
            var matrix = new Matrix<double>(3, 3);
            var transform = new PointTransformProjective(matrix);
            var m = transform.M;
            Assert.Equal(m.MatrixElementType, MatrixElementTypes.Double);
            Assert.Equal(m.Rows, 3);
            Assert.Equal(m.Columns, 3);
            this.DisposeAndCheckDisposedState(m);
            this.DisposeAndCheckDisposedState(matrix);
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void PointTransformProjectiveOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(3, 3);
                    var transform = new PointTransformProjective(matrix);
                    var vector = new DPoint(x, y);
                    var ret = transform.Operator(vector);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        #endregion

        #region RectangleTransform

        [Fact]
        public void RectangleTransformCreate()
        {
            var transform = new RectangleTransform();
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void RectangleTransformCreateException()
        {
            RectangleTransform transform = null;

            try
            {
                transform = new RectangleTransform(null);
                Assert.True(false, "RectangleTransform should not accept null object for 1st argument");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }
        }

        [Fact]
        public void RectangleTransformCreate1()
        {
            var pointTransformAffine = new PointTransformAffine();
            var transform = new RectangleTransform(pointTransformAffine);
            this.DisposeAndCheckDisposedState(pointTransformAffine);
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void RectangleTransformTransform()
        {
            var pointTransformAffine = new PointTransformAffine();
            var transform = new RectangleTransform(pointTransformAffine);
            var m = transform.Transform;
            this.DisposeAndCheckDisposedState(m);
            this.DisposeAndCheckDisposedState(transform);
            this.DisposeAndCheckDisposedState(pointTransformAffine);
        }

        [Fact]
        public void RectangleTransformOperator()
        {
            var l = this.NextRandom(1, 100);
            var t = this.NextRandom(1, 100);
            var r = l * 2;
            var b = t * 2;
            var rectangle = new Rectangle(l, t, r, b);
            var transform = new RectangleTransform();
            var ret = transform.Operator(rectangle);
            this.DisposeAndCheckDisposedState(transform);
        }

        [Fact]
        public void RectangleTransformOperator1()
        {
            var l = this.NextRandom(1, 100);
            var t = this.NextRandom(1, 100);
            var r = l * 2;
            var b = t * 2;
            var rectangle = new DRectangle(l, t, r, b);
            var transform = new RectangleTransform();
            var ret = transform.Operator(rectangle);
            this.DisposeAndCheckDisposedState(transform);
        }

        #endregion

    }
}
