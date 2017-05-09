using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Commander
{
    public class ColumnsControl : Canvas
    {
        public ColumnsControl()
        {
            Height = 16;
            SizeChanged += ColumnsControl_SizeChanged;
        }

        void ColumnsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var length = e.NewSize.Width / Children.Count;
            for (var i = 0; i < Children.Count; i++)
            {
                var tb = Children[i] as TextBlock;
                SetLeft(tb, length * i);
                tb.Width = length - 1;
            }
        }
    }
}
