using System;
using System.Collections.Generic;

namespace Day22
{
    class Program
    {
        static int maxSize = 240;

        static int currX = 0;
        static int currY = 0;
        static int currDir = 0;

        static int currNrOfInfectingBursts = 0;

        static Dictionary<Tuple<int,int>, char> originalGrid = new Dictionary<Tuple<int,int>, char>();
        static Dictionary<Tuple<int,int>, char> grid = new Dictionary<Tuple<int,int>, char>();

        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            var res = GetNrOfInfectedNodes();
            Console.WriteLine("Result part 1: " + res);

            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            res = GetNrOfInfectedNodes2();
            Console.WriteLine("Result part 2: " + res);
        }

        static int GetNrOfInfectedNodes() 
        {
            for (var i = 0; i < 10000; i++) {                               
                Turn();
                InfectOrCleanNode();
                MoveForward();
                //Console.WriteLine("{0},{1} dir {2}", currX, currY, currDir);
                //Console.WriteLine("i: {0}", i);
                //PrintGrid(grid);
            }

            return currNrOfInfectingBursts;       
        }

        static int GetNrOfInfectedNodes2() {

            //Console.WriteLine("{0},{1} dir {2}", currX, currY, currDir);
            //Console.WriteLine("i: {0}", "start");
            //PrintGrid(grid);

            for (var i = 0; i < 10000000; i++) {                               
                Turn2();
                ChangeState();
                MoveForward();
                //Console.WriteLine("{0},{1} dir {2}", currX, currY, currDir);
                //Console.WriteLine("i: {0}", i);
                //PrintGrid(grid);
            }
        
            //PrintGrid(grid);
            
            return currNrOfInfectingBursts;       
        }

        static int GetResult() {
            var result = 0;

            for (var i = -maxSize; i < maxSize; i++) {             
                for (var j = -maxSize; j < maxSize; j++) {
                    var currPos = new Tuple<int,int>(i,j);
                    if (originalGrid[currPos] == '.' && grid[currPos] == '#') {
                        result++;
                    }
                }
            }

            return result;
        }

        static void Turn() {
            var isInfected = grid[new Tuple<int,int>(currX, currY)] == '#';

            if (isInfected) {
                currDir++;

                if (currDir == 4) {
                    currDir = 0;
                }
            } else {
                currDir--;

                if (currDir == -1) {
                    currDir = 3;
                }
            }
        }

        static void Turn2() {
            var startState = grid[new Tuple<int,int>(currX, currY)];

            switch (startState) {
                case '.':
                    currDir--;

                    if (currDir == -1)
                    {
                        currDir = 3;
                    }

                    return;
                case 'W':
                    return;
                case '#':
                    currDir++;

                    if (currDir == 4) {
                        currDir = 0;
                    }

                    return;
                case 'F':
                    switch (currDir) {
                        case 0:
                            currDir = 2;
                            return;
                        case 1:
                            currDir = 3;
                            return;
                        case 2:
                            currDir = 0;
                            return;
                        case 3:
                            currDir = 1;
                            return;
                    }

                    throw new Exception("WTF weird dir on state..");
            }
            
            throw new Exception("WTF Faulty state: " + startState);
        }

        static void InfectOrCleanNode() {
            if (grid[new Tuple<int,int>(currX, currY)] == '.') {
                grid[new Tuple<int,int>(currX, currY)] = '#';
                currNrOfInfectingBursts++;
            } else {                
                grid[new Tuple<int,int>(currX, currY)] = '.';
            }
        }

        static void ChangeState() {            
            var startState = grid[new Tuple<int,int>(currX, currY)];
            switch (startState) {
                case '.':
                    grid[new Tuple<int,int>(currX, currY)] = 'W';
                    return;
                case 'W':
                    grid[new Tuple<int,int>(currX, currY)] = '#';
                    currNrOfInfectingBursts++;
                    return;
                case '#':
                    grid[new Tuple<int,int>(currX, currY)] = 'F';
                    return;
                case 'F':
                    grid[new Tuple<int,int>(currX, currY)] = '.';
                    return;
            }
            
            throw new Exception("WTF Faulty state: " + startState);
        }

        static void MoveForward() {
            switch (currDir) {
                case 0:
                    currX--;
                    return;
                case 1: 
                    currY++;
                    return;
                case 2:
                    currX++;
                    return;
                case 3: 
                    currY--;
                    return;
                default:
                    throw new Exception("WTF DIR " + currDir);
            }
        }
        
        static void SetupDataStructure(string file) {
            currX = 0;
            currY = 0;
            currDir = 0;
            currNrOfInfectingBursts = 0;

            originalGrid = new Dictionary<Tuple<int,int>, char>();
            grid = new Dictionary<Tuple<int,int>, char>();

            var inputs = System.IO.File.ReadAllLines(file);
            var xOffset = inputs.Length/2; 
            var yOffset = xOffset;

            for (var i = -maxSize; i < maxSize; i++) {
                for (var j = -maxSize; j < maxSize; j++) {
                    grid.Add(new Tuple<int,int>(i, j), '.');
                    originalGrid.Add(new Tuple<int,int>(i, j), '.');
                }
            }

            for (var i = 0; i < inputs.Length; i++) {
                var row = inputs[i];                
                for (var j = 0; j < row.Length; j++) {
                    grid[new Tuple<int,int>(i-xOffset, j-yOffset)] = row[j];
                    originalGrid[new Tuple<int,int>(i-xOffset, j-yOffset)] = row[j];
                }
            }
        }

        static void PrintGrid(Dictionary<Tuple<int,int>, char> printGrid) {
            for (var i = -maxSize; i < maxSize; i++) {             
                for (var j = -maxSize; j < maxSize; j++) {
                    if (i == currX && j == currY) {
                        Console.Write(printGrid[new Tuple<int,int>(i, j)] + "<");
                    } else {                        
                        Console.Write(printGrid[new Tuple<int,int>(i, j)] + " ");
                    }
                }

                Console.WriteLine("");
            }

            Console.WriteLine("------------------------------------------");
        }
    }
}


// . . . . . . . . .
// . . . . . . . . .
// . . . . . . . . .
// . . . . . # . . .
// . . . #[.]. . . .
// . . . . . . . . .
// . . . . . . . . .
// . . . . . . . . .


// . . . . . # # . .
// . . . . # . . # .
// . . . # . . . . #
// . . # . #[.]. . #
// . . # . # . . # .
// . . . . . # # . .
// . . . . . . . . .
// . . . . . . . . .
