using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;

namespace tekenprogramma
{
    class Context
    {
        private Strategy strategy;
        public double x, y, height, width = 0;
        public int id = 0;

        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public Shape Draw()
        {
            Shape shape = strategy.DrawShape(x, y, height, width, id);
            return shape;
        }
    }
}
