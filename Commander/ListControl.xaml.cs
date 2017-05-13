using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace Commander
{
    public partial class ListControl : UserControl
    {
        public Item[] Items
        {
            get { return GetValue(ItemsProperty) as Item[]; }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(Item[]), typeof(ListControl),
            new PropertyMetadata(Items_PropertyChanged));
        static void Items_PropertyChanged(DependencyObject dO, DependencyPropertyChangedEventArgs e)
        {
            var listControl = dO as ListControl;
            listControl.List.ItemsSource = e.NewValue as Item[];
            listControl.Dispatcher.BeginInvoke(DispatcherPriority.Background, (Action)(() =>
                (listControl.List.ItemContainerGenerator.ContainerFromIndex(0) as ListBoxItem)?.Focus()));
        }

        public static readonly DependencyProperty IsListItemSelectedProperty = DependencyProperty.RegisterAttached("IsListItemSelected",
            typeof(bool), typeof(ListControl), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));
        public static void SetIsListItemSelected(UIElement element, Boolean value)
        {
            element.SetValue(IsListItemSelectedProperty, value);
        }
        public static bool GetIsListItemSelected(UIElement element)
        {
            return (bool)element.GetValue(IsListItemSelectedProperty);
        }

        public ListControl()
        {
            InitializeComponent();
            SizeChanged += ListControl_SizeChanged;
            PreviewKeyDown += ListControl_PreviewKeyDown;
        }

        void SelectAll()
        {
            foreach (var fileItem in List.ItemsSource as Item[] ?? new Item[0])
                fileItem.IsSelected = true;
        }

        void UnselectAll()
        {
            foreach (var fileItem in List.ItemsSource as Item[] ?? new Item[0])
                fileItem.IsSelected = false;
        }

        void ListControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Add:
                    SelectAll();
                    break;
                case Key.Subtract:
                    UnselectAll();
                    break;
                case Key.Insert:
                    var fileItem = ((e.OriginalSource as ListBoxItem).DataContext as Item);
                    fileItem.IsSelected = !fileItem.IsSelected;
                    var index = List.ItemContainerGenerator.IndexFromContainer(e.OriginalSource as ListBoxItem);
                    var next = List.ItemContainerGenerator.ContainerFromIndex(index + 1) as ListBoxItem;
                    next?.Focus();
                    break;
                case Key.Home when (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift:
                    index = List.ItemContainerGenerator.IndexFromContainer(e.OriginalSource as ListBoxItem);
                    for (var i = 0; i <= index; i++)
                        (List.ItemsSource as Item[])[i].IsSelected = true;
                    for (var i = index + 1; i < (List.ItemsSource as Item[]).Length; i++)
                        (List.ItemsSource as Item[])[i].IsSelected = false;
                    e.Handled = true;
                    break;
                case Key.End when (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift:
                    index = List.ItemContainerGenerator.IndexFromContainer(e.OriginalSource as ListBoxItem);
                    for (var i = 0; i < index; i++)
                        (List.ItemsSource as Item[])[i].IsSelected = false;
                    for (var i = index; i < (List.ItemsSource as Item[]).Length; i++)
                        (List.ItemsSource as Item[])[i].IsSelected = true;
                    e.Handled = true;
                    break;
            }
        }

        //void ListControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    var item = ItemsControl.ContainerFromElement(List, e.OriginalSource as DependencyObject) as ListBoxItem;
        //    if (item != null)
        //        (item.DataContext as Item).IsSelected = !(item.DataContext as Item).IsSelected;
        //}

        public ColumnsSizes ColumnsSizes 
        {
            get; set;
        } = new ColumnsSizes();

        void ListControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged -= ListControl_SizeChanged;
            ColumnsControl.ColumnSizeChangedEvent += ColumnsControl_ColumnSizeChangedEvent;
        }

        void ColumnsControl_ColumnSizeChangedEvent(object sender, ColumnSizeChangedEventArgs e)
        {
            ColumnsSizes.Size1 = e.Lengths[0] - 30;
            if (ColumnsSizes.Size1 < 0)
                ColumnsSizes.Size1 = 0;
            if (e.Lengths.Length > 1)
            {
                ColumnsSizes.Size2 = e.Lengths[1] - 10;
                if (ColumnsSizes.Size2 < 0)
                    ColumnsSizes.Size2 = 0;
                ColumnsSizes.Left2 = e.Lengths[0];
            }
            if (e.Lengths.Length > 2)
            {
                ColumnsSizes.Size3 = e.Lengths[2] - 10;
                if (ColumnsSizes.Size3 < 0)
                    ColumnsSizes.Size3 = 0;
                ColumnsSizes.Left3 = ColumnsSizes.Left2 + e.Lengths[1];
            }
            if (e.Lengths.Length > 3)
            {
                ColumnsSizes.Size4 = e.Lengths[3] - 10;
                if (ColumnsSizes.Size4 < 0)
                    ColumnsSizes.Size4 = 0;
                ColumnsSizes.Left4 = ColumnsSizes.Left3 + e.Lengths[2];
            }
            if (e.Lengths.Length > 4)
            {
                ColumnsSizes.Size5 = e.Lengths[4] - 10;
                if (ColumnsSizes.Size5 < 0)
                    ColumnsSizes.Size5 = 0;
                ColumnsSizes.Left5 = ColumnsSizes.Left4 + e.Lengths[3];
            }
        }
    }
}
