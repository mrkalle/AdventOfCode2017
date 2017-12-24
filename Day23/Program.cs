using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day23
{
    class Program
    {
        // 6241 is too high
        static List<Tuple<string, string, string>> instructions = new List<Tuple<string, string, string>>();
        static Dictionary<char, BigInteger> registers = new Dictionary<char, BigInteger>();
        
        static void Main(string[] args)
        {
            // SetupDataStructure("input.txt", 0);
            // Console.WriteLine("Result part 1: " + GetResult(1));
            
            SetupDataStructure("input.txt", 1);
            Console.WriteLine("Result part 2: " + GetResult(2));
        }

        static BigInteger GetResult(int part) 
        {
            var counter = 0;
            var index = 0;
            var nrOfMuls = 0;
            BigInteger value;
            BigInteger val;
            BigInteger hValue;
            while (counter < 5000000) {
                try {
                    var instruction = instructions[index];
                    var regName = instruction.Item2[0];
                    //Console.WriteLine(instruction.Item1 + " " + instruction.Item2 + " " + instruction.Item3 ?? "");
                    switch (instruction.Item1) {
                        case "set":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = value;
                            Console.WriteLine("set " + regName + " " + value + " (" + instruction.Item3 + ")");
                            index++;
                            break;
                        case "sub":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] - value;
                            Console.WriteLine("sub " + regName + " " + value + " (" + instruction.Item3 + ")");
                            index++;
                            break;
                        case "mul":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            var mulVal = registers[regName] * value;
                            registers[regName] = mulVal;      
                            Console.WriteLine("mul " + regName + " " + value + " (" + instruction.Item3 + ") nrOfMuls: " + nrOfMuls);                                                  
                            //Console.WriteLine("mulVal " + mulVal);
                            nrOfMuls++;
                            index++;
                            break;
                        case "jnz":                            
                            value = BigInteger.TryParse(regName + "", out val) ? val : registers[regName];
                            if (value != 0) {                                
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                                index += (int)value;
                                Console.WriteLine("jnz " + regName + " " + value + " (" + instruction.Item3 + ")");
                            } else {
                                Console.WriteLine("jnz " + regName + " " + value + " (" + instruction.Item3 + ") skip");   
                                index++;
                            }

                            break;
                        default:
                            throw new Exception("Faulty instruction name: " + instruction);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("WTF CRASH!!! " + e.Message);

                    if (part == 1) {
                        return nrOfMuls;
                    } else {
                        return hValue;
                    }
                }

                counter++;
                PrintRegValues();
            }

            return 0;
        }

        static void PrintRegValues() {
            Console.WriteLine("                                     a: {0}, b: {1}, c: {2}, d: {3}, e: {4}, f: {5}, g: {6}, h: {7}", registers['a'], registers['b'], registers['c'], registers['d'], registers['e'], registers['f'], registers['g'], registers['h']);
        }

        static void SetupDataStructure(string file, BigInteger aValue) {
            instructions = new List<Tuple<string, string, string>>();
            registers = new Dictionary<char, BigInteger>();

            var inputs = System.IO.File.ReadAllLines(file);
            for (var j = 0; j < inputs.Length; j++) {
                var instrs = inputs[j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                
                var regName = instrs[0];                            
                string var1 = instrs[1];
                string var2 = instrs.Length == 3 ? instrs[2] : null;

                instructions.Add(new Tuple<string, string, string>(regName, var1, var2));
            }

            registers.Add('a', aValue);
            registers.Add('b', 0);
            registers.Add('c', 0);
            registers.Add('d', 0);
            registers.Add('e', 0);
            registers.Add('f', 0);
            registers.Add('g', 0);
            registers.Add('h', 0);
        }
    }
}
