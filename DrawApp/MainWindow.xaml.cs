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
        ColorManager clrm;
        Shape shapeExample;

        public MainWindow()
        {
            InitializeComponent();
            sm = new ShapeManager(this);
            cm = new CanvasManager(this);
            clrm = new ColorManager(this);
            cb_Shapes.ItemsSource = new ShapeManager(this).ListShapes;
            clrm.LoadColors();
            sm.LoadShapes();
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
                    shapeExample = sm.CreateNewShape((cb_Shapes.SelectedItem.ToString() == ShapeList.Ellipse.ToString() ? ShapeList.Ellipse : ShapeList.Rectangle), ((int)cvs_Example.Width < w ? (int)cvs_Example.Width : w), ((int)cvs_Example.Height < h ? (int)cvs_Example.Height : h), r, g, b);
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
                Shape shapeDrawing = cm.Draw((cb_Shapes.SelectedItem.ToString() == ShapeList.Ellipse.ToString() ? ShapeList.Ellipse : ShapeList.Rectangle), w, h, r, g, b, e);
                cvs_Drawing.Children.Add(shapeDrawing);
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured - Please provide all necessary values before using this canvas.");
            }
        }

        #endregion
        
        #region Color Saving and Selecting with DB

        private void bt_SaveColour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SAVED_COLOR c = new SAVED_COLOR()
                {
                    Red = Byte.Parse(tb_RedValue.Text),
                    Green = Byte.Parse(tb_GreenValue.Text),
                    Blue = Byte.Parse(tb_BlueValue.Text)
                };
                ctx.SAVED_COLORs.InsertOnSubmit(c);
                ctx.SubmitChanges();

                lb_ColourTemplates.Items.Clear();
                clrm.LoadColors();
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

        private void bt_SaveShape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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

                SAVED_COLOR c = new SAVED_COLOR()
                {
                    Red = Byte.Parse(tb_RedValue.Text),
                    Green = Byte.Parse(tb_GreenValue.Text),
                    Blue = Byte.Parse(tb_BlueValue.Text)
                };

                SAVED_COLOR savedColor = ctx.SAVED_COLORs.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
                if (savedColor == null)
                {
                    ctx.SAVED_COLORs.InsertOnSubmit(c);
                    ctx.SubmitChanges();
                }
                else
                {
                    c.Color_ID = savedColor.Color_ID;
                }

                SAVED_SHAPE s = new SAVED_SHAPE()
                {
                    Width = Int32.Parse(tb_Width.Text),
                    Height = Int32.Parse(tb_Height.Text),
                    Shape = name,
                    Color_ID = c.Color_ID
                };

                ctx.SAVED_SHAPEs.InsertOnSubmit(s);
                ctx.SubmitChanges();

                lb_ShapeTemplates.Items.Clear();
                sm.LoadShapes();
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
                SavedShape savedShape = (SavedShape)selectedShape.Tag;

                tb_RedValue.Text = savedShape.R.ToString();
                tb_GreenValue.Text = savedShape.G.ToString();
                tb_BlueValue.Text = savedShape.B.ToString();
                tb_Width.Text = savedShape.W.ToString();
                tb_Height.Text = savedShape.H.ToString();
                if (savedShape.Shape == "Circle" || savedShape.Shape == "Ellipse")
                {
                    cb_Shapes.SelectedIndex = 0;
                }
                else
                {
                    cb_Shapes.SelectedIndex = 1;
                }
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

        private void DrawApp_Closed(object sender, EventArgs e)
        {
            if (cvs_Drawing.Children != null)
            {
                foreach (var shapeOnCanvas in cvs_Drawing.Children)
                {
                    Shape shape = (Shape)shapeOnCanvas;
                    SAVED_DRAWING drawing = new SAVED_DRAWING()
                    {
                        X = Canvas.GetTop(shape),
                        Y = Canvas.GetLeft(shape),
                    };

                    ctx.SAVED_DRAWINGs.InsertOnSubmit(drawing);
                    ctx.SubmitChanges();
                }
            }
            else
            {
                MessageBox.Show("Empty");
            }

            // Create new method in ColorManager to recreate shapes on the canvas after starting the app. vb: public Redraw (X, Y, R, G, B, ...)

            var list = from c in ctx.SAVED_DRAWINGs
                       select c;

            foreach (var item in list)
            {
                MessageBox.Show("X: " + item.X + " Y: " + item.Y);
            }
        }
    }
}
