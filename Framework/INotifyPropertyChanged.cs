namespace Framework
{
    public delegate void PropertyChangedHandlerEx(object sender, PropertyChangedEventArgsEx args);
    public delegate void PropertyChangingHandlerEx(object sender, PropertyChangedEventArgsEx args);
    public delegate void SetPopupHandler();
    public interface INotifyPropertyChangedEx
    {
        event PropertyChangedHandlerEx PropertyChangedEx;
    }
    public interface INotifyPropertyChangingEx
    {
        event PropertyChangedHandlerEx PropertyChangingEx;
    }
}
