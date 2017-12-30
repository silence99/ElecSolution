using Busniess.Services.EventSvr;
using Emergence.Common.Model;
using Emergence.Business.ViewModel;
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
    /// Interaction logic for SubEventDetail.xaml
    /// </summary>
    public partial class SubEventDetail : UserControl
    {
        SubEvent se;
        MasterEvent me;
        VM_SubEventDetail subEventDetailVM;
        GetTaskSvr gTaskSvr;
        ObservableCollection<EventTask> omTSList;

        public SubEventDetail(SubEvent seL, MasterEvent meL)
        {
            InitializeComponent();
            se = seL;
            me = meL;
            subEventDetailVM = new VM_SubEventDetail();
            subEventDetailVM.SubEventInfo = seL;
            gTaskSvr = new GetTaskSvr();
            omTSList = new ObservableCollection<EventTask>();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SP_SubEventDetail.DataContext = subEventDetailVM.SubEventInfo;
            RequestTaskList();
        }


        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (me != null)
            {
                //MasterEventDetail md = new MasterEventDetail(me);
                //this.gridSubDetail.Children.Clear();
                //this.gridSubDetail.Children.Add(md);
            }
        }

        private void RequestTaskList()
        {
            //string subEventURL = ConfigurationSettings.AppSettings["BaseURL"].ToString() + ConfigurationSettings.AppSettings["GetTaskListURL"].ToString();

            //gTaskSvr.RequestData = "&pageIndex=" + subEventDetailVM.PageIndex + "&pageSize=" + subEventDetailVM.PageSize;

            //TaskListResponse rr = gTaskSvr.ProcessRequest(subEventURL);
            //if (rr != null && rr.result.taskList != null)
            //{
            //    subEventDetailVM.subEventList = rr.result.taskList.ToList();
            //    omTSList = new ObservableCollection<EventTask>(rr.result.taskList.ToList());
            //    this.Grid_TaskList.DataContext = omTSList;
            //}

            string taskLists = ",,]}";
            List<EventTask> ets = new List<EventTask>();
            string t1 = "{\"childEventId\":190,\"createdBy\":\"1\",\"createdName\":\"admin\",\"id\":14,\"taskExecutiveDept\":\"xxx供电局\",\"taskExecutor\":\"张三\",\"taskGrade\":\"grade_2\",\"taskGradeName\":\"紧急任务\",\"taskRemarkes\":\"测试2\",\"taskStartTime\":\"2017-12-02 17:22:11\",\"taskState\":\"state_2\",\"taskStateName\":\"进行中\",\"taskTime\":\"2017-12-02 17:22:11\",\"taskTitle\":\"测试任务13\"}";
            EventTask et1 = Utils.JSONHelper.ConvertToObject<EventTask>(t1);
            ets.Add(et1);
            string t2 = "{\"childEventId\":190,\"createdBy\":\"1\",\"createdName\":\"admin\",\"id\":13,\"taskExecutiveDept\":\"xxx供电局\",\"taskExecutor\":\"张三\",\"taskGrade\":\"grade_2\",\"taskGradeName\":\"紧急任务\",\"taskRemarkes\":\"测试2\",\"taskStartTime\":\"2017-12-02 17:22:11\",\"taskState\":\"state_2\",\"taskStateName\":\"进行中\",\"taskTime\":\"2017-12-02 17:22:11\",\"taskTitle\":\"测试任务11\"}";
            EventTask et2 = Utils.JSONHelper.ConvertToObject<EventTask>(t1);
            ets.Add(et2);
            string t3 = "{\"childEventId\":190,\"createdBy\":\"1\",\"createdName\":\"admin\",\"id\":12,\"taskExecutiveDept\":\"xxx供电局\",\"taskExecutor\":\"张三\",\"taskGrade\":\"grade_2\",\"taskGradeName\":\"紧急任务\",\"taskRemarkes\":\"测试2\",\"taskStartTime\":\"2017-12-02 17:22:11\",\"taskState\":\"state_2\",\"taskStateName\":\"进行中\",\"taskTime\":\"2017-12-02 17:22:11\",\"taskTitle\":\"测试任务9\"}";
            EventTask et3 = Utils.JSONHelper.ConvertToObject<EventTask>(t1);
            ets.Add(et3);
            string t4 = "{\"childEventId\":190,\"createdBy\":\"1\",\"createdName\":\"admin\",\"id\":10,\"taskExecutiveDept\":\"xxx供电局\",\"taskExecutor\":\"张三\",\"taskGrade\":\"grade_2\",\"taskGradeName\":\"紧急任务\",\"taskRemarkes\":\"测试\",\"taskStartTime\":\"2017-12-02 17:22:11\",\"taskState\":\"state_2\",\"taskStateName\":\"进行中\",\"taskTime\":\"2017-12-02 17:22:11\",\"taskTitle\":\"测试任务7\"}";
            EventTask et4 = Utils.JSONHelper.ConvertToObject<EventTask>(t1);
            ets.Add(et4);
            omTSList = new ObservableCollection<EventTask>(ets);
            this.Grid_TaskList.DataContext = omTSList;
        }
        
    }
}
