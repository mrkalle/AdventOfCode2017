using System;
using System.Collections.Generic;

namespace Day24
{
    class Program
    {
        static Dictionary<string, List<Tuple<string, string>>> ports = new Dictionary<string, List<Tuple<string, string>>>();

        static int bestBridgeWeight = 0;

        static void Main(string[] args)
        {
            SetupDataStructure("inputTest.txt");
            //SetupDataStructure("input.txt");

            // Börja på 0-noder... loopa över alla noder som har noll i sig och lägg på den på listan in
            var a = ports["0"].Count;

            FindBestBridge(new List<Tuple<string,string>>());
            Console.WriteLine("Result part 1: " + bestBridgeWeight);
        }

        static void FindBestBridge(List<Tuple<string, string>> bridgeSoFar) {
            // Om det inte finns fler noder att besöka, räkna ut vikten på "bridgeSoFar" och returnera

            // Annars lägg till en ny port och gå på djupet på den... skicka med "bridgeSoFar"
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
