using Commander.Properties;
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

// TODO: Tab-Steuerung zwischen den Grids
// TODO: Shift-Tab zur Pfadeingabe
// TODO: F9-Command

namespace Commander
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ChooseTheme();
            DataContext = new MainWindowDataContext();
            InitializeComponent();
            if (Settings.Default.WindowTop != 0 ||
                Settings.Default.WindowLeft != 0 ||
                Settings.Default.WindowHeight != 0 ||
                Settings.Default.WindowWidth != 0)
            {
                Top = Settings.Default.WindowTop;
                Left = Settings.Default.WindowLeft;
                Height = Settings.Default.WindowHeight;
                Width = Settings.Default.WindowWidth;
                WindowState = Settings.Default.WindowState;
            }
            MenuItemDarkTheme.IsChecked = Settings.Default.DarkTheme;
            SizeToFit();
            MoveIntoView();

            PreviewKeyDown += MainWindow_PreviewKeyDown;

            ChangeDirectory(@"c:\windows\system32");
            List.Focus();
        }

        void ChangeDirectory(string directory)
        {
            var di = new DirectoryInfo(directory);
            var pdi = di.Parent;
            var items = Enumerable.Repeat((Item)new ParentItem(pdi != null ? pdi.FullName : "drives"), 1)
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
                        
            List.ItemsSource = items;
            List1.ItemsSource = items;

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

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter when ((e.OriginalSource as ListBoxItem)?.DataContext is ParentItem):
                    var pi = (e.OriginalSource as ListBoxItem).DataContext as ParentItem;
                    ChangeDirectory(pi.Parent);
                    e.Handled = true;
                    break;
                case Key.Enter when ((e.OriginalSource as ListBoxItem)?.DataContext is DirectoryItem):
                    var di = (e.OriginalSource as ListBoxItem).DataContext as DirectoryItem;
                    ChangeDirectory(di.Name);
                    e.Handled = true;
                    break;
            }
        }

        void SizeToFit()
        {
            if (Height > SystemParameters.VirtualScreenHeight)
                Height = SystemParameters.VirtualScreenHeight;

            if (Width > SystemParameters.VirtualScreenWidth)
                Width = SystemParameters.VirtualScreenWidth;
        }

        void MoveIntoView()
        {
            if (Top + Height / 2 > SystemParameters.VirtualScreenHeight)
                Top = SystemParameters.VirtualScreenHeight - Height;

            if (Left + Width / 2 > SystemParameters.VirtualScreenWidth)
                Left = SystemParameters.VirtualScreenWidth - Width;

            if (Top < 0)
                Top = 0;

            if (Left < 0)
                Left = 0;
        }

        void ChooseTheme()
        {
            if (Settings.Default.DarkTheme)
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative) });
            else
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative) });
        }

        void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (WindowState != WindowState.Minimized)
            {
                Settings.Default.WindowTop = Top;
                Settings.Default.WindowLeft = Left;
                Settings.Default.WindowHeight = Height;
                Settings.Default.WindowWidth = Width;
                Settings.Default.WindowState = WindowState;

                Settings.Default.Save();
            }
        }

        void MenuItemDarkTheme_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Settings.Default.DarkTheme = (e.OriginalSource as MenuItem).IsChecked;
            ChooseTheme();
        }

        void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
