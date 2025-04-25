using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Continuations demonstration:");

            var cts = new CancellationTokenSource();

            Task parentTask = CreateParentTask(cts.Token);

            // Case a: Continuation executed regardless of the result of the parent task
            parentTask.ContinueWith(task =>
            {
                Console.WriteLine("Case a: Continuation executed regardless of the result.");
            });

            // Case b: Continuation executed when the parent task finished without success
            parentTask.ContinueWith(task =>
            {
                Console.WriteLine("Case b: Continuation executed when the parent task failed.");
            }, TaskContinuationOptions.OnlyOnFaulted);

            // Case c: Continuation task reuses the parent task thread when parent task fails
            parentTask.ContinueWith(task =>
            {
                Console.WriteLine("Case c: Continuation executed on the same thread as the parent task after failure.");
                Console.WriteLine($"Current Thread ID: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            // Case d: Continuation executed outside the thread pool when the parent task is cancelled
            parentTask.ContinueWith(task =>
            {
                Console.WriteLine("Case d: Continuation executed outside the thread pool after cancellation.");
                Console.WriteLine($"Task executed by thread ID: {Thread.CurrentThread.ManagedThreadId}");
            }, TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning);

            try
            {
                parentTask.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var ex in ae.InnerExceptions)
                {
                    Console.WriteLine($"Caught Exception: {ex.Message}");
                }
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }

        /// <summary>
        /// Creates and returns a parent task.
        /// Uncomment code sections in the parent task to demonstrate different scenarios.
        /// </summary>
        private static Task CreateParentTask(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Parent Task is starting...");
                Thread.Sleep(1000);

                // Uncomment one of the following lines to test different scenarios:

                // Simulate failure
                // throw new InvalidOperationException("Simulated failure in Parent Task.");

                // Simulate cancellation
                // if (cancellationToken.IsCancellationRequested)
                // {
                //     Console.WriteLine("Parent Task was cancelled.");
                //     cancellationToken.ThrowIfCancellationRequested();
                // }

                Console.WriteLine("Parent Task completed successfully.");
            }, cancellationToken);
        }
    }
}