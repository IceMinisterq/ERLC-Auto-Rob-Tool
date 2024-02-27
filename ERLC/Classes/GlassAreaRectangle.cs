using ERLC;

namespace ERLC
{
    public struct Rectangle
    {
        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }
    
        public bool IsEmpty;
        public int x = 0;
        public int y = 0;
        
        public Rectangle(int left, int right, int top, int bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
            
            x = (right - left) / 2;
            y = (bottom - top) / 2;
            
            Console.WriteLine($"{left}, {right}, {top}, {bottom}");
            IsEmpty = (Left == 0 && Right == 0 && Top == 0 && Bottom == 0);
        }
    }
}
