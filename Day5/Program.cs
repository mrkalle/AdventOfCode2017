using System;
using System.Collections.Generic;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] instructions = System.IO.File.ReadAllLines(@"C:\Users\clundin\Documents\AoC\Day5\instructions.txt");
            string[] instructionsTest1_1 = new string[] { "0", "3", "0", "1", "-3" };

            // Console.WriteLine("Test1 Result part 1: " + GetJumpOutCycle1(instructionsTest1_1));
            Console.WriteLine("Result part 1: " + GetJumpOutCycle1(instructions));

            //Console.WriteLine("Test1 Result part 2: " + GetJumpOutCycle2(instructionsTest1_1));
            Console.WriteLine("Result part 2: " + GetJumpOutCycle2(instructions));
        }
        
        static int GetJumpOutCycle1(string[] inputInstructions)
        { 
            var instructions = new List<int>();
            foreach (var instr in inputInstructions) {
                instructions.Add(int.Parse(instr));
            }

            var cyclesExecuted = 0;
            try {
                var index = 0;
                while (true) {
                    var value = instructions[index];
                    var newIndex = index + value;
                    cyclesExecuted++;
                    instructions[index] = value + 1;
                    index = newIndex;
                }
            } catch (ArgumentOutOfRangeException) {
                return cyclesExecuted;
            } 
        } 

        static int GetJumpOutCycle2(string[] inputInstructions)
        { 
            var instructions = new List<int>();
            foreach (var instr in inputInstructions) {
                instructions.Add(int.Parse(instr));
            }

            var cyclesExecuted = 0;
            try {
                var index = 0;
                while (true) {
                    var value = instructions[index];
                    var newIndex = index + value;
                    cyclesExecuted++;

                    if (value >= 3) {
                        instructions[index] = value - 1;                        
                    } else {
                        instructions[index] = value + 1;
                    }

                    index = newIndex;
                }
            } catch (ArgumentOutOfRangeException) {
                return cyclesExecuted;
            } 
        } 
    }
}
