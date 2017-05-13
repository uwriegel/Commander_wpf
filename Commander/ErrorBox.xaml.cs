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
using System.Windows.Shapes;

namespace Commander
{
    /// <summary>
    /// Interaction logic for ErrorBox.xaml
    /// </summary>
    public partial class ErrorBox : Window
    {
        public static void Show(string text, Exception exception = null, Window parent = null)
        {
            new ErrorBox(parent ?? Application.Current.MainWindow, text, exception).ShowDialog();
        }

        public ErrorBox(Window parent, string text, Exception exception)
        {
            InitializeComponent();
            Owner = parent;
            Text.Text = text;
            this.exception = exception;
            if (exception != null)
                ExceptionButton.Visibility = Visibility.Visible;

        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void ExceptionButton_Click(object sender, RoutedEventArgs e)
        {
            Exception.Visibility = Visibility.Visible;
            Exception.Text = exception.ToString();
        }

        Exception exception;
    }
}
