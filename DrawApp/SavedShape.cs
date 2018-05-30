using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    internal class SavedShape
    {
        public int H { get; set; }
        public int W { get; set; }
        public string Shape { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        
        public string ToString(string shape)
        {
            if (shape == "Circle" && H == W)
            {
                return "Circle - " + H;
            }
            else if (shape == "Ellipse" && H != W)
            {
                return "Ellipse - " + H + " x " + W;
            }
            else if (shape == "Square" && H == W)
            {
                return "Square - " + H;
            }
            else if (shape == "Rectangle" && H != W)
            {
                return "Rectangle - " + H + " x " + W;
            }
            else
            {
                return "Unknown";
            }
        }
    }
}
