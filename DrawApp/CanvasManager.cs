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
        public CanvasWindow CanvasWindow { get; set; }
        public MainWindow MainWindow { get; set; }
        public ShapeManager ShapeManager { get; set; }
        public List<CanvasWindow> ListOfActiveCanvas { get; set; }

        //Constructors
        public CanvasManager(MainWindow w)
        {
            MainWindow = w;
            ListOfActiveCanvas = new List<CanvasWindow>();
            ShapeManager = new ShapeManager();
            MainWindow.OnShapeChanged += OnShapeChangedHandler;
        }

        public CanvasManager()
        { }

        //Add a brand new canvas
        public CanvasWindow CreateNewCanvas(string name)
        {
            CanvasWindow = new CanvasWindow(name);
            ListOfActiveCanvas.Add(CanvasWindow);
            return CanvasWindow;
        }

        //Load saved Canvasses to DataGrid
        public void LoadCanvasses()
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

            var list = from o in ctx.TblOverviews
                       select o;
            MainWindow.dg_DrawingOverview.ItemsSource = list;

            // -- Not Working as Intended, Review if possible --

            //var list = (from o in ctx.TblOverviews
            //           select new SelectedCanvas
            //           {
            //               Name = o.Name,
            //               DateCreated = (DateTime)o.DateCreated,
            //               DateUpdated = (DateTime)o.DateUpdated
            //           }).ToList();
            //MainWindow.dg_DrawingOverview.ItemsSource = list;

        }

        //Delete canvas from overview
        public void DeleteCanvas(string name)
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();
            var deleteCanvas = (from o in ctx.TblOverviews
                                where o.Name == name
                                select o).FirstOrDefault();
            ctx.TblOverviews.DeleteOnSubmit(deleteCanvas);
            ctx.SubmitChanges();
            LoadCanvasses();
        }

        //Draw a shape on active canvas
        public Shape Draw(ShapeList shapeName, int w, int h, byte r, byte g, byte b, MouseButtonEventArgs e)
        {
            try
            {
                Shape shape = ShapeManager.CreateNewShape(shapeName, w, h, r, g, b);
                Point location = e.GetPosition(CanvasWindow.cvs_Drawing);
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

        //Event Handlers
        private void OnShapeChangedHandler(object sender, ShapeChangedEventArgs e)
        {
            foreach (var item in ListOfActiveCanvas)
            {
                item.Width = e.Width;
                item.Height = e.Height;
                item.Red = e.Red;
                item.Green = e.Green;
                item.Blue = e.Blue;
                item.ShapeName = e.ShapeList;
            }
        }


    }
}
