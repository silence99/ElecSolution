using System.Windows.Controls;
using Emergence.Business.ViewModel;
using Busniess.Services;
using Busniess.Strategies;

namespace Emergence_WPF
{
	/// <summary>
	/// Interaction logic for MasterEventDetail.xaml
	/// </summary>
	public partial class MasterEventDetail : Page
    {
        VM_MasterEventDetail MasterEventDetailViewModel;
        MasterEventService MasterEventService;
        SubeventService SubEventService;
        //Material

        public MasterEventDetail()
        {
            InitializeComponent();
        }
    }
}
