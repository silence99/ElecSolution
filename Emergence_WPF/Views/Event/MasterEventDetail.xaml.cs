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
        public MasterEventDetail(MasterEvent sme)
        {
            InitializeComponent();
            me = sme;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SetMasterEventDetail();
        }

        private void Pager_OnSubPageChanged()
        {
        }

        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MasterEventManagement em = new MasterEventManagement();
            this.gridEventDetail.Children.Clear();
            this.gridEventDetail.Children.Add(em);
        }

        public void SetMasterEventDetail()
        {
            SerialNumberTextBox.Text = me.SerialNumber;
            TitleTextBox.Text = me.Title;
            TypeTextBox.Text = me.EventTypeName;
            LevelTextBox.Text = me.GradeName;
            TimeTextBox.Text = me.Time;
            SubmitPartyTextBox.Text = me.SubmitParty;
            LocaleTextBox.Text = me.Locale;
            RemarkTextBox.Text = me.Remarks;
        }
    }
}
