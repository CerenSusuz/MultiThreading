using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier.Tests
{
    [TestClass]
    public class MultiplierTest
    {
        [TestMethod]
        public void MultiplyMatrix3On3Test()
        {
            TestMatrix3On3(new MatricesMultiplier());
            TestMatrix3On3(new MatricesMultiplierParallel());
        }

        [TestMethod]
        public void ParallelEfficiencyTest()
        {
            var sizes = new[] { 10, 50, 100, 200 };

            foreach (var size in sizes)
            {
                var matrix1 = new Matrix(size, size, randomInit: true);
                var matrix2 = new Matrix(size, size, randomInit: true);

                TimeSpan regularTime = MeasureExecutionTime(() => new MatricesMultiplier().Multiply(matrix1, matrix2));
                TimeSpan parallelTime = MeasureExecutionTime(() => new MatricesMultiplierParallel().Multiply(matrix1, matrix2));

                Console.WriteLine($"Matrix Size: {size}x{size}");
                Console.WriteLine($"Regular Time: {regularTime.TotalMilliseconds} ms");
                Console.WriteLine($"Parallel Time: {parallelTime.TotalMilliseconds} ms");

                if (size > 50)
                {
                    Assert.IsTrue(parallelTime <= regularTime, $"Parallel should not be slower for size {size}x{size}.");
                }
            }
        }

        private static TimeSpan MeasureExecutionTime(Action action)
        {
            var stopwatch = Stopwatch.StartNew();
            action.Invoke();
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }

        #region private methods

        void TestMatrix3On3(IMatricesMultiplier multiplier)
        {
            var m1 = new Matrix(3, 3);
            var m2 = new Matrix(3, 3);

            m1.SetElement(0, 0, 34);
            m1.SetElement(0, 1, 2);
            m1.SetElement(0, 2, 6);

            m1.SetElement(1, 0, 5);
            m1.SetElement(1, 1, 4);
            m1.SetElement(1, 2, 54);

            m1.SetElement(2, 0, 2);
            m1.SetElement(2, 1, 9);
            m1.SetElement(2, 2, 8);

            m2.SetElement(0, 0, 12);
            m2.SetElement(0, 1, 52);
            m2.SetElement(0, 2, 85);

            m2.SetElement(1, 0, 5);
            m2.SetElement(1, 1, 5);
            m2.SetElement(1, 2, 54);

            m2.SetElement(2, 0, 5);
            m2.SetElement(2, 1, 8);
            m2.SetElement(2, 2, 9);

            var result = multiplier.Multiply(m1, m2);

            Assert.AreEqual(448, result.GetElement(0, 0));
            Assert.AreEqual(1826, result.GetElement(0, 1));
            Assert.AreEqual(3052, result.GetElement(0, 2));

            Assert.AreEqual(350, result.GetElement(1, 0));
            Assert.AreEqual(712, result.GetElement(1, 1));
            Assert.AreEqual(1127, result.GetElement(1, 2));

            Assert.AreEqual(109, result.GetElement(2, 0));
            Assert.AreEqual(213, result.GetElement(2, 1));
            Assert.AreEqual(728, result.GetElement(2, 2));
        }

        #endregion
    }
}