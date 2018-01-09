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
using Busniess.Services;
using Emergence.Business.ViewModel;
using Emergence_WPF.Util;

namespace Emergence_WPF
{
    /// <summary>
    /// Interaction logic for EmergenceMainPage.xaml
    /// </summary>
    public partial class EmergenceMainPage : Page
    {
        private MainWindowViewModel ViewModel { get; set; }
        public EmergenceMainPage()
        {
            InitializeComponent();

            ViewModel = new MainWindowViewModel()
            {
                Statistics = new StatisticsViewModel()
                {
                    TeamStatisticsViewModel = new TeamStatisticsViewModel()
                    {
                        TeamMemberTotal = 10,
                        TeamMemberUseTotal = 5,
                        TeamTotal = 15,
                        TeamUseTotal = 5
                    },
                    MaterialsStatisticsViewModel = new MaterialsStatisticsViewModel[2]
                    {
                         new MaterialsStatisticsViewModel()
                         {
                              MaterialsName = "电缆",
                              TotalQuantity = 100
                         },
                         new MaterialsStatisticsViewModel()
                         {

                         }
                    }
                }
            };

            DataContext = ViewModel;
        }

        public EmergenceMainPage(bool isShowMaxPop) : this()
        {
            ViewModel.MainWindowSubTitleVisible = isShowMaxPop ? "Visible" : "Collapsed";
        }
       
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var svc = ServiceManager.Instance.GetService<MasterEventService>(Constant.Services.MasterEventService);
            //this.DataCodeing.ItemsSource = svc.GetMasterEventForMainPage();
            VideoList.BindingViewModel(svc.GetVideos());
        }

        private void MasterEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            map.Bind("119.931298", "28.469722");
        }
    }
}
