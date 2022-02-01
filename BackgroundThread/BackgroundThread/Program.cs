using System;
using System.Threading;

namespace BackgroundThread
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new thread (defaults to foreground)
            Thread t = new Thread(Worker);
            // Make the thread a background thread
            t.IsBackground = true;
            t.Start(); // Start the thread
                       // If it is a foreground thread, the application won't die for about 10 seconds
                       // If it is a background thread, the application dies immediately
            Console.WriteLine("Returning from Main");
        }

        private static void Worker()
        {
            Thread.Sleep(10000); // Simulate doing 10 seconds of work
                                 // The line below only gets displayed if this code is executed by a foreground thread
            Console.WriteLine("Returning from Worker");
        }
    }
}
