using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class Competitor
    {
        public int CompNumber { get; set; }
        public string CompName { get; set; }
        public int CompAge { get; set; }
        public string Hometown { get; set; }

        public BreastStroke CompEvent { get; set; }
        public Result Results { get; set; }
        public CompHistory History { get; set; }

        public bool NewPB => IsNewPB(); 

        public Competitor(int number, string name, int age, string hometown,
                          BreastStroke compEvent, Result results, CompHistory history)
        {
            CompNumber = number;
            CompName = name;
            CompAge = age;
            Hometown = hometown;
            CompEvent = compEvent;
            Results = results;
            History = history;
        }

        public bool IsNewPB()
        {
            if (Results.RaceTime < History.PersonalBest)
            {
                History.PersonalBest = Results.RaceTime; 
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"#{CompNumber} - {CompName} ({CompAge}), {Hometown}\n" +
                   $"- New PB: {NewPB}\n" +
                   $"- Event: {CompEvent}\n" +
                   $"- Results: {Results}\n" +
                   $"- History: {History}\n";
        }

        public string ToFile()
        {
            return $"{CompEvent.EventNo},{CompEvent.Venue},{CompEvent.EventDateTime},{CompEvent.GetRecord()}\n" +
                   $"{CompEvent.EventType},{CompEvent.Distance},{CompEvent.WinningTime},{CompEvent.NewRecord}\n" +
                   $"{Results.Placed},{Results.RaceTime},{Results.Qualified}\n" +
                   $"{History.MostRecentWin},{History.CareerWins},{string.Join(";", History.Medals)},{History.PersonalBest}\n" +
                   $"{CompNumber},{CompName},{CompAge},{Hometown},{NewPB}";
        }

    }
}