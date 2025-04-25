/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("4. Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");
            Console.WriteLine();

            Console.WriteLine("Option a) Using Thread class and Join:");
            CreateThreadsUsingJoin(10);
            Console.WriteLine();

            Console.WriteLine("Option b) Using ThreadPool and Semaphore:");
            CreateThreadsUsingThreadPoolAndSemaphore(10);
            Console.WriteLine();

            Console.ReadLine();
        }

        // OPTION A: Use Thread class and Join
        static void CreateThreadsUsingJoin(int state)
        {
            if (state == 0) return;

            var thread = new Thread(() =>
            {
                Console.WriteLine($"Thread {state} is running.");
                CreateThreadsUsingJoin(state - 1);
            });

            thread.Start();
            thread.Join();
        }

        // OPTION B: Use ThreadPool and Semaphore
        static void CreateThreadsUsingThreadPoolAndSemaphore(int state)
        {
            var semaphore = new Semaphore(1, 1); // Semaphore starts with 1 thread

            void ThreadBody(object? obj)
            {
                int currentState = (int)obj!;
                Console.WriteLine($"Thread {currentState} is running.");

                if (currentState > 1)
                {
                    semaphore.WaitOne(); // Wait for Semaphore before spawning next thread
                    ThreadPool.QueueUserWorkItem(ThreadBody, currentState - 1);
                    semaphore.Release();
                }
            }

            ThreadPool.QueueUserWorkItem(ThreadBody, state);
        }
    }
}