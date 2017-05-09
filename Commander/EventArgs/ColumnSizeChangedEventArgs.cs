using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    public class ColumnSizeChangedEventArgs : EventArgs
    {
        public readonly double[] Lengths;

        public ColumnSizeChangedEventArgs(double[] lengths)
        {
            Lengths = lengths;
        }
    }
}
