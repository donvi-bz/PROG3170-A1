using System;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Assignment1 {
    internal class Program {

        static ServiceReference1.IService service = new ServiceReference1.ServiceClient();

        static void Main(string[] args) {
            bool run = true;
            while (run) {
                Console.Clear();
                Console.Write(
                    "1. Prime number\n" + 
                    "2. Sum of digits\n" +
                    "3. Reverse a string\n" +
                    "4. Print HTML tags\n" +
                    "5. Sort 5 numbers\n" +
                    "6. Exit\n\n" +
                    "\tEnter your choice: "
                );
                var cursorPos = Console.GetCursorPosition();
                char c = 'x';
                bool valid = true;
                do {
                    c = Console.ReadKey().KeyChar;
                    valid = (c >= '0' && c <= '6') || c == 'q';
                    if (valid) {
                        Console.Clear();
                        switch (c) {
                            case '1': PrimeNumber(); break;
                            case '2': SumOfDigits(); break;
                            case '3': ReverseString(); break;
                            case '4': PrintHTMLTags(); break;
                            case '5': SortNumbers(); break;
                            case '6':
                            case 'q': run = false; break;
                        }
                    } else {
                        Console.WriteLine("\n\nGiven input '" + c + "' is not valid. Please enter a number 1-6.        ");
                        Console.SetCursorPosition(cursorPos.Left, cursorPos.Top);
                    }
                } while (run && !valid);

                if (run) {
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(); 
                }
            }
        }

        /// <summary>
        /// Prompts, parses, and acts on an input. An initial prompt can be specified, this is what the user will be shown first.
        /// There is also a secondary prompt that gets shown if the user adds an invalid input. For checking input validity, the
        /// `parser` lamda is used. The parser takes a nullable string, and returns a tuple containing a boolean saying if the
        /// input is valid, and the parsed input (assuming it is valid). The parsed input is then returned.
        /// </summary>
        /// <typeparam name="T">The type of the input after being parsed.</typeparam>
        /// <param name="initialPrompt">The first prompt the user is shown</param>
        /// <param name="errorPrompt">The prompt the user is shown if an input parse error occured</param>
        /// <param name="parser">A function that takes a nullable string, and returns a tuple telling 
        ///                      if the input was parsed, followed by the parsed input</param>
        static T InputPrompt<T>(string initialPrompt, string errorPrompt, Func<string, (bool, T)> parser) {
            // Show the initial prompt
            Console.Write(initialPrompt);
            do {
                // Got to first get the line...
                string? line = Console.ReadLine();
                if (line is null) {
                    Console.Write(errorPrompt);
                } else {
                    // Use the parser to parse the input. 
                    var parsed = parser(line);
                    // The first item tells us if the input is valid
                    if (parsed.Item1) {
                        return parsed.Item2;
                    } else {
                        Console.Write(errorPrompt);
                    }
                }
            } while (true);
        }


        static void PrimeNumber() {
            var input = InputPrompt<int>(
                "Prime Number Checker\nEnter a number: ",
                "Not a valid number. Please enter a valid number: ",
                line => {
                    bool valid = int.TryParse(line, out int num);
                    return (valid, valid ? num : 0);
                }
            );

            string result = service.IsPrimeAsync(input).Result ? "prime number" : "not prime number";
            Console.WriteLine($"\nOutput: {result}\n");
        }

        static void SumOfDigits() {
            var input = InputPrompt<int>(
                "Sum of Digits\nEnter a number: ",
                "Not a valid number. Please enter a valid number: ",
                line => {
                    bool valid = int.TryParse(line, out int num);
                    return (valid, valid ? num : 0);
                }
            );
            int result = service.SumOfDigitsAsync(input).Result;
            Console.WriteLine($"\nOutput: {result}\n");
        }

        static void ReverseString() {
            string input = InputPrompt<string>(
                "Reverse a String\nEnter a string: ",
                "No string given. Please enter a string: ",
                line => {
                    bool valid = line is not null && line!.Length > 0;
                    return (valid, valid ? line! : "");
                }
            );
            string result = service.ReverseStringAsync(input).Result;
            Console.WriteLine($"\nOutput: {result}\n");
        }

        static void PrintHTMLTags() {
            var tag = InputPrompt<string>(
                "Print HTML tags\nEnter a tag: ",
                "Tag invalid, please enter a valid tag: ",
                line => {
                    return (line.Length > 0, line);
                }
            );
            var text = InputPrompt<string>(
                "Enter data: ",
                "Data empty. Please enter data: ",
                line => {
                    return (line.Length > 0, line);
                }
            );
            string result = service.HtmlTagsAsync(tag, text).Result;
            Console.WriteLine($"\nOutput: {result}\n");
        }

        static void SortNumbers() {
            var nums = InputPrompt<int[]>(
                "Number Sorter\nEnter comma seperated list of numbers: ",
                "List invalid, please enter valid list of numbers: ",
                line => {
                    bool valid = Regex.IsMatch(line, "^(\\d+,\\s?)+\\d+$");
                    return (valid, valid ? Array.ConvertAll(line.Split(",", StringSplitOptions.TrimEntries), int.Parse) : Array.Empty<int>());
                }
            );
            var ascending = InputPrompt<bool>(
                "Sort 'ascending' or 'descending': ",
                "Must type either 'ascending' or 'descending: ",
                line => {
                    bool valid = line == "ascending" || line == "descending";
                    return (valid, line == "ascending");
                }
            );
            int[] numsSorted = service.SortNumbersAsync(nums, ascending).Result;
            string numsString = string.Join(", ", numsSorted);
            Console.WriteLine($"\nOutput: {numsString}\n");
        }
    }
}