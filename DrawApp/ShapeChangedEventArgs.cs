using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    public class ShapeChangedEventArgs
    {
        public ShapeList ShapeList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public ShapeChangedEventArgs(ShapeList shape, int w, int h)
        {
            ShapeList = shape;
            Width = w;
            Height = h;
        }
    }
}
