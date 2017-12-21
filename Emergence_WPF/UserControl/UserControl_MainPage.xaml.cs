using Emergence.Common.ViewModel;
using Emergence_WPF.Comm;
using Emergence_WPF.Model;
using Emergence_WPF.Util;
using Framework;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace Emergence_WPF
{
	/// <summary>
	/// main page user control
	/// </summary>
	public partial class UserControl_MainPage : UserControl, IEmergencyControl<MainPageUiModel>
	{
		private MainPageUiModel _uiModel;
		public MainPageUiModel ViewModel
		{
			get
			{
				return _uiModel;
			}
			set
			{
				_uiModel = value;
			}
		}

		public UserControl_MainPage()
		{
			InitializeComponent();
		}

		public string StrategyControllerName { get { return "mainPageStrategyController"; } }

		public StrategyController StrategyController { get; set; }

		List<Event> Events = new List<Event>();
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{

			//map.Children.Add(control);


			XmlDocument doc = new XmlDocument();

			// 2.把你想要读取的xml文档加载进来

			doc.Load(System.Windows.Forms.Application.StartupPath + @"\xml\Emergence.xml");
			XmlNodeList topM = doc.DocumentElement.ChildNodes;
			// 3.读取你指定的节点

			// XmlNodeList lis = doc.GetElementsByTagName("Row");

			foreach (XmlElement item in topM)
			{
				XmlNodeList nodelist = item.ChildNodes;
				var MessageHeader = nodelist[0].InnerText.ToString().Trim();
				var IncidentTime = nodelist[1].InnerText.ToString().Trim();
				var IncidentArea = nodelist[2].InnerText.ToString().Trim();
				var SubmittingUnit = nodelist[3].InnerText.ToString().Trim();
				var EventType = nodelist[4].InnerText.ToString().Trim();
				var EventLevel = nodelist[5].InnerText.ToString().Trim();
				var EventStatus = nodelist[6].InnerText.ToString().Trim();
				var EventSource = nodelist[7].InnerText.ToString().Trim();

				Event me = new Event();
				me.MessageHeader = MessageHeader;
				me.IncidentTime = IncidentTime;
				me.IncidentArea = IncidentArea;
				me.SubmittingUnit = SubmittingUnit;
				me.EventType = EventType;
				me.EventLevel = EventLevel;
				me.EventStatus = EventStatus;
				me.EventSource = EventSource;
				Events.Add(me);
			}

			this.DataCodeing.ItemsSource = Events;
		}

		private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//map.Children.Clear();
			map.Bind("119.931298", "28.469722");
		}

		public void BindingUiModel(StrategyController parent, NotificationObject uiModel)
		{
			StrategyController = ObjectFactory.GetInstance<StrategyController>(StrategyControllerName);
			StrategyController.Parent = parent;
			ViewModel = uiModel as MainPageUiModel;
			InitUiModel();
		}

		public void InitUiModel()
		{
		}
	}
}
