using System.Windows;

namespace Emergence_WPF.Comm
{
    public class PageChangedEventArg : RoutedEventArgs
    {
        public int PageIndex
        {
            get; set;
        }

        public PageChangedEventArg(int pageIndex) : base()
        {
            PageIndex = pageIndex;
        }
    }
}