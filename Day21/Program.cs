using System;
using System.Collections.Generic;

namespace Day21
{
    class Program
    {
        static List<Dictionary<string, string>> rules = new List<Dictionary<string, string>>();
        static string grid;
        
        static void Main(string[] args) {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetNrOfOnPixels());

            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 2: " + GetNrOfOnPixels2());
        }

        static int GetNrOfOnPixels() {
            for (var i = 0; i < 5; i++) {
                var gridLength = grid.Replace("/", "").Length;
                var size = gridLength % 2 == 0 ? 2 : 3;

                var subSquares = GetSubSquares(grid, size);

                var largerSubSquares = new List<string>();
                foreach (var subSquare in subSquares) {
                    largerSubSquares.Add(GetLargerSquare(subSquare, size)); 
                }

                if (largerSubSquares.Count > 1) {
                    SetCommonSquare(largerSubSquares, size);
                } else {
                    grid = largerSubSquares[0];
                }
            }

            var filteredGrid = grid.Replace(".", "").Replace("/", "");
            var nrOfOnPixels = filteredGrid.Length;
            return nrOfOnPixels;
        }

        static int GetNrOfOnPixels2() {
            for (var i = 0; i < 18; i++) {

                var gridLength = grid.Replace("/", "").Length;
                var size = gridLength % 2 == 0 ? 2 : 3;

                var subSquares = GetSubSquares(grid, size);

                var largerSubSquares = new List<string>();
                foreach (var subSquare in subSquares) {
                    largerSubSquares.Add(GetLargerSquare(subSquare, size));
                }

                if (largerSubSquares.Count > 1) {
                    SetCommonSquare(largerSubSquares, size);
                } else {
                    grid = largerSubSquares[0];
                }
            }

            var filteredGrid = grid.Replace(".", "").Replace("/", "");
            var nrOfOnPixels = filteredGrid.Length;
            return nrOfOnPixels;
        }

        static void SetCommonSquare(List<string> subSquares, int size) { 
            for (var i = 0; i < subSquares.Count; i++) {
                subSquares[i] = subSquares[i].Replace("/", "");    
            }    

            var nrOfSubSquares = subSquares.Count;
            var gridSize = (int)Math.Sqrt(nrOfSubSquares*subSquares[0].Length);
            var nrOfSquaresPerRow = (int)Math.Sqrt(nrOfSubSquares);
            size = gridSize / nrOfSquaresPerRow;            

            var newGrid = "";

            if (nrOfSubSquares > 1) {
                var newGridList = new List<string>();
                for (var i = 0; i < gridSize; i++) {
                    newGridList.Add("");
                }

                for (var i = 0; i < nrOfSquaresPerRow; i++) {
                    for (var j = 0; j < nrOfSquaresPerRow; j++) {                        
                        for (var k = 0; k < size; k++) {
                            for (var l = 0; l < size; l++) {
                                newGridList[i*size+k] = newGridList[i*size+k] + subSquares[i*nrOfSquaresPerRow+j][k*size + l];
                            }
                        }
                    }
                }
                
                newGrid = string.Join("", newGridList);
            }

            var startInsert = newGrid.Length - gridSize;
            for (var i = startInsert; i > 0; i-=gridSize) {
                newGrid = newGrid.Insert(i, "/");
            }

            grid = newGrid;
        }

        static List<string> GetSubSquares(string oldSquare, int size) {
            oldSquare = oldSquare.Replace("/", "");
            var gridSize = (int)Math.Sqrt(oldSquare.Length);
            var nrOfSubSquares = gridSize / size;        
            var nrOfSquaresPerRow = nrOfSubSquares == 1 ? 1 : nrOfSubSquares;
            nrOfSubSquares = nrOfSubSquares == 1 ? 1 : nrOfSubSquares * nrOfSubSquares;
            var subSquares = new List<string>();
            
            for (var i = 0; i < nrOfSubSquares; i++) {
                subSquares.Add("");
            }

            for (var i = 0; i < nrOfSquaresPerRow; i++) {
                for (var j = 0; j < nrOfSquaresPerRow; j++) {
                    // rader i denna subsquare
                    for (var k = 0; k < size; k++) {
                        // per char
                        for (var l = 0; l < size; l++) {
                            subSquares[i*nrOfSquaresPerRow + j] = subSquares[i*nrOfSquaresPerRow + j] + oldSquare[i*gridSize*size + j*size + k*gridSize + l];
                        }
                    }
                }  
            }

            for (var i = 0; i < nrOfSubSquares; i++) {
                for (var j = size - 2; j >= 0; j--) {
                    subSquares[i] = subSquares[i].Insert(size*j + size, "/");
                }
            }

            return subSquares;
        } 

        static string GetLargerSquare(string oldSquare, int size) {
            var newSquare = oldSquare;

            for (var i = 0; i < 4; i++) {
                newSquare = GetRotatedSquare(newSquare, size);
                if (rules[size].ContainsKey(newSquare)) {
                    return rules[size][newSquare];
                }

                newSquare = GetFlippedSquare(newSquare, size);
                if (rules[size].ContainsKey(newSquare)) {
                    return rules[size][newSquare];
                }

                newSquare = GetFlippedSquare(newSquare, size); // Flip back... just because..
            }

            return null;
        }        

        static string GetRotatedSquare(string oldSquare, int size) {
            oldSquare = oldSquare.Replace("/", "");
            var newSquare = "";

            for (var i = 0; i < size; i++) {
                for (var j = size - 1; j >= 0; j--) {
                    newSquare += oldSquare[j*size + i];
                }

                newSquare += "/";
            }

            return newSquare.Substring(0, newSquare.Length - 1);
        }

        static string GetFlippedSquare(string oldSquare, int size) {
            oldSquare = oldSquare.Replace("/", "");
            var newSquare = "";

            for (var i = 0; i < size; i++) {
                for (var j = size - 1; j >= 0; j--) {
                    newSquare += oldSquare[i*size + j];
                }

                newSquare += "/";
            }

            return newSquare.Substring(0, newSquare.Length - 1);
        }
        
        static int GetGridSize(string inputGrid) {
            return inputGrid.IndexOf("/");
        }
    
        static void SetupDataStructure(string file) {
            grid = ".#./..#/###";

            rules = new List<Dictionary<string, string>>();

            var inputs = System.IO.File.ReadAllLines(file);
            rules.Add(new Dictionary<string, string>());
            rules.Add(new Dictionary<string, string>());
            rules.Add(new Dictionary<string, string>());
            rules.Add(new Dictionary<string, string>());
            for (var i = 0; i < inputs.Length; i++) {
                var rule = inputs[i];   
                var parts = rule.Split(new string[] { " => " }, StringSplitOptions.RemoveEmptyEntries);                
                if (GetGridSize(rule) == 2) {  
                    rules[2].Add(parts[0], parts[1]);
                } else {         
                    rules[3].Add(parts[0], parts[1]);
                }
            }
        }
    }
}
