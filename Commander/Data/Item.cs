using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    public class Item : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Date)));
            }
        }
        DateTime _Date;

        public long Size { get; set; } = -1;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }
        }
        bool _IsSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
        }
    }
}
