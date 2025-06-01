using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicQualifiers.Models
{
    public abstract class Event
    {
        //event identity number
        public int EventNo { get; set; }

        //name of venue
        public string Venue { get; set; }

        //id number of venue
        public int VenueID { get; set; }

        //date and time of event
        public string EventDateTime { get; set; }

        //record time to beat
        protected double Record { get; set; }

        //sets up event using venue name
        public Event(int eventNo, string venue, string eventDateTime, double record)
        {
            EventNo = eventNo;
            Venue = venue;
            EventDateTime = eventDateTime;
            Record = record;
        }

        //sets event using venue id instead of the name
        public Event(int eventNo, int venueID, string eventDateTime, double record)
        {
            EventNo = eventNo;
            VenueID = venueID;
            EventDateTime = eventDateTime;
            Record = record;
        }

        //sets record of the event
        public double GetRecord()
        {
            return Record;
        }

        //updates record if it was beaten by competitor
        public void SetRecord(double newRecord)
        {
            Record = newRecord;
        }
    }
}