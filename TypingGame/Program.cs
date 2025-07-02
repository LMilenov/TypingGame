using System;
using System.Diagnostics;
using System.Text;

namespace TypingGame
{
    class Program
    {
        private static readonly string[] Subjects = { "The programmer", "A developer", "The algorithm", "This code", "The AI" };
        private static readonly string[] Verbs = { "writes", "debugs", "optimizes", "refactors", "deploys" };
        private static readonly string[] Objects = { "clean code", "a neural network", "the database", "an API", "a mobile app" };
        private static readonly string[] Adjectives = { "efficient", "buggy", "scalable", "broken", "secure" };
        private static readonly string[] Connectors = { "while", "because", "although", "since", "after" };
        private static readonly string[] SecondParts = { "testing it", "reading docs", "drinking coffee", "fixing bugs", "updating dependencies" };
        static void Main()
        {
            // Set the test sentence for typing challenge
            string testSentence = GenerateRandomSentence();

            // Display game instructions
            Console.WriteLine("Typing Game");
            Console.WriteLine("Type the following sentence as fast as you can");
            Console.WriteLine($"\n{testSentence}\n");  // Show the target sentence

            // Wait for user to start the challenge
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.Clear();  // Clear console for challenge

            // Redisplay the sentence and prompt for input
            Console.WriteLine($"Original: {testSentence}\n");
            Console.Write("Your turn: ");  // Using Write() for better cursor placement

            // Start timer and capture user input
            Stopwatch sw = Stopwatch.StartNew();
            string userInput = Console.ReadLine()?.Trim() ?? "";  // Trim whitespace
            sw.Stop();

            // Validate empty input
            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("You didn't type anything!");
                return;
            }

            // Calculate results
            int errorsCount = CountErrors(testSentence, userInput);
            double wpm = CalculateWPM(testSentence, sw.Elapsed.TotalMinutes);

            // Display results
            Console.WriteLine("\n--- Results ---");
            Console.WriteLine($"Time: {sw.Elapsed.TotalSeconds:F2} sec");
            Console.WriteLine($"Errors: {errorsCount}");
            Console.WriteLine($"Speed: {wpm:F1} WPM");

            Console.WriteLine("\nYour errors (red):");
            ShowColoredErrors(testSentence, userInput);
        }

        /// <summary>
        /// Compares original vs typed text character-by-character
        /// </summary>
        static int CountErrors(string original, string typed)
        {
            int errors = 0;
            int minLength = Math.Min(original.Length, typed.Length);

            // Count character mismatches
            for (int i = 0; i < minLength; i++)
            {
                if (original[i] != typed[i]) errors++;
            }

            // Add length difference as additional errors
            return errors + Math.Abs(original.Length - typed.Length);
        }

        /// <summary>
        /// Calculates words-per-minute (WPM) typing speed
        /// </summary>
        static double CalculateWPM(string text, double minutes)
        {
            if (minutes <= 0) return 0;
            int wordCount = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount / minutes;
        }
        static string GenerateRandomSentence()
        {
            Random rand = new Random();
            StringBuilder sentence = new StringBuilder();

            // Simple sentence structure: Subject + Verb + Adjective + Object
            sentence.Append(Subjects[rand.Next(Subjects.Length)] + " ");
            sentence.Append(Verbs[rand.Next(Verbs.Length)] + " ");

            // 50% chance to add an adjective
            if (rand.Next(2) == 0)
            {
                sentence.Append(Adjectives[rand.Next(Adjectives.Length)] + " ");
            }

            sentence.Append(Objects[rand.Next(Objects.Length)]);

            // 40% chance to add a connector and second part
            if (rand.Next(10) < 4)
            {
                sentence.Append(" " + Connectors[rand.Next(Connectors.Length)] + " ");
                sentence.Append(SecondParts[rand.Next(SecondParts.Length)]);
            }

            return sentence.ToString() + ".";
        }
        static void ShowColoredErrors(string original, string typed)
        {
            Console.WriteLine("Original: " + original);
            Console.Write("Your text: ");

            int origIndex = 0, typedIndex = 0;
            bool inError = false;

            while (origIndex < original.Length || typedIndex < typed.Length)
            {
                //If the symbols are same(and there is no error until now)
                if (origIndex < original.Length &&
                    typedIndex < typed.Length &&
                    original[origIndex] == typed[typedIndex] &&
                    !inError)
                {
                    Console.Write(typed[typedIndex]);
                    origIndex++;
                    typedIndex++;
                }
                else
                {
                    //Mark the error in red
                    if (!inError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        inError = true;
                    }

                    
                    // Prioritise printing the input
                    if (typedIndex < typed.Length)
                    {
                        Console.Write(typed[typedIndex]);
                        typedIndex++;
                    }
                    else//If there are only original symbols

                    {
                        Console.Write("_");
                        origIndex++;
                    }
                }

                
                //If the next symbol is right, the error chain is false
                if (origIndex < original.Length &&
                    typedIndex < typed.Length &&
                    original[origIndex] == typed[typedIndex] &&
                    inError)
                {
                    Console.ResetColor();
                    inError = false;
                }
            }

            Console.ResetColor();
            Console.WriteLine();
        }
    }
}