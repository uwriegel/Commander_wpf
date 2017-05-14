using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;

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

        public ObservableCollection<GridViewColumn> Columns { get; } = new ObservableCollection<GridViewColumn>();

        public void SetColumns(string[] columnsNames)
        {
            if (!columnsNames.Equals(this.columnsNames))
            {
                ColumnsControl.Children.Clear();
                var columns = columnsNames.Select(n => new TextBlock { Text = n });
                foreach (var column in columns)
                    ColumnsControl.Children.Add(column);
                this.columnsNames = columnsNames;
                //Resize();

                var gridView = new GridView();
                foreach (var columnName in columnsNames)
                {
                    var col = Columns.First(n => (string)n.Header == columnName);
                    gridView.Columns.Add(col);
                }
                List.View = gridView;
            }
        }

        public ListControl()
        {
            Columns.CollectionChanged += Columns_CollectionChanged;
            InitializeComponent();
            SizeChanged += ListControl_SizeChanged; 
        }

        void Columns_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        void ListControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged -= ListControl_SizeChanged;
            InitializeColumnSizeChanging();
        }

        void InitializeColumnSizeChanging()
        {
            var actionId = 0;
            ColumnsControl.ColumnSizeChangedEvent += (s, e) =>
            {
                actionId++;
                Dispatcher.BeginInvoke(DispatcherPriority.Send, (Action<int>)(n =>
                {
                    if (n < actionId)
                        // Drop frame!
                        return;
                    Resize(e.Lengths);
                }), actionId);
            };
        }

        void Resize(double[] lengths)
        {
            var gv = List.View as GridView;
            if (gv != null)
                for (var i = 0; i < lengths.Length; i++)
                {
                    var size = lengths[i];
                    if (size < 0)
                        size = 0;
                    gv.Columns[i].Width = size;
                }
        }

        string[] columnsNames;
    }
}
