using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Commander
{
    class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var path = value as string;
            if (path == null)
                return null;
            if (path == "..")
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Commander;component/Resources/parentfolder.png", UriKind.RelativeOrAbsolute)));
            else if (Directory.Exists(path))
                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,/Commander;component/Resources/folder.png", UriKind.RelativeOrAbsolute)));
            else
            {
                ImageBrush imageBrush = null;
                var index = path.LastIndexOf('.') + 1;
                if (index == 0)
                    return null;
                var extension = string.Format(".{0}", path.Substring(index).ToLower());
                if (string.Compare(extension, ".7z", true) == 0)
                    extension = ".zip";
                else if (string.Compare(extension, ".rar", true) == 0)
                    extension = ".zip";
                else if (string.Compare(extension, ".gz", true) == 0)
                    extension = ".zip";
                else if (string.Compare(extension, ".tar", true) == 0)
                    extension = ".zip";

                if (extension == ".exe")
                    imageBrush = ImageBrushCreator.ExtractIcon(path);
                else if (!icons.TryGetValue(extension, out imageBrush))
                {
                    imageBrush = ImageBrushCreator.ExtractIcon(extension);
                    icons[extension] = imageBrush;
                }
                return imageBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        static Dictionary<string, ImageBrush> icons = new Dictionary<string, ImageBrush>();
    }
}
