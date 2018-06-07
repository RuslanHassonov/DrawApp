using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawApp
{
    public class CanvasManager
    {
        public Shape Shape { get; set; }
        public Color Color { get; set; }
        public ShapeManager ShapeManager { get; set; }
        public MainWindow Window { get; set; }

        public CanvasManager(MainWindow w)
        {
            Window = w;
            ShapeManager = new ShapeManager(w);
        }

        public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        {
            Shape = ShapeManager.CreateNewShape(shapeName, w, h, r, g, b);
            Point location = e.GetPosition(Window.cvs_Drawing);
            Canvas.SetTop(Shape, location.Y);
            Canvas.SetLeft(Shape, location.X);

            return Shape;
        }
        
    }
}
