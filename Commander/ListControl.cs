using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            PreviewKeyDown += ListControl_PreviewKeyDown;
        }

        static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if (parent == null) return null;

            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }

        void ListControl_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Add:
                    SelectAll();
                    e.Handled = true;
                    break;
                case Key.Subtract:
                    UnselectAll();
                    e.Handled = true;
                    break;
                case Key.Insert:
                    var lvi = e.OriginalSource as ListViewItem;
                    lvi.IsSelected = !lvi.IsSelected;
                    var index = ItemContainerGenerator.IndexFromContainer(lvi);
                    var next = ItemContainerGenerator.ContainerFromIndex(index + 1) as ListViewItem;
                    next?.Focus();
                    break;
                case Key.Home when (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift:
                    UnselectAll();
                    index = ItemContainerGenerator.IndexFromContainer(e.OriginalSource as ListViewItem);
                    SetSelectedItems((ItemsSource as Item[]).Take(index + 1).ToArray());
                    e.Handled = true;
                    break;
                case Key.End when (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift:
                    UnselectAll();
                    index = ItemContainerGenerator.IndexFromContainer(e.OriginalSource as ListViewItem);
                    SetSelectedItems((ItemsSource as Item[]).Skip(index).ToArray());
                    e.Handled = true;
                    break;
            }
        }
    }
}
