using Emergence_WPF.Comm;
using System.Windows;
using Framework.Http;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
#if Release
            string passwordStr = this.txtPassword.Password.Trim();
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwordStr))
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                if (RequestLogin(userName, passwordStr))
                {
#endif
                    MainWindow main = new MainWindow();
                    main.Show();

                    this.Close();
#if Release
        }
                else
                {
                    MessageBox.Show("用户名或密码不正确!");
                }
            }
#endif
        }

        private bool RequestLogin(string userName, string passwordStr)
        {
            bool result = false;
            string loginURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["LoginURL"].ToString();
            string password = PasswordBoxHelper.GetMD5Password(passwordStr);
            string postData = @"name=" + userName + "&&pwd=" + password;
            List<HeaderInfo> headers = new List<HeaderInfo>();
            //headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
            HttpResult hr = HttpCommon.HttpPost(loginURL, postData, "", "application/x-www-form-urlencoded", headers);
            
            return result;
        }

        private void CacheRequestToken(HttpResult hr)
        {

        }
    }
}
