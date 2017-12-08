using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {            
            string[] programs = System.IO.File.ReadAllLines(@"C:\Development\Adventofcode2017\Day7\programs.txt");
            string[] programsTest = System.IO.File.ReadAllLines(@"C:\Development\Adventofcode2017\Day7\programsTest.txt");
            var firstProgramName = SetupDataStructure(programs);
            var programName = GetProgramAtBottom(firstProgramName);
            Console.WriteLine("Result part 1: " + programName);
                        
            var correctWeight = GetCorrectWeight(programName);
        }
        
        static Dictionary<string, Tuple<List<string>, int>> programLog = new Dictionary<string, Tuple<List<string>, int>>(); 

        static string GetProgramAtBottom(string firstProgramName) {
            var currentProgramName = firstProgramName;
            while (true) {
                var parentProgramName = GetParent(currentProgramName);

                if (parentProgramName == null) {
                    return currentProgramName;
                }

                currentProgramName = parentProgramName;
            }
        }

        static int GetCorrectWeight(string rootProgramName) {
            var currentProgramName = rootProgramName;
            while (true) {

                var children = programLog[currentProgramName].Item1;

                if (children.Count == 0) {
                    return programLog[currentProgramName].Item2;
                }

                var returnValues = new List<int>();
                var childNames = new List<string>();
                foreach (var child in children) {
                    returnValues.Add(GetCorrectWeight(child));
                    childNames.Add(child);
                }

                if (returnValues.Distinct().Skip(1).Any()) {                    

                    var weight = returnValues.First();
                    var otherWeighted = returnValues.Where(x => x != weight).ToList();
                    
                    var blackSheepWeight = 0;
                    var normalWeight = 0;
                    if (otherWeighted.Count > 1) {
                        blackSheepWeight = weight;
                        normalWeight = otherWeighted.First();
                    } else {
                        blackSheepWeight = otherWeighted.First();
                        normalWeight = weight;
                    }

                    for (var i = 0; i < returnValues.Count; i++) {

                        var childWeight = programLog[childNames[i]].Item2;
                        var childWeightPlusGrandChildren = returnValues[i];

                        if (childWeightPlusGrandChildren == blackSheepWeight) {
                            var diffBetweenTotals = normalWeight - childWeightPlusGrandChildren;
                            var correctWeight = childWeight + diffBetweenTotals;
                            Console.WriteLine("Result part 2: " + correctWeight);
                        }
                    }
                }

                return returnValues.Sum() + programLog[currentProgramName].Item2;
            }
        }

        static string GetParent(string programName) {
            var entry = programLog.FirstOrDefault(x => x.Value.Item1.Contains(programName));
            return entry.Key;
        }

        static string GetAsReadableString(List<int> list) {
            var readableString = "";
            foreach (var item in list)
            {
                readableString += item + ",";
            }

            return readableString;
        }

        static string SetupDataStructure(string[] programs) {
            var firstProgramName = "";
            foreach (var row in programs)
            {                
                var data = row.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                var programName = data[0];
                var weightString = data[1].Substring(1, data[1].Length - 2);
                var weight = int.Parse(weightString);
                var neighbours = new List<string>();

                if (data.Length > 2) {
                    for (var i = 3; i < data.Length; i++) {
                        neighbours.Add(data[i].Trim( new Char[] { ',' } ));
                    }
                }

                programLog.Add(programName, new Tuple<List<string>, int>(neighbours, weight));

                if (firstProgramName == "") {
                    firstProgramName = programName;
                }
            }

            return firstProgramName;
        }
    }
}
