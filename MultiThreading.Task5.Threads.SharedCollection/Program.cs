/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static List<int> _sharedCollection = new();

        private static readonly object _lockObject = new();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Thread addThread = new(AddElementsToCollection);
            Thread printThread = new(PrintCollectionAfterEachAdd);

            addThread.Start();
            printThread.Start();

            addThread.Join();
            printThread.Join();

            Console.WriteLine("All operations completed.");
        }

        private static void AddElementsToCollection()
        {
            for (int i = 1; i <= 10; i++)
            {
                lock (_lockObject)
                {
                    _sharedCollection.Add(i);
                    Monitor.Pulse(_lockObject);
                }

                Thread.Sleep(100);
            }
        }

        private static void PrintCollectionAfterEachAdd()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (_lockObject)
                {
                    while (_sharedCollection.Count <= i)
                    {
                        Monitor.Wait(_lockObject);
                    }

                    Console.WriteLine("Current collection:");

                    foreach (var item in _sharedCollection)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }

                Thread.Sleep(50);
            }
        }
    }
}