using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    // Clase que representa una entrada del diario
    public class JournalEntry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public JournalEntry(string prompt, string response)
        {
            Prompt = prompt;
            Response = response;
            Date = DateTime.Now.ToShortDateString();
        }

        public override string ToString()
        {
            return $"{Date} | {Prompt} | {Response}";
        }
    }

    // Clase que maneja el diario
    public class Journal
    {
        private List<JournalEntry> entries;
        private Random random;

        public Journal()
        {
            entries = new List<JournalEntry>();
            random = new Random();
        }

        public void AddEntry(string response)
        {
            string prompt = GetRandomPrompt();
            JournalEntry entry = new JournalEntry(prompt, response);
            entries.Add(entry);
        }

        public void DisplayEntries()
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    outputFile.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    entries.Add(new JournalEntry(parts[1], parts[2]) { Date = parts[0] });
                }
            }
        }

        private string GetRandomPrompt()
        {
            string[] prompts = new string[]
            {
                "Who was the most interesting person I interacted with today?",
                "What was the best part of my day?",
                "How did I see the hand of the Lord in my life today?",
                "What was the strongest emotion I felt today?",
                "If I had one thing I could do over today, what would it be?"
            };
            return prompts[random.Next(prompts.Length)];
        }
    }

    // Clase principal
    public class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\nJournal Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal entries");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1-5): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Your response: ");
                        string response = Console.ReadLine();
                        journal.AddEntry(response);
                        Console.WriteLine("Entry added!");
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        Console.Write("Enter filename to save: ");
                        string saveFilename = Console.ReadLine();
                        journal.SaveToFile(saveFilename);
                        Console.WriteLine("Journal saved!");
                        break;
                    case "4":
                        Console.Write("Enter filename to load: ");
                        string loadFilename = Console.ReadLine();
                        journal.LoadFromFile(loadFilename);
                        Console.WriteLine("Journal loaded!");
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("Exiting the program.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please choose again.");
                        break;
                }
            }
        }
    }
}
