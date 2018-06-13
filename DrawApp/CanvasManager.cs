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

        //CTOR
        public CanvasManager(MainWindow w)
        {
            MainWindow = w;
        }

        //Add a brand new canvas
        public CanvasWindow CreateNewCanvas(string name)
        {
            CanvasWindow = new CanvasWindow(name);
            CanvasWindow.Title = name;
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

    }
}
