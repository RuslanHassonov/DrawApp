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
        ShapeManager shapeManager = new ShapeManager();
        CanvasManager canvasManager = new CanvasManager();

        public string CanvasName { get; set; }
        public new int Width { get; set; }
        public new int Height { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public ShapeList ShapeName { get; set; }

        public CanvasWindow(string name)
        {
            InitializeComponent();
            CanvasName = name;
            this.Title = CanvasName;
        }

        //public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        //{
        //    try
        //    {
        //        Shape shape = shapeManager.CreateNewShape(shapeName, w, h, r, g, b);
        //        Point location = e.GetPosition(this.cvs_Drawing);
        //        Canvas.SetTop(shape, location.Y);
        //        Canvas.SetLeft(shape, location.X);
        //        return shape;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error - " + ex);
        //        return null;
        //    }
        //}

        private void cvs_Drawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int w = Width;
                int h = Height;
                byte r = Red;
                byte g = Green;
                byte b = Blue;
                Shape shapeDrawing = canvasManager.Draw(ShapeName, w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);

                SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

                TblColor c = new TblColor()
                {
                    Red = r,
                    Green = g,
                    Blue = b
                };
                TblColor savedColor = ctx.TblColors.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
                if (savedColor == null)
                {
                    ctx.TblColors.InsertOnSubmit(c);
                    ctx.SubmitChanges();
                }
                else
                {
                    c.Color_ID = savedColor.Color_ID;
                }

                TblShape s = new TblShape()
                {
                    Width = w,
                    Height = h,
                    Shape = ShapeName.ToString()
                };

                TblShape savedShape = ctx.TblShapes.Where(ss => ss.Width == s.Width && ss.Height == s.Height && ss.Shape == s.Shape).FirstOrDefault();
                {
                    if (savedShape == null)
                    {

                    }

                }


            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }

        private void CanvasWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
