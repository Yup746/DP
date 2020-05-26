using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace tekenprogramma
{
    class MoveVisitor : Visitor
    {
        private double x;
        private double y;
        public MoveVisitor(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void visit(Composite composite)
        {
            composite.RMove(x, y);
        }
    }

    class ResizeVisitor : Visitor
    {
        private double height;
        private double width;
        public ResizeVisitor(double height, double width)
        {
            this.height = height;
            this.width = width;
        }

        public void visit(Composite composite)
        {
            composite.RChangeSize(height, width);
        }
    }

    class SaveVisitor : Visitor
    {
        private DataWriter write;
        private int depth;
        public SaveVisitor(DataWriter write, int depth)
        {
            this.write = write;
            this.depth = depth;
        }

        public void visit(Composite composite)
        {
            composite.Savetofile(write, depth);
        }
    }
}
