// See https://aka.ms/new-console-template for more information
using System;
namespace src
{
    public class Ship
    {
        public int length;
        public int health;

        public Ship(int shipLength)
        {
            health = shipLength;
            length = shipLength;
        }

    }

    public class ShipGrid
    {
        public Ship[,] grid;
        public int length, width;

        // Permet le création de la grille. Length donne le nombre de lignes, width le nombre de colonnes. Il faudra changer width en height
        public ShipGrid(Ship[,] sgrid)
        {
            length = sgrid.GetLength(0);
            width = sgrid.GetLength(1);
            grid = new Ship[length, width];
            // Création de la grille i = A, puis j= 1. 2 .3 etc...
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    grid[i, j] = sgrid[i, j];
                }
            }
        }

        public ShipGrid(int l, int w)
        {
            length = l;
            width = w;
            grid = new Ship[length, width];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < width; j++)
                    grid[i, j] = null;
            }
        }

        public bool AddShip(Ship ship, int x, int y, string direction)
        {
            if (x < 0 || x >= width || y < 0 || y >= length)
            {
                Console.WriteLine("Your ship is out of bounds.");
                return false;
            }

            switch (direction)
            {
                case ("up"):
                    {
                        if ((y - ship.length) < 0)
                        {
                            Console.WriteLine("Your ship is out of bounds.");
                            return false;
                        }
                        for (int i = y; i > (y - ship.length); i--)
                        {
                            if (grid[i, x] != null)
                            {
                                Console.WriteLine("Your ship crosses another one.");
                                return false;
                            }
                        }
                        for (int i = y; i > (y - ship.length); i--)
                        {
                            grid[i, x] = ship;
                        }
                        break;
                    }
                case "down":
                    {
                        if ((y + ship.length) > length)
                        {
                            Console.WriteLine("Your ship is out of bounds.");
                            return false;
                        }
                        for (int i = y; i < (y + ship.length); i++)
                        {
                            if (grid[i, x] != null)
                            {
                                Console.WriteLine("Your ship crosses another one.");
                                return false;
                            }
                        }
                        for (int i = y; i < (y + ship.length); i++)
                        {
                            grid[i, x] = ship;
                        }
                        break;
                    }
                case "left":
                    {
                        if ((x - ship.length) < 0)
                        {
                            Console.WriteLine("Your ship is out of bounds.");
                            return false;
                        }
                        for (int i = x; i > (x - ship.length); i--)
                        {
                            if (grid[y, i] != null)
                            {
                                Console.WriteLine("Your ship crosses another one.");
                                return false;
                            }
                        }
                        for (int i = x; i > (x - ship.length); i--)
                        {
                            grid[y, i] = ship;
                        }
                        break;
                    }
                case "right":
                    {
                        if ((x + ship.length) > width)
                        {
                            Console.WriteLine("Your ship is out of bounds.");
                            return false;
                        }
                        for (int i = x; i < (x + ship.length); i++)
                        {
                            if (grid[y, i] != null)
                            {
                                Console.WriteLine("Your ship crosses another one.");
                                return false;
                            }
                        }
                        for (int i = x; i < (x + ship.length); i++)
                        {
                            grid[y, i] = ship;
                        }
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid direction.");
                        return false;
                    }
            }
            return true;
        }

        public void Display()
        {
            Console.WriteLine("Player field :");
            List<string> firstLine = new List<string> { " | A" };
            char[] letter = new char[1];
            letter[0] = 'B';

            for (int i = 1; i < width; i++)
            {
                firstLine.Add(new string(letter));
                letter[0]++;
            }

            Console.WriteLine();
            Console.WriteLine("   " + String.Join(" | ", firstLine) + " |");
            for (int i = 0; i < (4 * (width + 1)); i++)
                Console.Write("-");
            Console.WriteLine();

            for (int line = 1; line < (length + 1); line++)
            {
                List<string> ships = new List<string>();
                if (grid[line - 1, 0] != null)
                    ships.Add(" | X");
                else
                    ships.Add(" |  ");
                for (int j = 1; j < width; j++)
                {
                    if (grid[line - 1, j] != null)
                        ships.Add("X");
                    else
                        ships.Add(" ");
                }

                if (line >= 10)
                    Console.WriteLine(line + " " + String.Join(" | ", ships) + " |");
                else
                    Console.WriteLine(" " + line + " " + String.Join(" | ", ships) + " |");

                for (int i = 0; i < (4 * (width + 1)); i++)
                    Console.Write("-");
                Console.Write("\n");
            }
        }
    }
}


