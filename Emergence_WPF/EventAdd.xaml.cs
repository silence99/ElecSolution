using Emergence.Common;
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
    public partial class EventAdd : Window
    {
        public EventAdd()
        {
            InitializeComponent();
            fashengshijian.Text = DateTime.Now.ToString("yyyy-MM-dd");
            IncidentTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public Event even = new Event();
        public bool isclose=false;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string Guid = System.Guid.NewGuid().ToString();
            XmlDocument doc = new XmlDocument();
            doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");
           // XmlNodeList topM = doc.DocumentElement.ChildNodes;

            XmlNode root = doc.SelectSingleNode("Events");
            //创建一个结点
            XmlElement xelKey = doc.CreateElement("Event");
            //设置结点的属性:
            XmlAttribute xelType = doc.CreateAttribute("num");
            xelType.InnerText = Guid;
            xelKey.SetAttributeNode(xelType);
            //创建子结点
         
            XmlElement xelAuthor1 = doc.CreateElement("MessageHeader");
            xelAuthor1.InnerText = MessageHeader.Text;
            even.MessageHeader = MessageHeader.Text;
            XmlElement xelAuthor2 = doc.CreateElement("IncidentTime");
            xelAuthor2.InnerText = DateTime.Parse(IncidentTime.Text).ToString("yyyy-M-d");
            even.IncidentTime = DateTime.Parse(IncidentTime.Text).ToString("yyyy-M-d");
            XmlElement xelAuthor3 = doc.CreateElement("IncidentArea");
            xelAuthor3.InnerText = IncidentArea.Text;
            even.IncidentArea = IncidentArea.Text;
            XmlElement xelAuthor4 = doc.CreateElement("SubmittingUnit");
            xelAuthor4.InnerText = SubmittingUnit.Text;
            even.SubmittingUnit = SubmittingUnit.Text;
            XmlElement xelAuthor5 = doc.CreateElement("EventType");
            xelAuthor5.InnerText = EventType.Text;
            even.EventType = EventType.Text;
            XmlElement xelAuthor6 = doc.CreateElement("EventLevel");
            xelAuthor6.InnerText = EventLevel.Text;
            even.EventLevel = EventLevel.Text;
            XmlElement xelAuthor7 = doc.CreateElement("EventStatus");
            xelAuthor7.InnerText = "首报";
            even.EventStatus = "首报";
            XmlElement xelAuthor8 = doc.CreateElement("EventSource");
            xelAuthor8.InnerText = "外部录入";
            even.EventSource = "外部录入";
            XmlElement xelAuthor9 = doc.CreateElement("baosongshijian");
            xelAuthor9.InnerText = even.IncidentTime;
            even.baosongshijian = even.IncidentTime;
            XmlElement xelAuthor10 = doc.CreateElement("sheng");
            xelAuthor10.InnerText = sheng.Text;
            even.sheng = sheng.Text;
            XmlElement xelAuthor11 = doc.CreateElement("shi");
            xelAuthor11.InnerText = shi.Text;
            even.shi = shi.Text;
            XmlElement xelAuthor12 = doc.CreateElement("qu");
            xelAuthor12.InnerText = qu.Text;
            even.qu = qu.Text;
            XmlElement xelAuthor13 = doc.CreateElement("fashengshijian");
            xelAuthor13.InnerText = fashengshijian.Text;
            even.fashengshijian = fashengshijian.Text;
            XmlElement xelAuthor14 = doc.CreateElement("jingdu");
            xelAuthor14.InnerText = jingdu.Text;
            even.jingdu = jingdu.Text;
            XmlElement xelAuthor15 = doc.CreateElement("weidu");
            xelAuthor15.InnerText = weidu.Text;
            even.weidu = weidu.Text;
            XmlElement xelAuthor16 = doc.CreateElement("lianxidianhua");
            xelAuthor16.InnerText = lianxidianhua.Text;
            even.lianxidianhua = lianxidianhua.Text;
            XmlElement xelAuthor17 = doc.CreateElement("baosongren");
            xelAuthor17.InnerText = baosongren.Text;
            even.baosongren = baosongren.Text;
            XmlElement xelAuthor18 = doc.CreateElement("qianfalingdao");
            xelAuthor18.InnerText = qianfalingdao.Text;
            even.qianfalingdao = qianfalingdao.Text;
            XmlElement xelAuthor19 = doc.CreateElement("weizhixinxi");
            xelAuthor19.InnerText = weizhixinxi.Text;
            even.weizhixinxi = weizhixinxi.Text;
            XmlElement xelAuthor20 = doc.CreateElement("qingkuangmiaoshu");
            xelAuthor20.InnerText = qingkuangmiaoshu.Text;
            even.qingkuangmiaoshu = qingkuangmiaoshu.Text;
            XmlElement xelAuthor21 = doc.CreateElement("yuanyinfenxi");
            xelAuthor21.InnerText = yuanyinfenxi.Text;
            even.yuanyinfenxi = yuanyinfenxi.Text;
            XmlElement xelAuthor22 = doc.CreateElement("yingxiangfanwei");
            xelAuthor22.InnerText = yingxiangfanwei.Text;
            even.yingxiangfanwei = yingxiangfanwei.Text;
            XmlElement xelAuthor23 = doc.CreateElement("loginUser");

            even.loginUser = CommHelp.Name.Trim();
            xelAuthor23.InnerText = CommHelp.Name.Trim();


            XmlElement xelAuthor24 = doc.CreateElement("liuzhuanzhuangtai");

            even.liuzhuanzhuangtai = "录入";
            xelAuthor24.InnerText = "录入";

           
            XmlElement xelAuthor25 = doc.CreateElement("Guid");

            even.Guid = Guid;
            xelAuthor25.InnerText = Guid;

            xelKey.AppendChild(xelAuthor1);
            xelKey.AppendChild(xelAuthor2);
            xelKey.AppendChild(xelAuthor3);
            xelKey.AppendChild(xelAuthor4);
            xelKey.AppendChild(xelAuthor5);
            xelKey.AppendChild(xelAuthor6);
            xelKey.AppendChild(xelAuthor7);
            xelKey.AppendChild(xelAuthor8);
            xelKey.AppendChild(xelAuthor9);
            xelKey.AppendChild(xelAuthor10);
            xelKey.AppendChild(xelAuthor11);
            xelKey.AppendChild(xelAuthor12);
            xelKey.AppendChild(xelAuthor13);
            xelKey.AppendChild(xelAuthor14);
            xelKey.AppendChild(xelAuthor15);
            xelKey.AppendChild(xelAuthor16);
            xelKey.AppendChild(xelAuthor17);
            xelKey.AppendChild(xelAuthor18);
            xelKey.AppendChild(xelAuthor19);
            xelKey.AppendChild(xelAuthor20);
            xelKey.AppendChild(xelAuthor21);
            xelKey.AppendChild(xelAuthor22);
            xelKey.AppendChild(xelAuthor23);
            xelKey.AppendChild(xelAuthor24);
            xelKey.AppendChild(xelAuthor25);

            // 最后把book结点挂接在要结点上,并保存整个文件:
            root.AppendChild(xelKey);
            doc.Save(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");
            isclose = true;
            this.Close();
        }
    }
}
