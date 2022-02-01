using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cancellation.Demos
{
    public class Tasks
    {
        public void Run()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<int> t = Task.Run(() => Sum(cts.Token, 1000000000), cts.Token);
            // Sometime later, cancel the CancellationTokenSource to cancel the Task
            //cts.Cancel(); // This is an asynchronous request, the Task may have completed already
            try
            {
                // If the task got canceled, Result will throw an AggregateException
                Console.WriteLine("The sum is: " + t.Result); // An Int32 value
            }
            catch (AggregateException x)
            {
                // Consider any OperationCanceledException objects as handled.
                // Any other exceptions cause a new AggregateException containing
                // only the unhandled exceptions to be thrown
                x.Handle(e => e is OperationCanceledException);
                // If all the exceptions were handled, the following executes
                Console.WriteLine("Sum was canceled");
            }
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
