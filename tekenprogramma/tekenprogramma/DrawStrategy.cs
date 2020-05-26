using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace tekenprogramma
{
    class DrawEllipse : IDrawShape
    {
        public Shape DrawShape(Shape s, List<int> selected, Composite comp, Composite group)
        {
            Shape shape = s;

            shape.Height = comp.height;
            shape.Width = comp.width;
            SolidColorBrush brush = new SolidColorBrush();
            Composite tmp = comp;
            if (selected.Contains(comp.id))
                brush.Color = Colors.DarkSlateGray;
            else
            {
                brush.Color = Colors.SlateGray;
                while (tmp.id != 0 && selected.Count > 0)
                {
                    int parent = group.Findparent(tmp.id);
                    if (selected.Contains(parent))
                    {
                        brush.Color = Colors.DarkSlateGray;
                        break;
                    }
                    else
                        tmp = group.FindID(parent);
                }
            }
            shape.Fill = brush;
            shape.Name = "Ellipse";
            shape.Tag = comp.id;

            return shape;
        }
    }

    class DrawRectangle : IDrawShape
    {
        public Shape DrawShape(Shape s, List<int> selected, Composite comp, Composite group)
        {
            Shape shape = s;

            shape.Height = comp.height;
            shape.Width = comp.width;
            SolidColorBrush brush = new SolidColorBrush();
            Composite tmp = comp;
            if (selected.Contains(comp.id))
                brush.Color = Colors.DarkSlateGray;
            else
            {
                brush.Color = Colors.SlateGray;
                while (tmp.id != 0 && selected.Count > 0)
                {
                    int parent = group.Findparent(tmp.id);
                    if (selected.Contains(parent))
                    {
                        brush.Color = Colors.DarkSlateGray;
                        break;
                    }
                    else
                        tmp = group.FindID(parent);
                }
            }
            shape.Fill = brush;
            shape.Name = "Rectangle";
            shape.Tag = comp.id;

            return shape;
        }
    }
}
