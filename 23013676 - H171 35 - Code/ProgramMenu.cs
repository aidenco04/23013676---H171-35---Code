using System;
using System.Collections.Generic;
using System.IO;
using OlympicQualifiers.Models;

namespace OlympicQualifiers
{
    public class ProgramMenu
    {
        private Competition competition = new Competition();
        private bool running = true;

        public void Start()
        {
            Console.WriteLine("Loading system...");
            
            while (running)
            {
                Console.WriteLine("\n=== Olympic Swimming Qualifiers Menu ===");
                Console.WriteLine("1. Add Competitor");
                Console.WriteLine("2. Remove Competitor");
                Console.WriteLine("3. View All Competitors");
                Console.WriteLine("4. View Qualifiers");
                Console.WriteLine("5. View Winners (Career Wins > Target)");
                Console.WriteLine("6. Sort by Age");
                Console.WriteLine("7. Save to File");
                Console.WriteLine("8. Load from File");
                Console.WriteLine("9. Exit");
                Console.Write("Enter option (1-9): ");
                
                switch (Console.ReadLine())
                {
                    case "1": AddCompetitor(); break;
                    case "2": RemoveCompetitor(); break;
                    case "3": competition.PrintAll(); break;
                    case "4": ViewQualifiers(); break;
                    case "5": ViewWinners(); break;
                    case "6": competition.SortCompetitorsByAge(); break;
                    case "7": SaveToFile(); break;
                    case "8": LoadFromFile(); break;
                    case "9": running = false; break;
                    default: Console.WriteLine("Invalid option."); break;
                }
            }
        }

        private void AddCompetitor()
        {
            try
            {
                Console.Write("Enter Competitor Number (100–999): ");
                int compNum = int.Parse(Console.ReadLine());
                if (compNum < 100 || compNum > 999)
                    throw new ArgumentException("Competitor number must be between 100 and 999.");

                if (competition.CheckCompetitor(compNum))
                {
                    Console.WriteLine("Competitor already exists.");
                    return;
                }

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Age: ");
                int age = int.Parse(Console.ReadLine());
                if (age <= 0) throw new ArgumentException("Age must be greater than zero.");

                Console.Write("Hometown: ");
                string hometown = Console.ReadLine();

                
                Console.Write("Event Number (1–100): ");
                int eventNo = int.Parse(Console.ReadLine());
                if (eventNo < 1 || eventNo > 100)
                    throw new ArgumentException("Event number must be 1–100.");

                Console.Write("Venue Name: ");
                string venue = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(venue))
                    throw new ArgumentException("Venue name must not be empty.");

                Console.Write("Event DateTime (e.g. 21-05-1956 14:30): ");
                string eventDate = Console.ReadLine();

                Console.Write("Event Record Time (in seconds): ");
                double record = double.Parse(Console.ReadLine());

                Console.Write("Distance (50–1500m): ");
                int distance = int.Parse(Console.ReadLine());
                if (distance < 50 || distance > 1500)
                    throw new ArgumentException("Distance must be between 50 and 1500.");

                Console.Write("Winning Time (sec): ");
                double winningTime = double.Parse(Console.ReadLine());
                if (winningTime <= 0)
                    throw new ArgumentException("Winning time must be a positive number.");

                var eventObj = new BreastStroke(eventNo, venue, eventDate, record, distance, winningTime);

                
                Console.Write("Placed (1–8): ");
                int placed = int.Parse(Console.ReadLine());
                if (placed < 1 || placed > 8)
                    throw new ArgumentException("Placed must be between 1 and 8.");

                Console.Write("Race Time (sec): ");
                double raceTime = double.Parse(Console.ReadLine());
                if (raceTime <= 0)
                    throw new ArgumentException("Race time must be a positive number.");

                var result = new Result(placed, raceTime);


                Console.Write("Most Recent Win Location: ");
                string recentWin = Console.ReadLine();

                Console.Write("Career Wins: ");
                int careerWins = int.Parse(Console.ReadLine());
                if (careerWins < 0)
                    throw new ArgumentException("Career wins must be 0 or greater.");

                Console.Write("Medals (comma-separated e.g. 2G,1S): ");
                string[] medals = Console.ReadLine().Split(',');

                Console.Write("Personal Best Time: ");
                double personalBest = double.Parse(Console.ReadLine());
                if (personalBest <= 0)
                    throw new ArgumentException("Personal best must be a positive number.");

                var history = new CompHistory(recentWin, careerWins, new List<string>(medals), personalBest);

                
                var competitor = new Competitor(compNum, name, age, hometown, eventObj, result, history);
                competition.AddCompetitor(competitor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        


        private void RemoveCompetitor()
        {
            Console.Write("Enter Competitor Number to remove: ");
            if (int.TryParse(Console.ReadLine(), out int compNum))
            {
                competition.RemoveCompetitor(compNum);
                Console.WriteLine("Competitor removed (if they existed).");
            }
            else
            {
                Console.WriteLine("Invalid number.");
            }
        }

        private void ViewQualifiers()
        {
            var qualifiers = competition.GetQualifiers();
            if (qualifiers.Count == 0) Console.WriteLine("No qualifiers.");
            else foreach (var c in qualifiers) Console.WriteLine(c);
        }

        private void ViewWinners()
        {
            Console.Write("Enter minimum number of career wins: ");
            if (int.TryParse(Console.ReadLine(), out int target))
            {
                var winners = competition.Winners(target);
                if (winners.Count == 0) Console.WriteLine("No winners meet the criteria.");
                else foreach (var c in winners) Console.WriteLine(c);
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        private void SaveToFile()
        {
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string filePath = Path.Combine(downloadsPath, "competition_output.txt");

            competition.SaveToFile(filePath);
            Console.WriteLine($"✅ File saved to: {filePath}");
        }


        private void LoadFromFile()
        {
            Console.Write("Enter file name (e.g. data.txt): ");
            string fileName = Console.ReadLine();
            competition.LoadFromFile(fileName);
        }
    }
}
