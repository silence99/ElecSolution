using Emergence_WPF.Comm;
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
using System.Windows.Shapes;
using System.Xml;

namespace Emergence_WPF
{
    /// <summary>
    /// EventAdd.xaml 的交互逻辑
    /// </summary>
    public partial class TaskTracking : Window
    {
        public TaskTracking()
        {
            InitializeComponent();
        }

        public TaskTracking(Event mm)
        {
            InitializeComponent();
            even = mm;
        }

        public Event even = new Event();
        public bool isclose = false;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            MessageHeader.Text = even.MessageHeader;

            IncidentTime.Text = even.IncidentTime;

            IncidentArea.Text = even.IncidentArea;

            SubmittingUnit.Text = even.SubmittingUnit;

            EventLevel.Text = even.EventLevel;

            sheng.Text = even.sheng;
            shi.Text = even.shi;

            qu.Text = even.qu;

            weidu.Text = even.weidu;

            lianxidianhua.Text = even.lianxidianhua;

            qianfalingdao.Text = even.qianfalingdao;

            baosongren.Text = even.baosongren;


            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\TaskTracking.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点


            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                if (xe.GetAttribute("num") == even.Guid && xe.GetAttribute("Task") == "1")//判断该子节点是否是要查找的节点
                {
                    XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素

                    task1 = new tracking();
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;//转换类型
                        if (xe2.Name == "xiada")//判断是否是要查找的元素
                        {
                            task1.xiada = xe2.InnerText;
                        }
                        if (xe2.Name == "jieshou")//判断是否是要查找的元素
                        {
                            task1.jieshou = xe2.InnerText;
                        }
                        if (xe2.Name == "yanpan")//判断是否是要查找的元素
                        {
                            task1.yanpan = xe2.InnerText;
                        }
                        if (xe2.Name == "jindu")//判断是否是要查找的元素
                        {
                            task1.jindu = xe2.InnerText;
                        }
                        if (xe2.Name == "wancheng")//判断是否是要查找的元素
                        {
                            task1.wancheng = xe2.InnerText;
                        }
                    }

                }
                if (xe.GetAttribute("num") == even.Guid && xe.GetAttribute("Task") == "2")//判断该子节点是否是要查找的节点
                {
                    XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素

                    task2 = new tracking();
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;//转换类型
                        if (xe2.Name == "xiada")//判断是否是要查找的元素
                        {
                            task2.xiada = xe2.InnerText;
                        }
                        if (xe2.Name == "jieshou")//判断是否是要查找的元素
                        {
                            task2.jieshou = xe2.InnerText;
                        }
                        if (xe2.Name == "yanpan")//判断是否是要查找的元素
                        {
                            task2.yanpan = xe2.InnerText;
                        }
                        if (xe2.Name == "jindu")//判断是否是要查找的元素
                        {
                            task2.jindu = xe2.InnerText;
                        }
                        if (xe2.Name == "wancheng")//判断是否是要查找的元素
                        {
                            task2.wancheng = xe2.InnerText;
                        }
                    }
                }
            }




            if (task1 != null)
            {
                if (task1.xiada == "0")
                {
                    xiada1.Tag = 0;
                    xiada1.Source = img0;
                }
                else
                {
                    xiada1.Tag = 1;
                    xiada1.Source = img1;
                }

                if (task1.jieshou == "0")
                {
                    jieshou1.Tag = 0;
                    jieshou1.Source = img0;
                }
                else
                {
                    jieshou1.Tag = 1;
                    jieshou1.Source = img1;
                }


                yanpan1.SelectedIndex = int.Parse(task1.yanpan);
                jindu1.SelectedIndex = int.Parse(task1.jindu);
                if (task1.wancheng == "0")
                {
                    wancheng1.Tag = 0;
                    wancheng1.Source = img0;
                }
                else
                {
                    wancheng1.Tag = 1;
                    wancheng1.Source = img1;
                }
            }


            if (task2 != null)
            {
                if (task2.xiada == "0")
                {
                    xiada2.Tag = 0;
                    xiada2.Source = img0;
                }
                else
                {
                    xiada2.Tag = 1;
                    xiada2.Source = img1;
                }

                if (task2.jieshou == "0")
                {
                    jieshou2.Tag = 0;
                    jieshou2.Source = img0;
                }
                else
                {
                    jieshou2.Tag = 1;
                    jieshou2.Source = img1;
                }


                yanpan2.SelectedIndex = int.Parse(task2.yanpan);
                jindu2.SelectedIndex = int.Parse(task2.jindu);
                if (task2.wancheng == "0")
                {
                    wancheng2.Tag = 0;
                    wancheng2.Source = img0;
                }
                else
                {
                    wancheng2.Tag = 1;
                    wancheng2.Source = img1;
                }
            }



        }

        BitmapImage img0 = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\圆点2.png"));
        BitmapImage img1 = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\圆点1.png"));

        tracking task1 = null;
        tracking task2 = null;

        public class tracking
        {
            public string Guid { get; set; }
            public string xiada { get; set; }
            public string jieshou { get; set; }
            public string yanpan { get; set; }
            public string jindu { get; set; }
            public string wancheng { get; set; }
        }
        public int name = 0;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {



            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\TaskTracking.xml");//加载xml文件，文件
            XmlNode xns = xmlDoc.SelectSingleNode("Events");//查找要修改的节点
            XmlNodeList xnl = xns.ChildNodes;//取出所有的子节点


            int i = 0;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                if (xe.GetAttribute("num") == even.Guid&&xe.GetAttribute("Task") == "1")//判断是否存在当前任务的任务1数据
                {
                    i = 1;

                    XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;//转换类型
                        if (xe2.Name == "xiada")//判断是否是要查找的元素
                        {
                            
                            xe2.InnerText = xiada1.Tag.ToString();
                        }
                        if (xe2.Name == "jieshou")//判断是否是要查找的元素
                        {
                            xe2.InnerText = jieshou1.Tag.ToString();

                        }
                        if (xe2.Name == "yanpan")//判断是否是要查找的元素
                        {
                            xe2.InnerText = yanpan1.SelectedIndex.ToString();
                        }
                        if (xe2.Name == "jindu")//判断是否是要查找的元素
                        {
                            xe2.InnerText = jindu1.SelectedIndex.ToString();
                        }
                        if (xe2.Name == "wancheng")//判断是否是要查找的元素
                        {
                            xe2.InnerText = wancheng1.Tag.ToString();
                        }
                    }
                   
                }


                //break;
            }
            if (i == 0)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.SelectSingleNode("Events");
                //创建一个结点
                XmlElement xelKey = doc.CreateElement("Event");
                //设置结点的属性:
                XmlAttribute xelType = doc.CreateAttribute("num");
                xelType.InnerText = even.Guid;
                xelKey.SetAttributeNode(xelType);

                XmlAttribute xelType1 = doc.CreateAttribute("Task");
                xelType1.InnerText ="1";
                xelKey.SetAttributeNode(xelType1);

                XmlElement xelAuthor1 = doc.CreateElement("Guid");
                xelAuthor1.InnerText = even.Guid;

                XmlElement xelAuthor2 = doc.CreateElement("xiada");
                xelAuthor2.InnerText = xiada1.Tag.ToString();

                XmlElement xelAuthor3 = doc.CreateElement("jieshou");
                xelAuthor3.InnerText = jieshou1.Tag.ToString();

                XmlElement xelAuthor4 = doc.CreateElement("yanpan");
                xelAuthor4.InnerText = yanpan1.SelectedIndex.ToString();

                XmlElement xelAuthor5 = doc.CreateElement("jindu");
                xelAuthor5.InnerText = jindu1.SelectedIndex.ToString();

                XmlElement xelAuthor6 = doc.CreateElement("wancheng");
                xelAuthor6.InnerText = wancheng1.Tag.ToString();



                xelKey.AppendChild(xelAuthor1);
                xelKey.AppendChild(xelAuthor2);
                xelKey.AppendChild(xelAuthor3);
                xelKey.AppendChild(xelAuthor4);
                xelKey.AppendChild(xelAuthor5);
                xelKey.AppendChild(xelAuthor6);
                root.AppendChild(xelKey);
            }





            int i2 = 0;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;//将节点转换一下类型
                if (xe.GetAttribute("num") == even.Guid && xe.GetAttribute("Task") == "2")//判断是否存在当前任务的任务1数据
                {
                    i2 = 1;

                    XmlNodeList xnl2 = xe.ChildNodes;//取出该子节点下面的所有元素
                    foreach (XmlNode xn2 in xnl2)
                    {
                        XmlElement xe2 = (XmlElement)xn2;//转换类型
                        if (xe2.Name == "xiada")//判断是否是要查找的元素
                        {

                            xe2.InnerText = xiada2.Tag.ToString();
                        }
                        if (xe2.Name == "jieshou")//判断是否是要查找的元素
                        {
                            xe2.InnerText = jieshou2.Tag.ToString();

                        }
                        if (xe2.Name == "yanpan")//判断是否是要查找的元素
                        {
                            xe2.InnerText = yanpan2.SelectedIndex.ToString();
                        }
                        if (xe2.Name == "jindu")//判断是否是要查找的元素
                        {
                            xe2.InnerText = jindu2.SelectedIndex.ToString();
                        }
                        if (xe2.Name == "wancheng")//判断是否是要查找的元素
                        {
                            xe2.InnerText = wancheng2.Tag.ToString();
                        }
                    }

                }


                //break;
            }
            if (i2 == 0)
            {
                XmlDocument doc = xmlDoc;
                XmlNode root = doc.SelectSingleNode("Events");
                //创建一个结点
                XmlElement xelKey = doc.CreateElement("Event");
                //设置结点的属性:
                XmlAttribute xelType = doc.CreateAttribute("num");
                xelType.InnerText = even.Guid;
                xelKey.SetAttributeNode(xelType);

                XmlAttribute xelType1 = doc.CreateAttribute("Task");
                xelType1.InnerText = "2";
                xelKey.SetAttributeNode(xelType1);

                XmlElement xelAuthor1 = doc.CreateElement("Guid");
                xelAuthor1.InnerText = even.Guid;

                XmlElement xelAuthor2 = doc.CreateElement("xiada");
                xelAuthor2.InnerText = xiada2.Tag.ToString();

                XmlElement xelAuthor3 = doc.CreateElement("jieshou");
                xelAuthor3.InnerText = jieshou2.Tag.ToString();

                XmlElement xelAuthor4 = doc.CreateElement("yanpan");
                xelAuthor4.InnerText = yanpan2.SelectedIndex.ToString();

                XmlElement xelAuthor5 = doc.CreateElement("jindu");
                xelAuthor5.InnerText = jindu2.SelectedIndex.ToString();

                XmlElement xelAuthor6 = doc.CreateElement("wancheng");
                xelAuthor6.InnerText = wancheng2.Tag.ToString();



                xelKey.AppendChild(xelAuthor1);
                xelKey.AppendChild(xelAuthor2);
                xelKey.AppendChild(xelAuthor3);
                xelKey.AppendChild(xelAuthor4);
                xelKey.AppendChild(xelAuthor5);
                xelKey.AppendChild(xelAuthor6);
                root.AppendChild(xelKey);
            }






            xmlDoc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\TaskTracking.xml");//再一次强调 ，一定要记得保存的该XML文件


            name = 1;
            this.Close();
        }

        private void xiada1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if (img.Tag.ToString() == "0") 
            {
                img.Tag = 1;
                img.Source = img1;
            }
            else
            {
                img.Tag = 0;
                img.Source = img0;
            }
        }
        BitmapImage xiangshang = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\xiangshang.png"));
        BitmapImage xiangxia = new BitmapImage(new Uri(System.AppDomain.CurrentDomain.BaseDirectory + @"\DebugImage\xiangxia.png"));
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image img = sender as Image;
            if (img.Tag.ToString() == "0") 
            {
                img.Tag = "1";
                img.Source = xiangxia;
            }
            else
            {
                img.Tag = "0";
                img.Source = xiangshang;
            }

            if(img.Name=="paigongimg")
            {
                if (img.Tag.ToString() == "0")
                {
                    paigong.Visibility = Visibility.Collapsed;
                }
                else 
                {
                    paigong.Visibility = Visibility.Visible;
                }
            }

            if (img.Name == "wuzhiimg")
            {
                if (img.Tag.ToString() == "0")
                {
                    wuzhi.Visibility = Visibility.Collapsed;
                }
                else
                {
                    wuzhi.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
