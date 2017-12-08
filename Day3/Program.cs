using System;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = GetDistance1(361527);
            Console.WriteLine("Result Part 1: " + result);
            
            result = GetDistance2(361527);
            Console.WriteLine("Result Part 2: " + result);
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

        private static Dictionary<int, Dictionary<int, int>> values = new Dictionary<int, Dictionary<int, int>>();
        
        static int GetDistance2(int nr)
        {
            for (var i = -400; i < 400; i++) {
                values[i] = new Dictionary<int, int>();
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

                var sumVal = GetSum(newCoord.Item1, newCoord.Item2);
                values[newCoord.Item1][newCoord.Item2] = sumVal;

                if (sumVal > nr) {
                    return sumVal;
                }

                lastCoord = newCoord;
            }

            return 0;
        }
        
        static int GetSum(int x, int y)
        {
            var vals = new List<int>();
            vals.Add(values[x+1][y]); // right
            vals.Add(values[x+1][y+1]); // up right
            vals.Add(values[x][y+1]); // up
            vals.Add(values[x-1][y+1]); // up left
            vals.Add(values[x-1][y]); // left
            vals.Add(values[x-1][y-1]); // down left
            vals.Add(values[x][y-1]); // down
            vals.Add(values[x+1][y-1]); // down right

            int sum = 0;
            foreach (var val in vals) {
                sum += val;
            }

            return sum;
        }
    }
}
