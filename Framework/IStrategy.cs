using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IStrategy
    {
        void RegisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler);
        void UnregisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler);
        void RegisterHandler(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler);
        void UnregisterHandler(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler);
        void RegisterAction(string actionName, PropertyChangedHandlerEx handler);
        void UnregisterAction(string actionName, PropertyChangedHandlerEx handler);
        PropertyChangedHandlerEx GetAction(string actionName);
    }
}
