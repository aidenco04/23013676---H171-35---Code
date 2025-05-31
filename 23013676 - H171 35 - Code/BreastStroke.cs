using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public class BreastStroke : Event
    {
        public string EventType { get; set; } = "Breaststroke";  // Auto-set
        public int Distance { get; set; }
        public double WinningTime { get; set; }
        public bool NewRecord { get; private set; }

        // Constructor: using venue name
        public BreastStroke(int eventNo, string venue, string eventDateTime, double record,
                            int distance, double winningTime)
            : base(eventNo, venue, eventDateTime, record)
        {
            Distance = distance;
            WinningTime = winningTime;
            NewRecord = IsNewRecord(); // Auto-check
        }

        public bool IsNewRecord()
        {
            if (WinningTime < GetRecord())
            {
                SetRecord(WinningTime); // Update record
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{EventType} | Event #{EventNo} | Venue: {Venue ?? VenueID.ToString()} | Date: {EventDateTime} | " +
                   $"Distance: {Distance}m | Winning Time: {WinningTime}s | New Record: {NewRecord}";
        }

        public string ToFile()
        {
            return $"{EventNo},{Venue},{EventDateTime},{GetRecord()},{EventType},{Distance},{WinningTime},{NewRecord}";
        }
    }
}