using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    class FileItem : Item
    {
        public string Version
        {
            get
            {
                if (_Version == null)
                    _Version = FileVersion.Get(Name);
                return _Version;
            }
        }
        string _Version;
    }
}
