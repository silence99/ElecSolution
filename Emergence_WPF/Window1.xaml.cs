using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Emergence_WPF
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thread mThread = new Thread(ThreadProcess);
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            mThread.Name = "线程测试";
            mThread.Start();
        }
        private delegate void SetprogressBarHandle(int vaule);    //定义 代理函数 
        private void SetprogressBar(int vaule)
        {

            if (this.Dispatcher.Thread != System.Threading.Thread.CurrentThread)
            {
                this.Dispatcher.Invoke(new SetprogressBarHandle(this.SetprogressBar), vaule);
            }
            else
            {
                progressBar1.Value = vaule;

            } 

          
        }
        private void ThreadProcess(object obj)
        {
            int i = 0;
            while (true&&i<100)
            {
              
                i++;
                SetprogressBar(i);
                Thread.Sleep(300);
            }

        }
    }
}
