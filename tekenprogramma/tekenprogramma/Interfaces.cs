using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;


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

    public interface Strategy
    {
        Shape DrawShape(double x, double y, double height, double width, int id);
    }
}
