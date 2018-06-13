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
        private SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();
        private ShapeManager sm;
        private CanvasManager cm;
        private ColorManager clrm;
        private Shape shapeExample;

        public event EventHandler<ColorChangeEventArgs> OnColorChanged;
        public event EventHandler<ShapeChangedEventArgs> OnShapeChanged;


        public MainWindow()
        {
            InitializeComponent();
            sm = new ShapeManager(this);
            cm = new CanvasManager(this);
            clrm = new ColorManager(this);
            cb_Shapes.ItemsSource = new ShapeManager(this).ListShapes;
            clrm.LoadColors();
            sm.LoadShapes();
            cm.LoadCanvasses();
        }

        private void OnColorChangedHappened (ColorChangeEventArgs args)
        {
            OnColorChanged?.Invoke(this, args);
        }
        private void OnShapeChangedHappened (ShapeChangedEventArgs args)
        {
            OnShapeChanged?.Invoke(this, args);
        }

        //ComboBox Selection of Shapes
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
        
        #region Color Saving and Selecting from DB

        private void bt_SaveColour_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TblColor c = new TblColor()
                {
                    Red = Byte.Parse(tb_RedValue.Text),
                    Green = Byte.Parse(tb_GreenValue.Text),
                    Blue = Byte.Parse(tb_BlueValue.Text)
                };

                TblColor savedColor = ctx.TblColors.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
                if (savedColor == null)
                {
                    ctx.TblColors.InsertOnSubmit(c);
                    ctx.SubmitChanges();
                }
                else
                {
                    MessageBox.Show("This color already exists.");
                }
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
                OnColorChangedHappened(new ColorChangeEventArgs(Byte.Parse(tb_RedValue.Text), Byte.Parse(tb_GreenValue.Text), Byte.Parse(tb_BlueValue.Text)));
            }
        }

        #endregion

        #region Shape Saving and Selecting from DB

        private void bt_SaveShape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = sm.SetShapeName(cb_Shapes.SelectedItem.ToString(), Int32.Parse(tb_Width.Text), Int32.Parse(tb_Height.Text));
                
                TblColor c = new TblColor()
                {
                    Red = Byte.Parse(tb_RedValue.Text),
                    Green = Byte.Parse(tb_GreenValue.Text),
                    Blue = Byte.Parse(tb_BlueValue.Text)
                };

                TblColor savedColor = ctx.TblColors.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
                if (savedColor == null)
                {
                    ctx.TblColors.InsertOnSubmit(c);
                    ctx.SubmitChanges();
                    clrm.LoadColors();
                }
                else
                {
                    c.Color_ID = savedColor.Color_ID;
                }

                TblShape s = new TblShape()
                {
                    Width = Int32.Parse(tb_Width.Text),
                    Height = Int32.Parse(tb_Height.Text),
                    Shape = name,
                    Color_ID = c.Color_ID
                };

                ctx.TblShapes.InsertOnSubmit(s);
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
            cb_Shapes.SelectedIndex = -1;
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
                OnShapeChangedHappened(new ShapeChangedEventArgs((cb_Shapes.SelectedItem.ToString() == ShapeList.Ellipse.ToString() ? ShapeList.Ellipse : ShapeList.Rectangle), savedShape.W, savedShape.H));
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

        private void bt_NewDrawing_Click(object sender, RoutedEventArgs e)
        {
            NewDrawingWindow newDrawingWindow = new NewDrawingWindow(this);
            newDrawingWindow.OnCanvasCreated += OnCanvasCreatedHandler;
            newDrawingWindow.Show();
        }

        private void OnCanvasCreatedHandler(object sender, NewCanvasEventArgs e)
        {
            TblOverview newCanvas = new TblOverview
            {
                Name = e.CanvasName,
                DateCreated = e.CreationDate,
                DateUpdated = e.CreationDate
            };
            ctx.TblOverviews.InsertOnSubmit(newCanvas);
            ctx.SubmitChanges();
            cm.LoadCanvasses();
        }

        private void dg_DrawingOverview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TblOverview selection = dg_DrawingOverview.SelectedItem as TblOverview;

            if (selection != null)
            {
                try
                {
                    CanvasWindow canvas = cm.CreateNewCanvas(selection.Name);
                    canvas.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error - " + ex);
                }
            }
        }
    }
        
}
