using System;

namespace Snake
{
    public class Position
    {
        public int Left { get; }
        public int Top { get; }

        public Position(int left, int top)
        {
            Left = left;
            Top = top;
        }
    }
}
