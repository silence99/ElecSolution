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
        public virtual MasterEventService masterEventService { get; set; }
        public virtual SubeventService subEventService { get; set; }
        public virtual TeamService teamService { get; set; }
        public virtual MaterialService materialService { get; set; }
        #endregion

        #region [Property]
        public virtual MasterEvent MasterEventInfo { get; set; }

        public virtual string SubEventSearchValue { get; set; }

        public virtual ObservableCollection<SubEvent> SubEventList { get; set; }

        public virtual SubEvent SubEventDetail { get; set; }

        public virtual ObservableCollection<TeamModel> TeamList { get; set; }

        public virtual ObservableCollection<MaterialModel> MaterialList { get; set; }
        
        public virtual SubEventStatus SBStatus { get; set; }
        #endregion


        #region [Command]
        public virtual DelegateCommand<string> SearchSubEventListCommand { get; set; }
        public virtual DelegateCommand<string> SelectSubEventCommand { get; set; }
        public virtual DelegateCommand<int?> DeleteSubEventCommand { get; set; }
        public virtual DelegateCommand<string> UpdateSubEventStatusCommand { get; set; }

        #endregion

        public VM_MasterEventDetail(MasterEvent mEvent)
        {
            masterEventService = new MasterEventService();
            subEventService = new SubeventService();
            teamService = new TeamService();
            materialService = new MaterialService();
            SBStatus = new SubEventStatus("").CreateAopProxy();
            //this.eventAggregator = eventAggregator;

            SearchSubEventListCommand = new DelegateCommand<string>(new Action<string>(SearchSubEventAction));
            SelectSubEventCommand = new DelegateCommand<string>(new Action<string>(SelectSubEventAction));
            DeleteSubEventCommand = new DelegateCommand<int?>(DeleteSubEventAction);
            UpdateSubEventStatusCommand = new DelegateCommand<string>(new Action<string>(UpdateSubEventStatusAction), new Func<string, bool>(CheckSubEventStatusCanChangeAction));

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
        public void SelectSubEventAction(string subEventID)
        {
            RefershSubEventDetailBlock(subEventID);
        }

        private void UpdateSubEventStatusAction(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                List<string> ids = new List<string>();
                var st = Enum.Parse(typeof(Enumerator.SubEventStatus), status).GetHashCode();
                ids.Add(SubEventDetail.Id.ToString());
                var result = this.UpdateSubEvent(ids, st);
                if (result)
                {
                    SBStatus.SetSubEventStatus(status);
                }
            }
        }
        private bool CheckSubEventStatusCanChangeAction(string status)
        {
            if(string.IsNullOrEmpty(status))
            {
                return false;
            }
            else
            {
                return SBStatus.StatusColorList[status] == Enumerator.EllipseUnselectedColor;
            }
        }
        private void DeleteSubEventAction(int? subEventID)
        {
            if (subEventID != null)
            {
                List<string> ids = new List<string>();
                ids.Add(subEventID.ToString());
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
                var result = subEventService.UpdateChildeEventState(subEventIDs, status);
                return result;
            }
            return false;
        }

        private void GetSubEventListOb(string searchCondition)
        {
            var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID, searchCondition ?? "").Result;
            var thisAop = this.AopWapper as VM_MasterEventDetail;
            thisAop.SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
            //SubEventList.Clear();
            //SubEventList.join(result);
        }

        private void GetSelectedSubEventInfo(string subEventID)
        {
            if (!string.IsNullOrEmpty(subEventID))
            {
                int tempID = -1;
                if (int.TryParse(subEventID, out tempID))
                {
                    var subEvent = this.SubEventList.FirstOrDefault(a => a.Id == tempID);
                    RefershSubEventDetailBlock(subEvent.Id.ToString());
                }
            }
        }

        private void RefershSubEventDetailBlock(string subEventID)
        {
            var subEvent = this.SubEventList.FirstOrDefault(a => a.Id.ToString() == subEventID);
            SubEventDetail = subEvent;
            //GetTeamListOb();
            //GetMaterialListOb();
            SBStatus.ResetSubEventStatus();
            int sbStatus = -1;
            if (int.TryParse(SubEventDetail.State, out sbStatus))
            {
                Enum.GetName(typeof(Enumerator.SubEventStatus), sbStatus);
                SBStatus.SetSubEventStatus(SubEventDetail.State);
            }
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
        

        private void ShowMessageBox(string value)
        {
            System.Windows.MessageBox.Show(value);
        }
        #endregion
    }

    public class SubEventStatus : NotificationObject
    {
        public virtual string Registed { get; set; }
        public virtual string Published { get; set; }
        public virtual string Accepted { get; set; }
        public virtual string Executed { get; set; }
        public virtual string Completed { get; set; }

        public virtual string selectedColor { get; set; }
        public virtual string unSelectedColor { get; set; }

        public virtual Dictionary<string, string> StatusColorList { get; set; }

        public SubEventStatus(string status)
        {
            selectedColor = Enumerator.EllipseSelectedColor;
            unSelectedColor = Enumerator.EllipseUnselectedColor;

            Registed = unSelectedColor;
            Published = unSelectedColor;
            Accepted = unSelectedColor;
            Executed = unSelectedColor;
            Completed = unSelectedColor;
            StatusColorList = new Dictionary<string, string>();
            StatusColorList.Add("Registed", unSelectedColor);
            StatusColorList.Add("Published", unSelectedColor);
            StatusColorList.Add("Accepted", unSelectedColor);
            StatusColorList.Add("Executed", unSelectedColor);
            StatusColorList.Add("Completed", unSelectedColor);
            SetSubEventStatus(status);
        }

        public void ResetSubEventStatus()
        {
            Registed = unSelectedColor;
            Published = unSelectedColor;
            Accepted = unSelectedColor;
            Executed = unSelectedColor;
            Completed = unSelectedColor;
            StatusColorList["Registed"] = unSelectedColor;
            StatusColorList["Published"] = unSelectedColor;
            StatusColorList["Accepted"] = unSelectedColor;
            StatusColorList["Executed"] = unSelectedColor;
            StatusColorList["Completed"] = unSelectedColor;
        }
        public void SetSubEventStatus(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                var statusValue = (int)Enum.Parse(typeof(Enumerator.SubEventStatus), status);
                switch (statusValue)
                {
                    case (int)Enumerator.SubEventStatus.Registed:
                        this.Registed = selectedColor;
                        this.StatusColorList["Registed"] = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Published:
                        this.Registed = selectedColor;
                        this.Published = selectedColor;
                        this.StatusColorList["Registed"] = selectedColor;
                        this.StatusColorList["Published"] = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Accepted:
                        this.Registed = selectedColor;
                        this.Published = selectedColor;
                        this.Accepted = selectedColor;
                        this.StatusColorList["Registed"] = selectedColor;
                        this.StatusColorList["Published"] = selectedColor;
                        this.StatusColorList["Accepted"] = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Executed:
                        this.Registed = selectedColor;
                        this.Published = selectedColor;
                        this.Accepted = selectedColor;
                        this.Executed = selectedColor;
                        this.StatusColorList["Registed"] = selectedColor;
                        this.StatusColorList["Published"] = selectedColor;
                        this.StatusColorList["Accepted"] = selectedColor;
                        this.StatusColorList["Executed"] = selectedColor;
                        break;
                    case (int)Enumerator.SubEventStatus.Completed:
                        this.Registed = selectedColor;
                        this.Published = selectedColor;
                        this.Accepted = selectedColor;
                        this.Executed = selectedColor;
                        this.Completed = selectedColor;
                        this.StatusColorList["Registed"] = selectedColor;
                        this.StatusColorList["Published"] = selectedColor;
                        this.StatusColorList["Accepted"] = selectedColor;
                        this.StatusColorList["Executed"] = selectedColor;
                        this.StatusColorList["Completed"] = selectedColor;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
