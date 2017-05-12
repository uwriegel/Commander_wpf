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
            var di = new DirectoryInfo(@"c:\windows\system32");
            items = Enumerable.Repeat((Item)new ParentItem(), 1)
                .Concat(di.GetDirectories().Select(n => new DirectoryItem
                {
                    Name = n.FullName,
                    Date = n.LastAccessTime
                }))
                .Concat(di.GetFiles().Select(n => new FileItem
                {
                    Name = n.FullName,
                    Date = n.LastAccessTime,
                    Size = n.Length
                })).ToArray();
            List.Items = items;

            Task.Run(() =>
            {
                try
                {
                    foreach (var item in (items as Item[]).OfType<FileItem>())
                        item.Version = FileVersion.Get(item.Name);
                }
                catch
                {
                }
            });
        }

        Item[] items;
    }
}
