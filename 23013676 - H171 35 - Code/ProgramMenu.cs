using System;
using System.Collections.Generic;
using System.IO;
using OlympicQualifiers.Models;

namespace OlympicQualifiers
{
    public class ProgramMenu
    {
        //creates competition object to hold competitor
        private Competition competition = new Competition();
        private bool running = true;

        //creates the menu loop
        public void Start()
        {
            Console.WriteLine("Loading system...");
            
            //runs the menu until it is exited
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
                
                //runs chosen menu option
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

        //asks user to input details to create competitor
        private void AddCompetitor()
        {
            try
            {
                //checks to see if competitor id number is between 100-999
                Console.Write("Enter Competitor Number (100–999): ");
                int compNum = int.Parse(Console.ReadLine());
                if (compNum < 100 || compNum > 999)
                    throw new ArgumentException("Competitor number must be between 100 and 999.");

                //checks to see if number is already used
                if (competition.CheckCompetitor(compNum))
                {
                    Console.WriteLine("Competitor already exists.");
                    return;
                }

                Console.Write("Name: ");
                string name = Console.ReadLine();

                //checks to see if competitor age is over 0
                Console.Write("Age: ");
                int age = int.Parse(Console.ReadLine());
                if (age <= 0) throw new ArgumentException("Age must be greater than zero.");

                Console.Write("Hometown: ");
                string hometown = Console.ReadLine();

                //checks to see if event number is between 1-100
                Console.Write("Event Number (1–100): ");
                int eventNo = int.Parse(Console.ReadLine());
                if (eventNo < 1 || eventNo > 100)
                    throw new ArgumentException("Event number must be 1–100.");

                //makes sure user cant leave venue name blank
                Console.Write("Venue Name: ");
                string venue = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(venue))
                    throw new ArgumentException("Venue name must not be empty.");

                Console.Write("Event DateTime (e.g. 21-05-1956 14:30): ");
                string eventDate = Console.ReadLine();

                Console.Write("Event Record Time (in seconds): ");
                double record = double.Parse(Console.ReadLine());

                //checks to see if distance is between 50-100 meters
                Console.Write("Distance (50–1500m): ");
                int distance = int.Parse(Console.ReadLine());
                if (distance < 50 || distance > 1500)
                    throw new ArgumentException("Distance must be between 50 and 1500.");

                //checks to see if winning time is over 0
                Console.Write("Winning Time (sec): ");
                double winningTime = double.Parse(Console.ReadLine());
                if (winningTime <= 0)
                    throw new ArgumentException("Winning time must be a positive number.");

                //creates the breaststroke event object
                var eventObj = new BreastStroke(eventNo, venue, eventDate, record, distance, winningTime);

                
                //checks to see if position placed is between 1-8
                Console.Write("Position placed (1–8): ");
                int placed = int.Parse(Console.ReadLine());
                if (placed < 1 || placed > 8)
                    throw new ArgumentException("position placed must be between 1 and 8.");

                //checks to see if race time is above 0
                Console.Write("Race Time (sec): ");
                double raceTime = double.Parse(Console.ReadLine());
                if (raceTime <= 0)
                    throw new ArgumentException("Race time must be a positive number.");

                //creates event results object
                var result = new Result(placed, raceTime);


                Console.Write("Most Recent Win Location: ");
                string recentWin = Console.ReadLine();

                //checks to see if career wins is above 0
                Console.Write("Career Wins: ");
                int careerWins = int.Parse(Console.ReadLine());
                if (careerWins < 0)
                    throw new ArgumentException("Career wins must be 0 or greater.");

                Console.Write("Medals Won (comma-separated e.g. 2G,1S): ");
                string[] medals = Console.ReadLine().Split(',');

                //checks to see if personal best time is above 0
                Console.Write("Personal Best Time: ");
                double personalBest = double.Parse(Console.ReadLine());
                if (personalBest <= 0)
                    throw new ArgumentException("Personal best must be a positive number.");

                //creates history object
                var history = new CompHistory(recentWin, careerWins, new List<string>(medals), personalBest);

                
                var competitor = new Competitor(compNum, name, age, hometown, eventObj, result, history);
                competition.AddCompetitor(competitor);
            }
            catch (Exception ex)
            {
                //shows any input errors
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        

        //removes competitor by competitor number
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

        //shows all competitors who got place 3rd and above
        private void ViewQualifiers()
        {
            var qualifiers = competition.GetQualifiers();
            if (qualifiers.Count == 0) Console.WriteLine("No qualifiers.");
            else foreach (var c in qualifiers) Console.WriteLine(c);
        }

        //shows competitors with chosen number of wins
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
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");//saves file to Downloads
            string filePath = Path.Combine(downloadsPath, "competition_output.txt");//chooses name for file

            competition.SaveToFile(filePath);
            Console.WriteLine($"✅ File saved to: {filePath}");
        }

        //loads file from full path to file.
        private void LoadFromFile()
        {
            Console.Write("Enter file name (e.g. data.txt): ");
            string fileName = Console.ReadLine();
            competition.LoadFromFile(fileName);
        }
    }
}
