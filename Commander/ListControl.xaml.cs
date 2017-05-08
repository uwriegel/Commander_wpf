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
            ColumnsSizes.Size2 = 130;
            ColumnsSizes.Size3 = 300;
            InitializeComponent();
            InitializeDirectoryChange();
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
                var files = di.GetFiles().Select(n => new FileItem { Name = n.FullName, Date = n.LastAccessTime, Size = n.Length });
                List.ItemsSource = files;
            };
        }
    }
}
