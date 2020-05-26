using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    //History singleton which holds all the changes to be able to undo and redo
    class History
    {
        public List<Snapshot> timeline;

        public History()
        {
            timeline = new List<Snapshot>();
        }
    }
}
