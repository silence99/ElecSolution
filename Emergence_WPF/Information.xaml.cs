using Emergence_WPF.Comm;
using Emergence_WPF.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;

namespace Emergence_WPF
{
    /// <summary>
    /// MainUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class Information : UserControl
    {
        private int _totalCount = 3;
        private int _pageIndex = 1;
        private int _totalPage = 1;
        private int _pageSize = 6;
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
        public void Bind(double width, double height) 
        {
            this.Width = width;
            this.Height = height;
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
        public Information()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        List<Event> Events = new List<Event>();

        void Refresh() 
        {
            Events = null;
            Events = new List<Event>();
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
                var shen = nodelist[9].InnerText.ToString().Trim();
                var shi = nodelist[10].InnerText.ToString().Trim();
                var qu = nodelist[11].InnerText.ToString().Trim();


                var liuzhuanzhuangtai = nodelist[23].InnerText.ToString().Trim();
                var baosongshijian = nodelist[8].InnerText.ToString().Trim();
                var guid = nodelist[24].InnerText.ToString().Trim();
                var Approver = nodelist[25].InnerText.ToString().Trim();
                var Publisher = nodelist[26].InnerText.ToString().Trim();
                var Leadership = nodelist[27].InnerText.ToString().Trim();
                
                Event me = new Event();
                me.MessageHeader = MessageHeader;
                me.IncidentTime = IncidentTime;
                me.IncidentArea = IncidentArea;
                me.SubmittingUnit = SubmittingUnit;
                me.EventType = EventType;
                me.EventLevel = EventLevel;
                me.EventStatus = EventStatus;
                me.EventSource = EventSource;
                me.baosongshijian = baosongshijian;
                me.liuzhuanzhuangtai = liuzhuanzhuangtai;
                me.Approver = Approver;
                me.Publisher = Publisher;

                me.Guid = guid;
                me.Leadership = Leadership;
                me.sheng = shen;
                me.shi = shi;
                me.qu = qu;
                
                Events.Add(me);
                Events = Events;
            }

            // q1 手工填报
            // q2 信息上报
            // q10 事件生成
            // q3 应急发布
            // q5 领导批示
            // q4 续报重报

            if (CommHelp.Name.Trim() == "C")
            { // 显示： 手工填报，信息上报，，导出,[，应急发布]
              // q3.Visibility = Visibility.Collapsed;
                q4.Visibility = Visibility.Collapsed;
                q10.Visibility = Visibility.Collapsed;
                q5.Visibility = Visibility.Collapsed;
                ffff.Visibility = Visibility.Collapsed;
            }

            if (CommHelp.Name.Trim() == "A")
            { // 显示：事件生成，应急发布
                q1.Visibility = Visibility.Collapsed;
                q2.Visibility = Visibility.Collapsed;
                q4.Visibility = Visibility.Collapsed;
                q5.Visibility = Visibility.Collapsed;
                Events = Events.Where(c => c.liuzhuanzhuangtai == "已审批" || c.Publisher == "A" || c.liuzhuanzhuangtai == "任务已生成").ToList();
            }
            if (CommHelp.Name.Trim() == "B")
            { // 显示：领导批示，导出, [事件生成，应急发布]
                q1.Visibility = Visibility.Collapsed;
                // q3.Visibility = Visibility.Collapsed;
                q10.Visibility = Visibility.Collapsed;
                q2.Visibility = Visibility.Collapsed;
                q4.Visibility = Visibility.Collapsed;
                ffff.Visibility = Visibility.Collapsed;
                Events = Events.Where(c => c.liuzhuanzhuangtai == "待审批" || c.liuzhuanzhuangtai == "已审批" || c.liuzhuanzhuangtai == "已发布" || c.Approver == "B").ToList();
            }


            Events = Events.ToList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
            this.DataCodeing.ItemsSource = Events;


            _totalCount = Events.Count;

            _totalPage = Events.Count / _pageSize + Events.Count % _pageSize == 0 ? 0 : 1;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // map.Bind("120.136940", "30.268970");
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { // 手工填报
            EventAdd add = new EventAdd();
            add.ShowDialog();
            if (add.isclose) 
            {
                Events.Add(add.even);
            }

            this.DataCodeing.ItemsSource = null;
            this.DataCodeing.ItemsSource = Events;
            _totalCount = Events.Count;
            _totalPage = Events.Count / _pageSize + Events.Count % _pageSize == 0 ? 0 : 1;
        }
        
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var ff = this.ActualWidth - 1000;
            var hh = this.ActualHeight - 460;
            this.Width = 1000 + ff;
            this.Height = 460 + hh;
            border1.Width = 950 + ff;
            c.Width = 1000 + ff;
            c1.Width = 1000 + ff;
            c2.Width = 970 + ff;


            DataCodeing.Width = 970 + ff;
            c.Height = 460 + hh;
            c1.Height = 460 + hh;
            c2.Height = 340 + hh;
          
            c5.Height = 340 + hh;

            DataCodeing.Height = 260 + hh;
           
            n1.Width = 30 + ff/11;
            n2.Width = 95 + ff / 11;
            n3.Width = 95 + ff / 11;
            n4.Width = 95 + ff / 11;
            n5.Width = 95 + ff / 11;
            n6.Width = 95 + ff / 11;
            n7.Width = 95 + ff / 11;
            n8.Width = 95 + ff / 11;
            n9.Width = 95 + ff / 11;
            n10.Width = 95 + ff / 11;
            n11.Width = 95 + ff / 11;
           
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        { // 续报重报
            List<Event> list = new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
            if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }
            Restatement ship = new Restatement(list.FirstOrDefault());
            ship.ShowDialog();
            if (ship.name != 1)
            {
                return;
            }

            bind("D", list.FirstOrDefault());
            MessageBox.Show("续报重报成功");
            return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in list)
            {
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                    if (xe.GetAttribute("num") == item.Guid)//判断该子节点是否是要查找的节点
                    {
                        XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                        foreach (XmlNode xn2 in xnl2)
                        {
                            XmlElement xe2 = (XmlElement)xn2;//转换类型
                            if (xe2.Name == "EventStatus")//判断是否是要查找的元素
                            {

                                xe2.InnerText = "续报重报";
                              //  item.liuzhuanzhuangtai = "续报重报";
                                Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.EventStatus = "续报重报");// item.liuzhuanzhuangtai = "待审批";
                                Events = Events;
                                item.IsCheck = false;
                                break;
                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }
                      
                    }
                  
                  
                    //break;
                }
              
            }
            this.DataCodeing.ItemsSource=null;
            this.DataCodeing.ItemsSource = Events;
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("续报重报成功");
          
        }

        private void q3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { // 应急发布
            List<Event> list = new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
            if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }

            EventDetails ship = new EventDetails(list.FirstOrDefault());
            ship.ShowDialog();
            if (ship.name != 1)
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in list)
            {
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                    if (xe.GetAttribute("num") == item.Guid)//判断该子节点是否是要查找的节点
                    {
                        XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                        foreach (XmlNode xn2 in xnl2)
                        {
                            XmlElement xe2 = (XmlElement)xn2;//转换类型
                            if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                            {

                                xe2.InnerText = "已发布";
                                item.liuzhuanzhuangtai = "已发布";
                                Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "已发布");// item.liuzhuanzhuangtai = "待审批";
                                Events = Events;
                                item.IsCheck = false;
                              
                            }
                            if (xe2.Name == "Publisher")//判断是否是要查找的元素
                            {

                                xe2.InnerText = CommHelp.Name;
                                item.Publisher = CommHelp.Name;
                                Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.Publisher = CommHelp.Name);// item.liuzhuanzhuangtai = "待审批";
                                Events = Events;
                                item.IsCheck = false;

                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }

            }
           // this.DataCodeing.ItemsSource = null;
            //this.DataCodeing.ItemsSource =  Events.Where(c => c.liuzhuanzhuangtai == "已审批" || c.Publisher == "A" || c.liuzhuanzhuangtai == "任务已生成").ToList();
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("应急发布成功");
            UserControl_Loaded(null, null);
        }

        private void q5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { // 领导批示
            List<Event> list = new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
            if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }



            Leadership ship = new Leadership(list.FirstOrDefault());
            ship.ShowDialog();
            if (ship.name!=1) 
            {
                return;
            }

            bind("B", list.FirstOrDefault(), ship.pishineirong.Text);
            MessageBox.Show("领导批示成功");
            return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in list)
            {
                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                    if (xe.GetAttribute("num") == item.Guid)//判断该子节点是否是要查找的节点
                    {
                        XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                        foreach (XmlNode xn2 in xnl2)
                        {
                            XmlElement xe2 = (XmlElement)xn2;//转换类型
                            if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                            {

                                xe2.InnerText = "已审批";
                                item.liuzhuanzhuangtai = "已审批";
                                Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "已审批");// item.liuzhuanzhuangtai = "待审批";
                                Events = Events;
                                item.IsCheck = false;
                              
                            }
                            if (xe2.Name == "Approver")//判断是否是要查找的元素
                            {

                                xe2.InnerText = CommHelp.Name;
                                item.Approver = CommHelp.Name;
                                Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.Approver = CommHelp.Name);// item.liuzhuanzhuangtai = "待审批";
                                Events = Events;
                                item.IsCheck = false;
                              
                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }

            }
            this.DataCodeing.ItemsSource = null;
            this.DataCodeing.ItemsSource = Events.Where(c => c.liuzhuanzhuangtai == "待审批" || c.Approver == "B").ToList();
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("领导批示成功");
        }

        private void q2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        { // 信息上报
          // var kk = this.DataCodeing.ItemsSource as List<Event>;


            List<Event> list=new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
              if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }

              EventReporting report = new EventReporting(list.FirstOrDefault());
              report.ShowDialog();
              if (report.name != 1)
              {
                  return;
              }
              bind("C", list.FirstOrDefault());
            
            MessageBox.Show("上报成功");
        }

    

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;
            var a = txt.Tag.ToString();
            var mm = Events.Where(c => c.Guid == a).FirstOrDefault();
            if (mm != null)
            {
                EventDetails deta = new EventDetails(mm);
                deta.ShowDialog();
                if (deta.name != 1)
                {

                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
                XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
                XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点


                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                    if (xe.GetAttribute("num") == mm.Guid)//判断该子节点是否是要查找的节点
                    {
                        XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                        foreach (XmlNode xn2 in xnl2)
                        {
                            XmlElement xe2 = (XmlElement)xn2;//转换类型
                            if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                            {

                                xe2.InnerText = "待审批";
                                Events.Where(c => c.Guid == mm.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "待审批");

                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }


                this.DataCodeing.ItemsSource = null;
                this.DataCodeing.ItemsSource = Events;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            }
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sdfsdsdf");

        }
        
        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        //    TextBlock txt = sender as TextBlock;
        //    var a = txt.Tag.ToString();
            var mm = this.DataCodeing.SelectedItem as Event;
            if (mm != null)
            {
                EventDetails deta = new EventDetails(mm);
                deta.ShowDialog();
                if (deta.name != 1)
                {

                    return;
                }

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
                XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
                XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点


                foreach (XmlNode xn in xnl)
                {
                    XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                    if (xe.GetAttribute("num") == mm.Guid)//判断该子节点是否是要查找的节点
                    {
                        XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                        foreach (XmlNode xn2 in xnl2)
                        {
                            XmlElement xe2 = (XmlElement)xn2;//转换类型
                            if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                            {

                                xe2.InnerText = "待审批";
                                Events.Where(c => c.Guid == mm.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "待审批");

                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }


                this.DataCodeing.ItemsSource = null;
                this.DataCodeing.ItemsSource = Events;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            }
        }


        /// <summary>
        /// 操作，C为预算上报 B为审批 D为续报重报
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="item"></param>
        void bind(string Type, Event item,string pishineirong="") 
        {
               XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
                    XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
                    XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点


                    foreach (XmlNode xn in xnl)
                    {
                        XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                        if (xe.GetAttribute("num") == item.Guid)//判断该子节点是否是要查找的节点
                        {
                            XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                            foreach (XmlNode xn2 in xnl2)
                            {
                                XmlElement xe2 = (XmlElement)xn2;//转换类型

                       
                                if (Type == "C")
                                {
                                    if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = "待审批";
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "待审批");

                                    }
                                }
                                if (Type == "B")
                                {
                                  
                                    if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = "已审批";
                                        item.liuzhuanzhuangtai = "已审批";
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "已审批");// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;

                                    }
                                    if (xe2.Name == "Leadership")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = pishineirong;
                                    
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.Leadership = pishineirong);// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;

                                    }
                                    if (xe2.Name == "Approver")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = CommHelp.Name;
                                        item.Approver = CommHelp.Name;
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.Approver = CommHelp.Name);// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;

                                    }
                                }
                                if (Type == "D")
                                {
                                    if (xe2.Name == "EventStatus")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = "续报重报";
                                        //  item.liuzhuanzhuangtai = "续报重报";
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.EventStatus = "续报重报");// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;
                                        break;
                                    }
                                }
                                if (Type == "F")
                                {
                                    if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = "任务已生成";
                                        //  item.liuzhuanzhuangtai = "续报重报";
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "任务已生成");// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;
                                        break;
                                    }
                                }
                                if (Type == "W")
                                {
                                    if (xe2.Name == "liuzhuanzhuangtai")//判断是否是要查找的元素
                                    {

                                        xe2.InnerText = "已发布";
                                        //  item.liuzhuanzhuangtai = "续报重报";
                                        Events.Where(c => c.Guid == item.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "已发布");// item.liuzhuanzhuangtai = "待审批";
                                        Events = Events;
                                        item.IsCheck = false;
                                        break;
                                    }
                                }
                                //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                            }

                        }


                        //break;
                    }
                    this.DataCodeing.ItemsSource = null;
                this.DataCodeing.ItemsSource = Events;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
        }



        private void DataCodeing_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var mm = this.DataCodeing.SelectedItem as Event;
            if (mm != null)
            {

               
                if (CommHelp.Name == "C")
                {
                    EventReporting report = new EventReporting(mm);
                    report.ShowDialog();
                    if (report.name == 1)
                    {
                        bind("C", mm);
                        MessageBox.Show("上报成功");
                        return;
                    }
                    if (report.name == 2)
                    {
                        bind("W", mm);
                        MessageBox.Show("更新成功");
                        return;
                    }

                }
                 
                if (CommHelp.Name == "B")
                {
                    Leadership deta = new Leadership(mm);
                    deta.ShowDialog();
                    if (deta.name != 1)
                    {
                        return;
                    }
                    bind("B", mm);
                    MessageBox.Show("领导批示成功");
                }
                if (CommHelp.Name == "A")
                {
                    TaskTracking deta = new TaskTracking(mm);
                    deta.ShowDialog();
                    if (deta.name != 1)
                    {
                        return;
                    }
                    MessageBox.Show("更新成功");
                }
              


            
            }
        }

        private void q3_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void q10_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {// q10 事件生成
            List<Event> list = new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
            if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }
            EventGeneration deta = new EventGeneration(list.FirstOrDefault());
            deta.ShowDialog();
            if (deta.name != 1)
            {
                return;
            }

            bind("F", list.FirstOrDefault());



            MessageBox.Show("事件生成成功");
        }

        private void StackPanel_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            List<Event> list = new List<Event>();
            for (int index = 0; index < DataCodeing.Items.Count; index++)//foreach (var item in this.dataGrid.Items)
            {
                FrameworkElement item = DataCodeing.Columns[0].GetCellContent(DataCodeing.Items[index]);
                DataGridTemplateColumn temp = (DataCodeing.Columns[0] as DataGridTemplateColumn);
                CheckBox cb = temp.CellTemplate.FindName("select_checkBox", item) as CheckBox;

                if ((cb.IsChecked == true))
                {
                    list.Add(DataCodeing.Items[index] as Event);
                }
            }
            if (list.Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }
            TaskTracking deta = new TaskTracking(list.FirstOrDefault());
            deta.ShowDialog();
            if (deta.name != 1)
            {
                return;
            }
            MessageBox.Show("更新成功");
        }

       
        //private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        //{
        //    XmlDocument doc = new XmlDocument();

        //    // 2.把你想要读取的xml文档加载进来

        //    doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");
        //    XmlNodeList topM = doc.DocumentElement.ChildNodes;
        //    // 3.读取你指定的节点

        //    // XmlNodeList lis = doc.GetElementsByTagName("Row");

        //    foreach (XmlElement item in topM)
        //    {
        //        //if (item.Name.ToLower() == "Table")
        //        //{
        //        XmlNodeList nodelist = item.ChildNodes;
        //        var MessageHeader = nodelist[0].InnerText.ToString().Trim();
        //        var IncidentTime = nodelist[1].InnerText.ToString().Trim();
        //        var IncidentArea = nodelist[2].InnerText.ToString().Trim();
        //        var SubmittingUnit = nodelist[3].InnerText.ToString().Trim();
        //        var EventType = nodelist[4].InnerText.ToString().Trim();
        //        var EventLevel = nodelist[5].InnerText.ToString().Trim();
        //        var EventStatus = nodelist[6].InnerText.ToString().Trim();
        //        var EventSource = nodelist[7].InnerText.ToString().Trim();
        //        Event me = new Event();
        //        me.MessageHeader = MessageHeader;
        //        me.IncidentTime = IncidentTime;
        //        me.IncidentArea = IncidentArea;
        //        me.SubmittingUnit = SubmittingUnit;
        //        me.EventType = EventType;
        //        me.EventLevel = EventLevel;
        //        me.EventStatus = EventStatus;
        //        me.EventSource = EventSource;
        //        Events.Add(me);
        //    }
        //    this.DataCodeing.ItemsSource = Events;
        //}

       
    }
}
