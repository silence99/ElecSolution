using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Model
{
    public class PopupModel : NotificationObject
    {
        public virtual string PopupName { get; set; }
        public virtual string PopupWidth { get; set; }
        public virtual string PopupHeight { get; set; }
        public virtual string HorizontalOffset { get; set; }
        public virtual string VerticalOffset { get; set; }
        public virtual bool IsOpen { get; set; }

        public PopupModel()
        {
            IsOpen = false;
        }

        public PopupModel(string popupName, string popupWidth, string popupHeight, bool isOpen, string horizontalOffset, string verticalOffset)
        {
            PopupName = popupName;
            PopupHeight = popupHeight;
            PopupWidth = popupWidth;
            IsOpen = isOpen;
            HorizontalOffset = horizontalOffset;
            VerticalOffset = verticalOffset;
        }
    }
}
