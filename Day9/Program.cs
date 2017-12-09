using System;
using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    class Program
    {
        static string streamTest1_1 = "<>";
        static string streamTest1_2 = "<<<<>";
        static string streamTest1_3 = "<{!>}>";
        static string streamTest1_4 = "<!!>";
        static string streamTest1_5 = "<!!!>>";
        static string streamTest1_6 = "<{o\"i!a,<{i<a>";

        static string streamTest2_1 = "{}";
        static string streamTest2_2 = "{{{}}}";
        static string streamTest2_3 = "{{},{}}";
        static string streamTest2_4 = "{{{},{},{{}}}}";
        static string streamTest2_5 = "{<{},{},{{}}>}";
        static string streamTest2_6 = "{<a>,<a>,<a>,<a>}";
        static string streamTest2_7 = "{{<a>},{<a>},{<a>},{<a>}}";
        static string streamTest2_8 = "{{<!>},{<!>},{<!>},{<a>}}";
        static string streamTest2_9 = "{{<!!>}}";

        static string streamTest3_1 = "{}";
        static string streamTest3_2 = "{{{}}}"; // 6
        static string streamTest3_3 = "{{},{}}"; // 5
        static string streamTest3_4 = "{{{},{},{{}}}}"; // 16
        static string streamTest3_5 = "{<a>,<a>,<a>,<a>}"; // 1
        static string streamTest3_6 = "{{<ab>},{<ab>},{<ab>},{<ab>}}"; // 9
        static string streamTest3_7 = "{{<!!>},{<!!>},{<!!>},{<!!>}}"; // 9
        static string streamTest3_8 = "{{<a!>},{<a!>},{<a!>},{<ab>}}"; // 3

        static string streamTest4_1 = "<>"; 
        static string streamTest4_2 = "<random characters>"; 
        static string streamTest4_3 = "<<<<>"; 
        static string streamTest4_4 = "<{!>}>"; 
        static string streamTest4_5 = "<!!>"; 
        static string streamTest4_6 = "<!!!>>"; 
        static string streamTest4_7 = "<{o\"i!a,<{i<a>"; 

        static void Main(string[] args)
        {
            var stream = System.IO.File.ReadAllLines(@"C:\Development\Adventofcode2017\Day9\stream.txt")[0];
            //var stream = streamTest4_1;

            Console.WriteLine("Result part 1: " + GetGroupScore(CleanSteam(stream)));
            Console.WriteLine("Result part 2: " + GetNrOfGarbageChars(stream));
        }
        
        static string CleanSteam(string stream)
        {
            var cleanStream = "";
            var prevChar = ' ';
            var isCleanMode = false;

            stream = stream.Replace("!!", "");

            for (var i = 0; i < stream.Length; i++)
            {
                var currChar = stream[i];
                if (!isCleanMode && currChar == '<' && prevChar != '!') {
                    isCleanMode = true; // Nu börjar vi jakt på att ta bort skit
                } else if (isCleanMode && currChar == '>' && prevChar != '!') {
                    isCleanMode = false; // Nu avslutar vi jakt på att ta bort skit
                }

                prevChar = currChar;

                if (!isCleanMode && currChar != '>') {
                    cleanStream += currChar;
                }
            }

            cleanStream = cleanStream.Replace(",", "");

            return cleanStream;
        }

        static int GetGroupScore(string stream)
        {
            var vals = new List<int>();
            var counter = 0;
            for (var i = 0; i < stream.Length; i++) {
                if (stream[i] == '{') {
                    counter++;
                    vals.Add(counter);
                } else if (stream[i] == '}') {
                    vals.Add(counter);
                    counter--;
                } 
            }

            var score = vals.Sum() / 2;

            return score;
        }

        static int GetNrOfGarbageChars(string stream)
        {
            var prevChar = ' ';
            var isCleanMode = false;

            stream = stream.Replace("!!", "");

            var nrOfGarbageChars = 0;

            for (var i = 0; i < stream.Length; i++)
            {
                var currChar = stream[i];
                if (!isCleanMode && currChar == '<' && prevChar != '!') {
                    isCleanMode = true; // Nu börjar vi jakt på att ta bort skit
                } else if (isCleanMode && currChar == '>' && prevChar != '!') {
                    isCleanMode = false; // Nu avslutar vi jakt på att ta bort skit
                } else if (isCleanMode && prevChar != '!' && currChar != '!') {
                    nrOfGarbageChars++;
                }

                prevChar = currChar;
            }
       
            return nrOfGarbageChars;
        }
    }
}
