/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            ExecuteTasks();

            Console.ReadLine();
        }

        static void ExecuteTasks()
        {
            var random = new Random();

            var task1 = Task.Run(() =>
            {
                int[] array = Enumerable.Range(0, 10).Select(_ => random.Next(1, 101)).ToArray();
                Console.WriteLine("First Task: Array created: " + string.Join(", ", array));

                return array;
            });

            var task2 = task1.ContinueWith(previousTask =>
            {
                int multiplier = random.Next(1, 11);
                int[] multipliedArray = previousTask.Result.Select(x => x * multiplier).ToArray();
                Console.WriteLine($"Second Task: Array multiplied by {multiplier}: " + string.Join(", ", multipliedArray));

                return multipliedArray;
            });

            var task3 = task2.ContinueWith(previousTask =>
            {
                int[] sortedArray = previousTask.Result.OrderBy(x => x).ToArray();
                Console.WriteLine("Third Task: Array sorted: " + string.Join(", ", sortedArray));

                return sortedArray;
            });

            var task4 = task3.ContinueWith(previousTask =>
            {
                double average = previousTask.Result.Average();
                Console.WriteLine("Fourth Task: Average value: " + average);
            });

            task4.Wait();
        }
    }
}
