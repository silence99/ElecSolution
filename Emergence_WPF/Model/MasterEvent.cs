﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence_WPF.Model
{
    public class MasterEvent
    {
        public long ID { get; set; }
        public bool IsCheck{ get;set; }
        public string SerialNumber { get; set; }
        public string Title { get; set; }
        public string EventType { get; set; }
        public string Grade { get; set; }
        public string Time { get; set; }
        public string Describe { get; set; }
        public string SubmitParty { get; set; }
        public string TelephoneNumber { get; set; }
        public string SubmitDept { get; set; }
        public string Locale { get; set; }

    }
}
