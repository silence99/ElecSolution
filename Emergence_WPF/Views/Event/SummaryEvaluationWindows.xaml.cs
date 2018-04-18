using Emergence.Business.ViewModel;
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

namespace Emergence_WPF.Views.Event
{
    /// <summary>
    /// Interaction logic for SummaryEvaluationWindows.xaml
    /// </summary>
    public partial class SummaryEvaluationWindows : Window
    {
        public VM_MasterEventDetail ViewModel { get; set; }
        public SummaryEvaluationWindows()
        {
            InitializeComponent();
        }

        private void Button_CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_SummaryEvaluation_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog m_Dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }

            ViewModel.DownloadSummaryEvaluationAction(m_Dialog.SelectedPath.Trim());
        }

        private void Button_Preview_Click(object sender, RoutedEventArgs e)
        {
            SummaryEvaluationPreview sep = new SummaryEvaluationPreview();
            sep.Topmost = true;

            sep.Owner = Window.GetWindow(this);
            sep.Show();
            
        }

        public void DisplaySummaryBlock()
        {
            this.SEWSummaryLabel.Visibility = Visibility.Hidden;
            this.Txt_Summary.Visibility = Visibility.Hidden;
            this.SEWBaseGrid.Height = 450;
        }
    }
}
