using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Commander
{
    static class ImageBrushCreator
    {
        public static ImageBrush CreateBrush(Icon icon)
        {
            BitmapSource bitmapSource = null;
            using (var bitmap = icon.ToBitmap())
            {
                var hbm = bitmap.GetHbitmap();
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(hbm,
                                            IntPtr.Zero,
                                            Int32Rect.Empty,
                                            BitmapSizeOptions.FromEmptyOptions());
                Api.DeleteObject(hbm);
            }
            var ib = new ImageBrush(bitmapSource)
            {
                Stretch = Stretch.None
            };
            return ib;
        }

        public static ImageBrush ExtractIcon(string name)
        {
            var shinfo = new Api.SHFILEINFO();
            var ptr = Api.SHGetFileInfo(name,
                Api.FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                (int)(Api.SHGetFileInfoConstants.SHGFI_ICON |
                Api.SHGetFileInfoConstants.SHGFI_SMALLICON |
                Api.SHGetFileInfoConstants.SHGFI_USEFILEATTRIBUTES |
                Api.SHGetFileInfoConstants.SHGFI_TYPENAME));
            var icon = Icon.FromHandle(shinfo.hIcon).Clone() as Icon;
            Api.DestroyIcon(shinfo.hIcon);
            var brush = CreateBrush(icon);
            icon.Dispose();
            return brush;
        }
    }
}
