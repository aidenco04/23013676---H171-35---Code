using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class CompHistory
    {
        //where the competitor won last competition
        public string MostRecentWin { get; set; }


        //total number of competitions theyve won
        public int CareerWins { get; set; }


        //shows how many medals has been won
        public List<string> Medals { get; set; }

        //personal best time
        public double PersonalBest { get; set; }

        //sets new history record for competitor
        public CompHistory(string mostRecentWin, int careerWins, List<string> medals, double personalBest)
        {
            MostRecentWin = mostRecentWin;
            CareerWins = careerWins;
            Medals = medals ?? new List<string>();//creates empty list if none given
            PersonalBest = personalBest;
        }


        //shows history as readable string
        public override string ToString()
        {
            string medalsFormatted = string.Join(", ", Medals);
            return $"Recent Win: {MostRecentWin} | Career Wins: {CareerWins} | Medals: {medalsFormatted} | Personal Best: {PersonalBest}s";
        }

        //converts the history to one line to save to file
        public string ToFile()
        {
            string medalsFormatted = string.Join(";", Medals); 
            return $"{MostRecentWin},{CareerWins},{medalsFormatted},{PersonalBest}";
        }
    }
}