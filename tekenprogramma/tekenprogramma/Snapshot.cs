using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace tekenprogramma
{
    //A snapshot that can be saved in a history list
    public struct Snapshot
    {
        public Composite composite;
        public int itemcount;

        public Snapshot(Composite composite, int itemcount)
        {
            this.composite = composite;
            this.itemcount = itemcount;
        }
    }
}
