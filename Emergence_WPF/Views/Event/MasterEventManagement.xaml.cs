using Emergence.Common.Authorization;
using Emergence_WPF.Comm;
using Emergence_WPF.ViewModel;
using Framework.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Emergence_WPF
{
    /// <summary>
    /// Interaction logic for MasterEventManagement.xaml
    /// </summary>
    public partial class MasterEventManagement : UserControl
    {
        VM_MasterEventManagement masterEventVM;

        public MasterEventManagement()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            masterEventVM = new VM_MasterEventManagement();
            this.Content = masterEventVM;
            GetRequest();
            //GenerateSimulatedData();
        }

        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
        {
            var x = e.PageIndex;
        }

        private List<MasterEventUiModel> GetMasterEvents(int pageNum, int count)
        {
            List<MasterEventUiModel> meList = new List<MasterEventUiModel>();

            return meList;
        }

        private void GetRequest()
        {
            string loginURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["GetMainEventListURL"].ToString();
            List<HeaderInfo> headers = new List<HeaderInfo>();
            //headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            string signString = dateTime + "GET/getMainEventList";
            string authorization = AuthorizationControl.GetAuthorization(signString);

            loginURL += "&pageIndex=" + masterEventVM.PageIndex + "&pageSize=" + masterEventVM.PageSize;
            headers.Add(new HeaderInfo("X-Project-Date", dateTime));
            headers.Add(new HeaderInfo("Authorization", authorization));

            HttpResult hr = HttpCommon.HttpGet(loginURL, "", headers);
            var jsonObj = JsonConvert.DeserializeAnonymousType(
               hr.Html,
               new
               {
                   code = 0,
                   message = string.Empty,
                   result = new
                   {
                       count = 0,
                       data = string.Empty
                   }
               });
            
           // return hr;
            
        }

        private void masterEventSearchButton_Click(object sender, RoutedEventArgs e)
        {
            GetRequest();
        }

        private void GenerateSimulatedData()
        {
            string loginURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["AddMainEventURL"].ToString();
            List<HeaderInfo> headers = new List<HeaderInfo>();
            //headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
            string dateTime = TimeControl.GenerateTimeStamp();
            string signString = dateTime + "POST/mainEvent";
            string authorization = AuthorizationControl.GetAuthorization(signString);
            
            headers.Add(new HeaderInfo("X-Project-Date", dateTime));
            headers.Add(new HeaderInfo("Authorization", authorization));

            for (int i = 1; i < 21; i++)
            {
                StringBuilder postData = new StringBuilder();
                //postData.Append("id=" + i.ToString());
                postData.Append("&serialNumber=MD" + i.ToString().PadLeft(6, '0'));
                postData.Append("&title=测试主事件标题" + i.ToString());
                postData.Append("&eventType=type0");
                postData.Append("&grade=0");
                postData.Append("&time=" + dateTime);
                postData.Append("&describe=主事件" + i.ToString());
                postData.Append("&submitParty=张美娜");
                postData.Append("&telephoneNumber=13333333333");
                postData.Append("&submitDept=消防一中队");
                postData.Append("&locale=湖州市礼数街消防支队");
                postData.Append("&longitude=120.000");
                postData.Append("&latitude=120.000");

                HttpResult hr = HttpCommon.HttpPost(loginURL, postData.ToString(), "", "application/x-www-form-urlencoded", headers);
            }

        }
    }
}
