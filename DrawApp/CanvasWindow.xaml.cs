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
                    Shape = ShapeName.ToString(),
                    Color_ID = c.Color_ID
                };

                TblShape savedShape = ctx.TblShapes.Where(ss => ss.Color_ID == s.Color_ID && ss.Width == s.Width && ss.Height == s.Height && ss.Shape == s.Shape).FirstOrDefault();
                if (savedShape == null)
                {
                    ctx.TblShapes.InsertOnSubmit(s);
                    ctx.SubmitChanges();
                }
                else
                {
                    s.Shape_ID = savedShape.Shape_ID;
                }

                var canvasID = ctx.TblOverviews.Where(canvas => canvas.Name == this.Title).FirstOrDefault();
                if (canvasID != null)
                {
                    TblPosition position = new TblPosition()
                    {
                        Shape_ID = s.Shape_ID,
                        Drawing_ID = canvasID.Drawing_ID,
                        X = Canvas.GetLeft(shapeDrawing),
                        Y = Canvas.GetTop(shapeDrawing)
                    };

                    ctx.TblPositions.InsertOnSubmit(position);
                }

                ctx.SubmitChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }
    }
}
