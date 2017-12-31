using Emergence.Common.Model;
using Framework;
using System.Windows;
using System.Windows.Controls;
using Emergence.Business.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Unity;

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

            ViewModel = new VM_MasterEventDetail(masterEventID);//.CreateAopProxy();
            this.DataContext = ViewModel;

            this.ClickCommand = new DelegateCommand<object>(OnClick);

            this.Btn_CreateSubEvent.DataContext = ClickCommand;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        public void OnClick(object e)
        {
            System.Windows.MessageBox.Show("调用成功");
            //do something
        }

        public bool CanExecute(object e)
        {
            return true;
            //do something
        }
    }
}
