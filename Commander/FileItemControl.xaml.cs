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

// TODO: LastDirectory
// TODO: Einschränken der Anzeige per Texteingabe
// TODO: Drives

namespace Commander
{
    public partial class FileItemControl : UserControl
    {
        public string Path
        {
            get { return GetValue(PathProperty) as string; }
            set { SetValue(PathProperty, value); }
        }
        public static readonly DependencyProperty PathProperty = DependencyProperty.Register("Path", typeof(string), typeof(FileItemControl));

        public FileItemControl()
        {
            InitializeComponent();
            ChangePath(@"c:\windows\system32");
        }

        public void ChangePath(string path)
        {
            try
            {
                Path = path;
                switch (path)
                {
                    case "drives":
                        break;
                    default:
                        ChangeDirectory(path);
                        break;
                }
            }
            catch (Exception e)
            {
                ErrorBox.Show("Der Pfad konnte nicht geändert werden", e);
            }
        }

        void ChangeDirectory(string directory)
        {
            var di = new DirectoryInfo(directory);
            var pdi = di.Parent;
            items = Enumerable.Repeat((Item)new ParentItem(pdi != null ? pdi.FullName : "drives"), 1)
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
        }

        void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    ChangePath((sender as TextBox).Text);
                    break;
            }
        }

        void List_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter when ((e.OriginalSource as ListBoxItem)?.DataContext is ParentItem):
                    var pi = (e.OriginalSource as ListBoxItem).DataContext as ParentItem;
                    ChangePath(pi.Parent);
                    e.Handled = true;
                    break;
                case Key.Enter when ((e.OriginalSource as ListBoxItem)?.DataContext is DirectoryItem):
                    var di = (e.OriginalSource as ListBoxItem).DataContext as DirectoryItem;
                    ChangePath(di.Name);
                    e.Handled = true;
                    break;
            }
        }

        Item[] items;
    }
}
