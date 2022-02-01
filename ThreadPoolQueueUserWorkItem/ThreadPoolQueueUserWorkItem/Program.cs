using System;
using System.Threading;

namespace ThreadPoolQueueUserWorkItem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Main thread: queuing an asynchronous operation on thread={Thread.CurrentThread.ManagedThreadId}.");
            ThreadPool.QueueUserWorkItem(ComputeBoundOp, 5);
            Console.WriteLine("Main thread: Doing other work here...");
            Thread.Sleep(10000); // Simulating other work (10 seconds)

            Console.WriteLine("Hit <Enter> to end this program...");
            Console.ReadLine();
        }

        // This method's signature must match the WaitCallback delegate
        private static void ComputeBoundOp(object state)
        {
            // This method is executed by a thread pool thread
            Console.WriteLine($"In ComputeBoundOp on thread={Thread.CurrentThread.ManagedThreadId}: state={state}.");
            Thread.Sleep(1000); // Simulates other work (1 second)
                                // When this method returns, the thread goes back
                                // to the pool and waits for another task
        }
    }
}
