using System;
using System.Diagnostics;
using System.Linq;

namespace TypingGame
{
    class Program
    {
        //Difficulty levels
        enum Difficulty { Easy, Medium, Hard }

        //Sentences for every level of difficulty
        static readonly string[] EasySentences =
        {
            "The cat sits on the mat.",
            "I like to eat apples.",
            "It is a sunny day.",
            "The sun shines brightly.",
                "Dogs are loyal animals.",
                "I drink coffee every morning.",
                "Summer is my favorite season.",
                "We learn programming every day.",
                "Music makes me happy.",
                "The book is on the table.",
                "Children love to play outside."
        };

        static readonly string[] MediumSentences =
        {
            "Programming requires logical thinking.",
            "C# is a strongly typed language.",
            "Debugging can be challenging but rewarding.",
             "C# supports object-oriented programming.",
                "Git helps manage code versions.",
                "Algorithms are problem-solving tools.",
                "Debugging requires patience and skill.",
                "Visual Studio is powerful IDE.",
                "Computers understand binary code.",
                "The Internet connects people worldwide.",
                "Keyboard shortcuts save time."
        };

        static readonly string[] HardSentences =
        {
            "Asynchronous programming improves application responsiveness.",
            "The quick brown fox jumps over the lazy dog while quantum computing evolves.",
            "Implementing dependency injection promotes loose coupling between components.",
            "Polymorphism allows objects to take many forms.",
                "Quantum computing leverages quantum-mechanical phenomena.",
                "Machine learning models improve with more data.",
                "Blockchain technology enables decentralized systems.",
                "Asynchronous programming avoids thread-blocking operations.",
                "Design patterns provide reusable solutions to common problems.",
                "Cybersecurity threats evolve constantly in digital ecosystems.",
                "Containerization improves software deployment efficiency."
        };

        static void Main()
        {
            Console.WriteLine("Welcome to Typing Master!\n");

            while (true)
            {
                var difficulty = ChooseDifficulty();
                var sentence = GetRandomSentence(difficulty);

                PlayGame(sentence, difficulty);

                if (!AskToPlayAgain()) break;
                Console.Clear();
            }

            Console.WriteLine("\nThanks for playing!");
            Environment.Exit(0);
        }

        static Difficulty ChooseDifficulty()
        {
            Console.WriteLine("Choose difficulty:");
            Console.WriteLine("1 - Easy (short sentences)");
            Console.WriteLine("2 - Medium (technical terms)");
            Console.WriteLine("3 - Hard (complex sentences)");

            while (true)
            {
                var input = Console.ReadKey(true).KeyChar;
                switch (input)
                {
                    case '1': return Difficulty.Easy;
                    case '2': return Difficulty.Medium;
                    case '3': return Difficulty.Hard;
                }
            }
        }

        static string GetRandomSentence(Difficulty difficulty)
        {
            var rand = new Random();
            return difficulty switch
            {
                Difficulty.Easy => EasySentences[rand.Next(EasySentences.Length)],
                Difficulty.Medium => MediumSentences[rand.Next(MediumSentences.Length)],
                Difficulty.Hard => HardSentences[rand.Next(HardSentences.Length)],
                _ => EasySentences[0]
            };
        }

        static void PlayGame(string sentence, Difficulty difficulty)
        {
            // Setup
            Console.Clear();
            DisplayDifficultyHeader(difficulty);
            Console.WriteLine($"Type this:\n\n{sentence}\n");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey();
            Console.Clear();

            // Typing phase
            Console.WriteLine($"Original: {sentence}\n");
            Console.Write("Your turn: ");

            var timer = Stopwatch.StartNew();
            string input = Console.ReadLine()?.Trim() ?? "";
            timer.Stop();

            // Results
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("You didn't type anything!");
                return;
            }

            DisplayResults(sentence, input, timer.Elapsed, difficulty);
        }

        static void DisplayDifficultyHeader(Difficulty difficulty)
        {
            ConsoleColor color = difficulty switch
            {
                Difficulty.Easy => ConsoleColor.Green,
                Difficulty.Medium => ConsoleColor.Yellow,
                Difficulty.Hard => ConsoleColor.Red,
                _ => ConsoleColor.White
            };

            Console.ForegroundColor = color;
            Console.WriteLine($"=== {difficulty} MODE ===");
            Console.ResetColor();
        }

        static void DisplayResults(string original, string typed, TimeSpan time, Difficulty difficulty)
        {
            int errors = CountErrors(original, typed);
            double wpm = CalculateWPM(original, time.TotalMinutes);
            int score = CalculateScore(difficulty, errors, time.TotalSeconds);

            Console.WriteLine("\n--- Results ---");
            Console.WriteLine($"Time: {time.TotalSeconds:F2} seconds");
            Console.WriteLine($"Errors: {errors}");
            Console.WriteLine($"Speed: {wpm:F1} WPM");
            Console.WriteLine($"Score: {score}");

            Console.WriteLine("\nError analysis:");
            ShowColoredErrors(original, typed);
        }

        static int CountErrors(string original, string typed)
        {
            int errors = 0;
            int minLength = Math.Min(original.Length, typed.Length);

            for (int i = 0; i < minLength; i++)
                if (original[i] != typed[i]) errors++;

            return errors + Math.Abs(original.Length - typed.Length);
        }

        static double CalculateWPM(string text, double minutes)
        {
            if (minutes <= 0) return 0;
            int wordCount = text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
            return wordCount / minutes;
        }

        static int CalculateScore(Difficulty difficulty, int errors, double seconds)
        {
            int baseScore = difficulty switch
            {
                Difficulty.Easy => 100,
                Difficulty.Medium => 200,
                Difficulty.Hard => 350,
                _ => 0
            };

            double errorPenalty = errors * 5;
            double speedBonus = (50 / seconds) * 10;

            return (int)(baseScore - errorPenalty + speedBonus);
        }

        static void ShowColoredErrors(string original, string typed)
        {
            Console.WriteLine("Original: " + original);
            Console.Write("Your text: ");

            for (int i = 0; i < typed.Length; i++)
            {
                if (i >= original.Length || original[i] != typed[i])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(typed[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(typed[i]);
                }
            }

            if (typed.Length < original.Length)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(new string('_', original.Length - typed.Length));
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        static bool AskToPlayAgain()
        {
            Console.WriteLine("\nPlay again? (Y/N)");
            return Console.ReadKey(true).Key == ConsoleKey.Y;
        }
    }
}