using System;
using System.Collections;
using System.Numerics;

namespace Day15
{
    class Program
    {
        // static BigInteger startA = 65;
        // static BigInteger startB = 8921;

        static BigInteger startA = 679;
        static BigInteger startB = 771;

        static BigInteger factorA = 16807;
        static BigInteger factorB = 48271;


        static void Main(string[] args)
        {
            //Console.WriteLine("Result part 1: " + GetNrOfMatches(1));
            Console.WriteLine("Result part 2: " + GetNrOfMatches(2));
        }

        static int GetNrOfMatches(int part)
        {
            var nrOfMatches = 0;

            var prevA = startA;
            var prevB = startB;

            var nrOfRounds = part == 1 ? 40000000 : 5000000;

            for (var i = 0; i < nrOfRounds; i++) {
                var currA = GetNextValue(prevA, factorA, part, 0);
                var currB = GetNextValue(prevB, factorB, part, 1);

                var aLast16 = GetLastBits(currA);
                var bLast16 = GetLastBits(currB);

                // Console.WriteLine(i + ": " + BitsAsString(aLast16));
                // Console.WriteLine(i + ": " + BitsAsString(bLast16));
                // Console.WriteLine("----");

                if (Matches(aLast16, bLast16)) {
                    nrOfMatches++;
                }

                prevA = currA;
                prevB = currB;
            }

            return nrOfMatches;
        }

        static string BitsAsString(BitArray bits) {
            var result = "";

            for (var i = 0; i < 16; i++) {
                if (bits[i]) {
                    result += "1";
                } else {
                    result += "0";
                }
            }

            return result;
        }

        static bool Matches(BitArray a, BitArray b)
        {
            if (a.Count != b.Count && a.Count != 16) {
                throw new Exception("WTF");
            }

            for (var i = 0; i < 16; i++) {
                if (a[i] != b[i]) {
                    return false;
                }
            }

            return true;
        }

        static BitArray GetLastBits(BigInteger currValue)
        {
            var curValueAsInt = (int)currValue;
            var bitArray = new BitArray(new int[] { curValueAsInt });
            var bitArrayRes = new BitArray(new bool[16]);

            for (var i = 0; i < 16; i++) {
                bitArrayRes[bitArrayRes.Count - i - 1] = bitArray[i];
            }

            return bitArrayRes;
        }

        static BigInteger GetNextValue(BigInteger currValue, BigInteger factor, int part, int generator)
        {
            BigInteger.DivRem(BigInteger.Multiply(currValue, factor), 2147483647, out var nextValue);

            if (part == 2) {
                if (generator == 0) {
                    while (nextValue % 4 != 0) {
                        BigInteger.DivRem(BigInteger.Multiply(nextValue, factor), 2147483647, out nextValue);
                    }
                } else if (generator == 1) {
                    while (nextValue % 8 != 0) {
                        BigInteger.DivRem(BigInteger.Multiply(nextValue, factor), 2147483647, out nextValue);
                    }
                } else {
                    throw new Exception("WTF");
                }
            }

            return nextValue;
        }
    }
}
