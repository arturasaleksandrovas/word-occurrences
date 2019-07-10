using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordOccurrences
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a sentence:");
          
            var sentence = Console.ReadLine();

            string wordsOnlySentence = Regex.Replace(sentence, "[^A-Za-z0-9 ]", "");

            if (wordsOnlySentence.Trim() == string.Empty)
            {
                Console.WriteLine("Sorry, sentence without words was entered.");
                Console.ReadLine();
            }

            // ---------- Words occurences BEGIN ----------

            // If set to true words "i" and "I" will be treated as separate words, otherwise sentence format will be source of truth but "i" and "I" will be counted as same word
            var isLetterCasingImportant = false;
            var wordOccurencesDictionary = _getWordsOccurencesDictionary(wordsOnlySentence, isLetterCasingImportant);

            // Same method done with LINQ. For example purposes
            // var wordOccurencesDictionary = _getWordsOccurencesDictionaryLinq(wordsOnlySentence, isLetterCasingImportant);

            Console.WriteLine("List of word occurencies:");

            outputDictionary(wordOccurencesDictionary);

            // ---------- Words occurences END ----------

            // ---------- Word lengths occurences BEGIN ----------

            var wordLengthsOccurencies = _getWordLengthsOccurrenciesDictionary(wordsOnlySentence);

            Console.WriteLine("List of sorted word lengths occurencies:");

            var sortByKey = true;
            outputDictionary(wordLengthsOccurencies, sortByKey);

            // ---------- Word lengths occurences END ----------

            Console.ReadLine();
        }

        public static void outputDictionary<TKey>(Dictionary<TKey, int> dictionary, bool sortByKey = false)
        {
            int counter = 0;

            var keys = dictionary.Keys.ToList();

            if (sortByKey)
                keys.Sort();

            for (var i = 0; i < keys.Count; i++)
            {
                var key = keys[i];
                var value = dictionary[key];

                Console.Write(string.Format("\"{0}, {1}\"", key, value));

                // Is it last keys?
                if (i < keys.Count - 1)
                {
                    Console.Write(", ");
                    counter++;
                }
            }

            Console.WriteLine();
        }

        private static Dictionary<string, int> _getWordsOccurencesDictionary(string sentence, bool isLetterCasingImportant = false)
        {
            var wordOccurrencesDictionary = isLetterCasingImportant ? new Dictionary<string, int>() : new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            var words = sentence.Split(" ");

            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i].Trim();

                if (word.Length == 0)
                    continue;

                if (wordOccurrencesDictionary.ContainsKey(word))
                {
                    wordOccurrencesDictionary[word]++;
                }
                else
                {
                    wordOccurrencesDictionary[word] = 1;
                }
            }

            return wordOccurrencesDictionary;
        }

        private static Dictionary<string, int> _getWordsOccurencesDictionaryLinq(string sentence, bool isLetterCasingImportant = false)
        {
            var wordOccurrencesDictionary = isLetterCasingImportant ? new Dictionary<string, int>() : new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

            var words = sentence
                .Split(" ")
                .Select(word => word.Trim())
                .Where(str => str != string.Empty)
                .ToArray();

            foreach (var word in words)
            {
                if (wordOccurrencesDictionary.ContainsKey(word))
                {
                    wordOccurrencesDictionary[word]++;
                    continue;
                }

                wordOccurrencesDictionary[word] = 1;
            }

            return wordOccurrencesDictionary;
        }

        private static Dictionary<int, int> _getWordLengthsOccurrenciesDictionary(string sentence)
        {
            var wordsLengthOccurrenciesDictionary = new Dictionary<int, int>();

            var words = sentence
                .Split(" ")
                .Select(word => word.Trim())
                .Where(str => str != string.Empty)
                .ToArray();

            foreach (var word in words)
            {
                if (wordsLengthOccurrenciesDictionary.ContainsKey(word.Length))
                {
                    wordsLengthOccurrenciesDictionary[word.Length]++;
                    continue;
                }

                wordsLengthOccurrenciesDictionary[word.Length] = 1;
            }

            return wordsLengthOccurrenciesDictionary;
        }
    }
}
