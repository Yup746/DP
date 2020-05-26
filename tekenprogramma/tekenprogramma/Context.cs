using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace tekenprogramma
{
    class Context
    {
        private IDrawShape _strategy;

        public Context() { }

        public Context(IDrawShape strategy)
        {
            this._strategy = strategy;
        }

        public void SetStrategy(IDrawShape strategy)
        {
            this._strategy = strategy;
        }

        public Shape Draw(Shape s, List<int> selected, Composite comp, Composite group)
        {
            Shape shape = this._strategy.DrawShape(s, selected, comp, group);

            return shape;
        }
    }
}
