using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day18
{
    class Program
    {
        static List<Tuple<string, string, string>> instructions = new List<Tuple<string, string, string>>();
        static Dictionary<char, BigInteger> registers = new Dictionary<char, BigInteger>();
        static int index = 0;
        static BigInteger lastFreqSent = 0;

        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");

            Console.WriteLine("Result part 1: " + GetResult());
        }

        static BigInteger GetResult() 
        {
            BigInteger value;
            BigInteger val;
            while (true) {
                try {
                    var instruction = instructions[index];
                    var regName = instruction.Item2[0];
                    //Console.WriteLine(instruction.Item1 + " " + instruction.Item2 + " " + instruction.Item3 ?? "");
                    switch (instruction.Item1) {
                        case "set":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = value;
                            index++;
                            break;
                        case "add":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] + value;
                            index++;
                            break;
                        case "mul":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            var mulVal = registers[regName] * value;
                            registers[regName] = mulVal;
                            
                            Console.WriteLine("mulVal " + mulVal);
                            index++;
                            break;
                        case "mod":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            var modVal = registers[regName] % value;
                            registers[regName] = modVal;
                            
                            Console.WriteLine("modVal " + modVal);
                            index++;
                            break;
                        case "snd":
                            lastFreqSent = registers[regName];
                            index++;
                            break;
                        case "rcv":
                            if (registers[regName] != 0) {
                                //registers[regName] = lastFreqSent;
                                return lastFreqSent;
                            }

                            index++;
                            break;
                        case "jgz":                            
                            value = BigInteger.TryParse(regName + "", out val) ? val : registers[regName];
                            if (value > 0) {                                
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                                index += (int)value;
                                Console.WriteLine("index added with " + (int)value);
                            } else {
                                index++;
                            }

                            break;
                    }
                }
                catch (Exception) {
                    Console.WriteLine("WTF CRASH");
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
