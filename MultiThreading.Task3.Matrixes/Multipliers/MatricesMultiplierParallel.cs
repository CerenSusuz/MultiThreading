using System.Threading.Tasks;
using MultiThreading.Task3.MatrixMultiplier.Matrices;

namespace MultiThreading.Task3.MatrixMultiplier.Multipliers
{
    public class MatricesMultiplierParallel : IMatricesMultiplier
    {
        public IMatrix Multiply(IMatrix m1, IMatrix m2)
        {
            var resultMatrix = new Matrix(m1.RowCount, m2.ColCount);

            Parallel.For(0, m1.RowCount, rowIndex =>
            {
                for (long colIndex = 0; colIndex < m2.ColCount; colIndex++)
                {
                    long sum = 0;
                    for (long k = 0; k < m1.ColCount; k++)
                    {
                        sum += m1.GetElement(rowIndex, k) * m2.GetElement(k, colIndex);
                    }
                    resultMatrix.SetElement(rowIndex, colIndex, sum);
                }
            });

            return resultMatrix;
        }
    }
}