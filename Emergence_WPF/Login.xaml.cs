using Business.MainPageSvr;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence.Business.ViewModel;
using Emergence_WPF.Comm;
using Emergence_WPF.Util;
using Framework.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
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

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string userName = this.txtUserName.Text.Trim();
			//#if Release
			string passwordStr = this.txtPassword.Password.Trim();
			if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passwordStr))
			{
				MessageBox.Show("用户名或密码不能为空");
			}
			else
			{
				if (RequestLogin(userName, passwordStr))
				{
					//#endif
					var main = ObjectFactory.GetInstance<MainWindow>("mainWindow");
					var mainWindowUiModel = new MainWindowViewModel()
					{
						UserName = this.txtUserName.Text
					};
					main.BindingUiModel(null, mainWindowUiModel);
					if (main == null)
					{
						MessageBox.Show("应用程序错误，请联系管理员。");
					}

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

		private bool SetAuthorization(string responseHtml)
		{
			LoginInModel lim = new LoginInModel();

			var jsonObj = JsonConvert.DeserializeAnonymousType(
			   responseHtml,
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

			AuthorizationControl.SetLogin(lim);
			return true;
		}
		private bool RequestLogin(string userName, string passwordStr)
		{
			string password = PasswordBoxHelper.GetMD5Password(passwordStr);
			var loginService = new LoginServices();
			return loginService.LogIn(userName, password, SetAuthorization);
		}

        private void Label_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void Label_Exit_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
