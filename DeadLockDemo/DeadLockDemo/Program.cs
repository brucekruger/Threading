using System;
using System.Threading;

namespace DeadLockDemo
{
    class Program
    {
        private static readonly object locker1 = new object();
        private static readonly object locker2 = new object();

        static void Main(string[] args)
        {
            var backgroundThread = new Thread(() => {
                Console.WriteLine("Starting background thread.");

                Console.WriteLine($"Setting a lock on {nameof(locker1)}.");
                lock (locker1)
                {
                    Thread.Sleep(1000);

                    lock (locker2)
                    {
                    }
                }
            });

            backgroundThread.Start();

            Console.WriteLine("Working on main thread.");

            Console.WriteLine($"Setting a lock on {nameof(locker2)}.");
            lock (locker2)
            {
                Thread.Sleep(1000);

                lock (locker1)
                {
                }
            }

            Console.WriteLine("Never getting here!");
        }
    }
}
