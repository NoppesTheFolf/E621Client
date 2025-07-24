using System;
using System.Collections.Generic;
using System.Text;

namespace Noppes.E621
{
    public class NotePosition
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public NotePosition()
        {

        }

        public NotePosition(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
