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

        public Shape DrawShape(double x, double y, double height, double width, int id)
        {
            Ellipse ellipse = new Ellipse();

            ellipse.Height = height;
            ellipse.Width = width;
            ellipse.Name = "Ellipse";
            ellipse.Tag = id;
            Canvas.SetLeft(ellipse, x);
            Canvas.SetTop(ellipse, y);
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

        public Shape DrawShape(double x, double y, double height, double width, int id)
        {
            Rectangle rectangle = new Rectangle();

            rectangle.Height = height;
            rectangle.Width = width;
            rectangle.Name = "Rectangle";
            rectangle.Tag = id;
            Canvas.SetLeft(rectangle, x);
            Canvas.SetTop(rectangle, y);
            return rectangle;
        }
    }
}
