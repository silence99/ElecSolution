using Busniess.Services.EventSvr;
using Busniess.Strategies;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
using Emergence_WPF.ViewModel;
using Framework;
using Framework.Http;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventManagement.xaml
	/// </summary>
	public partial class MasterEventManagement : UserControl
	{
		Emergence.Common.ViewModel.VM_MasterEventManagement ViewModel;
		MasterEventService MasterEventService;
		MasterEventManagementStrategyController StrategyController = null;
		public delegate void GoToDetailHandler(MasterEvent masterEvent);
		public event GoToDetailHandler GoToDetail;


		public MasterEventManagement()
		{
			InitializeComponent();
			StrategyController = new MasterEventManagementStrategyController();
		}


		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			ViewModel = new Emergence.Common.ViewModel.VM_MasterEventManagement().CreateAopProxy();
			MasterEventService = new MasterEventService();
			RequestMasterEventList();
			this.DataContext = ViewModel;
		}

		private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
		{
			RequestMasterEventList();
		}

		private void RequestMasterEventList()
		{
			//var masterEvents = gMasterSvr.GetMasterEvents(ViewModel.PageIndex, ViewModel.PageSize);
			var masterEvents = MasterEventService.GetMasterEvents(ViewModel.PageIndex, 2, ViewModel.TxtTitle, default(DateTime), default(DateTime), string.Empty);
			if (masterEvents != null)
			{
				ViewModel.MasterEvents = masterEvents.MasterEvents;
				ViewModel.TotalCount = masterEvents.Count;
				ViewModel.PageIndex = masterEvents.PageIndex;
				ViewModel.PageSize = masterEvents.PageSize;
				ViewModel.TotalPage = ViewModel.TotalCount == 0 ? 0 : (int)Math.Ceiling((double)ViewModel.TotalCount / ViewModel.PageSize);
			}
		}

		private void masterEventSearchButton_Click(object sender, RoutedEventArgs e)
		{
			RequestMasterEventList();
		}

		//private void GenerateSimulatedData()
		//{
		//	string loginURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["AddMainEventURL"].ToString();
		//	List<HeaderInfo> headers = new List<HeaderInfo>();
		//	//headers.Add(new HeaderInfo("Content-Type", "application/x-www-form-urlencoded"));
		//	string dateTime = TimeControl.GenerateTimeStamp();
		//	string signString = dateTime + "POST/mainEvent";
		//	string authorization = AuthorizationControl.GetAuthorization(signString);

		//	headers.Add(new HeaderInfo("X-Project-Date", dateTime));
		//	headers.Add(new HeaderInfo("Authorization", authorization));

		//	for (int i = 1; i < 21; i++)
		//	{
		//		StringBuilder postData = new StringBuilder();
		//		//postData.Append("id=" + i.ToString());
		//		postData.Append("serialNumber=MD" + i.ToString().PadLeft(6, '0'));
		//		postData.Append("&title=测试主事件标题" + i.ToString());
		//		postData.Append("&eventType=event_1");
		//		postData.Append("&grade=grade_1");
		//		postData.Append("&time=" + dateTime);
		//		postData.Append("&describe=主事件" + i.ToString());
		//		postData.Append("&submitParty=张美娜");
		//		postData.Append("&telephoneNumber=13333333333");
		//		postData.Append("&submitDept=消防一中队");
		//		postData.Append("&locale=湖州市礼数街消防支队");
		//		postData.Append("&longitude=120.000");
		//		postData.Append("&latitude=120.000");
		//		HttpResult hr = HttpCommon.HttpPost(loginURL, postData.ToString(), "", "application/x-www-form-urlencoded", headers);
		//	}

		//}

		private void Grid_MasterEvent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var dg = e.Source as DataGrid;
			MasterEvent me = dg.SelectedItem as MasterEvent;
			GoToDetail?.Invoke(me);
		}

		private void Btn_CreateMasterEvent_Click(object sender, RoutedEventArgs e)
		{
			AddMasterEvent ame = new AddMasterEvent();
			ame.ShowDialog();
		}

		private void DeleteMasterEventHandler(object sender, RoutedEventArgs e)
		{
			var events = ViewModel.MasterEvents.Where(ent => ent.IsChecked).Select(ent => (long)ent.ID).ToList();
			MasterEventService.UpdateMasterEventState(events, 9); // state:9 删除, 1 归档, 0 正常
		}
	}
}
