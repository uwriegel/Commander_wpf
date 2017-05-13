using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    class ParentItem : Item
    {
        public string Parent { get; }

        public ParentItem(string parent)
        {
            Parent = parent;
        }
    }
}
