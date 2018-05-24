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
    public class ShapeManager
    {
        public MainWindow window { get; set; }
        public Shape NewShape { get; set; }
        public List<string> ListShapes { get; set; } = new List<string>();

        public ShapeManager( Window w)
        {
            window = w as MainWindow;
            ListShapes.Add("Ellipse");
            ListShapes.Add("Rectangle");
        }

        public Shape CreateNewShape(string shapeType, int w, int h, byte r, byte g, byte b)
        {
            switch (shapeType)
            {
                case "Ellipse":
                    NewShape = CreateNewEllipse(w, h, r, g, b);
                    break;
                case "Rectangle":
                    NewShape = CreateNewRectangle(w, h, r, g, b);
                    break;
                default:
                    break;
            }

            return NewShape;
        }

        public Shape CreateNewExampleShape(string shapeType, int w, int h, byte r, byte g, byte b)
        {
            switch (shapeType)
            {
                case "Ellipse":
                    if (w > window.cvs_Example.Width)
                    {
                        w = (int)window.cvs_Example.Width;
                        
                    }
                    if (h > window.cvs_Example.Height)
                    {
                        h = (int)window.cvs_Example.Height;
                    }
                    NewShape = CreateNewEllipse(w, h, r, g, b);
                    break;
                case "Rectangle":
                    if (w > window.cvs_Example.Width)
                    {
                        w = (int)window.cvs_Example.Width;

                    }
                    if (h > window.cvs_Example.Height)
                    {
                        h = (int)window.cvs_Example.Height;
                    }
                    NewShape = CreateNewRectangle(w, h, r, g, b);
                    break;
                default:
                    break;
            }

            return NewShape;
        }

        public Shape CreateNewEllipse(int w, int h, byte r, byte g, byte b)
        {
            NewShape = new Ellipse
            {
                Width = w,
                Height = h,
                Fill = new SolidColorBrush(new Color { R = r, G = g, B = b, A = 255 })
            };
            return NewShape;
        }

        public Shape CreateNewRectangle(int w, int h, byte r, byte g, byte b)
        {
            NewShape = new Rectangle
            {
                Width = w,
                Height = h,
                Fill = new SolidColorBrush(new Color { R = r, G = g, B = b, A = 255 })
            };
            return NewShape;
        }

    }
}
