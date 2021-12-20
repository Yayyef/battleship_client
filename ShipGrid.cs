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
        Ship[,] grid;
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
            if (x < 0 || x >= width || y < 0 || y > length)
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

        }


    }
}


