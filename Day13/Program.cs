using System;
using System.Collections.Generic;

namespace Day13
{
    class Program
    {
        static List<List<int>> FirewallLayersOriginal = new List<List<int>>();
        static List<List<int>> FirewallLayers = new List<List<int>>();
        static string[] input; 

        static void Main(string[] args)
        {
            input = System.IO.File.ReadAllLines(@"C:\Development\AdventOfCode2017\Day13\input.txt");
            SetupDataStructure();

            Console.WriteLine("Result part 1: " + GetDamage(false));

            var leastNrOfCyclesToDelay = GetWinningCycleNr();
            Console.WriteLine("Result part 2: " + leastNrOfCyclesToDelay);
        }

        static int GetWinningCycleNr() {
            var leastNrOfCyclesToDelay = 1;
            while (true) {
                if (!IsItEvenWorthToTry(leastNrOfCyclesToDelay)) {
                    leastNrOfCyclesToDelay++;
                    continue;
                }

                SetupDataStructureLight();

                for (var i = 0; i < leastNrOfCyclesToDelay; i++) 
                {
                    MoveScanner();
                }

                var damage = GetDamage(true);
                if (damage == 0) {
                    return leastNrOfCyclesToDelay;
                }

                Console.WriteLine(leastNrOfCyclesToDelay + ": " + damage);

                leastNrOfCyclesToDelay++;
            }
        }

        static bool IsItEvenWorthToTry(int nrOfDelayCycles) {
            for (var i = 0; i < FirewallLayers.Count; i++) {
                if (FirewallLayers[i].Count == 0) {
                    continue; // no worries, test next
                }

                var evilFactorVal = (FirewallLayers[i].Count - 1) * 2;
                if ((i + nrOfDelayCycles) % evilFactorVal == 0) {
                    return false; // we will hit the scanner for certain.
                }
            }

            return true;
        }

        static int GetDamage(bool onlyDamage) {
            var damage = 0;
            for (var i = 0; i < FirewallLayers.Count; i++) 
            {
                if (FirewallLayers[i].Count > 0 && FirewallLayers[i][0] != 0) {
                    if (onlyDamage) {
                        return i;
                    }

                    damage += i * FirewallLayers[i].Count;
                }

                MoveScanner();
            }

            return damage;
        }

        static void MoveScanner() {
            for (var i = 0; i < FirewallLayers.Count; i++) {
                if (FirewallLayers[i].Count == 0) {
                    continue; // No scanner in this layer
                }

                // Flytta pekaren i arrayen
                var indexGoingDown = FirewallLayers[i].IndexOf(1);
                if (indexGoingDown != -1) {
                    FirewallLayers[i][indexGoingDown] = 0;

                    if ((indexGoingDown + 1) == FirewallLayers[i].Count - 1) {
                        // Den är i botten nästa så sätt i botten men pekande uppåt
                        FirewallLayers[i][indexGoingDown+1] = -1;
                    } else {
                        FirewallLayers[i][indexGoingDown+1] = 1; // Fortsätt neråt nästa
                    }

                    continue;
                }

                var indexGoingUp = FirewallLayers[i].IndexOf(-1);
                if (indexGoingUp != -1) {
                    FirewallLayers[i][indexGoingUp] = 0;
                    if ((indexGoingUp - 1) == 0) {
                        // Den är i toppen nästa så peka den neråt
                        FirewallLayers[i][indexGoingUp-1] = 1;
                    } else {
                        FirewallLayers[i][indexGoingUp-1] = -1; // Fortsätt uppåt nästa
                    }
                }
            }
        }

        static void SetupDataStructure() {
            var lastRow = input[input.Length - 1].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
            var maxNrOfLayers = int.Parse(lastRow[0]);

            for (var i = 0; i <= maxNrOfLayers; i++) {
                FirewallLayersOriginal.Add(new List<int>());
            }

            for (var i = 0; i < input.Length; i++)
            {                                    
                var items = input[i].Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                var currNeighbours = new List<int>();

                var layerId = int.Parse(items[0]);
                var depth = int.Parse(items[1]);

                for (var j = 0; j < depth; j++) {
                    FirewallLayersOriginal[layerId].Add(0);
                }
            }
            
            for (var i = 0; i <= maxNrOfLayers; i++) {
                if (FirewallLayersOriginal[i].Count > 0) {
                    FirewallLayersOriginal[i][0] = 1;
                }
            }

            SetupDataStructureLight();
        }

        static void SetupDataStructureLight() {     
            FirewallLayers = new List<List<int>>();             
            for (var i = 0; i < FirewallLayersOriginal.Count; i++) { 
                FirewallLayers.Add(new List<int>());                      
                for (var j = 0; j < FirewallLayersOriginal[i].Count; j++) {
                    FirewallLayers[i].Add(0);
                }

                if (FirewallLayers[i].Count > 0) {
                    FirewallLayers[i][0] = 1;
                }
            }
        }
    }    
}
