﻿using System;
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
    public enum ShapeList { Ellipse, Rectangle }

    public class ShapeManager
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
        }
        #region Shape Creation

        public Shape CreateNewShape(ShapeList shapeList, int w, int h, byte r, byte g, byte b)
        {
            switch (shapeList)
            {
                case ShapeList.Ellipse:
                    NewShape = CreateNewEllipse(w, h, r, g, b);
                    break;
                case ShapeList.Rectangle:
                    NewShape = CreateNewRectangle(w, h, r, g, b);
                    break;
                default:
                    break;
            }

            return NewShape;
        }

        public Shape CreateNewEllipse(int w, int h, byte r, byte g, byte b)
        {
            NewShape = new Ellipse
            {
                Width = w,
                Height = h,
                Fill = new SolidColorBrush(new Color { R = r, G = g, B = b, A = 255 })
            };
            return NewShape;
        }

        public Shape CreateNewRectangle(int w, int h, byte r, byte g, byte b)
        {
            NewShape = new Rectangle
            {
                Width = w,
                Height = h,
                Fill = new SolidColorBrush(new Color { R = r, G = g, B = b, A = 255 })
            };
            return NewShape;
        }

        #endregion

        public void LoadShapes()
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();
            ColorManager = new ColorManager(Window);
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
                    Color color = ColorManager.AddColor(item.R, item.G, item.B);
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
    }
}
