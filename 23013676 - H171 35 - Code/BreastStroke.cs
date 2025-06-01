using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class BreastStroke : Event
    {
        //always gets set to breaststroke
        public string EventType { get; set; } = "Breaststroke";  

        //how ling the race is in meters
        public int Distance { get; set; }

        //winning time in seconds
        public double WinningTime { get; set; }

        //sets to true if the event broke the recod
        public bool NewRecord { get; private set; }

        //sets up a new breaststroke event
        public BreastStroke(int eventNo, string venue, string eventDateTime, double record,
                            int distance, double winningTime)
            : base(eventNo, venue, eventDateTime, record)
        {
            Distance = distance;
            WinningTime = winningTime;
            NewRecord = IsNewRecord(); //checks if record was beaten
        }

        //checks to see if winning time beat the old record
        public bool IsNewRecord()
        {
            if (WinningTime < GetRecord())
            {
                SetRecord(WinningTime); //updates old record
                return true;
            }
            return false;
        }

        //shows results in a readable string
        public override string ToString()
        {
            return $"{EventType} | Event #{EventNo} | Venue: {Venue ?? VenueID.ToString()} | Date: {EventDateTime} | " +
                   $"Distance: {Distance}m | Winning Time: {WinningTime}s | New Record: {NewRecord}";
        }

        //converts the event to one line to save to file
        public string ToFile()
        {
            return $"{EventNo},{Venue},{EventDateTime},{GetRecord()},{EventType},{Distance},{WinningTime},{NewRecord}";
        }
    }
}