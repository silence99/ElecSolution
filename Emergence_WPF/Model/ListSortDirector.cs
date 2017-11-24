using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF.Model
{
    public class ListSortDecorator : Control
    {
       
        public static readonly DependencyProperty SortDirectionProperty =
            DependencyProperty.Register("SortDirection", typeof(ListSortDirection), typeof(ListSortDecorator));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="adorner"></param>
        static ListSortDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListSortDecorator), new FrameworkPropertyMetadata(typeof(ListSortDecorator)));
        }

        public ListSortDirection SortDirection
        {
            get { return (ListSortDirection)GetValue(SortDirectionProperty); }
            set { SetValue(SortDirectionProperty, value); }
        }
    }
}
