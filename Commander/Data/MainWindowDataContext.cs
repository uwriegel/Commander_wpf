using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    class MainWindowDataContext
    {
        public StatusBar StatusBar { get; set; } = StatusBar.Current;
    }
}
