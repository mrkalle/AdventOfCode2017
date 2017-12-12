using System;
using System.Collections.Generic;
using System.Linq;

namespace Day12
{
    class Program
    {
        static List<Tuple<bool, List<int>>> neighbourList = new List<Tuple<bool, List<int>>>();
        static List<int> visitedNeighbours = new List<int>();
        static int nrOfGroups = 0;

        static void Main(string[] args)
        {
            var neighbours = System.IO.File.ReadAllLines(@"C:\Users\clundin\Documents\AoC\Day12\neighbours.txt");
            SetupDataStructure(neighbours);

            GetGroupSize(0);
            Console.WriteLine("Result part 1: " + visitedNeighbours.Count);

            while (neighbourList.Any(x => x.Item1 == false)) 
            {                
                var firstElementIndex = 0;
                for (var i = 0; i < neighbourList.Count; i++) {
                    if (neighbourList[i].Item1 == false) {
                        firstElementIndex = i;
                        break;
                    }
                }

                GetGroupSize(firstElementIndex);
                nrOfGroups++;

                for (var i = 0; i < visitedNeighbours.Count; i++) {
                    neighbourList[visitedNeighbours[i]] = new Tuple<bool, List<int>>(true, neighbourList[visitedNeighbours[i]].Item2);
                }         

                visitedNeighbours = new List<int>();               
            }

            Console.WriteLine("Result part 2: " + nrOfGroups);
        }
        
        static void GetGroupSize(int id)
        {
            visitedNeighbours.Add(id);

            var neighbours = GetNeighboursToVisit(id);

            if (neighbours.Count == 0) {
                return;
            }

            foreach (var neighbourId in neighbours)
            {
                GetGroupSize(neighbourId);
            }
        }

        static List<int> GetNeighboursToVisit(int id) {
            var unvisitedNeighbours = new List<int>();
            
            foreach (var neighbourId in neighbourList[id].Item2)
            {
                if (!visitedNeighbours.Contains(neighbourId)) {
                    unvisitedNeighbours.Add(neighbourId);
                }
            }

            return unvisitedNeighbours;
        }

        static void SetupDataStructure(string[] neighbours)
        {
            for (var i = 0; i < neighbours.Length; i++)
            {                    
                var items = neighbours[i].Split(new string[] { ",", " ", "<->" }, StringSplitOptions.RemoveEmptyEntries);
                var currNeighbours = new List<int>();

                if (i != int.Parse(items[0])) {
                    throw new Exception("WTF");
                }

                for (var j = 1; j < items.Length; j++)
                {                    
                    currNeighbours.Add(int.Parse(items[j]));
                }

                neighbourList.Add(new Tuple<bool, List<int>>(false, currNeighbours));
            }
        }
    }
}
