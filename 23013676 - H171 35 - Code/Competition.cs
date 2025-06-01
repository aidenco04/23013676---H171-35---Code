using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class Competition
    {
        //creates list to hold all of the competitors
        private List<Competitor> competitors = new List<Competitor>();

        public void AddCompetitor(Competitor c)
        {
            //adds a new competitor if competitor number doesnt already exist
            if (!competitors.Any(comp => comp.CompNumber == c.CompNumber))
            {
                competitors.Add(c); //adds competitor to the list
                Console.WriteLine("Competitor added.");
            }
            else
            {
                Console.WriteLine("Competitor with this number already exists.");
            }
        }

        // asks user for competitor number and removes that specific competitor
        public void RemoveCompetitor(int compNo)
        {
            competitors.RemoveAll(c => c.CompNumber == compNo);
        }

        //clears the full competitor list
        public void ClearAll()
        {
            competitors.Clear();
        }

        //returns all competitors who were in a specific event
        public List<Competitor> GetAllByEvent(int eventNo)
        {
            return competitors.Where(c => c.CompEvent.EventNo == eventNo).ToList();
        }

        //creates a dictionary that links competitor numbers to event numbers
        public Dictionary<int, int> CompIndex()
        {
            return competitors.ToDictionary(c => c.CompNumber, c => c.CompEvent.EventNo);
        }

        //sorts the competitors by age
        public void SortCompetitorsByAge()
        {
            competitors = competitors.OrderBy(c => c.CompAge).ToList();
        }

        //asks user for number and shows competitors that won more than that number
        public List<Competitor> Winners(int minWins)
        {
            return competitors.Where(c => c.History.CareerWins > minWins).ToList();
        }

        //lists the competitors who have placed top 3
        public List<Competitor> GetQualifiers()
        {
            return competitors.Where(c => c.Results.IsQualified()).ToList();
        }

        //prints all competitors
        public void PrintAll()
        {
            foreach (var c in competitors)
            {
                Console.WriteLine(c);
                Console.WriteLine("------------------------");
            }
        }

        //saves the competitors and their details to a file
        public void SaveToFile(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var c in competitors)
                {
                    sw.WriteLine(c.ToFile()); 
                    sw.WriteLine(); 
                }
            }
        }

        public void LoadFromFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("File not found.");
                    return;
                }

                var lines = File.ReadAllLines(filePath);
                for (int i = 0; i < lines.Length; i += 6)
                {
                    if (i + 4 >= lines.Length) break;

                    
                    var eventParts = lines[i].Split(',');
                    int eventNo = int.Parse(eventParts[0]);
                    string venue = eventParts[1];
                    string dateTime = eventParts[2];
                    double record = double.Parse(eventParts[3]);

                    
                    var bsParts = lines[i + 1].Split(',');
                    int distance = int.Parse(bsParts[1]);
                    double winningTime = double.Parse(bsParts[2]);

                    var bsEvent = new BreastStroke(eventNo, venue, dateTime, record, distance, winningTime);

                    
                    var resultParts = lines[i + 2].Split(',');
                    int placed = int.Parse(resultParts[0]);
                    double raceTime = double.Parse(resultParts[1]);

                    var result = new Result(placed, raceTime);

                    
                    var historyParts = lines[i + 3].Split(',');
                    string mostRecentWin = historyParts[0];
                    int careerWins = int.Parse(historyParts[1]);
                    List<string> medals = new List<string>(historyParts[2].Split(';'));
                    double personalBest = double.Parse(historyParts[3]);

                    var history = new CompHistory(mostRecentWin, careerWins, medals, personalBest);

                    
                    var compParts = lines[i + 4].Split(',');
                    int compNum = int.Parse(compParts[0]);
                    string name = compParts[1];
                    int age = int.Parse(compParts[2]);
                    string hometown = compParts[3];

                    var competitor = new Competitor(compNum, name, age, hometown, bsEvent, result, history);
                    AddCompetitor(competitor);
                }

                Console.WriteLine("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
            }
        }

        
        public bool CheckCompetitor(int compNo)
        {
            return competitors.Any(c => c.CompNumber == compNo);
        }

        public Competitor GetCompetitor(int compNo)
        {
            return competitors.FirstOrDefault(c => c.CompNumber == compNo);
        }

        public BreastStroke GetEvent(int eventNo)
        {
            return competitors
                .Select(c => c.CompEvent)
                .FirstOrDefault(e => e.EventNo == eventNo);
        }
    }
}