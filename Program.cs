using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    private static int logCount = 1;

    // Logging function.
    private static void LogActity(string message)
    {
        string time = DateTime.Now.ToString("hh:mm:ss tt");
        string logPath = Environment.CurrentDirectory.ToString() + "\\LogFiles\\Log" + logCount.ToString() + ".txt";

        Console.WriteLine($"{time}: {message}"); 

        using (var writer = File.AppendText(logPath))
        {
            writer.WriteLine($"{time}: {message}");
        }
    }

    // Logging function.
    private static void LogBreakPoint()
    {
        string logPath = Environment.CurrentDirectory.ToString() + "\\LogFiles\\Log" + logCount.ToString() + ".txt";

        Console.WriteLine("------------------------------------------------");
        using (var writer = File.AppendText(logPath))
        {
            writer.WriteLine("------------------------------------------------");
        }
    }

    // Function to write string to file according to string length.
    private static void WriteToFile(List<string> list, int length)
    {
        string outputPath = Environment.CurrentDirectory.ToString() + "\\ProcessedFiles\\ProcessedFile" + length.ToString() + ".txt";
        File.AppendAllLines(outputPath, list);
    }

    public static void Main()
    {
        try
        {
            for (int i = 1; i < 256; i++)
            {
                int fileCounter = 1;
                logCount = i;
                LogActity($"Starting to go though words of length {i}");
                LogBreakPoint();
                List<string> list = new List<string>();

                // Loop through all the files in the current loop.
                // Add the words that are of length i to the list.
                while (true)
                {
                    string inputPath = Environment.CurrentDirectory.ToString() + "\\TextFiles\\TextFile" + fileCounter.ToString() + ".txt";

                    if (File.Exists(inputPath))
                    {
                        LogActity($"Processing TextFile{fileCounter}.txt");
                        string[] words = File.ReadAllLines(inputPath);

                        foreach (var word in words)
                        {
                            if (word.Length == i)
                            {
                                list.Add(word);
                            }

                            if (list.Count >= 10000000)
                            {
                                LogBreakPoint();
                                LogActity("Performing an emergency write");
                                list = list.Distinct().ToList();
                                list.Remove(" ");
                                WriteToFile(list, i);

                                list = new List<string>();
                                LogBreakPoint();
                            }
                        }

                        LogActity($"Finished TextFile{fileCounter}.txt");
                        LogBreakPoint();
                    }
                    else
                    {
                        LogBreakPoint();
                        break;
                    }

                    fileCounter++;
                }

                LogActity("Cleaning up List.");
                list = list.Distinct().ToList();
                list.Remove(" ");

                LogActity("Writing out the contents of the list.");
                WriteToFile(list, i);
                LogBreakPoint();
            }
        }
        catch (Exception ex)
        {
            LogActity(ex.Message);
        }

        Console.WriteLine("Press any key to continue...");
        _ = Console.ReadKey();
    }
}
