using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Emergence_WPF
{
    public class EmergencyUserControl: UserControl
    {
        public NotificationObject UiModel { get; set; }
        public StrategyController<NotificationObject> StrategyController { get; set; }
    }
}
