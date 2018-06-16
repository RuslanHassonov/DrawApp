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
        public ShapeList ShapeName { get; set; }
        public CanvasManager CanvasManager { get; set; }

        SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

        public CanvasWindow(string name, MainWindow mainWindow)
        {
            InitializeComponent();
            CanvasManager= new CanvasManager(mainWindow);
            CanvasName = name;
            this.Title = CanvasName;
        }

        //Click event to draw the chosen shape on canvas
        private void cvs_Drawing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Shape shapeDrawing = CanvasManager.ShapeManager.CreateNewShape();
                Point location = e.GetPosition(cvs_Drawing);
                Canvas.SetTop(shapeDrawing, location.Y);
                Canvas.SetLeft(shapeDrawing, location.X);
                cvs_Drawing.Children.Add(shapeDrawing);

                SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

                TblColor c = new TblColor()
                {
                    Red = ((SolidColorBrush)shapeDrawing.Fill).Color.R,
                    Green = ((SolidColorBrush)shapeDrawing.Fill).Color.G,
                    Blue = ((SolidColorBrush)shapeDrawing.Fill).Color.B
                };

                var savedColor = ctx.TblColors.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
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
                    Shape = CanvasManager.ShapeManager.SetFinalShapeName(shapeDrawing, shapeDrawing.Width, shapeDrawing.Height),
                    Width = (float)shapeDrawing.Width,
                    Height = (float)shapeDrawing.Height,
                    Color_ID = c.Color_ID,
                };

                var savedShape = ctx.TblShapes.Where(ss => ss.Color_ID == s.Color_ID && ss.Width == s.Width && ss.Height == s.Height && ss.Shape == s.Shape).FirstOrDefault();
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

            catch (FormatException)
            {
                MessageBox.Show("Please provide necessary values or select a shape from the selection window");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured - " + ex);
            }
        }

        private void CanvasWindow1_Closed(object sender, EventArgs e)
        {
            TblOverview tblOverview = new TblOverview()
            {
                DateUpdated = DateTime.Now,
                Name = this.Title
            };

            var t = ctx.TblOverviews.Where(n => n.Name == tblOverview.Name).FirstOrDefault();
            if (t != null)
            {
                t.DateUpdated = tblOverview.DateUpdated;
                ctx.SubmitChanges();
            }
        }
    }
}
