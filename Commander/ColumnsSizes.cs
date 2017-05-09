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

        public double Size4
        {
            get { return _Size4; }
            set
            {
                _Size4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size4"));
            }
        }
        double _Size4;

        public double Size5
        {
            get { return _Size5; }
            set
            {
                _Size5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Size5"));
            }
        }
        double _Size5;

        public double Left2
        {
            get { return _Left2; }
            set
            {
                _Left2 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Left2"));
            }
        }
        double _Left2;

        public double Left3
        {
            get { return _Left3; }
            set
            {
                _Left3 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Left3"));
            }
        }
        double _Left3;

        public double Left4
        {
            get { return _Left4; }
            set
            {
                _Left4 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Left4"));
            }
        }
        double _Left4;

        public double Left5
        {
            get { return _Left5; }
            set
            {
                _Left5 = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Left5"));
            }
        }
        double _Left5;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
