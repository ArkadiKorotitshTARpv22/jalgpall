using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall;
public class Stadium
{
    public int Width { get; }
    public int Height { get; }

    List<Figure> wallList;
    public Stadium(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsIn(double x, double y)
    {
        // Adjust the boundaries to fit within the walls
        return x > 1 && x < Width - 3 && y > 1 && y < Height - 2;
    }
    internal bool IsHit(Figure figure)
    {

        foreach (var wall in wallList)
        {
            if (wall.IsHit(figure))
            {
                return true;
            }
        }
        return false;
    }
    

    public void Draw() //we draw stadium
    {
        wallList = new List<Figure>();
        // Draw the stadium within the adjusted boundaries
        Console.SetCursorPosition(3, 3); // Adjust positions for the desired spacing
        Console.WriteLine(new string('-', Width)); // Adjust width
        for (int i = 4; i < Height; i++) // Adjust height and subtract 1
        {
            Console.SetCursorPosition(2, i); // Adjust positions for the desired spacing
            Console.Write('|');
            Console.SetCursorPosition(Width, i); // Adjust positions for the desired spacing
            Console.Write('|');
        }
        Console.SetCursorPosition(2, Height); // Adjust positions for the desired spacing
        Console.WriteLine(new string('-', Width)); // Adjust width
    }
}