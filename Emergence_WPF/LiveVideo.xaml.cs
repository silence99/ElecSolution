using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Emergence_WPF
{
    /// <summary>
    /// FullScreen1.xaml 的交互逻辑
    /// </summary>
    public partial class LiveVideo : UserControl
    {
        public LiveVideo()
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
         
      

        }
        public void Close()
        {
            if (control != null)
            {
                control = null;
            }
        }
        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            
         
        }

      
    }
}
