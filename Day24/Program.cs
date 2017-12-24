using System;
using System.Collections.Generic;
using System.Linq;

namespace Day24
{
    class Program
    {
        static Dictionary<string, List<Tuple<string, string>>> ports = new Dictionary<string, List<Tuple<string, string>>>();
        static int bestBridgeWeight = 0;
        static int longestBridge = 0;
        static int longestBridgeWeight = 0;

        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");

            foreach (var startPort in ports["0"]) {
                var bridge = new List<Tuple<string,string>> { startPort };
                if (startPort.Item1 == "0") {
                    FindBestBridge(bridge, startPort.Item2);
                } else {
                    FindBestBridge(bridge, startPort.Item1);
                }
            }

            Console.WriteLine("Result part 1: " + bestBridgeWeight);
            Console.WriteLine("Result part 2: " + longestBridgeWeight);
        }

        static int GetBridgeWeight(List<Tuple<string,string>> bridge) {
            var weight = 0;
            foreach (var port in bridge) {
                weight += int.Parse(port.Item1) + int.Parse(port.Item2);
            }

            return weight;
        }

        static void FindBestBridge(List<Tuple<string, string>> bridge, string currOtherNumber) {
            var neighbours = ports[currOtherNumber].Where(x => !bridge.Contains(x)).ToList();
            if (neighbours.Count == 0) {
                var weight = GetBridgeWeight(bridge);
                if (weight > bestBridgeWeight) {
                    bestBridgeWeight = weight;
                }

                if (bridge.Count > longestBridge) {
                    longestBridge = bridge.Count;
                    longestBridgeWeight = weight;
                } else if (bridge.Count == longestBridge) {
                    if (weight > longestBridgeWeight) {
                        longestBridgeWeight = weight;
                    }
                }

                return;
            }

            foreach (var neighbour in neighbours)
            {
                bridge.Add(neighbour);
                if (neighbour.Item1 == currOtherNumber) {
                    FindBestBridge(bridge, neighbour.Item2);
                } else {
                    FindBestBridge(bridge, neighbour.Item1);
                }

                bridge.Remove(neighbour);
            }
        }

        static void SetupDataStructure(string file) {
            var rows = System.IO.File.ReadAllLines(file);
            for (var i = 0; i < rows.Length; i++) {                
                var dataPairs = rows[i].Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries); 

                var a = dataPairs[0];
                var b = dataPairs[1];
                var pairs = new Tuple<string,string>(a, b);

                if (!ports.ContainsKey(a)) {
                    ports.Add(a, new List<Tuple<string, string>>());
                }

                if (!ports.ContainsKey(b)) {
                    ports.Add(b, new List<Tuple<string, string>>());
                }

                var listA = ports[a];
                listA.Add(pairs);
                ports[a] = listA;

                if (a != b) {
                    var listB = ports[b];
                    listB.Add(pairs);
                    ports[b] = listB;
                }
            }
        }
    }
}
