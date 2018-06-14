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
using System.Windows.Markup;
using System.Xml;
using System.IO;

namespace DrawApp
{
    /// <summary>
    /// Interaction logic for CanvasWindow.xaml
    /// </summary>
    public partial class CanvasWindow : Window
    {
        public string CanvasName { get; set; }
        public new int Width { get; set; }
        public new int Height { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public ShapeList ShapeName { get; set; }
        public ShapeManager ShapeManager { get; set; } = new ShapeManager();
        SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

        public CanvasWindow(string name)
        {
            InitializeComponent();
            CanvasName = name;
            this.Title = CanvasName;
        }

        //Method to get a shape from ShapeManager and set it on the canvas
        public Shape GetShape(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        {
            try
            {
                Shape shape = ShapeManager.CreateNewShape(shapeName, w, h, r, g, b);
                Point location = e.GetPosition(cvs_Drawing);
                Canvas.SetTop(shape, location.Y);
                Canvas.SetLeft(shape, location.X);
                return shape;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error - " + ex);
                return null;
            }
        }

        //Click event to draw the chosen shape on canvas
        private void cvs_Drawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int w = Width;
                int h = Height;
                byte r = Red;
                byte g = Green;
                byte b = Blue;
                Shape shapeDrawing = GetShape(ShapeName, w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);

                TblPosition position = new TblPosition
                {
                    X = Canvas.GetLeft(shapeDrawing),
                    Y = Canvas.GetTop(shapeDrawing),

                };

            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }
    }
}
