using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Commander
{
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var strNumber = value.ToString();
            if (strNumber == "-1")
                return null;
            var thSep = '.';
            if (strNumber.Length > 3)
            {
                var begriff = strNumber;
                strNumber = "";
                for (var j = 3; j < begriff.Length; j += 3)
                {
                    var extract = begriff.Substring(begriff.Length - j, 3);
                    strNumber = thSep + extract + strNumber;
                }
                var strfirst = begriff.Substring(0, (begriff.Length % 3 == 0) ? 3 : (begriff.Length % 3));
                strNumber = strfirst + strNumber;
            }
            return strNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
