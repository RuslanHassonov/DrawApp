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
    public class ColorManager
    {
        public MainWindow Window { get; set; }
        public Color NewColor { get; set; }

        //Constructors
        public ColorManager(MainWindow w)
        {
            Window = w;
        }

        public ColorManager(){}


        //Add new color method
        public Color CreateNewColor()
        {
            NewColor = new Color
            {
                A = 255,
                R = Byte.Parse(Window.tb_RedValue.Text),
                G = Byte.Parse(Window.tb_GreenValue.Text),
                B = Byte.Parse(Window.tb_BlueValue.Text)
            };
            return NewColor;
        }

        public Color RecreateAColor(byte r, byte g, byte b)
        {
            Color color = new Color
            {
                A = 255,
                R = r,
                G = g,
                B = b
            };
            return color;
        }
        
        //Load colors from Databse method
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
                    Color color = RecreateAColor(item.R, item.G, item.B);
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
                MessageBox.Show("Error " + ex.Message);
            }

        }

        //Method to write color to database
        public void WriteColorToDB(byte r, byte g, byte b)
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
            }
            else
            {
                MessageBox.Show("This color already exists.");
            }
        }
    }
}
