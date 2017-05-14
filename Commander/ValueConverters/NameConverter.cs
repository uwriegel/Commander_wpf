using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Commander
{
    public class NameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (text != null)
            {
                if (text == "..")
                    return text;
                var pos = text.LastIndexOf('\\');
                if (pos != 0)
                    text = text.Substring(pos + 1);
                text = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0];
            }
            return text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
