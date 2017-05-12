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
    public partial class FileItemControl : UserControl
    {
        public FileItemControl()
        {
            InitializeComponent();
            var di = new DirectoryInfo(@"c:\windows");
            List.Items = di.GetFiles().Select(n => new FileItem
            {
                Name = n.FullName,
                Date = n.LastAccessTime,
                Size = n.Length
            }).ToArray();

            Task.Run(() =>
            {
                try
                {
                    foreach (var item in List.Items)
                        item.Version = FileVersion.Get(item.Name);
                }
                catch
                {
                }
            });
        }
    }
}
