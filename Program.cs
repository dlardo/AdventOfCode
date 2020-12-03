using System;

namespace AdventOfCode
{
    internal class Program
    {
        /// <summary>
        /// Main Entry Point
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            Day2a();
        }

        // https://adventofcode.com/2020/day/2#part2
        private static void Day2b()
        {
            string[] examplePasswordPolicy = { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };
            string[] PasswordPolicy = ReadInputData("day2.txt");

            int violations = 0;
            int passes = 0;

            foreach (var item in PasswordPolicy)
            {
                // Pull out interesting values
                int dashPos = item.IndexOf("-");
                int pOne = Convert.ToInt32(item.Substring(0, dashPos));
                // Console.WriteLine($"pOne: [{pOne}]");

                int pTwo = Convert.ToInt32(Between(item, "-", " "));
                // Console.WriteLine($"pTwo: [{pTwo}]");

                char reqLetter = Convert.ToChar(Between(item, " ", ":"));
                // Console.WriteLine($"reqLetter: [{reqLetter}]");

                string password = item.Substring(item.IndexOf(": ") + 2);
                // Console.WriteLine($"password: [{password}]");

                // Password meets policy logic

                /* Each policy actually describes two positions in the password, where 1 means the first character,
                2 means the second character, and so on. (Be careful; Toboggan Corporate Policies have no concept of "index zero"!)
                Exactly one of these positions must contain the given letter. */

                // Modify pOne and pTwo to account for 1 index in policy
                pOne--;
                pTwo--;

                Console.WriteLine(item);

                // test if exactly one of these positions contain the given letter
                if (password[pOne] == reqLetter ^ password[pTwo] == reqLetter)
                {
                    // if yes, record violation
                    Console.WriteLine($"Pass - letter: {reqLetter} Password: {password}");
                    passes++;
                }
                else
                {
                    Console.WriteLine($"Fail - letter: {reqLetter} Password: {password}");
                    violations++;
                }
            }

            Console.WriteLine($"Number of Violations: {violations}/{PasswordPolicy.Length}");
            Console.WriteLine($"Number of Passes: {passes}/{PasswordPolicy.Length}");
        }

        // https://adventofcode.com/2020/day/2
        private static void Day2a()
        {
            string[] examplePasswordPolicy = { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };
            string[] PasswordPolicy = ReadInputData("day2.txt");

            int violations = 0;
            int passes = 0;

            foreach (var item in PasswordPolicy)
            {
                // Pull out interesting values
                int dashPos = item.IndexOf("-");
                int minFreq = Convert.ToInt32(item.Substring(0, dashPos));
                // Console.WriteLine($"pOne: [{pOne}]");

                int maxFreq = Convert.ToInt32(Between(item, "-", " "));
                // Console.WriteLine($"pTwo: [{pTwo}]");

                string reqLetter = Between(item, " ", ":");

                // Console.WriteLine($"reqLetter: [{reqLetter}]");

                string password = item.Substring(item.IndexOf(": ") + 2);
                // Console.WriteLine($"password: [{password}]");

                // Password meets policy logic
                // The password policy indicates the lowest and highest number of times
                // a given letter must appear for the password to be valid

                // Count number of reqLetters
                int lettersFound = CountStringOccurrences(password, reqLetter);

                Console.WriteLine(item);

                // test if under min or over max
                if (lettersFound < minFreq)
                {
                    // if yes, record violation
                    Console.WriteLine($"MinF {lettersFound} < {minFreq} Letter: {reqLetter} Password: {password}");
                    violations++;
                }
                else if (lettersFound > maxFreq)
                {
                    Console.WriteLine($"MaxF {lettersFound} > {maxFreq} Letter: {reqLetter} Password: {password}");
                    violations++;
                }
                else
                {
                    Console.WriteLine($"Pass {minFreq} <= {lettersFound} <= {maxFreq} Letter: {reqLetter} Password: {password}");
                    passes++;
                }
            }

            Console.WriteLine($"Number of Violations: {violations}/{PasswordPolicy.Length}");
            Console.WriteLine($"Number of Passes: {passes}/{PasswordPolicy.Length}");
        }

        /// <summary>
        /// Read input data from .\input data\ dir into an array of strings
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string[] ReadInputData(string filename)
        {
            string fullpath = @"C:\Users\dlardo\Nextcloud\Documents\github.com\dlardo\AdventOfCode\input data\" + filename;
            return System.IO.File.ReadAllLines(fullpath);
        }

        /// <summary>
        /// Get the string that exists between two other strings
        /// </summary>
        /// <param name="STR">String to search through</param>
        /// <param name="FirstString"></param>
        /// <param name="LastString"></param>
        /// <returns></returns>
        private static string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR[Pos1..Pos2];
            return FinalString;
        }

        /// <summary>
        /// Count occurrences of strings.
        /// </summary>
        private static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        // https://adventofcode.com/2020/day/1
        private static void Day1()
        {
            int[] sampleExpenseReport = { 1721, 979, 366, 299, 675, 1456 };
            string[] expenseReportRaw = ReadInputData("day1.txt");
            int[] expenseReport = new int[expenseReportRaw.Length];

            // Convert the array into int[]
            for (int i = 0; i < expenseReportRaw.Length; i++)
            {
                expenseReport[i] = Convert.ToInt32(expenseReportRaw[i]);
            }

            foreach (var item in expenseReport)
            {
                foreach (var itemtwo in expenseReport)
                {
                    foreach (var itemthree in expenseReport)
                    {
                        if (item + itemtwo + itemthree == 2020)
                        {
                            Console.WriteLine($"Item1: {item} Item2: {itemtwo} Item3: {itemthree}");
                            Console.WriteLine($"Multiply them: {item * itemtwo * itemthree}");
                        }
                    }
                }
            }
        }
    }
}