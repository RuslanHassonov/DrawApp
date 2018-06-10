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
        public CanvasWindow CanvasWindow { get; set; }

        public string CreateNewCanvas(string name)
        {
            CanvasName = name;
            return CanvasName;
        }

        public CanvasWindow ReopenCanvas(TblOverview overviewSelection)
        {
            CanvasWindow = new CanvasWindow(overviewSelection);
            return CanvasWindow;
        }
    }
}
