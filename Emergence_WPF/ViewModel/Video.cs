using System;

namespace Emergence_WPF.ViewModel
{
    public class VideoUiModel: NotificationObject
    {
        public virtual string Uri { get; set; }
        public virtual Guid EventID { get; set; }
    }
}
