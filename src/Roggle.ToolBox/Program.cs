using Roggle.Core;
using System;
using System.Diagnostics;
using System.Linq;

namespace Roggle.ToolBox
{
    class Program
    {
        static int Main(string[] args)
        {
            bool isUserChoiceValid = false;

            while (!isUserChoiceValid)
            {
                string userChoice = GetUserChoice(args);

                switch (userChoice)
                {
                    case "C":
                        isUserChoiceValid = true;
                        CreateEventSource(
                            GetUserChoice(args, "Event source name: ", 1, false),
                            GetUserChoice(args, "Event log name: ", 2, false));
                        break;

                    case "W":
                        isUserChoiceValid = true;
                        TestWebRoggle(
                            GetUserChoice(args, "Url: ", 1, false),
                            new Guid(GetUserChoice(args, "Key: ", 2, false)),
                            GetUserChoice(args, "Source: ", 3, false));
                        break;

                    default:
                        isUserChoiceValid = false;
                        break;
                }
            }

            return 0;
        }

        static string GetUserChoice(string[] args)
        {
            string userChoice = args.ElementAtOrDefault(0);

            while (string.IsNullOrEmpty(userChoice))
            {
                Console.Clear();
                Console.WriteLine("Hello !");
                Console.WriteLine("What you want to do ?");
                Console.WriteLine("- (C)reate event source");
                Console.WriteLine("- Test a (W)eb Roggle");
                Console.Write("Your choice: ");
                userChoice = Console.ReadLine();
            }

            return userChoice;
        }

        static string GetUserChoice(string[] args, string question, int index, bool acceptEmpty)
        {
            string userChoice = args.ElementAtOrDefault(index);

            while (userChoice == null || (userChoice == "" && !acceptEmpty))
            {
                Console.Clear();
                Console.Write(question);
                userChoice = Console.ReadLine();
            }

            return userChoice;
        }

        static void CreateEventSource(string eventSource, string eventLog)
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("Create event source");
            Console.WriteLine("-------------------");
            Console.WriteLine();

            try
            {
                eventSource = eventSource ?? "Roggle";
                eventLog = eventLog ?? "Application";

                if (!EventLog.SourceExists(eventSource))
                {
                    Console.Write("Creating event source {0} in {1} event log... ", eventSource, eventLog);
                    EventLog.CreateEventSource(eventSource, eventLog);

                    if (EventLog.SourceExists(eventSource))
                    {
                        Console.WriteLine("OK");
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
                else
                {
                    Console.WriteLine("Event source already exists.");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("ERR");
                Console.WriteLine("Failed to create event source.");
            }
        }

        static void TestWebRoggle(string url, Guid key, string source)
        {
            Console.Clear();
            Console.WriteLine("-------------");
            Console.WriteLine("Test a Roggle");
            Console.WriteLine("-------------");
            Console.WriteLine();

            try
            {
                GRoggle.Use(new OverseerRoggle(url, key, source));
                GRoggle.Write("Test debug", RoggleLogLevel.Debug);
                GRoggle.Write("Test error very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very very", RoggleLogLevel.Error);
                GRoggle.Write("Test info", RoggleLogLevel.Info);
                GRoggle.Write("Test warning", RoggleLogLevel.Warning);
            }
            catch (Exception)
            {
                Console.WriteLine("ERR");
                Console.WriteLine("Failed to test Web Roggle.");
            }
        }
    }
}
