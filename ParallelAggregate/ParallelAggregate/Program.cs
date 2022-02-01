using System;
using System.Linq;

namespace ParallelAggregate
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Let’s suppose this is a really long string";
            var letterFrequencies = new int[26];
            foreach (char c in text)
            {
                int index = char.ToUpper(c) - 'A';
                if (index >= 0 && index <= 26) letterFrequencies[index]++;
            };

            int[] result = text.AsParallel().Aggregate(
                () => new int[26],             // Create a new local accumulator
                (localFrequencies, c) =>       // Aggregate into the local accumulator
                {
                    int index = char.ToUpper(c) - 'A';
                    if (index >= 0 && index <= 26) localFrequencies[index]++;
                    return localFrequencies;
                },
                // Aggregate local->main accumulator
                (mainFreq, localFreq) => mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(),
                finalResult => finalResult     // Perform any final transformation
              );

            foreach(var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
