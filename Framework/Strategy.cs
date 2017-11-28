using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework
{
    public abstract class Strategy
    {
        protected ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private Strategy _parent = null;
        private StrategyRegistry _registry = null;
        private Dictionary<string, PropertyChangedHandlerEx> _actions = null;
        private List<NotificationObject> _registered = new List<NotificationObject>();

        public Strategy(Strategy parent)
        {
            if (parent == null)
            {
                _actions = new Dictionary<string, PropertyChangedHandlerEx>();
            }
            else
            {
                _parent = parent as Strategy;
            }
        }

        private void Invoke(object notifier, PropertyChangedEventArgsEx args, PropertyChangeEvent eventType)
        {
            var sender = notifier as NotificationObject;
            _registry.Invoke(sender, args, eventType);
        }

        public void RegisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler)
        {
            if (notifier != null)
            {
                _registry.RegisterListener(notifier, propertyName, handler);
                if (!_registered.Contains(notifier))
                {
                    notifier.PropertyChangedEx += (sender, args) => { Invoke(sender, args, PropertyChangeEvent.Changed); };
                    notifier.PropertyChangingEx += (sender, args) => { Invoke(sender, args, PropertyChangeEvent.Changing); };
                }
            }
            else
            {
                Logger.ErrorFormat("Listener object is null");
                throw new Exception("Listener object is null");
            }
        }

        public void UnregisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler)
        {
            if (notifier != null)
            {
                _registry.UnregisterListener(notifier, propertyName, handler);
            }
            else
            {
                Logger.ErrorFormat("Listener object is null when unregister listener");
                throw new Exception("Listener object is null when unregister listener");
            }
        }

        public void RegisterOwer(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler)
        {
            if (notifier != null)
            {
                _registry.RegisterOwener(notifier, propertyName, changingHandler, changedHandler);
                if (!_registered.Contains(notifier))
                {
                    notifier.PropertyChangedEx += (sender, args) => { Invoke(sender, args, PropertyChangeEvent.Changed); };
                    notifier.PropertyChangingEx += (sender, args) => { Invoke(sender, args, PropertyChangeEvent.Changing); };
                }
            }
            else
            {
                Logger.ErrorFormat("Listener object is null when register handler");
                throw new Exception("Listener object is null when register handler");
            }
        }

        public void UnregisterOwer(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler)
        {
            if (notifier != null)
            {
                _registry.UnregisterOwener(notifier, propertyName, changingHandler, changedHandler);
            }
            else
            {
                Logger.ErrorFormat("Listener object is null when unregister handler");
                throw new Exception("Listener object is null when unregister handler");
            }
        }

        public void RegisterAction(string actionName, PropertyChangedHandlerEx handler)
        {
            Logger.DebugFormat("Strategy {0} register an action: {1}", this.GetType().FullName, actionName);
            if (_parent == null)
            {
                _actions[actionName] = handler;
            }
            else
            {
                _parent.RegisterAction(actionName, handler);
            }
        }

        public PropertyChangedHandlerEx GetAction(string actionName)
        {
            if (_parent == null && _actions == null)
            {
                Logger.ErrorFormat("{0} not init correctly, both parent and _actions properties are null", this.GetType().FullName);
                throw new Exception("Strategy not init correctly");
            }
            else
            {
                if (_parent != null)
                {
                    return _parent.GetAction(actionName);
                }
                else
                {
                    if (_actions.ContainsKey(actionName))
                    {
                        return _actions[actionName];
                    }
                    else
                    {
                        Logger.WarnFormat("action {0} not exist in {1}", actionName, this.GetType().FullName);
                        return null;
                    }
                }
            }
        }

        public void UnregisterAction(string actionName, PropertyChangedHandlerEx handler)
        {
            if (_actions.ContainsKey(actionName))
            {
                _actions.Remove(actionName);
            }
            else
            {
                Logger.WarnFormat("there is not action {0}", actionName);
            }
        }

        public virtual void RegisterProperties() { }
    }

    //public abstract class Strategy : IStrategy
    //{
    //    public abstract PropertyChangedHandlerEx GetAction(string actionName);

    //    public abstract void RegisterAction(string actionName, PropertyChangedHandlerEx handler);

    //    public abstract void RegisterHandler(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler);

    //    public abstract void RegisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler);

    //    public abstract void UnregisterAction(string actionName, PropertyChangedHandlerEx handler);

    //    public abstract void UnregisterHandler(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx changingHandler, PropertyChangedHandlerEx changedHandler);

    //    public abstract void UnregisterListener(NotificationObject notifier, string propertyName, PropertyChangedHandlerEx handler);

    //    public abstract void InitializationUiModel();
    //    public abstract void PostInitializationUiModel();
    //    public abstract void RegisterProperties();
    //}
}
