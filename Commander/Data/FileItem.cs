using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    public class FileItem : INotifyPropertyChanged
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

        public long Size { get; set; }

        public string Version
        {
            get { return _Version; }
            set
            {
                if (_Version != value)
                {
                    _Version = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Version)));
                }
            }
        }
        string _Version;

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
    }
}
