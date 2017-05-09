using Commander.Properties;
using System;
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
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
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
            SizeToFit();
            MoveIntoView();
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
            if ((e.OriginalSource as MenuItem).IsChecked)
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Themes/DarkTheme.xaml", UriKind.Relative) });
            else
                Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri("/Themes/LightTheme.xaml", UriKind.Relative) });
        }
    }
}
