using System;

namespace Day11
{
    class Program
    {
        static string testInput1 = "ne,ne,ne"; // 3
        static string testInput2 = "ne,ne,sw,sw"; // 0
        static string testInput3 = "ne,ne,s,s"; //2 
        static string testInput4 = "se,sw,se,sw,sw"; // 3

        static void Main(string[] args)
        {
            var steps = System.IO.File.ReadAllLines(@"C:\Users\clundin\Documents\AoC\Day11\steps.txt")[0];

            //Console.WriteLine("Test1 Result part 1: " + GetDistanceFromOrigo(testInput1));
            //Console.WriteLine("Test2 Result part 1: " + GetDistanceFromOrigo(testInput2));
            //Console.WriteLine("Test3 Result part 1: " + GetDistanceFromOrigo(testInput3));
            //Console.WriteLine("Test4 Result part 1: " + GetDistanceFromOrigo(testInput4));
            Console.WriteLine("Result part 1: " + GetDistanceFromOrigo(steps));
        }
        
        static int GetDistanceFromOrigo(string steps)
        {
            var stepsArray = steps.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            var xCoord = 0.0;
            var yCoord = 0.0;

            foreach (var dir in stepsArray) {
                switch (dir+"") {
                    case "n": 
                        yCoord++;
                        break;
                    case "ne": 
                        xCoord += 0.5;
                        yCoord += 0.5;
                        break;
                    case "se": 
                        xCoord += 0.5;
                        yCoord -= 0.5;
                        break;
                    case "s": 
                        yCoord--;
                        break;
                    case "sw": 
                        xCoord -= 0.5;
                        yCoord -= 0.5;
                        break;
                    case "nw": 
                        xCoord -= 0.5;
                        yCoord += 0.5;
                        break;
                }
            }

            // Räkna ut hur många steg bort slutpositionen är!

            var nrOfStepsBack = 0;
            while (xCoord != 0) {
                
                // Vi vill till y-axeln för där är det enkelt att komma till origo

                if (yCoord > 0) {
                    // Övre delen av array 
                    if (xCoord > 0) {
                        // Gå snett ner till vänster
                        xCoord -= 0.5;
                        yCoord -= 0.5;
                        nrOfStepsBack++;
                        continue;
                    }

                    if (xCoord < 0) {
                        // Gå snett ner till höger
                        xCoord += 0.5;
                        yCoord -= 0.5;
                        nrOfStepsBack++;
                        continue;
                    }
                } 

                if (yCoord < 0) {
                    // Nedre delen av array 
                    if (xCoord > 0) {
                        // Gå snett upp till vänster
                        xCoord -= 0.5;
                        yCoord += 0.5;
                        nrOfStepsBack++;
                        continue;
                    }

                    if (xCoord < 0) {
                        // Gå snett upp till höger
                        xCoord += 0.5;
                        yCoord += 0.5;
                        nrOfStepsBack++;
                        continue;
                    }
                } 
            }
            
            while (yCoord != 0) {
                if (yCoord > 0) {
                    yCoord--;
                    nrOfStepsBack++;
                    continue;
                } else {
                    yCoord++;
                    nrOfStepsBack++;
                    continue;
                }
            }

            return nrOfStepsBack;
        }
    }
}
