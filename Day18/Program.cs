using System;
using System.Collections.Generic;

namespace Day18
{
    class Program
    {
        static List<Tuple<string, string, string>> instructions = new List<Tuple<string, string, string>>();
        static Dictionary<char, int> registers = new Dictionary<char, int>();
        static int index = 0;
        static int lastFreqSent = 0;

        static void Main(string[] args)
        {
            SetupDataStructure("inputTest.txt");

            Console.WriteLine("Result part 1: " + GetResult());
        }

        static int GetResult() 
        {
            char regName;
            var value = 0;
            int val;
            while (true) {
                try {
                    var instruction = instructions[index];
                    switch (instruction.Item1) {
                        case "set":
                            regName = instruction.Item2[0];
                            value = int.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = value;
                            index++;
                            break;
                        case "add":
                            regName = instruction.Item2[0];
                            value = int.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] + value;
                            index++;
                            break;
                        case "mul":
                            regName = instruction.Item2[0];
                            value = int.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] * value;
                            index++;
                            break;
                        case "mod":
                            regName = instruction.Item2[0];
                            value = int.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] % value;
                            index++;
                            break;
                        case "snd":
                            regName = instruction.Item2[0];
                            lastFreqSent = registers[regName];
                            index++;
                            break;
                        case "rcv":
                            regName = instruction.Item2[0];
                            registers[regName] = lastFreqSent;
                            index++;
                            break;
                        case "jgz":
                            regName = instruction.Item2[0];
                            var regVal = registers[regName];
                            if (regVal > 0) {
                                var jumpVal = int.Parse(instruction.Item3);
                                index += jumpVal;
                            }

                            break;
                    }
                }
                catch (Exception e) {
                    return lastFreqSent;
                }
            }
        }

        static void SetupDataStructure(string file) {
            var inputs = System.IO.File.ReadAllLines(file);
            for (var j = 0; j < inputs.Length; j++) {
                var instrs = inputs[j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                
                var regName = instrs[0];                            
                string var1 = instrs[1];
                string var2 = instrs.Length == 3 ? instrs[2] : null;

                instructions.Add(new Tuple<string, string, string>(regName, var1, var2));
            }

            registers.Add('a', 0);
            registers.Add('b', 0);
            registers.Add('f', 0);
            registers.Add('i', 0);
            registers.Add('p', 0);
        }
    }
}
