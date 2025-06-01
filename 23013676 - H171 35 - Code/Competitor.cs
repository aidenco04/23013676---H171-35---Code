using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class Competitor
    {
        //competitor number
        public int CompNumber { get; set; }

        //competitors full name
        public string CompName { get; set; }

        //competitors age
        public int CompAge { get; set; }

        //cometitors hometown
        public string Hometown { get; set; }

        //event competitor is competing in
        public BreastStroke CompEvent { get; set; }

        //competitors result of the event
        public Result Results { get; set; }

        //result history and medal history of competitor
        public CompHistory History { get; set; }

        //shows if competitor beat their personal best
        public bool NewPB => IsNewPB(); 

        //uses all details and creates new competitor
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

        //checks to see if this race is new personal best
        public bool IsNewPB()
        {
            if (Results.RaceTime < History.PersonalBest)
            {
                History.PersonalBest = Results.RaceTime;  //updates personal best if beaten
                return true;
            }
            return false;
        }

        //shows competitor information as text
        public override string ToString()
        {
            return $"#{CompNumber} - {CompName} ({CompAge}), {Hometown}\n" +
                   $"- New PB: {NewPB}\n" +
                   $"- Event: {CompEvent}\n" +
                   $"- Results: {Results}\n" +
                   $"- History: {History}\n";
        }

        //converts the competitor to 5 line file format
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