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
    /// MainUserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class MainUserControl2 : UserControl
    {

        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 3;

        public MainUserControl2()
        {
            InitializeComponent();
        }

        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        public int TotalPage
        {
            get { return _totalPage; }
            set { _totalPage = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
        {
            var x = e.PageIndex;
        }

        public void bind(double width, double height)
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

            mainpanel.Height = 550 + hh;
            mainleft.Height = 550 + hh;
            mainMiddle.Height = 550 + hh;
            mainright.Height = 550 + hh;

            mainpanel.Width = 980 + ff;
            mainMiddle.Width = 680 + ff;


            f1.Width = 85 + ff / 8;
            f2.Width = 85 + ff / 8;
            f3.Width = 85 + ff / 8;
            f4.Width = 85 + ff / 8;
            f5.Width = 85 + ff / 8;
            f6.Width = 85 + ff / 8;
            f7.Width = 85 + ff / 8;
            f8.Width = 85 + ff / 8;
            
            mainMidTop.Height = 380 + hh;
            mainMidTop.Width = mainMiddle.Width;

            mapPanel.Height = mainMidTop.Height;
            mapPanel.Width = 380 + ff;

            map.Height = mapPanel.Height + 30;
            map.Width = mapPanel.Width;

            dataGridPanel.Width = 680 + ff;
            DataCodeing.Width = dataGridPanel.Width;

            dataGridPanel.Height = 170;
            DataCodeing.Height = dataGridPanel.Height;
        }

        private void DataCodeing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            map.Children.Clear();
            control.Bind("119.931298", "28.469722");

        }

    }
}
