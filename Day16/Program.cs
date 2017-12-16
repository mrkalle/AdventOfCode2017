using System;
using System.Collections.Generic;

namespace Day16
{
    class Program
    {
        static char[] programs = new char[] { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p'};
        static List<Tuple<char, string, string>> instructions = new List<Tuple<char, string, string>>();

        static void Main(string[] args)
        {
            programs = new char[] { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p'};

            var input = System.IO.File.ReadAllLines(@"C:\Development\AdventOfCode2017\Day16\input.txt")[0];
            SetupDataStructure(input);

            DancePrograms();

            var firstOutput = new string(programs);
            Console.WriteLine("Result part 1: " + firstOutput);

            var cycleLength = 0;
            for (var i = 1; i < 1000; i++) {
                DancePrograms();

                var output = new string(programs);
                if (firstOutput == output) {
                    cycleLength = i;
                    break;
                }
            }

            var restOfBillion = 1000000000%cycleLength;
           
            programs = new char[] { 'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p'};
            for (var i = 0; i < restOfBillion; i++) {
                DancePrograms();
            }

            Console.WriteLine("Result part 2: " + new string(programs));
        }

        static void DancePrograms() {
            for (var i = 0; i < instructions.Count; i++) {
                var instr = instructions[i];

                switch (instr.Item1) {
                    case 's':
                        Spin(int.Parse(instr.Item2));
                        break;
                    case 'x':
                        Exchange(int.Parse(instr.Item2), int.Parse(instr.Item3));
                        break;
                    case 'p':
                        Partner(instr.Item2[0], instr.Item3[0]);
                        break;
                }
            }
        }

        static void Spin(int index) {
            var programsFirstPartCopy = new string(programs).Substring(0, programs.Length - index);
            var indexFromFront = programs.Length - index;

            for (var i = 0; indexFromFront + i < programs.Length; i++) {
                Exchange(indexFromFront + i, i);
            }

            var startCopyBackIndex = programs.Length - indexFromFront;

            for (var i = 0; i < programsFirstPartCopy.Length; i++) {
                programs[startCopyBackIndex + i] = programsFirstPartCopy[i];
            }
        }

        static void Exchange(int pos1, int pos2) {
            var pos1Char = programs[pos1];
            programs[pos1] = programs[pos2];
            programs[pos2] = pos1Char;
        }

        static void Partner(char char1, char char2) {
            var pos1 = 0;
            var pos2 = 0;
            for (var i = 0; i < programs.Length; i++) {
                if (programs[i] == char1) {
                    pos1 = i;
                } else if (programs[i] == char2) {
                    pos2 = i;
                }
            }

            Exchange(pos1, pos2);
        }

        static void SetupDataStructure(string input) {        
            var instrs = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < instrs.Length; i++) {
                var instr = instrs[i];

                var type = instr[0];

                instr = instr.Remove(0, 1);
                string var1 = null;
                string var2 = null;
                var slashPos = instr.IndexOf('/');
                if (slashPos == -1) {
                    var1 = instr;
                } else {
                    var1 = instr.Substring(0, slashPos);
                    var2 = instr.Substring(slashPos + 1, instr.Length - slashPos - 1);
                }

                instructions.Add(new Tuple<char, string, string>(type, var1, var2));
            }            
        }
    }
}
