using Cancellation.Demos;

namespace Cancellation
{
    class Program
    {
        static void Main(string[] args)
        {
            new ThreadPoolCancellation().Run();
            new LinkedTokenSource().Run();
            new Tasks().Run();
        }
    }
}
