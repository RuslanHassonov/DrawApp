using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    public class SavedShape
    {
        public int H { get; set; }
        public int W { get; set; }
        public string Shape { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public int Shape_ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public string ToString(string shape)
        {
            if (shape == "Circle" && H == W)
            {
                Shape = "Ellipse";
                return "Circle - " + H;
            }
            else if (shape == "Ellipse" && H != W)
            {
                Shape = "Ellipse";
                return "Ellipse - " + H + " x " + W;
            }
            else if (shape == "Square" && H == W)
            {
                Shape = "Rectangle";
                return "Square - " + H;
            }
            else if (shape == "Rectangle" && H != W)
            {
                Shape = "Rectangle";
                return "Rectangle - " + H + " x " + W;
            }
            else
            {
                Shape = "Unknown";
                return "Unknown";
            }
        }
    }
}
