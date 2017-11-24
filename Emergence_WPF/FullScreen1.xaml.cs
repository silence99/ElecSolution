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
    public partial class FullScreen1 : UserControl
    {
        public FullScreen1()
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                
            XmlDocument doc = new XmlDocument();

            // 2.把你想要读取的xml文档加载进来

            doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Task.xml");
            XmlNodeList topM = doc.DocumentElement.ChildNodes;
            // 3.读取你指定的节点

            // XmlNodeList lis = doc.GetElementsByTagName("Row");

            foreach (XmlElement item in topM)
            {
                //if (item.Name.ToLower() == "Table")
                //{
                XmlNodeList nodelist = item.ChildNodes;

                var TaskName = nodelist[0].InnerText.ToString().Trim();
                var TaskGrade = nodelist[1].InnerText.ToString().Trim();
                var PersonCharge = nodelist[2].InnerText.ToString().Trim();
                var ExecutionStatus = nodelist[3].InnerText.ToString().Trim();

                Task me = new Task();
                me.TaskName = TaskName;
                me.TaskGrade = TaskGrade;
                me.PersonCharge = PersonCharge;
                me.ExecutionStatus = ExecutionStatus;
             

                list.Add(me);
            }

            this.DataCodeing.ItemsSource = list;

        }
        private void Item_GotFocus(object sender, RoutedEventArgs e)
        {
            
         
        }

        private void DataCodeing_GotFocus(object sender, RoutedEventArgs e)
        {

            Task task = e.Source as Task;
            var item = sender as Task;
        }
       
        void clear() 
        {
            BitmapImage img = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\圆点2.png"));
          a.Source = img;
          b.Source = img;
          c.Source = img;
          d.Source = img;
          f.Source = img;
          a1.Visibility = Visibility.Hidden;
          b1.Visibility = Visibility.Hidden;
          c1.Visibility = Visibility.Hidden;
          d1.Visibility = Visibility.Hidden;
          f1.Visibility = Visibility.Hidden;

          a2.Background = new SolidColorBrush(Color.FromRgb(225, 225, 225));
          b2.Background = new SolidColorBrush(Color.FromRgb(225, 225, 225));
          c2.Background = new SolidColorBrush(Color.FromRgb(225, 225, 225));
          d2.Background = new SolidColorBrush(Color.FromRgb(225, 225, 225));
          



        }
        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Task task = this.DataCodeing.SelectedItem as Task;
            clear();
            BitmapImage img = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\圆点1.png"));

            if (task.ExecutionStatus.Trim() == "任务下达")
            {
                a.Source = img;
                a1.Visibility = System.Windows.Visibility.Visible;
            }
            if (task.ExecutionStatus.Trim() == "任务接收")
            {
                a.Source = img;
                a1.Visibility = System.Windows.Visibility.Visible;

                b.Source = img;
                b1.Visibility = System.Windows.Visibility.Visible;

                a2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
            }
            if (task.ExecutionStatus.Trim() == "到达现场")
            {
                a.Source = img;
                a1.Visibility = System.Windows.Visibility.Visible;

                b.Source = img;
                b1.Visibility = System.Windows.Visibility.Visible;

                c.Source = img;
                c1.Visibility = System.Windows.Visibility.Visible;

                a2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                b2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));

            }
            if (task.ExecutionStatus.Trim() == "任务执行")
            {
                a.Source = img;
                a1.Visibility = System.Windows.Visibility.Visible;

                b.Source = img;
                b1.Visibility = System.Windows.Visibility.Visible;

                c.Source = img;
                c1.Visibility = System.Windows.Visibility.Visible;

                d.Source = img;
                d1.Visibility = System.Windows.Visibility.Visible;

                a2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                b2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                c2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));


            }
            if (task.ExecutionStatus.Trim() == "任务接单")
            {
                a.Source = img;
                a1.Visibility = System.Windows.Visibility.Visible;

                b.Source = img;
                b1.Visibility = System.Windows.Visibility.Visible;

                c.Source = img;
                c1.Visibility = System.Windows.Visibility.Visible;

                d.Source = img;
                d1.Visibility = System.Windows.Visibility.Visible;

                f.Source = img;
                f1.Visibility = System.Windows.Visibility.Visible;

                a2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                b2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                c2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));
                d2.Background = new SolidColorBrush(Color.FromRgb(1, 157, 133));

            }
        }
    }
}
