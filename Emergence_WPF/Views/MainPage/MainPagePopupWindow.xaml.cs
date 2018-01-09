using Business.Services;
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
using System.Windows.Shapes;

namespace Emergence_WPF
{
    /// <summary>
    /// Interaction logic for MainPagePopupWindow.xaml
    /// </summary>
    public partial class MainPagePopupWindow : Window
    {
        public MainPagePopupWindow()
        {
            InitializeComponent();
            this.KeyDown += ModifyPrice_KeyDown;
            ShowMainPage();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // show main page first
            //ShowMainPage();
        }

        private void ShowMainPage()
        {
            this.Width = ResolutionService.Width;
            this.Height = ResolutionService.Height;

            EmergenceMainPage emp = new EmergenceMainPage(true);
            this.Frame_MainWindowPopup.NavigationService.Navigate(emp);
        }

        private void ModifyPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)//Esc键  
            {
                this.Close();
            }
        }

    }
}
