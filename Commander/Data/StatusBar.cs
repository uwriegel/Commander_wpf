using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    class StatusBar : INotifyPropertyChanged
    {
        public static StatusBar Current { get; } = new StatusBar();

        public string Text 
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                }
            }
        }
        string _Text;

        public event PropertyChangedEventHandler PropertyChanged;

        static StatusBar()
        {
        }
    }
}
