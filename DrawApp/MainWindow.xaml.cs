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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShapeManager sm;
        CanvasManager cm;
        Shape shapeExample;
        int w = 0;
        int h = 0;
        byte r = 0;
        byte g = 0;
        byte b = 0;
        public MainWindow()
        {
            InitializeComponent();
            ShapeManager shapeManager = new ShapeManager(this);
            CanvasManager canvasManager = new CanvasManager(this);
            sm = shapeManager;
            cm = canvasManager;
            cb_Shapes.ItemsSource = shapeManager.ListShapes;
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
                Shape shapeDrawing = cm.Draw(cb_Shapes.SelectedValue.ToString(), w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }

        private void bt_SaveColour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                r = Byte.Parse(tb_RedValue.Text);
                g = Byte.Parse(tb_GreenValue.Text);
                b = Byte.Parse(tb_BlueValue.Text);
                Color color = cm.AddColor(r, g, b);
                Rectangle colorRec = new Rectangle
                {
                    Height = 20,
                    Fill = new SolidColorBrush(color)
                };
                lb_ColourTemplates.Items.Add(colorRec);
                tb_RedValue.Clear();
                tb_GreenValue.Clear();
                tb_BlueValue.Clear();
            }
            catch (FormatException)
            {
                MessageBox.Show("Error occured - Please provide correct values before choosing the shape.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Error occured - The value of a color is incorrect. Value must be between 0 and 255");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured - " + ex.Message);
            }
        }

        private void lb_ColourTemplates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rectangle selected = lb_ColourTemplates.SelectedItem as Rectangle;

            if (selected != null)
            {
                tb_RedValue.Text = ((SolidColorBrush)selected.Fill).Color.R.ToString();
                tb_GreenValue.Text = ((SolidColorBrush)selected.Fill).Color.G.ToString();
                tb_BlueValue.Text = ((SolidColorBrush)selected.Fill).Color.B.ToString();
            }
        }

        private void bt_SaveShape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StackPanel stack = new StackPanel();
                stack.Orientation = Orientation.Horizontal;

                Label colorLabel = new Label();
                r = Byte.Parse(tb_RedValue.Text);
                g = Byte.Parse(tb_GreenValue.Text);
                b = Byte.Parse(tb_BlueValue.Text);
                colorLabel.Width = 40;
                colorLabel.Height = 30;
                Color color = cm.AddColor(r, g, b);
                colorLabel.Background = new SolidColorBrush(color);

                Label descriptionLabel = new Label();

                if (cb_Shapes.SelectedItem.ToString() == "Ellipse" && tb_Height.Text == tb_Width.Text)
                {
                    descriptionLabel.Content = "Circle - " + tb_Height.Text;
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Ellipse" && tb_Height.Text != tb_Width.Text)
                {
                    descriptionLabel.Content = "Ellipse - " + tb_Height.Text + " x " + tb_Width.Text;
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Rectangle" && tb_Height.Text == tb_Width.Text)
                {
                    descriptionLabel.Content = "Square - " + tb_Height.Text;
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Rectangle" && tb_Height.Text != tb_Width.Text)
                {
                    descriptionLabel.Content = "Rectangle - " + tb_Height.Text + " x " + tb_Width.Text;
                }

                stack.Children.Add(colorLabel);
                stack.Children.Add(descriptionLabel);

                lb_ShapeTemplates.Items.Add(stack);
            }
            catch (FormatException)
            {
                MessageBox.Show("Error occured - Please provide correct values before choosing the shape.");
            }
            catch (OverflowException)
            {
                MessageBox.Show("Error occured - The value of a color is incorrect. Value must be between 0 and 255");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured - " + ex.Message);
            }
        }

        private void lb_ShapeTemplates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StackPanel selectedShape = lb_ShapeTemplates.SelectedItem as StackPanel;

            if (selectedShape != null)
            {
                
            }
        }
    }
}
