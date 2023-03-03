using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Lab3_sem4._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string directoryPath = @".\files\";
            var fileNames = Directory.GetFiles(directoryPath);

            Func<string, IEnumerable<string>> tokenizer = text =>
            {
                var separators = new[] { ' ', '\r', '\n', '\t', '.', ',', ';', ':', '-', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '|', '_', '@', '#', '$', '%', '^', '&', '*', '+', '=', '`', '~', '\"', '\'' };
                return text.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(w => w.ToLower());
            };

            Func<IEnumerable<string>, IDictionary<string, int>> frequencyCounter = words =>
            {
                var frequencyDictionary = new Dictionary<string, int>();
                foreach (var word in words)
                {
                    if (frequencyDictionary.ContainsKey(word))
                    {
                        frequencyDictionary[word]++;
                    }
                    else
                    {
                        frequencyDictionary[word] = 1;
                    }
                }
                return frequencyDictionary;
            };

            Action<IDictionary<string, int>> reportGenerator = frequencyDictionary =>
            {
                Console.WriteLine("{0,-15}{1,-15}", "Слово", "Частота");
                foreach (var item in frequencyDictionary.OrderByDescending(i => i.Value))
                {
                    Console.WriteLine("{0,-15}{1,-15}", item.Key, item.Value);
                }
            };

            foreach (var fileName in fileNames)
            {
                var text = File.ReadAllText(fileName);
                var words = tokenizer(text);
                var frequencies = frequencyCounter(words);

                Console.WriteLine("Список частоти файлів у: {0}", fileName);
                reportGenerator(frequencies);
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}