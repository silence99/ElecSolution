using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Framework
{
    /// <summary>
    /// MessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxResult : Window
    {
        public MessageBoxResult()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 消息框标题
        /// </summary>
        public new string Title {
            get { return txtTitle.Text; }
            set { txtTitle.Text = value; }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message {
            get { return txtMessage.Text; }
            set { txtMessage.Text = value; }
        }
        public bool IsExit = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsExit = false;
            this.DialogResult = false;
            this.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            IsExit = false;
            this.DialogResult = true;
            this.Close();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            IsExit = true;
            this.DialogResult = false;
            this.Close();
        }
    }
}
