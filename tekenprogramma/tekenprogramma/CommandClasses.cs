using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace tekenprogramma
{
    public class ActionManager
    {
        private List<Action> actionlist = new List<Action>();

        public void takeAction(Action action)
        {
            actionlist.Add(action);
        }

        public void executeActions()
        {
            foreach(Action a in actionlist)
            {
                a.Execute();
            }
            actionlist.Clear();
        }
    }

    public class Resize : Action
    {
        private Composite composite;
        private double newheight;
        private double newwidth;

        public Resize(Composite composite, double newheight, double newwidth)
        {
            this.composite = composite;
            this.newheight = newheight;
            this.newwidth = newwidth;
        }

        public void Execute()
        {
            composite.Accept(new ResizeVisitor(newheight, newwidth));
            //composite.RChangeSize(newheight, newwidth);
        }
    }

    public class Move : Action
    {
        private Composite composite;
        private double newx;
        private double newy;

        public Move(Composite composite, double newx, double newy)
        {
            this.composite = composite;
            this.newx = newx;
            this.newy = newy;
        }

        public void Execute()
        {
            composite.Accept(new MoveVisitor(newx, newy));
            //composite.RMove(newx, newy);
        }
    }

    public class Load : Action
    {
        private Composite composite;
        private List<string> lines;

        public Load(Composite composite, List<string> lines)
        {
            this.composite = composite;
            this.lines = lines;
        }

        public void Execute()
        {
            composite.Loadfromfile(lines);
        }
    }

    public class Save : Action
    {
        private Composite composite;
        private DataWriter write;
        private int depth;

        public Save(Composite composite, DataWriter write, int depth)
        {
            this.composite = composite;
            this.write = write;
            this.depth = depth;
        }

        public void Execute()
        {
            composite.Accept(new SaveVisitor(write, depth));
            //composite.Savetofile(write, depth);
        }
    }

    public class Remove : Action
    {
        private Composite composite;
        private int id;

        public Remove(Composite composite, int id)
        {
            this.composite = composite;
            this.id = id;
        }

        public void Execute()
        {
            composite.RemoveID(id);
        }
    }

    public class Replace : Action
    {
        private Composite composite;
        private Composite replacement;
        private int id;

        public Replace(Composite composite, Composite replacement, int id)
        {
            this.composite = composite;
            this.replacement = replacement;
            this.id = id;
        }

        public void Execute()
        {
            composite.SetID(replacement, id);
        }
    }

    public class Add : Action
    {
        private Composite composite;
        private Composite addition;

        public Add(Composite composite, Composite addition)
        {
            this.composite = composite;
            this.addition = addition;
        }

        public void Execute()
        {
            composite.Add(addition);
        }
    }
}
