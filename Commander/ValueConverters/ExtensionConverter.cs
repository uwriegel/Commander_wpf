using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Commander
{
    public class ExtensionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (text != null)
            {
                var pos = text.LastIndexOf('\\');
                if (pos != 0)
                    text = text.Substring(pos + 1);
                var parts = text.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                text = parts.Length > 1 ? parts[1] : null;
            }
            return text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
