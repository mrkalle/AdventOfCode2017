using System;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            // var result = GetDistance1(1);
            // Console.WriteLine("Test1 Result Part 1: " + result);
            
            // result = GetDistance1(12);
            // Console.WriteLine("Test2 Result Part 1: " + result);
           
            // result = GetDistance1(23);
            // Console.WriteLine("Test3 Result Part 1: " + result);
       
            // result = GetDistance1(1024);
            // Console.WriteLine("Test4 Result Part 1: " + result);
            
            // result = GetDistance1(361527);
            // Console.WriteLine("Result Part 1: " + result);

            // var result = GetDistance2(5);
            // Console.WriteLine("Test1 Result Part 1: " + result + " should be 5");

            // result = GetDistance2(10);
            // Console.WriteLine("Test2 Result Part 1: " + result + " should be 26");
       
            // result = GetDistance2(21);
            // Console.WriteLine("Test3 Result Part 1: " + result);

            var result = GetDistance2(100);
            Console.WriteLine("Test4 Result Part 1: " + result);
            
            // var result = GetDistance2(361527);
            // Console.WriteLine("Result Part 1: " + result);
        }
        
        static int GetDistance1(int nr)
        {
            var dir = 0;
            var lastCoord = new Tuple<int,int>(0,0);
            var length = 1;
            var lengthCounter = 0;
            var lengthIndex = 0;
            for (var i = 1; i <= nr; i++) {

                if (i == nr) {
                    return Math.Abs(lastCoord.Item1) + Math.Abs(lastCoord.Item2);
                }

                Tuple<int,int> nextCoord = null;
                switch (dir) {
                    case 0:
                        nextCoord = new Tuple<int, int>(lastCoord.Item1 + 1, lastCoord.Item2);
                        break;
                    case 1:
                        nextCoord = new Tuple<int, int>(lastCoord.Item1, lastCoord.Item2 + 1);
                        break;
                    case 2:
                        nextCoord = new Tuple<int, int>(lastCoord.Item1 - 1, lastCoord.Item2);
                        break;
                    case 3:
                        nextCoord = new Tuple<int, int>(lastCoord.Item1, lastCoord.Item2 - 1);
                        break;
                }

                lengthIndex++;

                if (lengthIndex == length) {
                    dir = (dir + 1) % 4;
                    lengthCounter++;
                    lengthIndex = 0;
                    if (lengthCounter == 2) {
                        length++;
                        lengthCounter = 0;
                    }
                }

                lastCoord = nextCoord;
            }

            return 0;
        }

        private static Dictionary<int, Dictionary<int, long>> values = new Dictionary<int, Dictionary<int, long>>();
        
        static long GetDistance2(int nr)
        {
            for (var i = -400; i < 400; i++) {
                values[i] = new Dictionary<int, long>();
                for (var j = -400; j < 400; j++) {
                    values[i][j] = 0;
                }
            }

            values[0][0] = 1; 

            var dir = 0;
            var lastCoord = new Tuple<int,int,int>(0,0,1);
            var length = 1;
            var lengthCounter = 0;
            var lengthIndex = 0;
            for (var i = 1; i <= nr; i++) {

                if (i == nr) {
                    return GetSum(lastCoord.Item1, lastCoord.Item2);
                }

                Tuple<int,int,int> newCoord = null;
                switch (dir) {
                    case 0:
                        newCoord = new Tuple<int, int, int>(lastCoord.Item1 + 1, lastCoord.Item2, 0);
                        break;
                    case 1:
                        newCoord = new Tuple<int, int, int>(lastCoord.Item1, lastCoord.Item2 + 1, 0);
                        break;
                    case 2:
                        newCoord = new Tuple<int, int, int>(lastCoord.Item1 - 1, lastCoord.Item2, 0);
                        break;
                    case 3:
                        newCoord = new Tuple<int, int, int>(lastCoord.Item1, lastCoord.Item2 - 1, 0);
                        break;
                }

                lengthIndex++;

                if (lengthIndex == length) {
                    dir = (dir + 1) % 4;
                    lengthCounter++;
                    lengthIndex = 0;
                    if (lengthCounter == 2) {
                        length++;
                        lengthCounter = 0;
                    }
                }

                //Console.WriteLine("x: " + newCoord.Item1 + ", y: " + newCoord.Item2);
                values[newCoord.Item1][newCoord.Item2] = GetSum(newCoord.Item1, newCoord.Item2);
                lastCoord = newCoord;
            }

            return 0;
        }
        
        static long GetSum(int x, int y)
        {
            var vals = new List<long>();
            vals.Add(values[x+1][y]);
            vals.Add(values[x+1][y+1]);
            vals.Add(values[x][y+1]);
            vals.Add(values[x-1][y+1]);
            vals.Add(values[x-1][y]);
            vals.Add(values[x-1][y-1]);
            vals.Add(values[x][y-1]);
            vals.Add(values[x+1][y-1]);

            long sum = 0;
            foreach (var val in vals) {
                sum += val;
            }

            return sum;
        }
    }
}
