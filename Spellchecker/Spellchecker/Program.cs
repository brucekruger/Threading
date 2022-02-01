using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Spellchecker
{
    class Program
    {
        struct IndexedWord { public string Word; public int Index; }

        static void Main(string[] args)
        {
            if (!File.Exists("WordLookup.txt"))    // Contains about 150,000 words
                new WebClient().DownloadFile("http://www.albahari.com/ispell/allwords.txt", "WordLookup.txt");

            var wordLookup = new HashSet<string>(File.ReadAllLines("WordLookup.txt"), StringComparer.InvariantCultureIgnoreCase);

            var random = new Random();
            string[] wordList = wordLookup.ToArray();

            string[] wordsToTest = Enumerable.Range(0, 1000000)
              .Select(i => wordList[random.Next(0, wordList.Length)])
              .ToArray();

            wordsToTest[12345] = "woozsh";     // Introduce a couple
            wordsToTest[23456] = "wubsie";     // of spelling mistakes.


            //PLINQ approach:
            var query = wordsToTest
              .AsParallel()
              .Select((word, index) => new IndexedWord { Word = word, Index = index })
              .Where(iword => !wordLookup.Contains(iword.Word))
              .OrderBy(iword => iword.Index);

            foreach (var word in query)
            {
                Console.WriteLine($"Index: {word.Index} - Word: {word.Word}");
            }

            //Parallel.ForEach approach:
            var misspellings = new ConcurrentBag<Tuple<int, string>>();

            Parallel.ForEach(wordsToTest, (word, state, i) =>
            {
                if (!wordLookup.Contains(word))
                {
                    misspellings.Add(Tuple.Create((int)i, word));
                }
            });

            foreach (var word in misspellings)
            {
                Console.WriteLine($"Index: {word.Item1} - Word: {word.Item2}");
            }
        }
    }
}
