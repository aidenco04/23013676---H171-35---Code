using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class Result
    {
        //what place competitor placed in event
        public int Placed { get; set; }

        //their race time in seconds
        public double RaceTime { get; set; }

        //if they got into top 3 returns true
        public bool Qualified => IsQualified(); 

        //sets up new result with place and time
        public Result(int placed, double raceTime)
        {
            Placed = placed;
            RaceTime = raceTime;
        }

        //checks if the competitor got 1st to 3rd and if so is qualified
        public bool IsQualified()
        {
            return Placed >= 1 && Placed <= 3;
        }

        //shows results as a readable string
        public override string ToString()
        {
            return $"Placed: {Placed}, Time: {RaceTime}s, Qualified: {Qualified}";
        }

        //converts the results to one line to save to file
        public string ToFile()
        {
            return $"{Placed},{RaceTime},{Qualified}";
        }
    }
}