using Emergence.Common.ViewModel;
using Framework;
using System.Collections.Generic;

namespace Emergence_WPF.ViewModel
{
    public class MasterEventUiModel : NotificationObject
    {
        public virtual long ID { get; set; }
        public virtual bool IsCheck { get; set; }
        public virtual string SerialNumber { get; set; }
        public virtual string Title { get; set; }
        public virtual string EventType { get; set; }
        public virtual string Grade { get; set; }
        public virtual string Time { get; set; }
        public virtual string Describe { get; set; }
        public virtual string SubmitParty { get; set; }
        public virtual string TelephoneNumber { get; set; }
        public virtual string SubmitDept { get; set; }
        public virtual string Locate { get; set; }
        public virtual List<VideoUiModel> Videos { get; set; }
    }
}
