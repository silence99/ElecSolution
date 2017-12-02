using Business.Strategies;
using Emergence.Common.ViewModel;
using Framework;
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
using System.Globalization;

namespace Emergence_WPF.Views.MainPage
{
    /// <summary>
    /// Weather.xaml 的交互逻辑
    /// </summary>
    public partial class Weather : UserControl
    {
        protected WeatherUiModel UIModel = new WeatherUiModel();
        protected StrategyController<WeatherUiModel> strategyController = null;
        public Weather()
        {
            InitializeComponent();
            DataContext = UIModel;
            strategyController = new WeatherStrategyController(UIModel);
        }
    }
}
