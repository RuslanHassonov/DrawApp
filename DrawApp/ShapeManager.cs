﻿using System;
using System.Collections.Generic;
using System.Data;
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
    public enum ShapeList { Ellipse, Rectangle }

    public class ShapeManager : IStorable
    {
        public MainWindow Window { get; set; }
        public Shape NewShape { get; set; }
        public ColorManager ColorManager { get; set; }
        public List<string> ListShapes { get; set; } = new List<string>();

        public ShapeManager(MainWindow w)
        {
            Window = w;
            ListShapes.Add("Ellipse");
            ListShapes.Add("Rectangle");
            ColorManager = new ColorManager(Window);
        }

        public ShapeManager() { }

        #region Shape Creation

        public Shape CreateNewShape()
        {

            Color newShapeColor = ColorManager.CreateNewColor();

            if (Window.cb_Shapes.SelectedItem.ToString() == "Ellipse")
            {
                NewShape = new Ellipse
                {
                    Width = Int32.Parse(Window.tb_Width.Text),
                    Height = Int32.Parse(Window.tb_Height.Text),
                    Fill = new SolidColorBrush(newShapeColor)
                };
                return NewShape;
            }
            else
            {
                NewShape = new Rectangle
                {
                    Width = Int32.Parse(Window.tb_Width.Text),
                    Height = Int32.Parse(Window.tb_Height.Text),
                    Fill = new SolidColorBrush(newShapeColor)
                };
                return NewShape;
            }

        }

        public Shape RecreateShape(SavedShape savedShape)
        {
            Color newShapeColor = ColorManager.RecreateAColor(savedShape.R, savedShape.G, savedShape.B);

            if (savedShape.Shape == "Ellipse" || savedShape.Shape == "Circle")
            {
                NewShape = new Ellipse
                {
                    Width = savedShape.W,
                    Height = savedShape.H,
                    Fill = new SolidColorBrush(newShapeColor)
                };
                return NewShape;
            }
            else
            {
                NewShape = new Rectangle
                {
                    Width = savedShape.W,
                    Height = savedShape.H,
                    Fill = new SolidColorBrush(newShapeColor)
                };
                return NewShape;
            }
        }

        public string SetFinalShapeName(Shape s, double w, double h)
        {
            string finalName = string.Empty;
            if (s is Ellipse && h == w)
            {
                return finalName = "Circle";
            }
            else if (s is Ellipse && h != w)
            {
                return finalName = "Ellipse";
            }
            else if (s is Rectangle && h == w)
            {
                return finalName = "Square";
            }
            else if (s is Rectangle && h != w)
            {
                return finalName = "Rectangle";
            }
            else
            {
                return "Unknown";
            }
        }

        #endregion

        //Loads shapes to ListBox from DataBase
        public void LoadShapes()
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();
            ColorManager = new ColorManager(Window);
            try
            {
                var list = from s in ctx.TblShapes
                           from c in ctx.TblColors
                           where s.Color_ID == c.Color_ID
                           select new SavedShape
                           {
                               W = (int)s.Width,
                               H = (int)s.Height,
                               Shape = s.Shape,
                               R = (byte)c.Red,
                               G = (byte)c.Green,
                               B = (byte)c.Blue,
                               Shape_ID = s.Shape_ID
                           };

                foreach (var item in list)
                {
                    StackPanel stack = new StackPanel
                    {
                        Orientation = Orientation.Horizontal
                    };

                    Label colorLabel = new Label
                    {
                        Width = 40,
                        Height = 30
                    };
                    Color color = ColorManager.RecreateAColor(item.R, item.G, item.B);
                    colorLabel.Background = new SolidColorBrush(color);

                    Label descriptionLabel = new Label
                    {
                        Content = item.ToString(item.Shape)
                    };

                    stack.Children.Add(colorLabel);
                    stack.Children.Add(descriptionLabel);
                    stack.Tag = item;

                    Window.lb_ShapeTemplates.Items.Add(stack);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error " + ex.Message);
            }
        }

        //Write a new shape to DB
        public void WriteShapeToDB(string name, byte r, byte g, byte b, int w, int h)
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

            TblColor c = new TblColor()
            {
                Red = r,
                Green = g,
                Blue = b
            };

            TblColor savedColor = ctx.TblColors.Where(sc => sc.Red == c.Red && sc.Blue == c.Blue && sc.Green == c.Green).FirstOrDefault();
            if (savedColor == null)
            {
                ctx.TblColors.InsertOnSubmit(c);
                ctx.SubmitChanges();
                ColorManager.LoadColors();
            }
            else
            {
                c.Color_ID = savedColor.Color_ID;
            }

            TblShape s = new TblShape()
            {
                Width = w,
                Height = h,
                Shape = name,
                Color_ID = c.Color_ID
            };

            TblShape savedShape = ctx.TblShapes.Where(ss => ss.Color_ID == s.Color_ID && ss.Width == s.Width && ss.Height == s.Height && ss.Shape == s.Shape).FirstOrDefault();
            if (savedShape == null)
            {
                ctx.TblShapes.InsertOnSubmit(s);
                ctx.SubmitChanges();
            }
            else
            {
                MessageBox.Show("This shape already exists");
            }
        }

        public DataTable CreateDataTable()
        {
            DataTable localShapeBackup = new DataTable();
            return localShapeBackup;
        }

        public void CreateLocalBackup()
        {
            //
        }
    }
}
