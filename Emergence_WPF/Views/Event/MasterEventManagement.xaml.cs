using Busniess.Services.EventSvr;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
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
using System.Web;
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
        GetMasterEventSvr gMasterSvr;
        ObservableCollection<MasterEvent> omList;
        public delegate void ChangePageDelegate(MasterEvent me);
        public event ChangePageDelegate ChangePageEvent;

        public MasterEventManagement()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            masterEventVM = new VM_MasterEventManagement();
            gMasterSvr = new GetMasterEventSvr();
            RequestMasterEventList();
            this.DataContext = masterEventVM;
            //GenerateSimulatedData();
        }

        private void Grid_MasterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gridList = sender as DataGrid;

            if (gridList.SelectedItems.Count > 0)
            {
                foreach (MasterEvent dd in gridList.SelectedItems)
                {
                    dd.IsChecked = true;
                }
            }
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

        private void RequestMasterEventList()
        {
            string masterEventURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["GetMainEventListURL"].ToString();

            gMasterSvr.RequestData = "&pageIndex=" + masterEventVM.PageIndex + "&pageSize=" + masterEventVM.PageSize;

            MasterEventListResponse rr = gMasterSvr.ProcessRequest(masterEventURL);
            if (rr.Result.MasterEventList != null)
            {
                masterEventVM.masterEventList = rr.Result.MasterEventList.ToList();
                omList = new ObservableCollection<MasterEvent>(rr.Result.MasterEventList.ToList());
                this.Grid_MasterEvent.DataContext = omList;
            }
        }

        private void masterEventSearchButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateSimulatedData();
        }

        private void Cbx_CheckAll_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in Grid_MasterEvent.Items)
            {
                DataGridTemplateColumn templeCm = Grid_MasterEvent.Columns[0] as DataGridTemplateColumn;
                FrameworkElement fwElement = Grid_MasterEvent.Columns[0].GetCellContent(item);
                if (fwElement != null)
                {
                    CheckBox cBox = templeCm.CellTemplate.FindName("cb", fwElement) as CheckBox;
                    if (cBox.IsChecked == true)
                    {
                        cBox.IsChecked = false;
                    }
                    else
                    {
                        if (cBox != null)
                        {
                            cBox.IsChecked = true;
                        }
                        else
                        {
                            cBox.IsChecked = false;
                        }
                    }
                }
            }
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
                postData.Append("serialNumber=MD" + i.ToString().PadLeft(6, '0'));
                postData.Append("&title=测试主事件标题" + i.ToString());
                postData.Append("&eventType=event_1");
                postData.Append("&grade=grade_1");
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

        private void Grid_MasterEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dg = e.Source as DataGrid;
            MasterEvent me = dg.SelectedItem as MasterEvent;
            if (me != null)
            {
                MasterEventDetail md = new MasterEventDetail(me);
                this.gridEventMain.Children.Clear();
                this.gridEventMain.Children.Add(md);
            }
            //md.SetMasterEventDetail(dg.CurrentItem as MasterEvent);
        }
    }
}
