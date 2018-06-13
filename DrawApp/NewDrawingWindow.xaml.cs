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
    /// Interaction logic for NewDrawingWindow.xaml
    /// </summary>
    public partial class NewDrawingWindow : Window
    {
        MainWindow Window;
        public CanvasManager canvasManager;
        public event EventHandler<NewCanvasEventArgs> OnCanvasCreated;

        public NewDrawingWindow(MainWindow w)
        {
            Window = w;
            canvasManager = new CanvasManager(w);
            InitializeComponent();
        }

        private void OnCanvasCreation(NewCanvasEventArgs args)
        {
            OnCanvasCreated?.Invoke(this, args);
        }
        
        private void bt_AddNewDrawing_Click(object sender, RoutedEventArgs e)
        {
            if (tb_Drawing_Name.Text != string.Empty)
            {
                canvasManager.CreateNewCanvas(tb_Drawing_Name.Text);
                CanvasWindow canvasWindow = new CanvasWindow(tb_Drawing_Name.Text);
                canvasWindow.Title = canvasWindow.CanvasName;
                OnCanvasCreation(new NewCanvasEventArgs(canvasWindow.CanvasName, DateTime.Now));
                canvasWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please provide a proper name for the canvas.");
            }
        }
    }
}
