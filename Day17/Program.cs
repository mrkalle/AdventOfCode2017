using System;
using System.Collections.Generic;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Result part 1: " + GetResult(376));
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
                    Console.WriteLine(currPos + ": " + val);
                }
            } 

            return theList[currPos + 1];
        }
        
// 1, 23832
// 1, 31082
// 1, 48691
// 1, 53719
// 1, 60700
// 1, 442596


// 1: 37
// 1: 239
// 1: 361
// 1: 392
// 1: 823
// 1: 950
// 1: 1612
        static int GetResult2(int steps) {
            //var listSize = 442596;
            
            // var listSize = 392 + 1;
            // //steps = steps;
            // var currPos = 1;

            // var listSize = 3 + 1;
            // steps = 3;
            // var currPos = 2;

            var listSize = 23832 + 1;
            var currPos = 1;
            
            var lastAddedOnPos1 = listSize - 1;

            while (true) {
                currPos += steps;

                if (currPos == listSize - 1) {
                    listSize++;
                    //Console.WriteLine("hit last pos = " + listSize);
                } else if (currPos >= listSize) {
                    currPos = currPos - listSize; 
                    if (currPos == 0) {
                        lastAddedOnPos1 = listSize;
                        Console.WriteLine("1 pos = " + listSize);
                    }

                    listSize++;
                    currPos++;                    
                } else {
                    currPos++;
                    listSize++;
                    //Console.WriteLine("normal add");
                }

                //Console.WriteLine("round complete, " + currPos + ", " + listSize);

                if (listSize > 50000) {
                    return lastAddedOnPos1;
                }
            }
        }
    }
}
