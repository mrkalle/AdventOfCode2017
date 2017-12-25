using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day23
{
    class Program
    {
        static List<Tuple<string, string, string>> instructions = new List<Tuple<string, string, string>>();
        static Dictionary<char, BigInteger> registers = new Dictionary<char, BigInteger>();
        
        static void Main(string[] args)
        {
            SetupDataStructure("input.txt", 0);
            Console.WriteLine("Result part 1: " + GetResult());
            
            SetupDataStructure("input.txt", 1);
            Console.WriteLine("Result part 2: " + GetResult2());
        }        

        static List<int> interestingNrs = new List<int>();

        static BigInteger GetResult2() 
        {
            var b = 108100;
            var c = 125100;
            var d = 2;
            var f = 1;
            var h = 0;

            while (true) {
                f = 1;
                d = 2; 

                while (d != b) {           
                    if (b % d == 0) {
                        f = 0;
                        break;
                    }

                    d++;
                }

                //Console.WriteLine("b: {0}, c: {1}, d: {2}, f: {3}, h: {4}", b, c, d, f, h);

                if (f == 0) {
                    h++;
                }

                if (b >= c) {
                    return h;
                }

                b += 17;
            }
        }

        static BigInteger GetResult() 
        {
            var index = 0;
            var nrOfMuls = 0;
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
                            //Console.WriteLine("set " + regName + " " + value + " (" + instruction.Item3 + ")");
                            index++;
                            break;
                        case "sub":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            registers[regName] = registers[regName] - value;
                            //Console.WriteLine("sub " + regName + " " + value + " (" + instruction.Item3 + ")");
                            index++;
                            break;
                        case "mul":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            var mulVal = registers[regName] * value;
                            registers[regName] = mulVal;      
                            //Console.WriteLine("mul " + regName + " " + value + " (" + instruction.Item3 + ") nrOfMuls: " + nrOfMuls);                                                  
                            //Console.WriteLine("mulVal " + mulVal);
                            nrOfMuls++;
                            index++;
                            break;
                        case "jnz":                            
                            value = BigInteger.TryParse(regName + "", out val) ? val : registers[regName];
                            if (value != 0) {                                
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                                index += (int)value;
                                //Console.WriteLine("jnz " + regName + " " + value + " (" + instruction.Item3 + ")");
                            } else {
                                //Console.WriteLine("jnz " + regName + " " + value + " (" + instruction.Item3 + ") skip");   
                                index++;
                            }

                            break;
                        default:
                            throw new Exception("Faulty instruction name: " + instruction);
                    }
                }
                catch (Exception) {
                    return nrOfMuls;
                }
            }
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
