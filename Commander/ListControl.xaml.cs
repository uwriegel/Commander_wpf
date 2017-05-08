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
        }

        void InitializeDirectoryChange()
        {
            var index = 0;

            List.MouseRightButtonDown += (s, e) =>
            {
                DirectoryInfo di;
                if (index == 0)
                    di = new DirectoryInfo(@"a:\bilder");
                else if (index == 1)
                    di = new DirectoryInfo(@"c:\windows");
                else
                    di = new DirectoryInfo(@"c:\windows\system32");
                index++;
                var files = di.GetFiles().Select(n => new FileItem { Name = n.FullName, Date = n.LastAccessTime.ToString(), Size = n.Length });
                List.ItemsSource = files;
            };
        }
    }
}
