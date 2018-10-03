
using System;
using System.IO;

namespace Carbon.Calendar
{
    public class VCalendarWriter : StreamWriter
    {
        private const string DEFAULT_CALNAME = "getinsured.com";
        private string calendarName;

        public string CalendarName
        {
            get
            {
                if (string.IsNullOrEmpty(calendarName))
                {
                    calendarName = DEFAULT_CALNAME;
                }
                return calendarName;
            }
            set
            {
                calendarName = value;
            }
        }

        public VCalendarWriter(Stream stream)
          : base(stream)
        {
        }

        public void Write(VCalendar vcalendar)
        {
            this.WriteLine("BEGIN:VCALENDAR");
            this.WriteLine($"PRODID:-//acaxlabs //{calendarName} //EN");
            this.WriteLine("VERSION:2.0");
            this.WriteLine("CALSCALE:GREGORIAN");
            this.WriteLine("METHOD:PUBLISH");
            this.WriteLine($"X-WR-CALNAME:{calendarName} Events");
            foreach (VEvent vevent in vcalendar.Events)
                this.Write(vevent);
            this.WriteLine("END:VCALENDAR");
            this.Flush();
        }

        public void Write(VEvent vevent)
        {
            this.WriteLine("BEGIN:VEVENT");
            this.WriteLine("LOCATION;ENCODING=QUOTED-PRINTABLE:{0}", (object)vevent.Location);
            if (vevent.AllDayEvent)
            {
                this.WriteLine("DTSTART;VALUE=DATE:{0:yyyyMMdd}", (object)vevent.UtcStartDate);
                this.WriteLine("DTEND;VALUE=DATE:{0:yyyyMMdd}", (object)vevent.UtcEndDate);
            }
            else
            {
                this.WriteLine("DTSTART:{0}Z", (object)vevent.UtcStartDate.ToString("s"));
                this.WriteLine("DTEND:{0}Z", (object)vevent.UtcEndDate.ToString("s"));
            }
            this.WriteLine("DESCRIPTION;ENCODING=QUOTED-PRINTABLE:{0}", (object)vevent.Note.Replace("=", "=3D").Replace("\r\n", "=0D=0A"));
            this.WriteLine("SUMMARY:{0}", (object)vevent.Summary.Replace("\r\n", ""));
            this.WriteLine("PRIORITY:3");
            this.WriteLine("UID:{0}", (object)Guid.NewGuid().ToString());
            this.WriteLine("DTSTAMP:{0}Z", (object)DateTime.UtcNow.ToString("s"));
            this.WriteLine("ORGANIZER;CN={0}:MAILTO:{1}", (object)vevent.FullName, (object)vevent.Email);
            this.WriteLine("CLASS:PRIVATE");
            this.WriteLine("CREATED:20070822T023640Z");
            this.WriteLine("LAST-MODIFIED:20070822T034209Z");
            this.WriteLine("SEQUENCE:4");
            this.WriteLine("STATUS:CONFIRMED");
            this.WriteLine("END:VEVENT");
        }
    }
}
