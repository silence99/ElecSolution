using Emergence.Common.ViewModel;
using Emergence_WPF.Comm;
using Emergence_WPF.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Emergence_WPF
{
    /// <summary>
    /// Interaction logic for UserControl_MainPage.xaml
    /// </summary>
    public partial class UserControl_MainPage : UserControl
    {
        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 3;
        private MainPageUiModel _uiModel;
        public MainPageUiModel UiModel
        {
            get
            {
                return _uiModel;
            }
            set
            {
                _uiModel = value;
                Videos.UiModel = UiModel.MasterEvents[0].Videos;
            }
        }        

        public UserControl_MainPage()
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
                control.close();
                control = null;
            }
        }


        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var dynamicWidth = this.ActualWidth - 694 > 0 ? this.ActualWidth - 694 : 1;
            var dynamicHeight = this.ActualHeight - 750 > 0 ? this.ActualHeight - 750 : 1; ;
            mapPanel.Height = this.ActualHeight > 246 ? this.ActualHeight - 246 : 1;
            mapPanel.Width = dynamicWidth;

            map.Height = mapPanel.Height + 30;
            map.Width = mapPanel.Width - 10;

            dataGridPanel.Width = dynamicWidth;

            f1.Width = 0 + dynamicWidth / 8;
            f2.Width = 0 + dynamicWidth / 8;
            f3.Width = 0 + dynamicWidth / 8;
            f4.Width = 0 + dynamicWidth / 8;
            f5.Width = 0 + dynamicWidth / 8;
            f6.Width = 0 + dynamicWidth / 8;
            f7.Width = 0 + dynamicWidth / 8;
            f8.Width = 0 + dynamicWidth / 8;

            DataCodeing.Width = dataGridPanel.Width;

            //dataGridPanel.Height = 246;
            DataCodeing.Height = dataGridPanel.Height;
            Videos.Height = this.ActualHeight - 68;
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
