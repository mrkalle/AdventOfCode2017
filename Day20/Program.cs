using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day20
{
    class Program
    {
        static int[][] p;
        static int[][] v;
        static int[][] a;

        static List<Tuple<int,int,int>> pList = new List<Tuple<int,int,int>>();
        static List<Tuple<int,int,int>> vList = new List<Tuple<int,int,int>>();
        static List<Tuple<int,int,int>> aList = new List<Tuple<int,int,int>>();
        static List<int> collisions = new List<int>();
        
        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetResult());            
            Console.WriteLine("Result part 2: " + GetResult2());
        }

        static BigInteger GetResult2() 
        {
            MarkParticlesForCollisions();
            RemoveCollidingParticles();

            for (var i = 0; i < 1000; i++) 
            {
                SetNewPositions();
                MarkParticlesForCollisions();
                RemoveCollidingParticles();
            }

            return pList.Count;
        }

        static void RemoveCollidingParticles() {
            for (var i = collisions.Count - 1; i >= 0; i--) {
                pList.RemoveAt(collisions[i]);
                vList.RemoveAt(collisions[i]);
                aList.RemoveAt(collisions[i]);
            }

            collisions = new List<int>();
        }

        static void MarkParticlesForCollisions() {
            collisions = new List<int>();
            for (var i = 0; i < pList.Count - 1; i++) {
                for (var j = i + 1; j < pList.Count; j++) {
                    if (pList[i].Item1 == pList[j].Item1 &&
                        pList[i].Item2 == pList[j].Item2 &&
                        pList[i].Item3 == pList[j].Item3) {

                        collisions.Add(i);
                        collisions.Add(j);
                    }
                }
            }

            if (collisions.Count > 0) {
                collisions = collisions.Distinct().ToList();
                collisions.Sort();
            }
        }

        static void SetNewPositions() {
            for (var i = 0; i < pList.Count; i++) {
                var vel = new int[3];
                var pos = new int[3];

                var accOld = new int[] {aList[i].Item1, aList[i].Item2, aList[i].Item3};
                var velOld = new int[] {vList[i].Item1, vList[i].Item2, vList[i].Item3};
                var posOld = new int[] {pList[i].Item1, pList[i].Item2, pList[i].Item3};

                for (var j = 0; j < 3; j++) {
                    vel[j] = velOld[j] + accOld[j];   
                    pos[j] = posOld[j] + vel[j];    
                }

                vList[i] = new Tuple<int,int,int>(vel[0], vel[1], vel[2]);
                pList[i] = new Tuple<int,int,int>(pos[0], pos[1], pos[2]);
            }
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
                        pList.Add(new Tuple<int,int,int>(o.Item1, o.Item2, o.Item3));
                    } else if (j == 1) {
                        v[i] = new int[] {o.Item1, o.Item2, o.Item3};
                        vList.Add(new Tuple<int,int,int>(o.Item1, o.Item2, o.Item3));
                    } else if (j == 2) {
                        a[i] = new int[] {o.Item1, o.Item2, o.Item3};
                        aList.Add(new Tuple<int,int,int>(o.Item1, o.Item2, o.Item3));
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

