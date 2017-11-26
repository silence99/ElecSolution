using AopAlliance.Intercept;

namespace Framework
{
    public class PropertyInterceptor : IMethodInterceptor
    {
        private NotificationObject Notifier { get; set; }
        public PropertyInterceptor(NotificationObject obj)
        {
            Notifier = obj;
        }
        public object Invoke(IMethodInvocation invocation)
        {
            return Notifier.Invoke(invocation);
        }
    }
}
