using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    //Invoker class
    public class Shift
    {
        Timeshift Forwardcommand;
        Timeshift Backwardcommand;

        public Shift(Timeshift Forward, Timeshift Backward)
        {
            Forwardcommand = Forward;
            Backwardcommand = Backward;
        }

        public void Forward()
        {
            Forwardcommand.Activate();
        }

        public void Backward()
        {
            Backwardcommand.Activate();
        }
    }

    //Receiver class
    public class Time : Shiftable
    {
        public void Forward()
        {
            MainPage.timeindex++;
        }

        public void Backward()
        {
            MainPage.timeindex--;
        }
    }

    //Concrete command
    public class Forward : Timeshift
    {
        private Shiftable shiftable;

        public Forward(Shiftable shiftable)
        {
            this.shiftable = shiftable;
        }

        public void Activate()
        {
            shiftable.Forward();
        }
    }

    //Concrete command
    public class Backward : Timeshift
    {
        private Shiftable shiftable;

        public Backward(Shiftable shiftable)
        {
            this.shiftable = shiftable;
        }

        public void Activate()
        {
            shiftable.Backward();
        }
    }
}
