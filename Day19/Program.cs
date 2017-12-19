using System;
using System.Collections.Generic;

namespace Day19
{
    class Program
    {
        //18132 too high
        static char[][] theMap;
        static List<char> chars = new List<char>();
        static int startCol = 0;
        static List<char> letters = new List<char> {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        static int nrOfSteps = 0;
        static void Main(string[] args)
        {
            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetResult());
            Console.WriteLine("Result part 2: " + nrOfSteps);
        }

        static string GetResult() {
            var dir = 2;

            var rowNr = 0;
            var colNr = startCol;

            while (true) {
                try {
                    var next = GetNext(dir, rowNr, colNr);                    
                    dir = next.Item1;
                    rowNr = next.Item2;
                    colNr = next.Item3;  
                    nrOfSteps++;
                }
                catch (Exception e) {
                    return string.Join("", chars.ToArray());
                }              
            }            
        }

        static Tuple<int, int, int> GetNext(int dir, int currX, int currY) {
            int nextX = 0;
            int nextY = 0;
            int nextDir = dir;
            switch (dir) {
                case 0:
                    nextX = currX - 1;
                    nextY = currY;
                    break;
                case 1:
                    nextX = currX;
                    nextY = currY + 1;
                    break;
                case 2:
                    nextX = currX + 1;
                    nextY = currY;
                    break;
                case 3:
                    nextX = currX;
                    nextY = currY - 1;
                    break;
                default:
                    throw new Exception("WTF crazy dir!");
            }

            if (theMap[nextX][nextY] == '+') {
                nextDir = GetNewDir(dir, nextX, nextY);
            } else {
                nextDir = dir;
                if (letters.Contains(theMap[nextX][nextY])) {
                    chars.Add(theMap[nextX][nextY]);
                }
            }

            return new Tuple<int, int, int>(nextDir, nextX, nextY);
        }

        static int GetNewDir(int oldDir, int x, int y) 
        {
            // Going up, go left or right?
            if (oldDir == 0 && y > 0 && theMap[x][y-1] != ' ') {
                return 3;
            } else if (oldDir == 0 && y < theMap[x].Length - 1 && theMap[x][y+1] != ' ') {
                return 1;
            } 

            // Going down, go left or right?
            if (oldDir == 2 && y > 0 && theMap[x][y-1] != ' ') {
                return 3;
            } else if (oldDir == 2 && y < theMap[x].Length - 1 && theMap[x][y+1] != ' ') {
                return 1;
            } 

            // Going right, go up or down?
            if (oldDir == 1 && x > 0 && theMap[x-1][y] != ' ') {
                return 0;
            } else if (oldDir == 1 && x < theMap.Length - 1 && theMap[x+1][y] != ' ') {
                return 2;
            }

            // Going left, go up or down?
            if (oldDir == 3 && x > 0 && theMap[x-1][y] != ' ') {
                return 0;
            } else if (oldDir == 3 && x < theMap.Length - 1 && theMap[x+1][y] != ' ') {
                return 2;
            }
            
            throw new Exception("FOUND EXIT");
        }

        static void SetupDataStructure(string file) {
            var rows = System.IO.File.ReadAllLines(file);
            theMap = new char[rows.Length][];
            for (var i = 0; i < rows.Length; i++) {

                var columns = rows[i].ToCharArray();
                theMap[i] = new char[columns.Length];

                for (var j = 0; j < columns.Length; j++) {
                    theMap[i][j] = columns[j];
                    if (i == 0) {
                        if (theMap[i][j] == '|') {
                            startCol = j;
                        }
                    }
                } 
            }
        }
    }
}
