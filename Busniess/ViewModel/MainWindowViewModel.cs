﻿using Framework;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Busniess.Services;
using System.Collections.ObjectModel;
using Emergence.Common.Model;

namespace Emergence.Business.ViewModel
{
    public class MainWindowViewModel : NotificationObject
    {

        public virtual string UserName { get; set; }
        public virtual double Left { get; set; }
        public virtual double Top { get; set; }
        public virtual double Height { get; set; }
        public virtual double Width { get; set; }
        public virtual WindowState WindowState { get; set; }
        public virtual WindowStyle WindowStyle { get; set; }
        public virtual ResizeMode ResizeMode { get; set; }
        public virtual string UserNameLabel { get { return "用户" + UserName; } }
        public virtual MainPageUiModel MainPageData { get; set; }
        public virtual ObservableCollection<Notification> NotificationList { get; set; }
        public virtual ObservableCollection<MasterEvent> MasterEventList { get; set; }
        public StatisticsViewModel Statistics { get; set; }

        public virtual AnnouncementService NoticeService { get; set; }
        public virtual MasterEventService MasterEService { get; set; }

        public virtual DelegateCommand<string> NavigateCommand { get; set; }

        public MainWindowViewModel()
        {
            NoticeService = new AnnouncementService();
            MasterEService = new MasterEventService();
            GetNoticeList();
            GetMasterEventList();
        }

        public void GetNoticeList()
        {
            var data = NoticeService.GetAnnouncements(1, 10);
            var model = IsAopWapper ? this : this.CreateAopProxy();
            model.NotificationList = new ObservableCollection<Notification>(data.Data.Select(item => new Notification { NotificationTitle = item.Time.ToString("yyyy-MM-dd") + ":" + item.Title }).ToList().Select(a => a.CreateAopProxy()));
        }

        public void GetMasterEventList()
        {
            var masterEvents = MasterEService.GetMasterEventsData(1, 5, "");
            MasterEventList = new ObservableCollection<MasterEvent>( masterEvents.Result.Data.Select(a => a.CreateAopProxy()));
        }

    }

    public class Notification : NotificationObject
    {
        public virtual string NotificationTitle { get; set; }
    }
}
