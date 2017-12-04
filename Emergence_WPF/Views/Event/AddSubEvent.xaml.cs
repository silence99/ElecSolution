﻿using System;
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
    /// Interaction logic for AddMasterEvent.xaml
    /// </summary>
    public partial class AddSubEvent : Window
    {
        public bool isclose = false;

        public AddSubEvent()
        {
            InitializeComponent();
            DP_MasterEventCreateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void DP_CreateMasterEvent_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isclose = true;
            this.Close();
        }
    }
}
