using Business.Services;
using Emergence.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Business.CommonControl
{
    public static class DictControl
    {
        public static List<DictItem> Event_type;
        public static List<DictItem> Event_grade;
        public static List<DictItem> Event_state;
        public static DictSvr dictSvr;

        static DictControl()
        {
            dictSvr = new DictSvr();
        }

        public static List<DictItem> GetEventTypes()
        {
            if (Event_type == null)
            {
                List<DictItem> di = new List<DictItem>();
                dictSvr.RequestData = "&type=event_type";
                di = dictSvr.ProcessRequest("");
                Event_type = di;
            }

            return Event_type;
        }
        public static List<DictItem> GetEventGrades()
        {
            if (Event_type == null)
            {
                List<DictItem> di = new List<DictItem>();
                dictSvr.RequestData = "&type=event_grade";
                di = dictSvr.ProcessRequest("");
                Event_type = di;
            }

            return Event_type;
        }
        public static List<DictItem> GetEventStates()
        {
            if (Event_type == null)
            {
                List<DictItem> di = new List<DictItem>();
                dictSvr.RequestData = "&type=event_state";
                di = dictSvr.ProcessRequest("");
                Event_type = di;
            }

            return Event_type;
        }
    }
}
