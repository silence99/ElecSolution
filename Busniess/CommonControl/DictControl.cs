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
        //public static DictSvr dictSvr;

        static DictControl()
        {
            //dictSvr = new DictSvr();
            //dictSvr.InitializeHttpRequest("");
        }

        //public static List<DictItem> GetEventTypes()
        //{
        //    List<DictItem> di = new List<DictItem>();

        //}
    }
}
