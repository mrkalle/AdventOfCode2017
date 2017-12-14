using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Day14
{
    class Program
    {
        static string input = "wenycdww";
        //static string input = "flqrgnkx"; // test input

        static Dictionary<int, List<Tuple<int,int>>> regions = new Dictionary<int, List<Tuple<int, int>>>();
        static List<List<Tuple<bool, bool>>> disk = new List<List<Tuple<bool, bool>>>();

        static void Main(string[] args)
        {
            var sum = 0;
            var hashInputs = new string[128];
            for (var i = 0; i < 128; i++) {
                disk.Add(new List<Tuple<bool,bool>>());
                var currHash = input + "-" + i;
                var hash = GetHash(currHash);
                var hashAsBitArray = GetHashAsBitArray(hash);
                sum += GetNrOfTrueForBitArray(hashAsBitArray);

                for (var j = 0; j < 128; j++) {
                    disk[i].Add(new Tuple<bool, bool>(false, hashAsBitArray[j]));
                }
            }

            Console.WriteLine("Result part 1: " + sum);

            var currId = 0;
            for (var i = 0; i < 128; i++) {
                for (var j = 0; j < 128; j++) {
                    if (disk[i][j].Item1 == true) {
                        continue; // Square already visited
                    }

                    if (disk[i][j].Item2 == false) {
                        continue; // Uninteresting square (not true)
                    }

                    AddToCurrentRegion(i, j, currId);

                    currId++;
                }
            }

            Console.WriteLine("Result part 2: " + currId);
        }

        static void AddToCurrentRegion(int x, int y, int currId) {      
            if (!regions.ContainsKey(currId)) {
                regions.Add(currId, new List<Tuple<int, int>>());
            }

            regions[currId].Add(new Tuple<int, int>(x , y));
            disk[x][y] = new Tuple<bool, bool>(true, disk[x][y].Item2);

            var neighbours = GetNeighboursForNode(x, y);
            if (neighbours.Count == 0) {
                return;
            }

            foreach (var neighbour in neighbours) {
                AddToCurrentRegion(neighbour.Item1, neighbour.Item2, currId);
            }
        }

        static List<Tuple<int, int>> GetNeighboursForNode(int x, int y) {
            // Search for neighbours in all four directions
            var neighbours = new List<Tuple<int, int>>();
            for (var i = 0; i < 4; i++) {
                var neighbour = GetNeighbour(i, x, y);
                if (neighbour != null) {
                    neighbours.Add(neighbour);
                }
            }

            return neighbours;
        }

        static Tuple<int, int> GetNeighbour(int dir, int x, int y) {
            switch (dir) {
                case 0:
                    if (x == 0) {
                        return null;
                    }
                
                    return GetNeighbourOrNull(x - 1, y);
                case 1:
                    if (y == 127) {
                        return null;
                    }
                 
                    return GetNeighbourOrNull(x, y + 1);
                case 2:
                    if (x == 127) {
                        return null;
                    }
             
                    return GetNeighbourOrNull(x + 1, y);
                case 3:
                    if (y == 0) {
                        return null;
                    }
                
                    return GetNeighbourOrNull(x, y - 1);
            }

            throw new Exception("Faulty direction");
        }

        static Tuple<int, int> GetNeighbourOrNull(int x, int y) {
            var neighbour = disk[x][y];
            if (neighbour.Item1 == true || neighbour.Item2 == false) {
                return null;
            }

            return new Tuple<int, int>(x , y);
        }

        static int GetNrOfTrueForBitArray(BitArray inputBitArray) {
            var sum = 0;
            for (var i = 0; i < inputBitArray.Count; i++) {
                if (inputBitArray[i]) {
                    sum++;
                }
            }

            return sum;
        }
        
        static BitArray GetHashAsBitArray(string hash) {
            var bitArray = new BitArray(new byte[16]);
            for (var i = 0; i < 32; i++) {
                var res = GetBitsFromHex(hash[i]);
                for (var j = 0; j < 4; j++) {
                    bitArray[i*4+j] = res[j];
                }
            }

            return bitArray;
        }

        static string GetHash(string hashkey) {
            var realInputLength = 256;
            
            // This is the 256 long array... 1, 2, 3, 4 etc
            var list = new int[256];
            for (var i = 0; i < realInputLength; i++) {
                list[i] = i;
            }

            // Add extra lengths
            var extraLengths = new byte[] { 17, 31, 73, 47, 23};
            var bytes = Encoding.ASCII.GetBytes(hashkey); 
            List<byte> tempList = new List<byte>(bytes.Length + extraLengths.Length);
            tempList.AddRange(bytes);
            tempList.AddRange(extraLengths);
            bytes = tempList.ToArray();
            var realInputLengths = GetIntArray(bytes);

            // Scramble 64 times
            var currPosition = 0; 
            var skipSize = 0;
            for (var i = 0; i < 64; i++) {
                var result2 = ScrambleOneRound(list, realInputLengths, currPosition, skipSize);
                list = result2.Item1;
                currPosition = result2.Item2;
                skipSize = result2.Item3;
            }

            // XOR
            var xorList = new int[16];
            for (var i = 0; i < 16; i++) {
                var xorValue = list[i*16];
                for (var j = 1; j < 16; j++) {
                    xorValue = xorValue ^ list[i*16 + j];
                }

                xorList[i] = xorValue;
            }

            // To string
            string finalHash = BitConverter.ToString(GetByteArray(xorList)).Replace("-", string.Empty).ToLower();
            return finalHash;
        }
        
        static Tuple<int[], int, int> ScrambleOneRound(int[] list, int[] lengths, int currPosition, int skipSize) 
        {
            for (int i = 0; i < lengths.Length; i++)
            {
                var currLength = lengths[i];
                if (currLength > 1 && currLength != list.Length) {                    
                    int[] listCopy = new int[list.Length];
                    Array.Copy(list, listCopy, list.Length);                    

                    var k = 0;
                    for (int j = currLength - 1; j >= 0; j--)
                    {
                        var realPos = (currPosition + k) % list.Length;
                        var revPos = (currPosition + j) % list.Length;
                        list[realPos] = listCopy[revPos];
                        k++;
                    }
                }

                currPosition = (currPosition + currLength + skipSize) % list.Length;
                skipSize++;
            }

            return new Tuple<int[], int, int>(list, currPosition, skipSize);
        }

        static int[] GetIntArray(byte[] bytes) {
            var ints = new int[bytes.Length];
            for (var i = 0; i < bytes.Length; i++) {
                ints[i] = (int)bytes[i];
            }

            return ints;
        }

        static byte[] GetByteArray(int[] ints) {
            var bytes = new byte[ints.Length];
            for (var i = 0; i < ints.Length; i++) {
                bytes[i] = (byte)ints[i];
            }

            return bytes;
        }

        static BitArray GetBitsFromHex(char hexChar) {
            switch(hexChar) {
                case '0': return new BitArray(new bool[] { false, false, false, false});
                case '1': return new BitArray(new bool[] { false, false, false, true});
                case '2': return new BitArray(new bool[] { false, false, true, false});
                case '3': return new BitArray(new bool[] { false, false, true, true});
                case '4': return new BitArray(new bool[] { false, true, false, false});
                case '5': return new BitArray(new bool[] { false, true, false, true});
                case '6': return new BitArray(new bool[] { false, true, true, false});
                case '7': return new BitArray(new bool[] { false, true, true, true});
                case '8': return new BitArray(new bool[] { true, false, false, false});
                case '9': return new BitArray(new bool[] { true, false, false, true});
                case 'a': return new BitArray(new bool[] { true, false, true, false});
                case 'b': return new BitArray(new bool[] { true, false, true, true});
                case 'c': return new BitArray(new bool[] { true, true, false, false});
                case 'd': return new BitArray(new bool[] { true, true, false, true});
                case 'e': return new BitArray(new bool[] { true, true, true, false});
                case 'f': return new BitArray(new bool[] { true, true, true, true});
                default: return new BitArray(new bool[] { });
            }
        }
    }
}
