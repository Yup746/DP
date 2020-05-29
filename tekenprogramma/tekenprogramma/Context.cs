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
        public DrawPackage drawpackage = new DrawPackage();

        public Context(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public Shape Draw()
        {
            Shape shape = strategy.Draw(drawpackage);
            return shape;
        }
    }
}
