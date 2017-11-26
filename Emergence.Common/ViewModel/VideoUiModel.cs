using Framework;
using System;

namespace Emergence.Common.ViewModel
{
    public class VideoUiModel : NotificationObject
    {
        public virtual Uri Uri { get; set; }
        public virtual Guid EventID { get; set; }
        public virtual string ImageUri { get; set; }
    }

    public enum VideoStatus
    {
        Playing,
        Stop,
        Pasue
    }
}
