using Emergence_WPF.Comm;
using System.Windows;
using Framework.Http;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using Newtonsoft;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using System;
using Business.MainPageSvr;

namespace Emergence_WPF
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public LoginServices loginS;

        public Login()
        {
            InitializeComponent();
            loginS = new LoginServices();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = this.txtUserName.Text.Trim();
//#if Release
            string passwordStr = this.txtPassword.Password.Trim();
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwordStr))
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            else
            {
                if (RequestLogin(userName, passwordStr))
                {
//#endif
            MainWindow main = new MainWindow();
            main.Show();

            this.Close();
//#if Release
        }
                else
                {
                    MessageBox.Show("用户名或密码不正确!");
                }
            }
//#endif
        }

        private bool RequestLogin(string userName, string passwordStr)
        {
            string loginURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["LoginURL"].ToString();
            string password = PasswordBoxHelper.GetMD5Password(passwordStr);
            string postData = @"name=" + userName + "&&pwd=" + password;
            List<HeaderInfo> headers = new List<HeaderInfo>();
            //headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
            HttpResult hr = HttpCommon.HttpPost(loginURL, postData, "", "application/x-www-form-urlencoded", headers);

            LoginInModel lim = new LoginInModel();

            var jsonObj = JsonConvert.DeserializeAnonymousType(
               hr.Html,
               new
               {
                   code = 0,
                   message = string.Empty,
                   result = new
                   {
                       nike = string.Empty,
                       region = string.Empty,
                       token = string.Empty,
                       tokenSecret = string.Empty,
                       userId = 0
                   }
               });
            if (jsonObj.code != 1)
            {
                MessageBox.Show(jsonObj.message);
                return false;
            }
            lim.Code = jsonObj.code;
            lim.Message = jsonObj.message;
            lim.Nick = jsonObj.result.nike;
            lim.Region = jsonObj.result.region;
            lim.Token = jsonObj.result.token;
            lim.TokenSecret = jsonObj.result.tokenSecret;
            lim.Nick = jsonObj.result.nike;

            //Application.Current.Properties["CurrentUser"] = lim;
            //lim.Authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes("token="+lim.Token+"&signature"))

            AuthorizationControl.SetLogin(lim);
            return true;
        }

        private void CacheRequestToken(HttpResult hr)
        {

        }

    }
}
