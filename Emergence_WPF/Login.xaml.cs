using Emergence_WPF.Comm;
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

namespace Emergence_WPF
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = this.txtKeyword.Text.Trim();
            if(name == "C" || name == "B")
            {
                CommHelp.Name = this.txtKeyword.Text;
                CommHelp.Name = string.IsNullOrWhiteSpace(CommHelp.Name) ? "C" : CommHelp.Name.Trim();
                MainWindow main = new MainWindow();
                main.Show();

                this.Close();
            }
            else
            {

                MessageBox.Show("用户名或密码不能为空");
            }
            
        }
    }
}
