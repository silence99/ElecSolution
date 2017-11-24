using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence_WPF.Model
{
    public class Event : NotificationObject
    {
        public string Guid { get; set; }
        public bool IsCheck
        {
            get;
            set;
        }
        public string MessageHeader{get;set;}
        public string IncidentTime{get;set;}
        public string IncidentArea{get;set;}
        public string SubmittingUnit{get;set;}
        public string EventType{get;set;}
        public string EventLevel{get;set;}
        public string EventStatus{get;set;}
        public string EventSource{get;set;}

        public string baosongshijian { get; set; }
        public string sheng { get; set; }
        public string shi { get; set; }
        public string qu { get; set; }
        public string fashengshijian { get; set; }
        public string jingdu { get; set; }
        public string weidu { get; set; }
        public string lianxidianhua { get; set; }
        public string baosongren { get; set; }

        public string qianfalingdao { get; set; }
        public string weizhixinxi { get; set; }
        public string qingkuangmiaoshu { get; set; }
        public string yuanyinfenxi { get; set; }
        public string yingxiangfanwei { get; set; }
        public string loginUser { get; set; }

        public string liuzhuanzhuangtai { get; set; }
        public string Approver { get; set; }
        public string Publisher { get; set; }
        public string Leadership { get; set; }
       
    
    }
    public class EmergencyOrganizationClass : NotificationObject
    {

        public bool IsCheck { get; set; }
        public string TeamName { get; set; }
        public string Rescue { get; set; }
        public string IsitFree { get; set; }
        public string DetailedAddress { get; set; }
        public string DataResponsibility { get; set; }
    

    }
    public class ResourcesClass : NotificationObject
    {

        public bool IsCheck { get; set; }
        public string MaterialName { get; set; }
        public string MaterialType { get; set; }
        public string Specifications { get; set; }
        public string Number { get; set; }
        public string Organization { get; set; }
        public string OnceOrTwice { get; set; }


    }
}

