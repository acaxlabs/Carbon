
using System;

namespace Carbon.Calendar
{
    public class VEvent
    {
        private DateTime _utcStartDate = DateTime.MinValue;
        private DateTime _utcEndDate = DateTime.MinValue;
        private bool _allDayEvent = false;
        private string _location;
        private string _note;
        private string _summary;
        private string _fullName;
        private string _eMail;

        public bool AllDayEvent
        {
            get
            {
                return this._allDayEvent;
            }
            set
            {
                this._allDayEvent = value;
            }
        }

        public string Email
        {
            get
            {
                return this._eMail;
            }
            set
            {
                this._eMail = value;
            }
        }

        public string FullName
        {
            get
            {
                return this._fullName;
            }
            set
            {
                this._fullName = value;
            }
        }

        public string Summary
        {
            get
            {
                return this._summary;
            }
            set
            {
                this._summary = value;
            }
        }

        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        public DateTime UtcEndDate
        {
            get
            {
                return this._utcEndDate;
            }
            set
            {
                this._utcEndDate = value;
                if (!(this._utcStartDate == DateTime.MinValue))
                    return;
                this._utcStartDate = value;
            }
        }

        public DateTime UtcStartDate
        {
            get
            {
                return this._utcStartDate;
            }
            set
            {
                this._utcStartDate = value;
                if (!(this._utcEndDate == DateTime.MinValue))
                    return;
                this._utcEndDate = value;
            }
        }

        public string Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }
    }
}
