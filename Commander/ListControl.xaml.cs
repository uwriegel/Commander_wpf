using System;
using System.Collections;
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

namespace Commander
{
    /// <summary>
    /// Interaction logic for ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(ListControl),
            new FrameworkPropertyMetadata(ItemsSourceChanged));
        static void ItemsSourceChanged(DependencyObject dO, DependencyPropertyChangedEventArgs e)
        {
            var listControl = dO as ListControl;
            listControl.List.ItemsSource = e.NewValue as IEnumerable;
        }

        public static void SetView(UIElement element, ViewBase value)
        {
            element.SetValue(ViewProperty, value);
        }
        public static ViewBase GetView(UIElement element)
        {
            return (ViewBase)element.GetValue(ViewProperty);
        }
        public static readonly DependencyProperty ViewProperty = DependencyProperty.RegisterAttached("View", typeof(ViewBase), typeof(ListControl),
            new FrameworkPropertyMetadata(ViewChanged));
        static void ViewChanged(DependencyObject dO, DependencyPropertyChangedEventArgs e)
        {
            var listControl = dO as ListControl;
            listControl.List.View = e.NewValue as ViewBase;
        }

        public ListControl()
        {
            InitializeComponent();
            SizeChanged += ListControl_SizeChanged; ;
        }

        void ListControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged -= ListControl_SizeChanged;
            ColumnsControl.ColumnSizeChangedEvent += ColumnsControl_ColumnSizeChangedEvent;
        }

        void ColumnsControl_ColumnSizeChangedEvent(object sender, ColumnSizeChangedEventArgs e)
        {
            var gv = List.View as GridView;

            var size = e.Lengths[0];
            if (size < 0)
                size = 0;
            gv.Columns[0].Width = size;

            if (e.Lengths.Length > 1)
            {
                size = e.Lengths[1];
                if (size < 0)
                    size = 0;
                gv.Columns[1].Width = size;
            }
            if (e.Lengths.Length > 2)
            {
                size = e.Lengths[2];
                if (size < 0)
                    size = 0;
                gv.Columns[2].Width = size;
            }
        }
    }
}
