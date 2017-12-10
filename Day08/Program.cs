using System;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {            
            string[] registers = System.IO.File.ReadAllLines(@"C:\Development\Adventofcode2017\Day8\registers.txt");
            //string[] registersTest = System.IO.File.ReadAllLines(@"C:\Development\Adventofcode2017\Day8\registersTest.txt");

            SetupDataStructure(registers);

            var result = GetLargestRegisterValue();

            Console.WriteLine("Result part 1: " + result.Item1);
            Console.WriteLine("Result part 2: " + result.Item2);
        }
        
        static Dictionary<string, int> values = new Dictionary<string, int>();
        static List<Tuple<string, int, int, string, string, int>> instructions = new List<Tuple<string, int, int, string, string, int>>();

        static Tuple<int, int> GetLargestRegisterValue() {
            var highestEverVal = 0;

            foreach (var instruction in instructions) {
                var valOfComparatorRegister = values[instruction.Item4];
                switch (instruction.Item5) {
                    case ">":
                        if (valOfComparatorRegister > instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                    case "<":
                        if (valOfComparatorRegister < instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                    case ">=":
                        if (valOfComparatorRegister >= instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                    case "<=":
                        if (valOfComparatorRegister <= instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                    case "==":
                        if (valOfComparatorRegister == instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                    case "!=":
                        if (valOfComparatorRegister != instruction.Item6) {
                            UpdateValue(instruction.Item1, instruction.Item2, instruction.Item3);
                        }
                        break;
                }

                var currHighestValue = GetHighestValue();
                if (currHighestValue > highestEverVal) {
                    highestEverVal = currHighestValue;
                }
            }

            return new Tuple<int, int>(GetHighestValue(), highestEverVal);
        }

        static void UpdateValue(string regName, int inc, int incVal) {
            values[regName] = values[regName] + inc * incVal;
        }

        static int GetHighestValue() {
            var largestValue = 0;
            foreach (var val in values.Values) {
                if (val > largestValue) {
                    largestValue = val;
                }
            }

            return largestValue;
        }

        static void SetupDataStructure(string[] registers) {
            foreach (var registerData in registers)
            {                
                var data = registerData.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                var registerName = data[0];

                if (!values.ContainsKey(registerName)) {
                    values.Add(registerName, 0);
                }

                var inceaseOrDecrease = data[1] == "inc" ? 1 : -1;
                var incValue = int.Parse(data[2]);

                var compareRegisterName = data[4];
                var comparator = data[5];
                var compareValue = int.Parse(data[6]);

                instructions.Add(new Tuple<string, int, int, string, string, int>(registerName, inceaseOrDecrease, incValue, compareRegisterName, comparator, compareValue));
            }
        }
    }
}
