using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Commander
{
    public class ColumnsControl : Canvas
    {
        public ColumnsControl()
        {
            Height = 16;
            SizeChanged += ColumnsControl_SizeChanged;
            MouseMove += ColumnsControl_MouseMove;
            MouseDown += ColumnsControl_MouseDown;
        }

        void ColumnsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (lengths == null)
                lengths = Enumerable.Repeat<double>(e.NewSize.Width / Children.Count, Children.Count).ToArray();
            else
            {
                var factor = e.NewSize.Width / e.PreviousSize.Width;
                lengths = lengths.Select(n => n * factor).ToArray();
            }

            ResizeColumns();
        }

        void ColumnsControl_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                var pos = e.GetPosition(this).X;
                double actualPos = 0;
                isSizable = false;
                for (var i = 0; i < lengths.Length - 1; i++)
                {
                    actualPos += lengths[i];
                    if (pos > actualPos - 3 && pos < actualPos + 3)
                    {
                        isSizable = true;
                        dragIndex = i + 1;
                        dragStart = pos;
                        break;
                    }
                }

                if (isSizable)
                    Cursor = Cursors.SizeWE;
                else
                    Cursor = Cursors.Arrow;
            }
        }

        void ColumnsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isSizable)
            {
                MouseMove += ColumnsControl_MouseMove;
                MouseUp += ColumnsControl_MouseUp;
                CaptureMouse();
                
                void ColumnsControl_MouseMove(object mmSender, MouseEventArgs mmE)
                {
                    Cursor = Cursors.SizeWE;
                    var pos = e.GetPosition(this).X;
                    var diff = pos - dragStart;

                    var lengthList = lengths.ToList();
                    var newLength = lengthList[dragIndex - 1] + diff;
                    if (newLength < 5)
                        diff = 5 - lengthList[dragIndex - 1];
                    newLength = lengthList[dragIndex] - diff;
                    if (newLength < 5)
                        diff = lengthList[dragIndex] - 5;
                    
                    lengthList[dragIndex - 1] += diff;
                    lengthList[dragIndex] -= diff;
                    lengths = lengthList.ToArray();
                    ResizeColumns();
                    dragStart += diff;
                }

                void ColumnsControl_MouseUp(object mmSender, MouseButtonEventArgs mmE)
                {
                    MouseMove -= ColumnsControl_MouseMove;
                    MouseUp -= ColumnsControl_MouseUp;
                    ReleaseMouseCapture();
                }
            }
        }

        void ResizeColumns()
        {
            double left = 0;
            for (var i = 0; i < Children.Count; i++)
            {
                var tb = Children[i] as TextBlock;
                SetLeft(tb, left);
                tb.Width = lengths[i] - 1;
                left += lengths[i];
            }
        }

        bool isSizable = false;
        double dragStart;
        int dragIndex;
        double[] lengths;
    }
}
