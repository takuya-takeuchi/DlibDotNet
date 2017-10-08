using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DlibDotNet.Tests.Geometry
{

    [TestClass]
    public class PointTransformsTest : TestBase
    {

        #region PointRotator

        [TestMethod]
        public void PointRotatorCreate()
        {
            var rotator = new PointRotator();
            this.DisposeAndCheckDisposedState(rotator);
        }

        [TestMethod]
        public void PointRotatorCreate1()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
            {
                var rotator = new PointRotator(angle);
                this.DisposeAndCheckDisposedState(rotator);
            }
        }

        [TestMethod]
        public void PointRotatorM()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
            {
                var rotator = new PointRotator(angle);
                var m = rotator.M;
                Assert.AreEqual(m.MatrixElementType, MatrixElementTypes.Double);
                Assert.AreEqual(m.Rows, 2);
                Assert.AreEqual(m.Columns, 2);
                this.DisposeAndCheckDisposedState(m);
                this.DisposeAndCheckDisposedState(rotator);
            }
        }

        [TestMethod]
        public void PointRotatorOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var vector = new DPoint(x, y);
                    var rotator = new PointRotator(15);
                    var ret = rotator.Operator(vector);
                    this.DisposeAndCheckDisposedState(ret);
                    this.DisposeAndCheckDisposedState(rotator);
                    this.DisposeAndCheckDisposedState(vector);
                }
        }

        #endregion

        #region PointTransform

        [TestMethod]
        public void PointTransformCreate()
        {
            var transform = new PointTransform();
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void PointTransformCreateException()
        {
            PointTransform transform = null;

            try
            {
                transform = new PointTransform(0, null);
                Assert.Fail("PointTransformAffine should not accept null object for 2nd argument");
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

        [TestMethod]
        public void PointTransformCreate1()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        this.DisposeAndCheckDisposedState(vector);
                        this.DisposeAndCheckDisposedState(transform);
                    }
        }

        [TestMethod]
        public void PointTransformB()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        var b = transform.B;
                        Assert.AreEqual(b.X, x);
                        Assert.AreEqual(b.Y, y);
                        this.DisposeAndCheckDisposedState(b);
                        this.DisposeAndCheckDisposedState(transform);
                        this.DisposeAndCheckDisposedState(vector);
                    }
        }

        [TestMethod]
        public void PointTransformM()
        {
            for (var angle = -360d; angle <= 360d; angle += 2.5)
                for (var x = -10d; x <= 10d; x += 0.5)
                    for (var y = -10d; y <= 10d; y += 0.5)
                    {
                        var vector = new DPoint(x, y);
                        var transform = new PointTransform(angle, vector);
                        var m = transform.M;
                        Assert.AreEqual(m.MatrixElementType, MatrixElementTypes.Double);
                        Assert.AreEqual(m.Rows, 2);
                        Assert.AreEqual(m.Columns, 2);
                        this.DisposeAndCheckDisposedState(m);
                        this.DisposeAndCheckDisposedState(transform);
                        this.DisposeAndCheckDisposedState(vector);
                    }
        }

        [TestMethod]
        public void PointTransformOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var vector = new DPoint(x, y);
                    var vector2 = new DPoint(x * 2, y * 2);
                    var transform = new PointTransform(15, vector);
                    var ret = transform.Operator(vector2);
                    this.DisposeAndCheckDisposedState(ret);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(vector2);
                    this.DisposeAndCheckDisposedState(vector);
                }
        }

        #endregion

        #region PointTransformAffine

        [TestMethod]
        public void PointTransformAffineCreate()
        {
            var transform = new PointTransformAffine();
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void PointTransformAffineCreateException()
        {
            PointTransformAffine transform = null;
            DPoint vector = null;
            Matrix<double> matrix = null;

            try
            {
                vector = new DPoint();
                transform = new PointTransformAffine(null, vector);
                Assert.Fail("PointTransformAffine should not accept null object for 1st argument");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (vector != null)
                    this.DisposeAndCheckDisposedState(vector);
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }

            try
            {
                matrix = new Matrix<double>(2, 2);
                transform = new PointTransformAffine(matrix, null);
                Assert.Fail("PointTransformAffine should not accept null object for 2nd argument");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (matrix != null)
                    this.DisposeAndCheckDisposedState(matrix);
                if (vector != null)
                    this.DisposeAndCheckDisposedState(vector);
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }

            try
            {
                matrix = new Matrix<double>();
                vector = new DPoint();
                transform = new PointTransformAffine(matrix, vector);
                Assert.Fail("PointTransformAffine should not accept not 2x2 matrix");
            }
            catch
            {
                Console.WriteLine("OK");
            }
            finally
            {
                if (matrix != null)
                    this.DisposeAndCheckDisposedState(matrix);
                if (vector != null)
                    this.DisposeAndCheckDisposedState(vector);
                if (transform != null)
                    this.DisposeAndCheckDisposedState(transform);
            }
        }

        [TestMethod]
        public void PointTransformAffineCreate1()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    this.DisposeAndCheckDisposedState(matrix);
                    this.DisposeAndCheckDisposedState(vector);
                    this.DisposeAndCheckDisposedState(transform);
                }
        }

        [TestMethod]
        public void PointTransformAffineB()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    var b = transform.B;
                    Assert.AreEqual(b.X, x);
                    Assert.AreEqual(b.Y, y);
                    this.DisposeAndCheckDisposedState(b);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(vector);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        [TestMethod]
        public void PointTransformAffineM()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(2, 2);
                    var vector = new DPoint(x, y);
                    var transform = new PointTransformAffine(matrix, vector);
                    var m = transform.M;
                    Assert.AreEqual(m.MatrixElementType, MatrixElementTypes.Double);
                    Assert.AreEqual(m.Rows, 2);
                    Assert.AreEqual(m.Columns, 2);
                    this.DisposeAndCheckDisposedState(m);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(vector);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        [TestMethod]
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
                    this.DisposeAndCheckDisposedState(ret);
                    this.DisposeAndCheckDisposedState(vector2);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(vector);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        #endregion

        #region PointTransformProjective

        [TestMethod]
        public void PointTransformProjectiveCreate()
        {
            var transform = new PointTransformProjective();
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void PointTransformProjectiveCreateException()
        {
            PointTransformProjective transform = null;
            Matrix<double> matrix = null;

            try
            {
                transform = new PointTransformProjective(null);
                Assert.Fail("PointTransformProjective should not accept null object for 1st argument");
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
                Assert.Fail("PointTransformProjective should not accept not 3x3 matrix");
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

        [TestMethod]
        public void PointTransformProjectiveCreate1()
        {
            var matrix = new Matrix<double>(3, 3);
            var transform = new PointTransformProjective(matrix);
            this.DisposeAndCheckDisposedState(matrix);
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void PointTransformProjectiveM()
        {
            var matrix = new Matrix<double>(3, 3);
            var transform = new PointTransformProjective(matrix);
            var m = transform.M;
            Assert.AreEqual(m.MatrixElementType, MatrixElementTypes.Double);
            Assert.AreEqual(m.Rows, 3);
            Assert.AreEqual(m.Columns, 3);
            this.DisposeAndCheckDisposedState(m);
            this.DisposeAndCheckDisposedState(matrix);
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void PointTransformProjectiveOperator()
        {
            for (var x = -10d; x <= 10d; x += 0.5)
                for (var y = -10d; y <= 10d; y += 0.5)
                {
                    var matrix = new Matrix<double>(3, 3);
                    var transform = new PointTransformProjective(matrix);
                    var vector = new DPoint(x, y);
                    var ret = transform.Operator(vector);
                    this.DisposeAndCheckDisposedState(ret);
                    this.DisposeAndCheckDisposedState(transform);
                    this.DisposeAndCheckDisposedState(matrix);
                }
        }

        #endregion

        #region RectangleTransform

        [TestMethod]
        public void RectangleTransformCreate()
        {
            var transform = new RectangleTransform();
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void RectangleTransformCreateException()
        {
            RectangleTransform transform = null;

            try
            {
                transform = new RectangleTransform(null);
                Assert.Fail("RectangleTransform should not accept null object for 1st argument");
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

        [TestMethod]
        public void RectangleTransformCreate1()
        {
            var pointTransformAffine = new PointTransformAffine();
            var transform = new RectangleTransform(pointTransformAffine);
            this.DisposeAndCheckDisposedState(pointTransformAffine);
            this.DisposeAndCheckDisposedState(transform);
        }

        [TestMethod]
        public void RectangleTransformTransform()
        {
            var pointTransformAffine = new PointTransformAffine();
            var transform = new RectangleTransform(pointTransformAffine);
            var m = transform.Transform;
            this.DisposeAndCheckDisposedState(m);
            this.DisposeAndCheckDisposedState(transform);
            this.DisposeAndCheckDisposedState(pointTransformAffine);
        }

        [TestMethod]
        public void RectangleTransformOperator()
        {
            var l = this.NextRandom(1, 100);
            var t = this.NextRandom(1, 100);
            var r = l * 2;
            var b = t * 2;
            var rectangle = new Rectangle(l, t, r, b);
            var transform = new RectangleTransform();
            var ret = transform.Operator(rectangle);
            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(transform);
            this.DisposeAndCheckDisposedState(rectangle);
        }

        [TestMethod]
        public void RectangleTransformOperator1()
        {
            var l = this.NextRandom(1, 100);
            var t = this.NextRandom(1, 100);
            var r = l * 2;
            var b = t * 2;
            var rectangle = new DRectangle(l, t, r, b);
            var transform = new RectangleTransform();
            var ret = transform.Operator(rectangle);
            this.DisposeAndCheckDisposedState(ret);
            this.DisposeAndCheckDisposedState(transform);
            this.DisposeAndCheckDisposedState(rectangle);
        }

        #endregion

    }
}
