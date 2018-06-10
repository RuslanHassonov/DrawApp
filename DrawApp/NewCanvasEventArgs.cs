using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    public class NewCanvasEventArgs : EventArgs
    {
        public string CanvasName { get; set; }
        public DateTime CreationDate { get; set; }

        public NewCanvasEventArgs(string name, DateTime dateTime)
        {
            CanvasName = name;
            CreationDate = dateTime;
        }
    }
}
