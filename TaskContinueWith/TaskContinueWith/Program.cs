using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskContinueWith
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and start a Task, continue with another task
            Task<int> t = Task.Run(() => Sum(CancellationToken.None, 10000));
            // ContinueWith returns a Task but you usually don't care
            Task cwt = t.ContinueWith(task => Console.WriteLine("The sum is: " + task.Result));

            //cwt.Wait();

            Console.WriteLine("End of the main thread!");
        }

        private static int Sum(CancellationToken ct, int n)
        {
            int sum = 0;
            for (; n > 0; n--)
            {
                // The following line throws OperationCanceledException when Cancel
                // is called on the CancellationTokenSource referred to by the token
                ct.ThrowIfCancellationRequested();
                checked { sum += n; } // if n is large, this will throw System.OverflowException
            }
            return sum;
        }
    }
}
