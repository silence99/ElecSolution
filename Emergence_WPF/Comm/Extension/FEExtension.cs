using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Emergence_WPF.Comm
{
    public static class FEExtension
    {
        public static bool GetValidateOnLostFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(ValidateOnLostFocusProperty);
        }

        public static void SetValidateOnLostFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(ValidateOnLostFocusProperty, value);
        }



        public static readonly DependencyProperty ValidateOnLostFocusProperty =
            DependencyProperty.RegisterAttached("ValidateOnLostFocus", typeof(bool), typeof(FEExtension),
                new FrameworkPropertyMetadata(false, OnValidateOnLostFocusChanged));

        private static void OnValidateOnLostFocusChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            if (e.NewValue.Equals(true))
            {
                fe.LostFocus += fe_LostFocus;
            }
            else
            {
                fe.LostFocus -= fe_LostFocus;
            }
        }

        static void fe_LostFocus(object sender, RoutedEventArgs e)
        {
            var fe = sender as FrameworkElement;
            foreach (var item in fe.BindingGroup.BindingExpressions)
            {
                if (item.Target == e.Source)
                {
                    item.UpdateSource();
                    break;
                }
            }
        }
    }
}