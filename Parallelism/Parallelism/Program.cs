using System;
using System.Threading.Tasks;

namespace Parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Operation 1:");

            for (int i = 0; i<100; i++)
            {
                DoWork(i);
            }

            Console.WriteLine("Parallel flow:");

            Parallel.For(0, 100, i => DoWork(i));

            Console.WriteLine("Operation 2:");

            Method1();
            Method2();
            Method3();

            Console.WriteLine("Parallel flow:");

            Parallel.Invoke(
                () => Method1(),
                () => Method2(),
                () => Method3());
        }

        private static void Method1()
        {
            Console.WriteLine(nameof(Method1));
        }

        private static void Method2()
        {
            Console.WriteLine(nameof(Method2));
        }

        private static void Method3()
        {
            Console.WriteLine(nameof(Method3));
        }

        private static void DoWork(int num)
        {
            Console.WriteLine(num);
        }
    }
}
