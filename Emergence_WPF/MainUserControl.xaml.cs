using Emergence_WPF.Model;
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
using Emergence_WPF.Comm;
using System.ComponentModel;

namespace Emergence_WPF
{
    /// <summary>
    /// MainUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 3;
        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
            set
            {
                _totalCount = value;
              
            }
        }

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
               
            }
        }

        public int TotalPage
        {
            get
            {
                return _totalPage;
            }
            set
            {
                _totalPage = value;
               
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
                
            }
        }
        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
        {
            var x = e.PageIndex;
        }
        public MainUserControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public void bind(double width,double height) 
        {

            this.Width = width;
            this.Height = height;
            
        }

        List<Event> Events = new List<Event>();

        BMap control = new BMap();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
          
            map.Children.Add(control);


            XmlDocument doc = new XmlDocument();

            // 2.把你想要读取的xml文档加载进来

            doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");
            XmlNodeList topM = doc.DocumentElement.ChildNodes;
            // 3.读取你指定的节点

            // XmlNodeList lis = doc.GetElementsByTagName("Row");

            foreach (XmlElement item in topM)
            {
                //if (item.Name.ToLower() == "Table")
                //{
                XmlNodeList nodelist = item.ChildNodes;
                var MessageHeader = nodelist[0].InnerText.ToString().Trim();
                var IncidentTime = nodelist[1].InnerText.ToString().Trim();
                var IncidentArea = nodelist[2].InnerText.ToString().Trim();
                var SubmittingUnit = nodelist[3].InnerText.ToString().Trim();
                var EventType = nodelist[4].InnerText.ToString().Trim();
                var EventLevel = nodelist[5].InnerText.ToString().Trim();
                var EventStatus = nodelist[6].InnerText.ToString().Trim();
                var EventSource = nodelist[7].InnerText.ToString().Trim();
                Event me = new Event();
                me.MessageHeader = MessageHeader;
                me.IncidentTime = IncidentTime;
                me.IncidentArea = IncidentArea;
                me.SubmittingUnit = SubmittingUnit;
                me.EventType = EventType;
                me.EventLevel = EventLevel;
                me.EventStatus = EventStatus;
                me.EventSource = EventSource;
                Events.Add(me);
            }

            this.DataCodeing.ItemsSource = Events;
        }
        public void Close() 
        {
            if (control != null)
            {
                control.Close();
                control = null;
            }
        }
        

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           

            var ff = this.ActualWidth - 990;
            var hh = this.ActualHeight - 550;
            this.Width = 990 + ff;
            this.Height = 550 + hh;


            mainpanel.Width = 980 + ff;
            mainpanel.Height = 540 + hh;

            TopDock.Width = 540 + ff;
            TopDock.Height = 530 + hh;


            mainleft.Height = 540 + hh;


            leftimage.Margin = new Thickness(0, 20 + hh, 0, 0);
            mainleft.Height = 540 + hh;
            mainright.Height = 540 + hh;
            mainvico.Height = 500 + hh;

            //k1.Margin = new Thickness(5, 10 + hh/3, 0, 0);
            //k2.Margin = new Thickness(5, 10 + hh / 3, 0, 0);
            //k3.Margin = new Thickness(5, 10 + hh / 3, 0, 0);
            foreach (var item in RightPanel.Children)
            {
                
            }

            if (RightPanel.Children.Count > 5) 
            {
                for (int i = 0; i < RightPanel.Children.Count-5; i++)
                {
                    RightPanel.Children.RemoveAt(5);
                }
            }

            BitmapImage img = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"DebugImage\22.jpg"));

            for (int i = 0; i < hh / 150-1; i++)
            {

                Image img1 = new Image();
                img1.Margin = new Thickness(5, 10, 0, 0);
                img1.Stretch = Stretch.Fill;
                img1.Width = 190;
                img1.Height = 140;
                img1.Source = img;
                img1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                RightPanel.Children.Add(img1);

                Border bor = new Border();
                bor.BorderThickness = new Thickness(0, 1, 0, 0);
                bor.BorderBrush = new SolidColorBrush(Color.FromRgb(237, 237, 239));

                RightPanel.Children.Add(bor);
            }


            EventTitle.Width = 530 + ff;
            DataCodeing.Width = 530 + ff;

            f1.Width = 66 + ff / 8;
            f2.Width = 66 + ff / 8;
            f3.Width = 66 + ff / 8;
            f4.Width = 66 + ff / 8;
            f5.Width = 66 + ff / 8;
            f6.Width = 66 + ff / 8;
            f7.Width = 66 + ff / 8;
            f8.Width = 66 + ff / 8;

            EventTitle.Height = 230 + hh / 2;
            DataCodeing.Height = 150 + hh / 2;
            kk.Height = 190 + hh / 2;


            map.Height = 290 + hh / 2;
            map.Width = 530 + ff;
        

        }

        private void DataCodeing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            map.Children.Clear();
            control.Bind("120.136940", "30.268970");

        }
        //private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{

          
        //}
       
    }
}
