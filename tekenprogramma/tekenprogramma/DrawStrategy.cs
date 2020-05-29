using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace tekenprogramma
{
    class DrawEllipse : Strategy
    {
        private static DrawEllipse instance = new DrawEllipse();

        private DrawEllipse() { }

        public static DrawEllipse getInstance()
        {
            return instance;
        }

        public Shape Draw(DrawPackage drawpackage)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Height = drawpackage.height;
            ellipse.Width = drawpackage.width;
            ellipse.Name = "Ellipse";
            ellipse.Tag = drawpackage.id;
            Canvas.SetLeft(ellipse, drawpackage.x);
            Canvas.SetTop(ellipse, drawpackage.y);
            return ellipse;
        }
    }

    class DrawRectangle : Strategy
    {
        private static DrawRectangle instance = new DrawRectangle();

        private DrawRectangle() { }

        public static DrawRectangle getInstance()
        {
            return instance;
        }

        public Shape Draw(DrawPackage drawpackage)
        {
            Rectangle rectangle = new Rectangle();

            rectangle.Height = drawpackage.height;
            rectangle.Width = drawpackage.width;
            rectangle.Name = "Rectangle";
            rectangle.Tag = drawpackage.id;
            Canvas.SetLeft(rectangle, drawpackage.x);
            Canvas.SetTop(rectangle, drawpackage.y);
            return rectangle;
        }
    }
}
