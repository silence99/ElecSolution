using Emergence.Common.Model;
using Framework;
using System.Windows;
using System.Windows.Controls;
using Emergence.Business.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;
using System.Data;

namespace Emergence_WPF
{
    /// <summary>
    /// Interaction logic for MasterEventDetail.xaml
    /// </summary>
    public partial class MasterEventDetail : Page
    {
        public VM_MasterEventDetail ViewModel { get; set; }
        //{
    //        get { return this.DataContext as VM_MasterEventDetail; }
    //set { this.DataContext = value; }
//}
public DelegateCommand<object> ClickCommand { get; private set; }

        public MasterEventDetail(MasterEvent masterEventID)
        {
            InitializeComponent();
            ViewModel = new VM_MasterEventDetail(masterEventID).CreateAopProxy();
            this.DataContext = ViewModel;

        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SubEventList.Count > 0)
            {
                DataGridRow row = (DataGridRow)this.Grid_SubEventList.ItemContainerGenerator.ContainerFromIndex(0);
                row.IsSelected = true;
                var item = row.Item as SubEvent;
                ViewModel.SelectSubEventAction(item.Id.ToString());
            }
        }
        
        private void Grid_SubEventList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = sender as DataGrid;
            var item = dg.CurrentItem as SubEvent;
            //DataRowView item = cell.Item as DataRowView;
            if (item != null)
            {
                ViewModel.SelectSubEventAction(item.Id.ToString());
            }
        }
    }
}
