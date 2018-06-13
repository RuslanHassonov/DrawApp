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
        MainWindow Window;
        ShapeManager shapeManager;
        ColorManager colorManager;

        public string CanvasName { get; set; }
        public Color Color { get; set; }
        public new int Width { get; set; }
        public new int Height { get; set; }
        public ShapeList ShapeName { get; set; }

        public CanvasWindow(MainWindow w)
        {
            InitializeComponent();
            shapeManager = new ShapeManager(w);
            colorManager = new ColorManager(w);
            Window = w;
        }

        public CanvasWindow(string name)
        {
            InitializeComponent();
            CanvasName = name;
        }

        private void cvs_Drawing_Loaded(object sender, RoutedEventArgs e)
        {
            Window = new MainWindow();
            Window.OnColorChanged += OnColorChangedHandler;
            Window.OnShapeChanged += OnShapeChangedHandler;
        }

        private void OnColorChangedHandler(object sender, ColorChangeEventArgs e)
        {
            Color color = colorManager.AddColor(e.Red, e.Green, e.Blue);
            Color = new Color();
            Color = color;
        }

        private void OnShapeChangedHandler(object sender, ShapeChangedEventArgs e)
        {
            Width = e.Width;
            Height = e.Height;
            ShapeName = e.ShapeList;
        }

        public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        {
            try
            {
                Shape shape = shapeManager.CreateNewShape(shapeName, w, h, r, g, b);
                Point location = e.GetPosition(this.cvs_Drawing);
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

        private void cvs_Drawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int w = Width;
                int h = Height;
                byte r = Color.R;
                byte g = Color.G;
                byte b = Color.B;
                Shape shapeDrawing = Draw(ShapeName, w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }
    }
}
