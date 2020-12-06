using System;
using System.Collections.Generic;

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
            Day4a();
        }

        /// <summary>
        /// https://adventofcode.com/2020/day/4
        /// </summary>
        private static void Day4a()
        {
            /*
            byr(Birth Year)
            iyr(Issue Year)
            eyr(Expiration Year)
            hgt(Height)
            hcl(Hair Color)
            ecl(Eye Color)
            pid(Passport ID)
            cid(Country ID) - optional
            */

            string[] examplePassportList = ReadInputData("day4-example.txt");
            string[] fullPassportList = ReadInputData("day4.txt");
            var passportList = examplePassportList;
            var passportDictList = new List<Dictionary<string, string>> { };

            /*
            ecl:gry pid:860033327 eyr:2020 hcl:#fffffd 
            byr:1937 iyr:2017 cid:147 hgt:183cm

            iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
            hcl:#cfa07d byr:1929

            hcl:#ae17e1 iyr:2013
            eyr:2024
            ecl:brn pid:760753108 byr:1931
            hgt:179cm

            hcl:#cfa07d eyr:2025 pid:166559648
            iyr:2011 ecl:brn hgt:59in
            */

            var passport = new Dictionary<string, string>();
                  
            foreach (var line in passportList)
            {
                bool endOfLine = false;
                int pSearchStart = 0;
                int pSearchEnd = line.Length;
                int pGrabColonStart = 0;
                int pGrabSpaceEnd = 0;

                // loop until end of line
                while (!endOfLine)
                {
                    // empty line is the end of the passport
                    if (line == "")
                    {
                        // Capture the entire passport into the master list
                        passportDictList.Add(passport);
                        
                        // Get a fresh new passport
                        passport = new Dictionary<string, string>();
                        // Skip finding any K,V's on this line
                        break;
                    }

                    // Find positions of Colon and Space
                    var pColon = line.IndexOf(':', pSearchStart, pSearchEnd - pSearchStart);
                    var pSpace = line.IndexOf(" ", pSearchStart, pSearchEnd - pSearchStart);

                    // If there is no space, we are at the end of the line
                    if (pSpace == -1)
                    {
                        // Grab to the end of the line
                        pGrabSpaceEnd = line.Length;
                        endOfLine = true;
                    }
                    else
                    {
                        // Grab up until the next space
                        pGrabSpaceEnd = pSpace;
                    }
                   
                    // Grab from the start to the :
                    string key = line[pGrabColonStart..pColon];
                    
                    // Grab from after the : to the " " || the EOL
                    string value = line[(pColon + 1)..pGrabSpaceEnd];

                    // Start the next search after the K,V we just found
                    pSearchStart = pSpace + 1;
                    pGrabColonStart = pSpace + 1;

                    // Store the K,V into the passport
                    passport.Add(key, value);
                }
            }
            
            // capture the last passport
            passportDictList.Add(passport);

            Console.WriteLine($"You have {passportDictList.Count} passports.");

            // Check for required fields
            /*
            byr(Birth Year)
            iyr(Issue Year)
            eyr(Expiration Year)
            hgt(Height)
            hcl(Hair Color)
            ecl(Eye Color)
            pid(Passport ID)
            cid(Country ID) - optional
            */


        }

        /// <summary>
        /// https://adventofcode.com/2020/day/3
        /// </summary>
        private static void Day3b()
        {
            string[] exampleTreeMap = ReadInputData("day3-example.txt");
            string[] treeMap = ReadInputData("day3.txt");
            var map = treeMap;

            // Create X,Y grid in a 2d array
            char[,] grid = new char[map[0].Length, map.Length];

            // load chars (# or .) into each slot in the 2d array
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string curr_line = map[y];
                for (int x = 0; x < curr_line.Length; x++)
                {
                    grid[x, y] = curr_line[x];
                }
            }

            uint[] treeResults = new uint[5];
            treeResults[0] = (uint)FindTrees(grid, 1, 1);
            treeResults[1] = (uint)FindTrees(grid, 3, 1);
            treeResults[2] = (uint)FindTrees(grid, 5, 1);
            treeResults[3] = (uint)FindTrees(grid, 7, 1);
            treeResults[4] = (uint)FindTrees(grid, 1, 2);

            static int FindTrees(char[,] grid, int slopeX, int slopeY)
            {
                /// Find open squares (.) and trees (#)
                int finalTrees = 0;
                int open = 0;

                // Start at 0,0. Loop with X+3, Y+1.
                int _x = 0;
                for (int y = 0; y < grid.GetLength(1); y += slopeY)
                {
                    if (y >= grid.GetLength(1))
                    {
                        break;
                    }

                    if (_x >= grid.GetLength(0))
                    {
                        // if we would have attempted to get a X coord that is outside the data range,
                        // reset the X to where it would be as if the range infinitley repeated.
                        _x -= grid.GetLength(0);
                    }

                    // Console.Write($"{_x},{y}");
                    if (grid[_x, y].Equals('#'))
                    {
                        // Console.WriteLine(" #");
                        finalTrees++;
                    }
                    else if (grid[_x, y].Equals('.'))
                    {
                        // Console.WriteLine(" .");
                        open++;
                    }
                    else
                    {
                        Console.WriteLine($"? [{grid[_x, y]}]");
                    }
                    _x += slopeX;
                }
                Console.WriteLine($"Slope: {slopeX}/{slopeY} Trees: {finalTrees}");
                //Console.WriteLine($"# of Trees: {finalTrees}");
                //Console.WriteLine($"# of Open : {open}");

                return finalTrees;
            }

            uint solution = treeResults[0] * treeResults[1] * treeResults[2] * treeResults[3] * treeResults[4];
            Console.WriteLine(solution.ToString());
        }
        /// <summary>
        /// https://adventofcode.com/2020/day/3
        /// </summary>
        private static void Day3a()
        {
            string[] exampleTreeMap = ReadInputData("day3-example.txt");
            string[] treeMap = ReadInputData("day3.txt");
            var map = treeMap;

            // Create X,Y grid in a 2d array
            char[,] grid = new char[map[0].Length, map.Length];

            // load chars (# or .) into each slot in the 2d array
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                string curr_line = map[y];
                for (int x = 0; x < curr_line.Length; x++)
                {
                    grid[x, y] = curr_line[x];
                    //Console.WriteLine(grid[x, y]);
                }
            }

            /// Find open squares (.) and trees (#)
            int trees = 0;
            int open = 0;

            // Start at 0,0. Loop with X+3, Y+1.
            int _x = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (_x >= grid.GetLength(0))
                {
                    // if we would have attempted to get a X coord that is outside the data range,
                    // reset the X to where it would be as if the range infinitley repeated.
                    _x -= grid.GetLength(0);
                }

                Console.Write($"{_x},{y}");
                if (grid[_x, y].Equals('#'))
                {
                    Console.WriteLine(" #");
                    trees++;
                }
                else if (grid[_x, y].Equals('.'))
                {
                    Console.WriteLine(" .");
                    open++;
                }
                else
                {
                    Console.WriteLine($"? [{grid[_x, y]}]");
                }
                _x += 3;
            }

            Console.WriteLine($"# of Trees: {trees}");
            Console.WriteLine($"# of Open : {open}");

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