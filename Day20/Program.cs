using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day20
{
    class Program
    {
        static int[][] p;
        static int[][] v;
        static int[][] a;
        
        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetResult());
        }

        static BigInteger GetResult() 
        {
            var lowestA = int.MaxValue;
            var lowestIndex = 0;
            for (var i = 0; i < a.Length; i++) {
                var highestA = 0;
                var aString = "";
                for (var j = 0; j < 3; j++) {
                    aString += a[i][j] + ",";
                    if (Math.Abs(a[i][j]) > highestA) {
                        highestA = Math.Abs(a[i][j]);
                    }
                }

                if (highestA < lowestA) {
                    lowestA = highestA;
                    lowestIndex = i;
                    //Console.WriteLine("found lowest, lowestA: " + lowestA + ", i: " + i + ", aString: " + aString);
                }
            }

            // Hitta nod med lägst absolut belopp och returnera det indexet

            return lowestIndex;
        }
        
        static void SetupDataStructure(string file) {
            var inputs = System.IO.File.ReadAllLines(file);
            p = new int[inputs.Length][];
            v = new int[inputs.Length][];
            a = new int[inputs.Length][];
            for (var i = 0; i < inputs.Length; i++) {
                var dataPairs = inputs[i].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);                
                for (var j = 0; j < 3; j++) {
                    var o = GetData(dataPairs[j]);   
                    if (j == 0) {
                        p[i] = new int[] {o.Item1, o.Item2, o.Item3};
                    } else if (j == 1) {
                        v[i] = new int[] {o.Item1, o.Item2, o.Item3};
                    } else if (j == 2) {
                        a[i] = new int[] {o.Item1, o.Item2, o.Item3};
                    } else {
                        throw new Exception("WTF error");
                    }          
                }
            }
        }

        static Tuple<int, int, int> GetData(string data) {
            data = data.Substring(data.IndexOf('<') + 1, data.Length - (data.IndexOf('<') + 1));
            data = data.Substring(0, data.IndexOf('>'));
            
            var values = data.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            return new Tuple<int, int, int>(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
        }
    }
}

