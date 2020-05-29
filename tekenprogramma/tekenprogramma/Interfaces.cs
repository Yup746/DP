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
        Shape Draw(DrawPackage drawpackage);
    }

    public abstract class OrnamentDecorator : Strategy
    {
        public Strategy _strategy;
        public string _ornament;

        public OrnamentDecorator(Strategy strategy, string ornament)
        {
            _strategy = strategy;
            _ornament = ornament;
        }

        public abstract Shape Draw(DrawPackage drawpackage);
    }
}
