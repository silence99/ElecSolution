using Emergence_WPF.Comm;
using Emergence_WPF.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MasterEventManagement.xaml
    /// </summary>
    public partial class MasterEventManagement : UserControl
    {
        VM_MasterEventManagement masterEventVM;

        public MasterEventManagement()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            masterEventVM = new VM_MasterEventManagement();
            this.Content = masterEventVM;
        }

        private void DataCodeing_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Pager_OnPageChanged(object sender, PageChangedEventArg e)
        {
            var x = e.PageIndex;
        }
    }
}
