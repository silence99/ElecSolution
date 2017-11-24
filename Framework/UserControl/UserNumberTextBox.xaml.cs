using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Framework
{
    /// <summary>
    /// UserNumberTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class UserNumberTextBox : UserControl
    {
        public UserNumberTextBox()
        {
            InitializeComponent();
        }


        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(UserNumberTextBox), new FrameworkPropertyMetadata("Null"));
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(UserNumberTextBox), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// 获得焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(232, 116, 57));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 失去焦点时边框的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(0xCA, 0xC3, 0xBA));
            b1.BorderBrush = brush;
        }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string UserLblText
        {
            get { return lbl.Content.ToString(); }
            set { lbl.Content = value; }
        }
        public int MaxLength {
            get { return txtbox.MaxLength; }
            set { txtbox.MaxLength = value; }
        }
        /// <summary>
        /// 文本框内容
        /// </summary>
        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }
        public string UserText
        {
            get { return txtbox.Text; }
            set { txtbox.Text = value; }
        }


        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
        }
        private void txtbox_KeyDown(object sender, KeyEventArgs e)
        {

            if (Keyboard.Modifiers == ModifierKeys.Shift && ((e.Key >= Key.D0 && e.Key <= Key.D9)))
            {
                e.Handled = true;
                return;
            }
            if (e.Key == Key.Space)
            {
                e.Handled = true;
                return;
            }
            
            
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Delete) )
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
          
        }

        private void txtbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtbox_KeyUp(object sender, KeyEventArgs e)
        {

        }
        void RaiseTapEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(UserNumberTextBox.TextChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly RoutedEvent TextChangedEvent = EventManager.RegisterRoutedEvent(
       "TextChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserNumberTextBox));
        public event RoutedEventHandler TextChanged
        {
            add { AddHandler(TextChangedEvent, value); }
            remove { RemoveHandler(TextChangedEvent, value); }
        }
        private void txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RaiseTapEvent();

            //屏蔽中文输入和非法字符粘贴输入
            TextBox textBox = sender as TextBox;

            TextChange[] change = new TextChange[e.Changes.Count];
            e.Changes.CopyTo(change, 0);

            int offset = change[0].Offset;
            if (change[0].AddedLength > 0)
            {

                long num = 0;
                if (!Int64.TryParse(textBox.Text, out num))
                {

                    textBox.Text = textBox.Text.Remove(offset, change[0].AddedLength);
                    textBox.Select(offset, 0);

                }
               
            }
        }



    }
}
