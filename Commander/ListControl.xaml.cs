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

namespace Commander
{
    /// <summary>
    /// Interaktionslogik für ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        public ListControl()
        {
            InitializeComponent();
            InitializeDirectoryChange();
            SizeChanged += ListControl_SizeChanged;
            PreviewMouseDown += ListControl_PreviewMouseDown;
        }

        void ListControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(List, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null)
            {
                (item.DataContext as FileItem).IsSelected = !(item.DataContext as FileItem).IsSelected;
            }
        }

        public ColumnsSizes ColumnsSizes 
        {
            get; set;
        } = new ColumnsSizes();

        void InitializeDirectoryChange()
        {
            var index = 0;

            List.MouseRightButtonDown += (s, e) =>
            {
                DirectoryInfo di = null;
                if (index == 0)
                    di = new DirectoryInfo(@"a:\bilder");
                else if (index == 1)
                    di = new DirectoryInfo(@"c:\windows");
                else if (index == 2)
                    di = new DirectoryInfo(@"c:\windows\system32");
                else
                {
                    ColumnsSizes.Size2 = 350;
                    ColumnsSizes.Size3 = 450;
                    return;
                }
                index++;
                var files = di.GetFiles().Select(n => new FileItem { Name = n.FullName, Date = n.LastAccessTime, Size = n.Length }).ToArray();
                List.ItemsSource = files;

                Task.Run(() =>
                {
                    try
                    {
                        foreach (var item in files)
                            item.Version = FileVersion.Get(item.Name);
                    }
                    catch
                    {
                    }

                });
            };
        }

        void ListControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChanged -= ListControl_SizeChanged;
            ColumnsControl.ColumnSizeChangedEvent += ColumnsControl_ColumnSizeChangedEvent;
        }

        void ColumnsControl_ColumnSizeChangedEvent(object sender, ColumnSizeChangedEventArgs e)
        {
            ColumnsSizes.Size1 = e.Lengths[0] - 30;
            if (e.Lengths.Length > 1)
            {
                ColumnsSizes.Size2 = e.Lengths[1] - 10;
                ColumnsSizes.Left2 = e.Lengths[0];
            }
            if (e.Lengths.Length > 2)
            {
                ColumnsSizes.Size3 = e.Lengths[2] - 10;
                ColumnsSizes.Left3 = ColumnsSizes.Left2 + e.Lengths[1];
            }
            if (e.Lengths.Length > 3)
            {
                ColumnsSizes.Size4 = e.Lengths[3] - 10;
                ColumnsSizes.Left4 = ColumnsSizes.Left3 + e.Lengths[2];
            }
            if (e.Lengths.Length > 4)
            {
                ColumnsSizes.Size5 = e.Lengths[4] - 10;
                ColumnsSizes.Left5 = ColumnsSizes.Left4 + e.Lengths[3];
            }
        }
    }
}
