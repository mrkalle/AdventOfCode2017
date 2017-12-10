using System;
using System.Collections.Generic;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {            
            string[] phrases = System.IO.File.ReadAllLines(@"C:\Users\clundin\Documents\AoC\Day4\phrases.txt");

            Console.WriteLine("Result part 1: " + GetNrOfOkPhrases1(phrases));
            Console.WriteLine("Result part 2: " + GetNrOfOkPhrases2(phrases));
        }
        
        static int GetNrOfOkPhrases1(string[] phrases)
        {     
            var nrOfOkPhrases = 0;
            foreach (var phrase in phrases) {
                var words = phrase.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var dict = new Dictionary<string, bool>();
                var isBad = false;
                foreach (var word in words) {
                    var ok = true;
                    if (!dict.TryAdd(word, ok)) {
                        isBad = true;
                        break;
                    }
                }

                if (!isBad) {
                    nrOfOkPhrases++;
                }
            }

            return nrOfOkPhrases;
        }

        static int GetNrOfOkPhrases2(string[] phrases)
        {     
            var nrOfOkPhrases = 0;
            foreach (var phrase in phrases) {
                var words = phrase.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var dict = new Dictionary<string, bool>();
                var isBad = false;
                foreach (var word in words) {                    
                    var sortedWord = string.Concat(word.OrderBy(c => c));

                    var ok = true;
                    if (!dict.TryAdd(sortedWord, ok)) {
                        isBad = true;
                        break;
                    }
                }

                if (!isBad) {
                    nrOfOkPhrases++;
                }
            }

            return nrOfOkPhrases;
        }
    }
}
