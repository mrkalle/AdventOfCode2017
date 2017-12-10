using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day10
{
    class Program
    {
        // 38612 too high
        static int[] realInputLengths = new int[] {106, 118, 236, 1, 130, 0, 235, 254, 59, 205, 2, 87, 129, 25, 255, 118};
        static int[] testInputLengths = new int[] {2, 4, 1, 5};

        static int realInputLength = 256;
        static int testInputLength = 5;

        static string inputTextReal = "106,118,236,1,130,0,235,254,59,205,2,87,129,25,255,118";
        
        static string inputText1 = "";
        static string inputText2 = "AoC 2017";
        static string inputText3 = "1,2,3";
        static string inputText4 = "1,2,4";

        static byte[] extraLengths = new byte[] { 17, 31, 73, 47, 23};

        static void Main(string[] args)
        {
            // This is the 255 long array... 1, 2, 3, 4 etc
            var list = new int[realInputLength];
            for (var i = 0; i < realInputLength; i++) {
                list[i] = i;
            }

            var result1 = ScrambleOneRound(list, realInputLengths, 0, 0);
            Console.WriteLine("Result part 1: " + result1.Item1[0] * result1.Item1[1]);
            
            // #####################################################
            // Part 2 

            // This is the 255 long array... 1, 2, 3, 4 etc
            list = new int[realInputLength];
            for (var i = 0; i < realInputLength; i++) {
                list[i] = i;
            }

            // Add extra lengths
            var bytes = Encoding.ASCII.GetBytes(inputTextReal); // Change here for other input
            List<byte> tempList = new List<byte>(bytes.Length + extraLengths.Length);
            tempList.AddRange(bytes);
            tempList.AddRange(extraLengths);
            bytes = tempList.ToArray();
            realInputLengths = GetIntArray(bytes);

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
            Console.WriteLine("Result part 2: " + finalHash);
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
    }
}
