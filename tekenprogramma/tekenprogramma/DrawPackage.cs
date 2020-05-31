using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace tekenprogramma
{
    //DrawPackage class is used for ornament decorators or draw strategies, this stores all needed information for either
    public class DrawPackage
    {
        public double x, y, height, width;
        public int id;
        public string ornament;
        public Canvas paintSurface;
    }
}
