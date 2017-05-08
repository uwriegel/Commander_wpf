using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    public class ColumnsSizes : INotifyPropertyChanged
    {
        public double Size1 
        {
            get { return _Size1; }
            set
            {
                _Size1 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size1"));
            }
        }
        double _Size1;

        public double Size2
        {
            get { return _Size2; }
            set
            {
                _Size2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size2"));
            }
        }
        double _Size2;

        public double Size3
        {
            get { return _Size3; }
            set
            {
                _Size3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size3"));
            }
        }
        double _Size3;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
