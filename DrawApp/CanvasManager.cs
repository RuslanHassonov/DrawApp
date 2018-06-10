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
    public class CanvasManager
    {
        public string CanvasName { get; set; }

        public string CreateNewCanvas(string name)
        {
            CanvasName = name;
            return CanvasName;
        }

        //public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        //{
        //    Shape = ShapeManager.CreateNewShape(shapeName, w, h, r, g, b);
        //    Point location = e.GetPosition(Window.cvs_Drawing);
        //    Canvas.SetTop(Shape, location.Y);
        //    Canvas.SetLeft(Shape, location.X);

        //    return Shape;
        //}


        //private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        int w = Int32.Parse(tb_Width.Text);
        //        int h = Int32.Parse(tb_Height.Text);
        //        byte r = Byte.Parse(tb_RedValue.Text);
        //        byte g = Byte.Parse(tb_GreenValue.Text);
        //        byte b = Byte.Parse(tb_BlueValue.Text);
        //        //Shape shapeDrawing = cm.Draw((cb_Shapes.SelectedItem.ToString() == ShapeList.Ellipse.ToString() ? ShapeList.Ellipse : ShapeList.Rectangle), w, h, r, g, b, e);
        //        //cvs_Drawing.Children.Add(shapeDrawing);
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
        //    }
        //}

    }
}
