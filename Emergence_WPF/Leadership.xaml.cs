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
    public partial class Leadership : Window
    {
        public Leadership()
        {
            InitializeComponent();
        }

        public Leadership(Event mm)
        {
            InitializeComponent();
            even = mm;
        }
        
        public Event even = new Event();
        public bool isclose=false;
    
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
         
        }
        public int name = 0;

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            name = 1;
            this.Close();
        }

        private void StackPanel_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
