using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class Result
    {
        public int Placed { get; set; }
        public double RaceTime { get; set; }
        public bool Qualified => IsQualified(); 

        public Result(int placed, double raceTime)
        {
            Placed = placed;
            RaceTime = raceTime;
        }

        public bool IsQualified()
        {
            return Placed >= 1 && Placed <= 3;
        }

        public override string ToString()
        {
            return $"Placed: {Placed}, Time: {RaceTime}s, Qualified: {Qualified}";
        }

        public string ToFile()
        {
            return $"{Placed},{RaceTime},{Qualified}";
        }
    }
}