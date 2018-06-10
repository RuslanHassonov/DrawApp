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
        public CanvasWindow(MainWindow w)
        {
            InitializeComponent();
            shapeManager = new ShapeManager(w);
            Window = w;
        }

        public CanvasWindow(TblOverview overview)
        {
            InitializeComponent();
        }

        public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        {
            Shape shape = shapeManager.CreateNewShape(shapeName, w, h, r, g, b);
            Point location = e.GetPosition(this.cvs_Drawing);
            Canvas.SetTop(shape, location.Y);
            Canvas.SetLeft(shape, location.X);

            return shape;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int w = Int32.Parse(Window.tb_Width.Text);
                int h = Int32.Parse(Window.tb_Height.Text);
                byte r = Byte.Parse(Window.tb_RedValue.Text);
                byte g = Byte.Parse(Window.tb_GreenValue.Text);
                byte b = Byte.Parse(Window.tb_BlueValue.Text);
                Shape shapeDrawing = Draw((Window.cb_Shapes.SelectedItem.ToString() == ShapeList.Ellipse.ToString() ? ShapeList.Ellipse : ShapeList.Rectangle), w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }
        
    }
}
