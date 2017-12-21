using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF
{
    /// <summary>
    /// FullScreen1.xaml 的交互逻辑
    /// </summary>
    public partial class GeographicInformation : UserControl
    {
        public GeographicInformation()
        {
            InitializeComponent();
        }
        public class Task 
        {
            public string TaskName { get; set; }
            public string TaskGrade { get; set; }
            public string PersonCharge { get; set; }
            public string ExecutionStatus { get; set; }
        }
        List<Task> list = new List<Task>();
        BMap control = new BMap();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            map.Children.Add(control);   
      

        }
        
        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            
         
        }

      
    }
}
