using System;
using System.Collections.Generic;
using System.Text;

namespace Noppes.E621
{
    public class NotePosition
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

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
