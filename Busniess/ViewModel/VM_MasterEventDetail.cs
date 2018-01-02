using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Common.Data;
using Busniess.Services;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace Emergence.Business.ViewModel
{
    public class VM_MasterEventDetail : NotificationObject
    {
        #region [Services]
        MasterEventService masterEventService;
        SubeventService subEventService;
        TeamService teamService;
        MaterialService materialService;
        #endregion

        #region [Property]
        public virtual MasterEvent MasterEventInfo { get; set; }

        public virtual string SubEventSearchValue { get; set; }

        public virtual ObservableCollection<SubEvent> SubEventList { get; set; }

        public virtual SubEvent SubEventDetail { get; set; }

        public virtual SubTaskStatus SubTaskStatuColor { get; set; }

        public virtual ObservableCollection<TeamModel> TeamList { get; set; }

        public virtual ObservableCollection<MaterialModel> MaterialList { get; set; }
        #endregion


        #region [Command]
        public virtual DelegateCommand<string> SearchSubEventListCommand { get; set; }
        public virtual DelegateCommand<string> SelectSubEventCommand { get; set; }
        public virtual DelegateCommand<string> DeleteSubEventCommand { get; set; }
        #endregion

        public VM_MasterEventDetail(MasterEvent mEvent)
        {
            masterEventService = new MasterEventService();
            subEventService = new SubeventService();
            teamService = new TeamService();
            materialService = new MaterialService();
            //this.eventAggregator = eventAggregator;

            SearchSubEventListCommand = new DelegateCommand<string>(new Action<string>(SearchSubEventAction));
            SelectSubEventCommand = new DelegateCommand<string>(new Action<string>(SelectSubEventAction));
            DeleteSubEventCommand = new DelegateCommand<string>(new Action<string>(DeleteSubEventAction));

            if (mEvent != null)
            {
                InitializVM(mEvent);
            }
        }

        #region [Public Method]
        public void InitializVM(MasterEvent masterEvent)
        {
            this.MasterEventInfo = masterEvent;
            var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID).Result;
            SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
        }

        #endregion



        #region [Private Method]
        private void SearchSubEventAction(string searchCondition)
        {
            if (!string.IsNullOrEmpty(searchCondition))
            {
                GetSubEventListOb(searchCondition);
            }
        }
        private void SelectSubEventAction(string subEventID)
        {

        }
        private void DeleteSubEventAction(string subEventID)
        {
            if (!string.IsNullOrEmpty(subEventID))
            {
                List<string> ids = new List<string>();
                ids.Add(subEventID);
                var result = this.UpdateSubEvent(ids, 9);
                if (result)
                {
                    ShowMessageBox("删除成功！");
                }
                else
                {
                    ShowMessageBox("删除失败！");
                }
                GetSubEventListOb("");
            }
        }

        private bool UpdateSubEvent(List<string> subEventIDs, int status)
        {
            if (subEventIDs != null && subEventIDs.Count() > 0 && status >= 0)
            {
                var result = subEventService.UpdateChildeEventState(subEventIDs, 9);
                return result;
            }
            return false;
        }

        private void GetSubEventListOb(string searchCondition)
        {
            var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID, searchCondition ?? "").Result;
            SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
        }

        private void GetSelectedSubEventInfo(string subEventID)
        {
            if(!string.IsNullOrEmpty(subEventID))
            {
                int tempID = -1;
                if (int.TryParse(subEventID, out tempID))
                {
                    var subEvent = this.SubEventList.FirstOrDefault(a => a.Id == tempID);
                    RefershSubEventDetailBlock();
                }
            }
        }

        private void RefershSubEventDetailBlock()
        {
            GetTeamListOb();
            GetMaterialListOb();
            ResetSubEventStatus();
        }

        private void GetTeamListOb()
        {
            var teams = teamService.GetbindingTeamData(0, 1000, SubEventDetail.Id).Result;
            TeamList = new ObservableCollection<TeamModel>(teams.Data.Select(o => o.CreateAopProxy()));
        }
        private void GetMaterialListOb()
        {
            var materials = materialService.GetMaterialsBindingToSubevent(0, 1000, this.MasterEventInfo.ID).Result;
            MaterialList = new ObservableCollection<MaterialModel>(materials.Data.Select(o => o.CreateAopProxy()));
        }

        private void ResetSubEventStatus()
        {

        }

        private void ShowMessageBox(string value)
        {
            System.Windows.MessageBox.Show(value);
        }
        #endregion
    }

    public struct SubTaskStatus
    {
        public string RegistedColor { get; set; }
        public string PublishedColor { get; set; }
        public string AcceptedColor { get; set; }
        public string ExecutedColor { get; set; }
        public string CompletedColor { get; set; }

        public SubTaskStatus(string status)
        {
            var statusValue = (int)Enum.Parse(typeof(Enumerator.SubEventStatus), status);
            string selectedColor = "";
            string unSelectedColor = "";

            RegistedColor = unSelectedColor;
            PublishedColor = unSelectedColor;
            AcceptedColor = unSelectedColor;
            ExecutedColor = unSelectedColor;
            CompletedColor = unSelectedColor;

            if (statusValue > 0)
            {
                switch (statusValue)
                {
                    case (int)Enumerator.SubEventStatus.Registed:
                        RegistedColor = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Published:
                        RegistedColor = selectedColor;
                        PublishedColor = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Accepted:
                        RegistedColor = selectedColor;
                        PublishedColor = selectedColor;
                        AcceptedColor = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Executed:
                        RegistedColor = selectedColor;
                        PublishedColor = selectedColor;
                        AcceptedColor = selectedColor;
                        PublishedColor = selectedColor;
                        ExecutedColor = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Completed:
                        RegistedColor = selectedColor;
                        AcceptedColor = selectedColor;
                        ExecutedColor = selectedColor;
                        CompletedColor = selectedColor;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
