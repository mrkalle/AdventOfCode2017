using System;
using System.Collections.Generic;

namespace Day21
{
    class Program
    {
        static List<Dictionary<string, string>> rules = new List<Dictionary<string, string>>();
        static string grid = ".#./..#/###";
        
        static void Main(string[] args)
        {
            SetupDataStructure("inputTest.txt");
            //SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetNrOfOnPixels());
        }

        static int GetNrOfOnPixels() {
            for (var i = 0; i < 5; i++) {
                var gridSize = GetGridSize(grid);
                var size = gridSize % 2 == 0 ? 2 : 3;

                var subSquares = GetSubSquares(grid, size); // Divide into subs

                var largerSubSquares = new List<string>();
                foreach (var subSquare in subSquares) {
                    largerSubSquares.Add(GetLargerSquare(subSquare, size)); // Make larger
                }

                if (largerSubSquares.Count > 1) {
                    SetCommonSquare(largerSubSquares, size + 1); // Unite
                } else {
                    grid = largerSubSquares[0];
                }
            }

            var nrOfOnPixels = grid.Replace(".", "").Length;
            return nrOfOnPixels;
        }

        static void SetCommonSquare(List<string> subSquares, int size) {
            var nrOfSubSquares = subSquares.Count;
            var gridSize = GetGridSize(subSquares[0]) * 2;
            var nrOfSquaresPerRow = nrOfSubSquares/2;
            
            for (var i = 0; i < subSquares.Count; i++) {
                subSquares[i] = subSquares[i].Replace("/", "");    
            }     

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
            var gridSize = GetGridSize(oldSquare);
            var nrOfSubSquares = gridSize / size;        
            var nrOfSquaresPerRow = nrOfSubSquares == 1 ? 1 : nrOfSubSquares;
            nrOfSubSquares = nrOfSubSquares == 1 ? 1 : nrOfSubSquares * nrOfSubSquares;
            oldSquare = oldSquare.Replace("/", "");
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
                            subSquares[i*size + j] = subSquares[i*size + j] + oldSquare[i*gridSize*size + j*size + k*gridSize + l];
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
