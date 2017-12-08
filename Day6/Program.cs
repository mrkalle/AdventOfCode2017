using System;
using System.Collections.Generic;

namespace Day6
{
    class Program
    {
        static int[] banksReal = new int[] { 4, 10, 4, 1, 8, 4,	9, 14, 5, 1, 14, 15, 0,	15,	3, 5};
        static int[] banksTest1_1 = new int[] { 0, 2, 7, 0 };

        static void Main(string[] args)
        {
            // Console.WriteLine("Test1 Result part 1: " + GetNrOfReDists1(banksTest1_1));
            Console.WriteLine("Result part 1: " + GetNrOfReDists1(banksReal));

            // Console.WriteLine("Test1 Result part 2: " + GetNrOfReDists2(banksTest1_1));
            Console.WriteLine("Result part 2: " + GetNrOfReDists2(banksReal));
        }

        static int GetNrOfReDists1(int[] banks) 
        {
            var bankHistory = new List<string>();
            bankHistory.Add(GetString(banks));

            while (true) {
                // Find bank with highest value
                var highestBankIndex = 0;
                var highestBankValue = 0;                
                for (var i = 0; i < banks.Length; i++) {
                    if (banks[i] > highestBankValue) {
                        highestBankIndex = i;
                        highestBankValue = banks[i];
                    }
                }

                banks[highestBankIndex] = 0;

                // Redistribute values on banks
                for (var i = 1; i <= highestBankValue; i++) {
                    var currIndex = (highestBankIndex + i) % banks.Length;
                    banks[currIndex] = banks[currIndex] + 1;
                }

                var currBankValue = GetString(banks);
                if (bankHistory.Contains(currBankValue)) {
                    return bankHistory.Count;
                }

                bankHistory.Add(GetString(banks));
            }
        }
        
        static int GetNrOfReDists2(int[] banks) 
        {
            var bankHistory = new List<string>();
            bankHistory.Add(GetString(banks));

            while (true) {
                // Find bank with highest value
                var highestBankIndex = 0;
                var highestBankValue = 0;                
                for (var i = 0; i < banks.Length; i++) {
                    if (banks[i] > highestBankValue) {
                        highestBankIndex = i;
                        highestBankValue = banks[i];
                    }
                }

                banks[highestBankIndex] = 0;

                // Redistribute values on banks
                for (var i = 1; i <= highestBankValue; i++) {
                    var currIndex = (highestBankIndex + i) % banks.Length;
                    banks[currIndex] = banks[currIndex] + 1;
                }

                var currBankValue = GetString(banks);
                if (bankHistory.Contains(currBankValue)) {
                    var indexOfPrev = bankHistory.IndexOf(currBankValue);
                    return bankHistory.Count - indexOfPrev;
                }

                bankHistory.Add(GetString(banks));
            }
        }

        static string GetString(int[] banks) 
        {
            var result = "";
            foreach (var bankValue in banks)
            {
                result += bankValue.ToString() + "_";                
            }

            return result;
        }
    }
}
