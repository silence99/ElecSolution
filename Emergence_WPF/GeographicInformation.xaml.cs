using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

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
        public class Task : NotificationObject
        {
            public string TaskName { get; set; }
            public string TaskGrade { get; set; }
            public string PersonCharge { get; set; }
            public string ExecutionStatus { get; set; }
        }
        List<Task> list = new List<Task>();
        UserControl1 control = new UserControl1();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            map.Children.Add(control);   
      

        }
        public void Close()
        {
            if (control != null)
            {
                control.close();
                control = null;
            }
        }
        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            
         
        }

      
    }
}
