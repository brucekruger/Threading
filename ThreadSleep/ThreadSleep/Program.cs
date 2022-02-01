using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSleep
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Started execution with thread {Thread.CurrentThread.ManagedThreadId}");

            Task.Run(() => ThreadSleep());

            var threadSwitched = Task.Run(() => ThreadYield()).Result;

            Task.Run(() => ThreadSpinWait());

            Console.WriteLine($"Finishing execution with thread {Thread.CurrentThread.ManagedThreadId}");

            Console.ReadKey();
        }

        private static void ThreadSleep()
        {
            Console.WriteLine($"Started execution of {nameof(ThreadSleep)} with thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.Sleep(1);

            Console.WriteLine($"Finishing execution of {nameof(ThreadSleep)} with thread {Thread.CurrentThread.ManagedThreadId}");
        }

        private static bool ThreadYield()
        {
            Console.WriteLine($"Started execution of {nameof(ThreadYield)} with thread {Thread.CurrentThread.ManagedThreadId}");

            var threadSwitched = Thread.Yield();
            Console.WriteLine($"Switching thread: {threadSwitched}");

            Console.WriteLine($"Finishing execution of {nameof(ThreadYield)} with thread {Thread.CurrentThread.ManagedThreadId}");

            return threadSwitched;
        }

        private static void ThreadSpinWait()
        {
            Console.WriteLine($"Started execution of {nameof(ThreadSpinWait)} with thread {Thread.CurrentThread.ManagedThreadId}");

            Thread.SpinWait(100);

            Console.WriteLine($"Finishing execution of {nameof(ThreadSpinWait)} with thread {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
