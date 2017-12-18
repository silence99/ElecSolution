using Emergence.Common;
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
    public partial class EmergencyOrganization : UserControl
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
        public EmergencyOrganization()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        List<Event> Events = new List<Event>();
        List<EmergencyOrganizationClass> list = new List<EmergencyOrganizationClass>();
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();

            // 2.把你想要读取的xml文档加载进来

            doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");
            XmlNodeList topM = doc.DocumentElement.ChildNodes;
            // 3.读取你指定的节点

            // XmlNodeList lis = doc.GetElementsByTagName("Row");

            foreach (XmlElement item in topM)
            {
                //if (item.Name.ToLower() == "Table")
                //{
                XmlNodeList nodelist = item.ChildNodes;
             
                var TeamName = nodelist[0].InnerText.ToString().Trim();
                var Rescue = nodelist[1].InnerText.ToString().Trim();
                var IsitFree = nodelist[2].InnerText.ToString().Trim();
                var DetailedAddress = nodelist[3].InnerText.ToString().Trim();
                var DataResponsibility = nodelist[4].InnerText.ToString().Trim();

                EmergencyOrganizationClass me = new EmergencyOrganizationClass();
                me.TeamName = TeamName;
                me.Rescue = Rescue;
                me.IsitFree = IsitFree;
                me.DetailedAddress = DetailedAddress;
                me.DataResponsibility = DataResponsibility;

                list.Add(me);
            }

            this.DataCodeing.ItemsSource = list;


            _totalCount = list.Count;

            _totalPage = list.Count / _pageSize + list.Count % _pageSize == 0 ? 0 : 1;


        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // map.Bind("120.136940", "30.268970");
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            n2.Width = 150 + ff / 11;
            n3.Width = 150 + ff / 11;
            n4.Width = 150 + ff / 11;
            n5.Width = 150 + ff / 11;
            n6.Width = 150 + ff / 11;
            n7.Width = 180 + ff / 11;
            //n8.Width = 95 + ff / 11;
            //n9.Width = 95 + ff / 11;
            //n10.Width = 95 + ff / 11;
            //n11.Width = 95 + ff / 11;
           
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var kk = this.DataCodeing.ItemsSource as List<Event>;

            if (kk.Where(c => c.IsCheck).Count() == 0) 
            {
                MessageBox.Show("请选择一项事件");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in kk.Where(c => c.IsCheck))
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
                                item.liuzhuanzhuangtai = "续报重报";
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
            this.DataCodeing.ItemsSource = kk;
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("续报重报成功");
          
        }

        private void q3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var kk = this.DataCodeing.ItemsSource as List<Event>;

            if (kk.Where(c => c.IsCheck).Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in kk.Where(c => c.IsCheck))
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
                                item.IsCheck = false;
                              
                            }
                            if (xe2.Name == "Publisher")//判断是否是要查找的元素
                            {

                                xe2.InnerText = CommHelp.Name;
                                item.Publisher = CommHelp.Name;
                                item.IsCheck = false;

                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }

            }
            this.DataCodeing.ItemsSource = null;
            this.DataCodeing.ItemsSource = kk.Where(c => c.liuzhuanzhuangtai == "已审批" || c.Publisher == "A").ToList();
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("应急发布成功");
        }

        private void q5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var kk = this.DataCodeing.ItemsSource as List<Event>;

            if (kk.Where(c => c.IsCheck).Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }

          

            Leadership ship = new Leadership(kk.Where(c => c.IsCheck).FirstOrDefault());
            ship.ShowDialog();
            if (ship.name!=1) 
            {
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\EmergencyOrganization.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in kk.Where(c => c.IsCheck))
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
                                item.IsCheck = false;
                              
                            }
                            if (xe2.Name == "Approver")//判断是否是要查找的元素
                            {

                                xe2.InnerText = CommHelp.Name;
                                item.Approver = CommHelp.Name;
                                item.IsCheck = false;
                              
                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }

            }
            this.DataCodeing.ItemsSource = null;
            this.DataCodeing.ItemsSource = kk.Where(c => c.liuzhuanzhuangtai == "待审批"||c.Approver=="B").ToList();
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
            MessageBox.Show("领导批示成功");
        }

        private void q2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var kk = this.DataCodeing.ItemsSource as List<Event>;

            if (kk.Where(c => c.IsCheck).Count() == 0)
            {
                MessageBox.Show("请选择一项事件");
                return;
            }

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点

            foreach (var item in kk.Where(c => c.IsCheck))
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

                                xe2.InnerText = "待审批";
                                item.liuzhuanzhuangtai = "待审批";
                                item.IsCheck = false;
                                break;
                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }

            }
            this.DataCodeing.ItemsSource = null;
            this.DataCodeing.ItemsSource = kk;
            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
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

        private void DataCodeing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock txt = sender as TextBlock;
            var a = txt.Tag.ToString();
            var mm = Events.Where(c => c.Guid == a).FirstOrDefault();
            if (mm != null)
            {
                EventDetails deta = new EventDetails(mm);
                deta.ShowDialog();
                var kk = this.DataCodeing.ItemsSource as List<Event>;

                if (kk.Where(c => c.IsCheck).Count() == 0)
                {
                    MessageBox.Show("请选择一项事件");
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

                                xe2.InnerText = "已上报";
                                Events.Where(c => c.Guid == mm.Guid).ToList().ForEach(m => m.liuzhuanzhuangtai = "已上报");

                            }
                            //break;//这里为了明显效果 我注释了break,用的时候不用，这个大家都明白的哈
                        }

                    }


                    //break;
                }


                this.DataCodeing.ItemsSource = null;
                this.DataCodeing.ItemsSource = Events;
                xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");//再一次强调 ，一定要记得保存的该XML文件
                MessageBox.Show("上报成功");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("sdfsdsdf");

        }

        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
