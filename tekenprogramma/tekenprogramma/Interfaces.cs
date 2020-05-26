using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace tekenprogramma
{
    //Action/order interface
    public interface Action
    {
        void Execute();
    }

    //Visitor interface
    public interface Visitor
    {
        void visit(Composite composite);
    }

    public interface IDrawShape
    {
        Shape DrawShape(Shape s, List<int> selected, Composite comp, Composite group);
    }
}
