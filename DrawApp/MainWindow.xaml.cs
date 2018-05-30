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
        SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();
        ShapeManager sm;
        CanvasManager cm;
        Shape shapeExample;

        public MainWindow()
        {
            InitializeComponent();
            ShapeManager shapeManager = new ShapeManager(this);
            CanvasManager canvasManager = new CanvasManager(this);
            sm = shapeManager;
            cm = canvasManager;
            cb_Shapes.ItemsSource = shapeManager.ListShapes;
            LoadColors();
            LoadShapes();
        }

        #region Shape Definition and Drawing

        private void cb_Shapes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Shapes.SelectedIndex != -1)
            {
                try
                {
                    int w = Int32.Parse(tb_Width.Text);
                    int h = Int32.Parse(tb_Height.Text);
                    byte r = Byte.Parse(tb_RedValue.Text);
                    byte g = Byte.Parse(tb_GreenValue.Text);
                    byte b = Byte.Parse(tb_BlueValue.Text);
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

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int w = Int32.Parse(tb_Width.Text);
                int h = Int32.Parse(tb_Height.Text);
                byte r = Byte.Parse(tb_RedValue.Text);
                byte g = Byte.Parse(tb_GreenValue.Text);
                byte b = Byte.Parse(tb_BlueValue.Text);
                Shape shapeDrawing = cm.Draw(cb_Shapes.SelectedValue.ToString(), w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }

        #endregion

        #region Color Saving and Selecting with DB

        private void LoadColors()
        {
            try
            {
                var list = from c in ctx.SAVED_COLORs
                           select new SavedColor
                           {
                               R = (byte)c.Red,
                               G = (byte)c.Green,
                               B = (byte)c.Blue
                           };
                foreach (var item in list)
                {
                    Color color = cm.AddColor(item.R, item.G, item.B);
                    Rectangle colorRec = new Rectangle
                    {
                        Height = 20,
                        Fill = new SolidColorBrush(color)
                    };
                    lb_ColourTemplates.Items.Add(colorRec);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void bt_SaveColour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                byte r = Byte.Parse(tb_RedValue.Text);
                byte g = Byte.Parse(tb_GreenValue.Text);
                byte b = Byte.Parse(tb_BlueValue.Text);

                SAVED_COLOR c = new SAVED_COLOR()
                {
                    Red = r,
                    Green = g,
                    Blue = b
                };
                ctx.SAVED_COLORs.InsertOnSubmit(c);
                ctx.SubmitChanges();

                lb_ColourTemplates.Items.Clear();
                LoadColors();
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

        #endregion

        #region Shape Saving and Selecting with DB

        private void LoadShapes()
        {
            try
            {
                var list = from s in ctx.SAVED_SHAPEs
                           from c in ctx.SAVED_COLORs
                           where s.Color_ID == c.Color_ID
                           select new SavedShape
                           {
                               W = (int)s.Width,
                               H = (int)s.Height,
                               Shape = s.Shape,
                               R = (byte)c.Red,
                               G = (byte)c.Green,
                               B = (byte)c.Blue
                           };

                foreach (var item in list)
                {
                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;

                    Label colorLabel = new Label();
                    colorLabel.Width = 40;
                    colorLabel.Height = 30;
                    Color color = cm.AddColor(item.R, item.G, item.B);
                    colorLabel.Background = new SolidColorBrush(color);

                    Label descriptionLabel = new Label();
                    descriptionLabel.Content = item.ToString(item.Shape);

                    stack.Children.Add(colorLabel);
                    stack.Children.Add(descriptionLabel);

                    lb_ShapeTemplates.Items.Add(stack);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message);
            }
        }

        private void bt_SaveShape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int w = Int32.Parse(tb_Width.Text);
                int h = Int32.Parse(tb_Height.Text);
                byte r = Byte.Parse(tb_RedValue.Text);
                byte g = Byte.Parse(tb_GreenValue.Text);
                byte b = Byte.Parse(tb_BlueValue.Text);
                string name = string.Empty;

                if (cb_Shapes.SelectedItem.ToString() == "Ellipse" && tb_Height.Text == tb_Width.Text)
                {
                    name = "Circle";
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Ellipse" && tb_Height.Text != tb_Width.Text)
                {
                    name = "Ellipse";
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Rectangle" && tb_Height.Text == tb_Width.Text)
                {
                    name = "Square";
                }
                else if (cb_Shapes.SelectedItem.ToString() == "Rectangle" && tb_Height.Text != tb_Width.Text)
                {
                    name = "Rectangle";
                }

                // Issue happens from here on out. Color gets saved as a new color, no idea how to check if color already exists

                SAVED_COLOR c = new SAVED_COLOR()
                {
                    Red = r,
                    Green = g,
                    Blue = b
                };

                ctx.SAVED_COLORs.InsertOnSubmit(c);
                ctx.SubmitChanges();

                SAVED_SHAPE s = new SAVED_SHAPE()
                {
                    Width = w,
                    Height = h,
                    Shape = name,
                    Color_ID = c.Color_ID
                };

                ctx.SAVED_SHAPEs.InsertOnSubmit(s);
                ctx.SubmitChanges();

                lb_ShapeTemplates.Items.Clear();
                LoadShapes();
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
                MessageBox.Show("Shape Changed");
            }
        }

        #endregion


        private void bt_ClearAll_Click(object sender, RoutedEventArgs e)
        {
            tb_Width.Clear();
            tb_Height.Clear();
            tb_RedValue.Clear();
            tb_GreenValue.Clear();
            tb_BlueValue.Clear();
            cvs_Example.Children.Clear();
            cb_Shapes.SelectedIndex = -1;
            lb_ColourTemplates.SelectedIndex = -1;
            shapeExample = null;
        }

    }
}
