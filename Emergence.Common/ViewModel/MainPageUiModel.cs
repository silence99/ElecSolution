using Framework;
using System.Collections.Generic;

namespace Emergence.Common.ViewModel
{
    public class MainPageUiModel : NotificationObject
    {
        public virtual List<MasterEventUiModel> MasterEvents { get; set; }
    }
}
