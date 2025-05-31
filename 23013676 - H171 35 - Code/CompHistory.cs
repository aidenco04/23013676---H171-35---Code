using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class CompHistory
    {
        public string MostRecentWin { get; set; }
        public int CareerWins { get; set; }
        public List<string> Medals { get; set; }
        public double PersonalBest { get; set; }

        public CompHistory(string mostRecentWin, int careerWins, List<string> medals, double personalBest)
        {
            MostRecentWin = mostRecentWin;
            CareerWins = careerWins;
            Medals = medals ?? new List<string>();
            PersonalBest = personalBest;
        }

        public override string ToString()
        {
            string medalsFormatted = string.Join(", ", Medals);
            return $"Recent Win: {MostRecentWin} | Career Wins: {CareerWins} | Medals: {medalsFormatted} | Personal Best: {PersonalBest}s";
        }

        public string ToFile()
        {
            string medalsFormatted = string.Join(";", Medals); // Use ; to avoid conflict with commas
            return $"{MostRecentWin},{CareerWins},{medalsFormatted},{PersonalBest}";
        }
    }
}