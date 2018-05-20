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

namespace DrawApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeManager sm = new ShapeManager();
        Shape shapeExample;
        int w = 0;
        int h = 0;
        byte r = 0;
        byte g = 0;
        byte b = 0;
        public MainWindow()
        {
            InitializeComponent();
            cb_Shapes.ItemsSource = sm.ListShapes;
        }

        private void cb_Shapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Shapes.SelectedIndex != -1)
            {
                try
                {
                    w = Int32.Parse(tb_Width.Text);
                    h = Int32.Parse(tb_Height.Text);
                    r = Byte.Parse(tb_RedValue.Text);
                    g = Byte.Parse(tb_GreenValue.Text);
                    b = Byte.Parse(tb_BlueValue.Text);
                    cvs_Example.Children.Clear();
                    shapeExample = sm.CreateNewExampleShape(cb_Shapes.SelectedValue.ToString(), w, h, r, g, b);
                    Canvas.SetTop(shapeExample, 0);
                    Canvas.SetLeft(shapeExample, 0);
                    cvs_Example.Children.Add(shapeExample);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Error occured - Please provide correct values before choosing the shape.");
                    cb_Shapes.SelectedIndex = -1;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Error occured - The value of a color is incorrect. Value must be between 0 and 255");
                    cb_Shapes.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error occured - " + ex.Message);
                    cb_Shapes.SelectedIndex = -1;
                }
            }

        }

        private void bt_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            tb_Width.Clear();
            tb_Height.Clear();
            tb_RedValue.Clear();
            tb_GreenValue.Clear();
            tb_BlueValue.Clear();
            cvs_Example.Children.Clear();
            cb_Shapes.SelectedIndex = -1;
            shapeExample = null;
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Shape shapeDrawing = sm.CreateNewShape(cb_Shapes.SelectedValue.ToString(), w, h, r, g, b);
                Point location = e.GetPosition(cvs_Drawing);
                Canvas.SetTop(shapeDrawing, location.Y);
                Canvas.SetLeft(shapeDrawing, location.X);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }
    }
}
