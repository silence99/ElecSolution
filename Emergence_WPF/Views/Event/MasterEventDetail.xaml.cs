using Busniess.Services;
using Busniess.Strategies;
using Emergence.Business.CommonControl;
using Emergence.Common.Model;
using Emergence_WPF.Comm;
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
using Emergence.Business.ViewModel;
using System.Windows.Input;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventDetail.xaml
	/// </summary>
	public partial class MasterEventDetail : Page
    {
        VM_MasterEventDetail ViewModel;
        //Material

        public MasterEventDetail(string masterEventID)
        {
            InitializeComponent();

            ViewModel = new VM_MasterEventDetail().CreateAopProxy();
            this.DataContext = ViewModel;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
        
    }
}
