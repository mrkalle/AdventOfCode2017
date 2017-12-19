using System;
using System.Collections.Generic;
using System.Numerics;

namespace Day18
{
    class Program
    {
        static List<Tuple<string, string, string>> instructions = new List<Tuple<string, string, string>>();
        static Dictionary<char, BigInteger> registers = new Dictionary<char, BigInteger>();
        static List<Dictionary<char, BigInteger>> registers2 = new List<Dictionary<char, BigInteger>>();
        static int index = 0;
        static List<int> indexes = new List<int>();
        static int cp = 0; // current program
        static BigInteger lastFreqSent = 0;
        static List<List<BigInteger>> inQueue = new List<List<BigInteger>>();
        static int programOneSendCounter = 0;
        static List<char> waitingRegisters = new List<char>();

        static void Main(string[] args)
        {
            //SetupDataStructure("inputTest.txt");
            SetupDataStructure("input.txt");
            Console.WriteLine("Result part 1: " + GetResult());
            
            //SetupDataStructure2("inputTest2.txt");
            SetupDataStructure2("input.txt");
            Console.WriteLine("Result part 2: " + GetResult2());
        }

        static BigInteger GetResult2() 
        {
            BigInteger value;
            BigInteger val;
            while (true) {
                try {
                    if (waitingRegisters[0] != ' ' && waitingRegisters[1] != ' ') {
                        return programOneSendCounter;
                    }
                    
                    if (waitingRegisters[cp] != ' ') {
                        if (inQueue[cp].Count == 0) {
                            //Console.WriteLine("Program " + cp + " wait");
                        } else {
                            BigInteger tmp = inQueue[cp][0];
                            inQueue[cp].RemoveAt(0);
                            registers2[cp][waitingRegisters[cp]] = tmp;
                            waitingRegisters[cp] = ' ';
                        }
                    } else {
                        var instruction = instructions[indexes[cp]];
                        var regName = instruction.Item2[0];
                        //Console.WriteLine(instruction.Item1 + " " + instruction.Item2 + " " + instruction.Item3 ?? "");
                        switch (instruction.Item1) {
                            case "set":
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers2[cp][instruction.Item3[0]];
                                registers2[cp][regName] = value;
                                indexes[cp] = indexes[cp] + 1;
                                break;
                            case "add":
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers2[cp][instruction.Item3[0]];
                                registers2[cp][regName] = registers2[cp][regName] + value;
                                indexes[cp] = indexes[cp] + 1;
                                break;
                            case "mul":
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers2[cp][instruction.Item3[0]];
                                var mulVal = registers2[cp][regName] * value;
                                registers2[cp][regName] = mulVal;                                
                                //Console.WriteLine("mulVal " + mulVal);
                                indexes[cp] = indexes[cp] + 1;
                                break;
                            case "mod":
                                value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers2[cp][instruction.Item3[0]];
                                var modVal = registers2[cp][regName] % value;
                                registers2[cp][regName] = modVal;                                
                                //Console.WriteLine("modVal " + modVal);
                                indexes[cp] = indexes[cp] + 1;
                                break;
                            case "snd":
                                var otherCp = cp == 0 ? 1 : 0;
                                value = BigInteger.TryParse(regName + "", out val) ? val : registers2[cp][regName];
                                inQueue[otherCp].Add(value);

                                if (cp == 1) {
                                    programOneSendCounter++;
                                }
                                
                                indexes[cp] = indexes[cp] + 1;

                                break;
                            case "rcv":                              
                                if (inQueue[cp].Count > 0) {
                                    BigInteger tmp = inQueue[cp][0];
                                    inQueue[cp].RemoveAt(0);
                                    registers2[cp][regName] = tmp;
                                } else  {                           
                                    waitingRegisters[cp] = regName;
                                }

                                indexes[cp] = indexes[cp] + 1;
                                break;
                            case "jgz":                            
                                value = BigInteger.TryParse(regName + "", out val) ? val : registers2[cp][regName];
                                if (value > 0) {                                
                                    value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers2[cp][instruction.Item3[0]];
                                    indexes[cp] = indexes[cp] + (int)value;
                                    //Console.WriteLine("index added with " + (int)value);
                                } else {
                                    indexes[cp] = indexes[cp] + 1;
                                }

                                break;
                        }
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("WTF CRASH!!! " + e.StackTrace);
                    return programOneSendCounter;
                }
                
                cp = cp == 0 ? 1 : 0;
            }
        }
        
        static void SetupDataStructure2(string file) {
            var inputs = System.IO.File.ReadAllLines(file);
            for (var j = 0; j < inputs.Length; j++) {
                var instrs = inputs[j].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                
                var regName = instrs[0];                            
                string var1 = instrs[1];
                string var2 = instrs.Length == 3 ? instrs[2] : null;

                instructions.Add(new Tuple<string, string, string>(regName, var1, var2));
            }

            for (var i = 0; i < 2; i++) {
                indexes.Add(0);

                var dict = new Dictionary<char, BigInteger>();
                dict.Add('a', 0);
                dict.Add('b', 0);
                dict.Add('f', 0);
                dict.Add('i', 0);
                dict.Add('p', i);
                registers2.Add(dict);

                waitingRegisters.Add(' ');

                inQueue.Add(new List<BigInteger>());
            }
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
                            //Console.WriteLine("mulVal " + mulVal);
                            index++;
                            break;
                        case "mod":
                            value = BigInteger.TryParse(instruction.Item3, out val) ? val : registers[instruction.Item3[0]];
                            var modVal = registers[regName] % value;
                            registers[regName] = modVal;                            
                            //Console.WriteLine("modVal " + modVal);
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
                                //Console.WriteLine("index added with " + (int)value);
                            } else {
                                index++;
                            }

                            break;
                    }
                }
                catch (Exception) {
                    Console.WriteLine("WTF CRASH!!!");
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
