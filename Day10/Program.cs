using System;
using System.Collections.Generic;

namespace Day10
{
    class Program
    {
        // 38612 too high
        static int[] realInputLengths = new int[] {106, 118, 236, 1, 130, 0, 235, 254, 59, 205, 2, 87, 129, 25, 255, 118};
        static int[] testInputLengths = new int[] {2, 4, 1, 5};
        static int[] testInputLengths2 = new int[] {5, 0, 1, 6};
        static int[] testInputLengths3 = new int[] {2, 5};

        static int realInputLength = 256;
        static int testInputLength = 5;
        static int testInputLength2 = 6;
        static int testInputLength3 = 5;

        static List<int> theList = new List<int>();

        static void Main(string[] args)
        {
            //Console.WriteLine("Test Result part 1: " + GetResult(testInputLengths, testInputLength));
            //Console.WriteLine("Test Result part 1: " + GetResult(testInputLengths2, testInputLength2));
            //Console.WriteLine("Test Result part 1: " + GetResult(testInputLengths3, testInputLength3));
            Console.WriteLine("Result part 1: " + GetResult(realInputLengths, realInputLength));
        }

        static int GetResult(int[] lengths, int nrOfElements) 
        {
            for (var i = 0; i < nrOfElements; i++) 
            {
                theList.Add(i);
            }

            var currPosition = 0;
            var skipSize = 0;
            for (int i = 0; i < lengths.Length; i++)
            {
                var currLength = lengths[i];
                if (currLength > 1 && currLength != theList.Count) {
                    
                    var theListCopy = new List<int>(theList);

                    var k = 0;
                    for (int j = currLength - 1; j >= 0; j--)
                    {
                        var realPos = (currPosition + k) % theList.Count;
                        var revPos = (currPosition + j) % theList.Count;
                        theList[realPos] = theListCopy[revPos];
                        k++;
                    }
                }

                currPosition = (currPosition + currLength + skipSize) % theList.Count;
                skipSize++;
            }

            var first = theList[0];
            var second = theList[1];
            var result = first * second;

            return result;
        }
    }
}
