
using System.Collections.Generic;

namespace Carbon.Calendar
{
    public class VCalendar
    {
        private string _name;
        private List<VEvent> _events;

        public VCalendar()
        {
            this._events = new List<VEvent>();
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public List<VEvent> Events
        {
            get
            {
                return this._events;
            }
            set
            {
                this._events = value;
            }
        }
    }
}
