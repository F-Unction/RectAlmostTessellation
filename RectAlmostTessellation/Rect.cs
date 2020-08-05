using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Media;

public class Rect : IComparable<Rect>
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public Color Color { get; set; }
    public bool Done { get; set; }

    public Rect(int x, int y, int width, int height)
    {
        Random rd = new Random();

        X = x;
        Y = y;
        Width = width;
        Height = height;
        Done = false;
        Color = Color.FromArgb(255, (byte)rd.Next(0, 255), (byte)rd.Next(0, 255), (byte)rd.Next(0, 255));
    }

    public int CompareTo([AllowNull] Rect other)
    {
        if (this.Height > other.Height)
        {
            return -1;
        }
        else if (this.Height < other.Height)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}