namespace DrawApp
{
    public class ShapeChangedEventArgs
    {
        //private ShapeList ShapeList;
        //private int Width;
        //private int Height;
        //private byte Red;
        //private byte Green;
        //private byte Blue;
        public ShapeList ShapeList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public ShapeChangedEventArgs(ShapeList name, int w, int h, byte r, byte g, byte b)
        {
            ShapeList = name;
            Width = w;
            Height = h;
            Red = r;
            Green = g;
            Blue = b;
        }
    }
}