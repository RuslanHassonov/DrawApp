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
    public class ColorManager
    {
        public MainWindow Window { get; set; }

        public ColorManager(MainWindow w)
        {
            Window = w;
        }
        
        public Color AddColor(byte r, byte g, byte b)
        {
            Color c = new Color
            {
                A = 255,
                R = r,
                G = g,
                B = b
            };
            return c;
        }

        public void LoadColors()
        {
            SQLServer_DrawAppDataContext ctx = new SQLServer_DrawAppDataContext();

            try
            {
                var list = from c in ctx.TblColors
                           select new SavedColor
                           {
                               R = (byte)c.Red,
                               G = (byte)c.Green,
                               B = (byte)c.Blue
                           };
                foreach (var item in list)
                {
                    Color color = AddColor(item.R, item.G, item.B);
                    Rectangle colorRec = new Rectangle
                    {
                        Height = 20,
                        Fill = new SolidColorBrush(color)
                    };
                    Window.lb_ColourTemplates.Items.Add(colorRec);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }

        }
    }
}
