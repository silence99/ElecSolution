using Emergence.Common.Model;
using Emergence.Common.ViewModel;
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
    /// Interaction logic for SubEventDetail.xaml
    /// </summary>
    public partial class SubEventDetail : UserControl
    {
        SubEvent se;
        MasterEvent me;
        VM_SubEventDetail subEventDetailVM;

        public SubEventDetail(SubEvent seL, MasterEvent meL)
        {
            InitializeComponent();
            se = seL;
            me = meL;
            subEventDetailVM = new VM_SubEventDetail();
            subEventDetailVM.SubEventInfo = seL;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SP_SubEventDetail.DataContext = subEventDetailVM.SubEventInfo;
        }


        private void labelPageReturn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (me != null)
            {
                MasterEventDetail md = new MasterEventDetail(me);
                this.gridSubDetail.Children.Clear();
                this.gridSubDetail.Children.Add(md);
            }
        }
    }
}
