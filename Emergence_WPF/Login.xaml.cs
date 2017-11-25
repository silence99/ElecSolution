using Emergence_WPF.Comm;
using System.Windows;

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
