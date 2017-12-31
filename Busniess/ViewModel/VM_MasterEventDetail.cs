﻿using Emergence.Common.Model;
using Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergence.Common.Data;
using Busniess.Services;
using Prism.Commands;

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
        public DelegateCommand SearchSubEventListCommand { get; private set; }
        #endregion

        public VM_MasterEventDetail(MasterEvent mEvent)
        {
            masterEventService = new MasterEventService();
            subEventService = new SubeventService();
            teamService = new TeamService();
            materialService = new MaterialService();

            SearchSubEventListCommand = new DelegateCommand(SerachSubEventListAction);

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
        public void SerachSubEventListAction()
        {
            var subEvents = subEventService.GetSubevents(0, 1000, this.MasterEventInfo.ID, "").Result;
            SubEventList = new ObservableCollection<SubEvent>(subEvents.Data.Select(o => o.CreateAopProxy()));
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
             var statusValue  =  (int)Enum.Parse(typeof(Enumerator.SubEventStatus), status);
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
