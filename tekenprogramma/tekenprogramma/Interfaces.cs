using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    //Command interface
    public interface Timeshift
    {
        void Activate();
    }

    //ACtions that receiver can perform
    public interface Shiftable
    {
        void Forward();
        void Backward();
    }
}
