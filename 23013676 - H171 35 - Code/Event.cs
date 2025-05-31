using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public abstract class Event
    {
        public int EventNo { get; set; }
        public string Venue { get; set; }
        public int VenueID { get; set; }
        public string EventDateTime { get; set; }
        protected double Record { get; set; }

        // Constructor: uses venue name
        public Event(int eventNo, string venue, string eventDateTime, double record)
        {
            EventNo = eventNo;
            Venue = venue;
            EventDateTime = eventDateTime;
            Record = record;
        }

        // Constructor overload: uses venue ID
        public Event(int eventNo, int venueID, string eventDateTime, double record)
        {
            EventNo = eventNo;
            VenueID = venueID;
            EventDateTime = eventDateTime;
            Record = record;
        }

        public double GetRecord()
        {
            return Record;
        }

        public void SetRecord(double newRecord)
        {
            Record = newRecord;
        }
    }
}