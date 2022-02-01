using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpinLockDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            SpinLock sl = new SpinLock();

            StringBuilder sb = new StringBuilder();

            // Action taken by each parallel job.
            // Append to the StringBuilder 10000 times, protecting
            // access to sb with a SpinLock.
            Action action = () =>
            {
                bool gotLock = false;
                for (int i = 0; i < 10000; i++)
                {
                    gotLock = false;
                    try
                    {
                        sl.Enter(ref gotLock);
                        sb.Append((i % 10).ToString());
                    }
                    finally
                    {
                        // Only give up the lock if you actually acquired it
                        if (gotLock) sl.Exit();
                    }
                }
            };

            // Invoke 3 concurrent instances of the action above
            Parallel.Invoke(action, action, action);

            //SemaphoreSlimAnalogueDemo(sb);

            // Check/Show the results
            Console.WriteLine("sb.Length = {0} (should be 30000)", sb.Length);
            Console.WriteLine("number of occurrences of '5' in sb: {0} (should be 3000)",
                sb.ToString().Where(c => c == '5').Count());
        }

        private static void SemaphoreSlimAnalogueDemo(StringBuilder sb)
        {
            using var semaphore = new SemaphoreSlim(1);
            var taskList = new List<Task>();
            for (var i = 0; i < 3; i++)
            {
                var task = Task.Run(() =>
                {
                    for (int i = 0; i < 10000; i++)
                    {
                        try
                        {
                            semaphore.Wait();
                            sb.Append((i % 10).ToString());
                        }
                        finally
                        {
                                // Only give up the lock if you actually acquired it
                                semaphore.Release();
                        }
                    }
                });

                taskList.Add(task);
            }

            Task.WaitAll(taskList.ToArray());
        }
    }
}
