
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace Carbon.Calendar
{
    public class VCalendarReader : StreamReader
    {
        private VCalendar _vcal;
        private VEvent _vevent;

        public VCalendarReader(Stream stream)
          : base(stream)
        {
            this._vcal = new VCalendar();
            this._vevent = new VEvent();
        }

        public static VCalendar Read(string url)
        {
            return new VCalendarReader(WebRequest.Create(url).GetResponse().GetResponseStream()).Read();
        }

        public VCalendar Read()
        {
            while (!this.EndOfStream)
            {
                string line = this.ReadLine();
                if (line.IndexOf(":") > 0)
                    this.DoLine(line);
            }
            return this._vcal;
        }

        private void DoLine(string line)
        {
            switch (line.Trim())
            {
                case "BEGIN:VCALENDAR":
                    this._vcal = new VCalendar();
                    this._vcal.Events = new List<VEvent>();
                    break;
                case "BEGIN:VEVENT":
                    this._vevent = new VEvent();
                    break;
                case "END:VEVENT":
                    this._vcal.Events.Add(this._vevent);
                    break;
                default:
                    this.ParseLine(line);
                    break;
            }
        }

        private void ParseLine(string line)
        {
            int length = line.IndexOfAny(":;".ToCharArray());
            string str1 = line.Substring(0, length);
            string str2 = line.Substring(length + 1);
            switch (str1.Trim())
            {
                case "DTSTART":
                    this._vevent.UtcStartDate = this.ParseDate(str2);
                    break;
                case "DTEND":
                    this._vevent.UtcEndDate = this.ParseDate(str2);
                    break;
                case "X-WR-CALNAME":
                    this._vcal.Name = str2;
                    break;
                case "SUMMARY":
                    this._vevent.Summary = str2;
                    break;
            }
        }

        private DateTime ParseDate(string value)
        {
            string s;
            if (value.Contains(":"))
            {
                int num = value.LastIndexOf(":");
                s = value.Substring(num + 1);
            }
            else
                s = value;
            string[] formats = "yyyyMMddTHHmmss,yyyyMMdd".Split(",".ToCharArray());
            return XmlConvert.ToDateTime(s, formats);
        }
    }
}
