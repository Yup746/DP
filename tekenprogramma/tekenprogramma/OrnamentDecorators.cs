using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace tekenprogramma
{
    public class UpperOrnamentDecorator : OrnamentDecorator
    {
        public UpperOrnamentDecorator(Strategy strategy, string ornament) : base(strategy, ornament) { }

        public override Shape Draw(DrawPackage drawpackage)
        {
            Shape shape = _strategy.Draw(drawpackage);
            TextBlock element = new TextBlock();
            element.Text = _ornament;
            element.FontSize = 25;
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(element, drawpackage.x + ((drawpackage.width - element.DesiredSize.Width) / 2));
            Canvas.SetTop(element, drawpackage.y - element.DesiredSize.Height);
            drawpackage.paintSurface.Children.Add(element);
            return shape;
        }
    }

    public class LowerOrnamentDecorator : OrnamentDecorator
    {
        public LowerOrnamentDecorator(Strategy strategy, string ornament) : base(strategy, ornament) { }

        public override Shape Draw(DrawPackage drawpackage)
        {
            Shape shape = _strategy.Draw(drawpackage);
            TextBlock element = new TextBlock();
            element.Text = _ornament;
            element.FontSize = 25;
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(element, drawpackage.x + ((drawpackage.width - element.DesiredSize.Width) / 2));
            Canvas.SetTop(element, drawpackage.y + drawpackage.height);
            drawpackage.paintSurface.Children.Add(element);
            return shape;
        }
    }

    public class LeftOrnamentDecorator : OrnamentDecorator
    {
        public LeftOrnamentDecorator(Strategy strategy, string ornament) : base(strategy, ornament) { }

        public override Shape Draw(DrawPackage drawpackage)
        {
            Shape shape = _strategy.Draw(drawpackage);
            TextBlock element = new TextBlock();
            element.Text = _ornament;
            element.FontSize = 25;
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(element, drawpackage.x - element.DesiredSize.Width);
            Canvas.SetTop(element, drawpackage.y + ((drawpackage.height - element.DesiredSize.Height) / 2));
            drawpackage.paintSurface.Children.Add(element);
            return shape;
        }
    }

    public class RightOrnamentDecorator : OrnamentDecorator
    {
        public RightOrnamentDecorator(Strategy strategy, string ornament) : base(strategy, ornament) { }

        public override Shape Draw(DrawPackage drawpackage)
        {
            Shape shape = _strategy.Draw(drawpackage);
            TextBlock element = new TextBlock();
            element.Text = _ornament;
            element.FontSize = 25;
            element.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(element, drawpackage.x + drawpackage.width);
            Canvas.SetTop(element, drawpackage.y + ((drawpackage.height - element.DesiredSize.Height) / 2));
            drawpackage.paintSurface.Children.Add(element);
            return shape;
        }
    }
}
