using System;
using System.Collections.Generic;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Result part 1: " + GetResult(376));
            Console.WriteLine("Result part 2: " + GetResult2(376));
        }

        static int GetResult(int steps) {
            var theList = new List<int>() { 0 };
            var currPos = 0;

            while (theList.Count < 2018) {
                for (var i = 0; i < steps; i++) {
                    currPos = (currPos + 1) % theList.Count;
                }

                currPos++;

                var val = theList.Count;

                theList.Insert(currPos, theList.Count);

                if (currPos == 1) {
                    //Console.WriteLine(currPos + ": " + val);
                }
            } 

            return theList[currPos + 1];
        }

        static int GetResult2(int steps) {
            var lastValOnPos1 = 1;
            var index = 1;

            for (var i = 2; i < 50000000; i++) {
                index = (steps + index) % i; 

                if (index == 0)  {
                    lastValOnPos1 = i;
                    //Console.WriteLine("New pos 1 value: " + i);
                }

                index++;
            }

            return lastValOnPos1;
        }
    }
}
