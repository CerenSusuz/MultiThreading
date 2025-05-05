using System;
using MultiThreading.Task3.MatrixMultiplier.Matrices;
using MultiThreading.Task3.MatrixMultiplier.Multipliers;

namespace MultiThreading.Task3.MatrixMultiplier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3. Write a program, which multiplies two matrices and uses class Parallel.");
            Console.WriteLine();

            const byte matrixSize = 6;
            CompareMultiplications(matrixSize);

            Console.ReadLine();
        }

        private static void CompareMultiplications(byte matrixSize)
        {
            var firstMatrix = new Matrix(matrixSize, matrixSize, randomInit: true);
            var secondMatrix = new Matrix(matrixSize, matrixSize, randomInit: true);

            Console.WriteLine("firstMatrix:");
            firstMatrix.Print();

            Console.WriteLine("secondMatrix:");
            secondMatrix.Print();

            Console.WriteLine("\nMultiplying using synchronous implementation...");
            var regularMultiplier = new MatricesMultiplier();
            var regularResult = regularMultiplier.Multiply(firstMatrix, secondMatrix);
            Console.WriteLine("Result from synchronous multiplication:");
            regularResult.Print();

            Console.WriteLine("\nMultiplying using parallel implementation...");
            var parallelMultiplier = new MatricesMultiplierParallel();
            var parallelResult = parallelMultiplier.Multiply(firstMatrix, secondMatrix);
            Console.WriteLine("Result from parallel multiplication:");
            parallelResult.Print();
        }
    }
}