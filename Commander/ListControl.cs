using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Commander
{
    public class ListControl : ListView
    {
        public ListControl()
        {
            BorderThickness = new System.Windows.Thickness(0);
            SelectionMode = SelectionMode.Multiple;
            VirtualizingPanel.SetIsVirtualizing(this, true);
            VirtualizingPanel.SetVirtualizationMode(this, VirtualizationMode.Recycling);
            PreviewMouseLeftButtonDown += ListControl_PreviewMouseLeftButtonDown;
        }

        void ListControl_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var lvi = FindAncestor<ListViewItem>(e.OriginalSource as DependencyObject);
            if (lvi != null)
            {
                lvi.Focus();
                e.Handled = true;
            }
        }
        
        public static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }
    }
}
