using Busniess.Services.EventSvr;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence.Common.ViewModel;
using Emergence_WPF.Comm;
using Emergence_WPF.ViewModel;
using Emergence_WPF.Views;
using Framework.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MasterEventDetail.xaml
    /// </summary>
    public partial class MasterEventDetail : UserControl
    {
        MasterEvent me;
        GetSubEventSvr gSubSvr;
        VM_MasterEventDetail subEventVM;
        ObservableCollection<SubEvent> omSubList;

        public MasterEventDetail(MasterEvent sme)
        {
            InitializeComponent();
            me = sme;
            gSubSvr = new GetSubEventSvr();
            subEventVM = new VM_MasterEventDetail();
            subEventVM.MasterEventInfo = sme;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //SetMasterEventDetail();
            RequestSubEventList();
            SP_MasterEventDetail.DataContext = subEventVM.MasterEventInfo;
        }
        

        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MasterEventManagement em = new MasterEventManagement();
            this.gridEventDetail.Children.Clear();
            this.gridEventDetail.Children.Add(em);
        }

        private void SetMasterEventDetail()
        {
            //SerialNumberTextBox.Text = me.SerialNumber;
            //TitleTextBox.Text = me.Title;
            //TypeTextBox.Text = me.EventTypeName;
            //LevelTextBox.Text = me.GradeName;
            //TimeTextBox.Text = me.Time;
            //SubmitPartyTextBox.Text = me.SubmitParty;
            //LocaleTextBox.Text = me.Locale;
            //RemarkTextBox.Text = me.Remarks;
        }

        private void RequestSubEventList()
        {
            string subEventURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["GetSubEventListURL"].ToString();

            gSubSvr.RequestData = "&pageIndex=" + subEventVM.PageIndex + "&pageSize=" + subEventVM.PageSize + "&mainEventId=1"; // + subEventVM.MasterEventInfo.Id;

            SubEventListResponse rr = gSubSvr.ProcessRequest(subEventURL);
            if (rr != null && rr.Result.SubEventList != null)
            {
                subEventVM.subEventList = rr.Result.SubEventList.ToList();
                omSubList = new ObservableCollection<SubEvent>(rr.Result.SubEventList.ToList());
                this.Grid_SubEvent.DataContext = omSubList;
            }
        }


        private void GenerateSubEventSimulatedData()
        {
            string addSubEventURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["AddSubEventURL"].ToString();
            List<HeaderInfo> headers = new List<HeaderInfo>();
            //headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
            string dateTime = TimeControl.GenerateTimeStamp();
            string signString = dateTime + "POST/childEvent";
            string authorization = AuthorizationControl.GetAuthorization(signString);

            headers.Add(new HeaderInfo("X-Project-Date", dateTime));
            headers.Add(new HeaderInfo("Authorization", authorization));

            //var dict1 = DictControl.GetEventGrades();
            //var dict2 = DictControl.GetEventStates();
            //var dict3 = DictControl.GetEventTypes();


            for (int i = 1; i < 15; i++)
            {
                for (int j = 1; j < 20; j++)
                {
                    StringBuilder postData = new StringBuilder();
                    //postData.Append("id=" + i.ToString());
                    postData.Append("&mainEventId=" + i.ToString().PadLeft(6, '0'));
                    postData.Append("&childTitle=测试子事件标题" + i.ToString() + j.ToString());
                    postData.Append("&childTime=" + dateTime);
                    postData.Append("&childLocale=苏州市礼数街消防支队");
                    postData.Append("&childSubmitParty=张美娜");
                    postData.Append("&childSubmitTelephoneNumber=13666666666");
                    postData.Append("&childSubmitDept=消防一中队");
                    postData.Append("&childEventType=event_1");
                    postData.Append("&childGrade=grade_1");
                    postData.Append("&childEventState=state_1");
                    postData.Append("&childRemarks=子事件" + i.ToString() + j.ToString());

                    HttpResult hr = HttpCommon.HttpPost(addSubEventURL, postData.ToString(), "", "application/x-www-form-urlencoded", headers);
                }
            }

        }

        private void Button_Btn_DeleteSubEventClick(object sender, RoutedEventArgs e)
        {
            //GenerateSubEventSimulatedData();
            //RequestSubEventList();
        }

        private void Grid_SubEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dg = e.Source as DataGrid;
            SubEvent sme = dg.SelectedItem as SubEvent;
            if (sme != null)
            {
                SubEventDetail sd = new SubEventDetail(sme,me);
                this.gridEventDetail.Children.Clear();
                this.gridEventDetail.Children.Add(sd);
            }
        }
    }
}
